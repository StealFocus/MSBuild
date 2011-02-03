// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DeleteHost.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DeleteHost type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DeleteHost Class.
    /// </summary>
    public class Bts2009DeleteHost : BizTalkHostTask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (Host.Exists(HostName))
            {
                Log.LogMessage("Deleting BizTalk Host '{0}'.", HostName);
                Host.Delete(HostName);
            }
            else
            {
                Log.LogMessage("BizTalk Host '{0}' does not exist, skipping delete action.", HostName);
            }

            return true;
        }
    }
}