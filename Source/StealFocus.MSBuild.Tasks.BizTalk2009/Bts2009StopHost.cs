// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StopHost.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StopHost type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StopHost Class.
    /// </summary>
    public class Bts2009StopHost : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (Host.Exists(HostName))
            {
                Log.LogMessage("Stopping BizTalk Host '{0}'.", HostName);
                Host.Stop(HostName);
            }
            else
            {
                Log.LogMessage("BizTalk Host '{0}' does not exist, skipping stop action.", HostName);
            }

            return true;
        }
    }
}