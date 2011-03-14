using System;

namespace Ext.Direct.Parameters
{
    /// <summary>
    /// Represents a sort parameter.
    /// </summary>
    public class SortParam
    {
        #region Properties
        /// <summary>
        /// The property to sort by.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The direction to sort.
        /// </summary>
        /// <remarks>
        /// One of ASC or DESC.
        /// </remarks>
        public string Direction { get; set; }
        #endregion
    }
}
