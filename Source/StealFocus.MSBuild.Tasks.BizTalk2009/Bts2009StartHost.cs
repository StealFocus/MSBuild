// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StartHost.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StartHost type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Executes the task.
    /// </summary>
    /// <returns>Indicating if the task was successful.</returns>
    public class Bts2009StartHost : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (Host.Exists(HostName))
            {
                Log.LogMessage("Starting BizTalk Host '{0}'.", HostName);
                Host.Start(HostName);
            }
            else
            {
                Log.LogMessage("BizTalk Host '{0}' does not exist, skipping start action.", HostName);
            }

            return true;
        }
    }
}