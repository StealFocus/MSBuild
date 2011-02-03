// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DisableAllReceiveLocations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DisableAllReceiveLocations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009DisableAllReceiveLocations Class.
    /// </summary>
    public class Bts2009DisableAllReceiveLocations : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkApplication bizTalkApplication = new BizTalkApplication(ManagementDatabaseConnectionString, ApplicationName);
            Log.LogMessage("Disabling all Receive Locations for BizTalk application '{0}'.", ApplicationName);
            bizTalkApplication.DisableAllReceiveLocations();
            return true;
        }

        #endregion // Methods
    }
}
