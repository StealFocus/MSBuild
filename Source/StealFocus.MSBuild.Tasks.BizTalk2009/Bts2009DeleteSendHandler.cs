// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DeleteSendHandler.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DeleteSendHandler type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DeleteSendHandler Class.
    /// </summary>
    public class Bts2009DeleteSendHandler : BizTalkSendReceiveHandlerTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (SendHandler.Exists(AdapterName, HostName))
            {
                Log.LogMessage("Deleting Send Handler for Host '{0}' and Adapter '{1}'.", HostName, AdapterName);
                SendHandler.Delete(AdapterName, HostName);
            }
            else
            {
                Log.LogMessage("Send Handler for Host '{0}' and Adapter '{1}' was not found.", HostName, AdapterName);
            }

            return true;
        }

        #endregion // Methods
    }
}