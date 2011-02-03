// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DeleteAllReceiveHandlers.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DeleteAllReceiveHandlers type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DeleteAllReceiveHandlers Class.
    /// </summary>
    public class Bts2009DeleteAllReceiveHandlers : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            string[] receiveHandlers = Host.GetReceiveHandlers(HostName);
            foreach (string receiveHandler in receiveHandlers)
            {
                Log.LogMessage("Deleting Receive Handler for Host '{0}' and Adapter '{1}'.", HostName, receiveHandler);
                ReceiveHandler.Delete(receiveHandler, HostName);
            }

            return true;
        }
    }
}