// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DeleteReceiveHandler.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DeleteReceiveHandler type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DeleteReceiveHandler Class.
    /// </summary>
    public class Bts2009DeleteReceiveHandler : BizTalkSendReceiveHandlerTask
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
                Log.LogMessage("Deleting Receive Handler for Host '{0}' and Adapter '{1}'.", HostName, AdapterName);
                ReceiveHandler.Delete(AdapterName, HostName);
            }
            else
            {
                Log.LogMessage("Receive Handler for Host '{0}' and Adapter '{1}' was not found.", HostName, AdapterName);
            }

            return true;
        }

        #endregion // Methods
    }
}