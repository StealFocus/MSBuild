// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StartAllSendPorts.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StartAllSendPorts type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StartAllSendPorts Class.
    /// </summary>
    public class Bts2009StartAllSendPorts : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Starting all Send Ports for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.StartAllSendPorts();
            return true;
        }

        #endregion // Methods
    }
}