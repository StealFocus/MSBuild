// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CreateReceiveHandler.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CreateReceiveHandler type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009CreateReceiveHandler Class.
    /// </summary>
    public class Bts2009CreateReceiveHandler : BizTalkSendReceiveHandlerTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (ReceiveHandler.Exists(AdapterName, HostName))
            {
                Log.LogMessage("Receive Handler for Host '{0}' and Adapter '{1}' already exists, skipping create action.", HostName, AdapterName);
            }
            else
            {
                Log.LogMessage("Creating Receive Handler for Host '{0}' and Adapter '{1}'.", HostName, AdapterName);
                ReceiveHandler.Create(AdapterName, HostName);
            }

            return true;
        }

        #endregion // Methods
    }
}