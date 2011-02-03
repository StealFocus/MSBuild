// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StartAllOrchestrations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StartAllOrchestrations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StartAllOrchestrations Class.
    /// </summary>
    public class Bts2009StartAllOrchestrations : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Starting all Orchestrations for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.StartAllOrchestrations();
            return true;
        }

        #endregion // Methods
    }
}