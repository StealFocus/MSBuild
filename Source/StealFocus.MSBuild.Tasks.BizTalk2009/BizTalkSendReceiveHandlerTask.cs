// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="BizTalkSendReceiveHandlerTask.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the BizTalkSendReceiveHandlerTask type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// BizTalkSendReceiveHandlerTask Class.
    /// </summary>
    public abstract class BizTalkSendReceiveHandlerTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        [Required]
        public string HostName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the adapter.
        /// </summary>
        /// <value>The name of the adapter.</value>
        [Required]
        public string AdapterName
        {
            get;
            set;
        }

        #endregion // Properties
    }
}