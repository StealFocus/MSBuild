// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CreateSendHandler.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CreateSendHandler type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009CreateSendHandler Class.
    /// </summary>
    public class Bts2009CreateSendHandler : BizTalkSendReceiveHandlerTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        public bool IsDefault
        {
            get;
            set;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (SendHandler.Exists(AdapterName, HostName))
            {
                Log.LogMessage("Send Handler for Host '{0}' and Adapter '{1}' already exists, skipping create action.", HostName, AdapterName);
            }
            else
            {
                Log.LogMessage("Creating Send Handler for Host '{0}' and Adapter '{1}'.", HostName, AdapterName);
                SendHandler.Create(this.AdapterName, this.HostName, this.IsDefault);
            }

            return true;
        }

        #endregion // Methods
    }
}