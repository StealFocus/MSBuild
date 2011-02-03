// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DeleteAllSendHandlers.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DeleteAllSendHandlers type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DeleteAllSendHandlers Class.
    /// </summary>
    public class Bts2009DeleteAllSendHandlers : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            string[] sendHandlers = Host.GetSendHandlers(HostName);
            foreach (string sendHandler in sendHandlers)
            {
                Log.LogMessage("Deleting Send Handler for Host '{0}' and Adapter '{1}'.", HostName, sendHandler);
                SendHandler.Delete(sendHandler, HostName);
            }

            return true;
        }
    }
}