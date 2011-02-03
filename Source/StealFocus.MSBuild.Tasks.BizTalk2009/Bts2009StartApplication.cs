// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StartApplication.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StartApplication type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009StartApplication Class.
    /// </summary>
    public class Bts2009StartApplication : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Starting BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.StartAll();
            return true;
        }

        #endregion // Methods
    }
}