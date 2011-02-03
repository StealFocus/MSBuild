// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009TerminateAllOrchestrations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009TerminateAllOrchestrations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009TerminateAllOrchestrations Class.
    /// </summary>
    public class Bts2009TerminateAllOrchestrations : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Terminating all Orchestrations for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.TerminateAllOrchestrations();
            return true;
        }

        #endregion // Methods
    }
}