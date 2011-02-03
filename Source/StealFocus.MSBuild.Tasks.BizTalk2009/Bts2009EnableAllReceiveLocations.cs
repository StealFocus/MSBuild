// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009EnableAllReceiveLocations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009EnableAllReceiveLocations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009EnableAllReceiveLocations Class.
    /// </summary>
    public class Bts2009EnableAllReceiveLocations : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Enabling all Receive Locations for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.EnableAllReceiveLocations();
            return true;
        }

        #endregion // Methods
    }
}