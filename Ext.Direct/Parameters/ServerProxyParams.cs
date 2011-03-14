using System;
using System.Collections.Generic;

namespace Ext.Direct.Parameters
{
    /// <summary>
    /// Represents the parameters that could be passed by a Ext.data.ServerProxy or decendant.
    /// </summary>
    public class ServerProxyParams
    {
        #region Properties
        /// <summary>
        /// The page number being requested.
        /// </summary>
        public long? Page { get; set; }

        /// <summary>
        /// The start record of the page.
        /// </summary>
        public long? Start { get; set; }

        /// <summary>
        /// The size of a page.
        /// </summary>
        public long? Limit { get; set; }

        /// <summary>
        /// A list of sort params.
        /// </summary>
        public IEnumerable<SortParam> Sort { get; set; }

        /// <summary>
        /// A list of group params.
        /// </summary>
        public IEnumerable<GroupParam> Group { get; set; }

        /// <summary>
        /// A list of filter params.
        /// </summary>
        public IEnumerable<FilterParam> Filter { get; set; }
        #endregion
    }
}
