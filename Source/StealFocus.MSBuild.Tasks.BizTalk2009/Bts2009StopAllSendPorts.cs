// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StopAllSendPorts.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StopAllSendPorts type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StopAllSendPorts Class.
    /// </summary>
    public class Bts2009StopAllSendPorts : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Stopping all Send Ports for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.StopAllSendPorts();
            return true;
        }

        #endregion // Methods
    }
}