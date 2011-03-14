﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace Ext.Direct
{
    internal class ResultConverter : JsonConverter
    {
        /// <summary>
        /// Reading is not supported by this converter
        /// </summary>
        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(JContainer));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //no custom reading here
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JContainer o = value as JContainer;
            if (o != null)
            {
                writer.WriteRawValue(o.ToString(Formatting.None, new JavaScriptDateTimeConverter()));
            }
        }

    }
}
