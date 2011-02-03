// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CleanHostQueue.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CleanHostQueue type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Cleans all messages and service instances from a host queue.
    /// </summary>
    public class Bts2009CleanHostQueue : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            Log.LogMessage("Executing task: {0}", GetType().FullName);
            Log.LogMessage("Cleaning Queue for BizTalk Host '{0}'.", HostName);
            Host.CleanQueue(HostName);
            Log.LogMessage("Finished Executing task: {0}", GetType().FullName);
            return true;
        }
    }
}