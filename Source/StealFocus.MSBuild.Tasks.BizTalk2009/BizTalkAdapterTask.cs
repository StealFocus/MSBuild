// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="BizTalkAdapterTask.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the BizTalkAdapterTask type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// BizTalkAdapterTask Class.
    /// </summary>
    public abstract class BizTalkAdapterTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Adapter Name.
        /// </summary>
        [Required]
        public string AdapterName
        {
            get;
            set;
        }

        #endregion // Properties
    }
}
