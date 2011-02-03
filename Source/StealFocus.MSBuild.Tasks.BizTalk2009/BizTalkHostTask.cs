// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="BizTalkHostTask.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the BizTalkHostTask type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// BizTalkHostTask Class.
    /// </summary>
    public abstract class BizTalkHostTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        [Required]
        public string HostName
        {
            get;
            set;
        }

        #endregion // Properties
    }
}