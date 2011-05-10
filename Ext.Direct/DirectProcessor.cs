using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace Ext.Direct
{
    /// <summary>
    /// Class for processing Ext Direct requests.
    /// </summary>
    internal class DirectProcessor
    {

        /// <summary>
        /// Processes an incoming request.
        /// </summary>
        /// <param name="provider">The provider that triggered the request.</param>
        /// <param name="httpRequest">The http information.</param>
        /// <returns>The result from the client method.</returns>
        internal static DirectExecutionResponse Execute(DirectProvider provider, HttpRequest httpRequest, IEnumerable<JsonConverter> converters)
        {
            List<DirectResponse> responses = new List<DirectResponse>();
            if (!String.IsNullOrEmpty(httpRequest[DirectRequest.RequestFormAction]))
            {
                DirectRequest request = new DirectRequest()
                {
                    Action = httpRequest[DirectRequest.RequestFormAction] ?? string.Empty,
                    Method = httpRequest[DirectRequest.RequestFormMethod] ?? string.Empty,
                    Type = httpRequest[DirectRequest.RequestFormType] ?? string.Empty,
                    IsUpload = Convert.ToBoolean(httpRequest[DirectRequest.RequestFormUpload]),
                    TransactionId = Convert.ToInt32(httpRequest[DirectRequest.RequestFormTransactionId]),
                    Data = new object[] { httpRequest }
                };
                responses.Add(DirectProcessor.ProcessRequest(provider, request));
            }
            else
            {
                // Sometimes it seems that TotalBytes and ContentLength are different.
                // Therefore am using my own stream reader rather than request's BinaryRead.
                string json = new UTF8Encoding().GetString(Utility.ReadAllInputStream(httpRequest));

                try
                {
                    // Force into an array shape
                    if (!json.StartsWith("["))
                    {
                        json = String.Format("[{0}]", json);
                    }

                    // Get raw array data
                    JArray raw = JArray.Parse(json);

                    // And also deserialize the requests
                    List<DirectRequest> requests = JsonConvert.DeserializeObject<List<DirectRequest>>(json);

                    int i = 0;
                    foreach (DirectRequest request in requests)
                    {
                        request.RequestData = (JObject)raw[i];
                        responses.Add(DirectProcessor.ProcessRequest(provider, request));
                        ++i;
                    }
                }
                catch (Exception ex)
                {
                    responses.Add(new DirectResponse(String.Format("An exception occurred when attempting to decode the requests: {0}", ex)));
                }
            }

            DirectExecutionResponse response = new DirectExecutionResponse();
            JsonSerializerSettings outputSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            foreach (JsonConverter converter in converters)
            {
                outputSettings.Converters.Add(converter);
            }

            // Updated this to guard against having no responses at all.
            // Was before I added the above else, but good practice anyway...
            if (responses.Count > 1 || ((responses.Count > 0) && !responses[0].IsUpload))
            {
                response.Data = JsonConvert.SerializeObject(responses, Formatting.None, outputSettings);
            }
            else if (responses.Count > 0)
            {
                response.IsUpload = true;
                string outputJson = JsonConvert.SerializeObject(responses[0], Formatting.None, outputSettings);
                response.Data = String.Format("<html><body><textarea>{0}</textarea></body></html>", outputJson.Replace("&quot;", "\\&quot;"));
            }
            return response;
        }

        private static DirectResponse ProcessRequest(DirectProvider provider, DirectRequest request)
        {
            DirectResponse r = new DirectResponse(request);
            try
            {
                r.Result = provider.Execute(request);
            }
            catch (DirectException ex)
            {
                r.ExceptionMessage = (ex.InnerException != null) ? ex.ToString() : ex.Message;
                r.Type = DirectResponse.ResponseExceptionType;
            }
            return r;
        }
    }
}
