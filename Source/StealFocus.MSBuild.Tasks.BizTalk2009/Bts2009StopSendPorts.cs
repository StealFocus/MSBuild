// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StopSendPorts.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StopSendPorts type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009StopSendPorts Class.
    /// </summary>
    public class Bts2009StopSendPorts : BizTalkApplicationTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the send port names.
        /// </summary>
        [Required]
        public ITaskItem[] SendPortNames
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
            foreach (ITaskItem sendPortName in this.SendPortNames)
            {
                Log.LogMessage("Stopping Send Port '{0}' for BizTalk application '{1}'.", sendPortName.ItemSpec, ApplicationName);
                bizTalkApplication.StopSendPort(sendPortName.ItemSpec);
            }

            return true;
        }

        #endregion // Methods
    }
}