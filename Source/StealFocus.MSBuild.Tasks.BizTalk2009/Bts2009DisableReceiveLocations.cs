// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009DisableReceiveLocations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009DisableReceiveLocations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009DisableReceiveLocations Class.
    /// </summary>
    public class Bts2009DisableReceiveLocations : BizTalkApplicationTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the receive location names.
        /// </summary>
        [Required]
        public ITaskItem[] ReceiveLocationNames
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
            foreach (ITaskItem recieveLocationName in this.ReceiveLocationNames)
            {
                Log.LogMessage("Disabling Receive Location '{0}' for BizTalk application '{1}'.", recieveLocationName.ItemSpec, ApplicationName);
                bizTalkApplication.DisableReceiveLocation(recieveLocationName.ItemSpec);
            }

            return true;
        }

        #endregion // Methods
    }
}
