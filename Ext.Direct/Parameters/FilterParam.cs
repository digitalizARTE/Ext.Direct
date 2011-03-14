using System;

namespace Ext.Direct.Parameters
{
    /// <summary>
    /// Represents a filter parameter.
    /// </summary>
    public class FilterParam
    {
        #region Properties
        /// <summary>
        /// The property to filter on.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The value to filter on.
        /// </summary>
        public string Value { get; set; }
        #endregion
    }
}
