using System;

namespace Ext.Direct.Parameters
{
    /// <summary>
    /// Represents a group parameter.
    /// </summary>
    public class GroupParam
    {
        #region Properties
        /// <summary>
        /// The field to group by.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The direction to group on.
        /// </summary>
        /// <remarks>
        /// One of ASC or DESC.
        /// </remarks>
        public string Direction { get; set; }
        #endregion
    }
}
