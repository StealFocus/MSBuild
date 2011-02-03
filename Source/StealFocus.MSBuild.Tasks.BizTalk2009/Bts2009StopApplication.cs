// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StopApplication.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StopApplication type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StopApplication Class.
    /// </summary>
    public class Bts2009StopApplication : BizTalkApplicationTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [terminate orchestrations].
        /// </summary>
        public bool TerminateOrchestrations
        {
            get;
            set;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            if (this.TerminateOrchestrations)
            {
                Log.LogMessage("Disabling all Receive Locations for BizTalk application '{0}'.", ApplicationName);
                bizTalkApplication.DisableAllReceiveLocations();
                Log.LogMessage("Terminating all Orchestrations for BizTalk application '{0}'.", ApplicationName);
                bizTalkApplication.TerminateAllOrchestrations();
            }

            Log.LogMessage("Bringing BizTalk application '{0}' to a complete stop.", ApplicationName);
            bizTalkApplication.StopAll();
            return true;
        }

        #endregion // Methods
    }
}