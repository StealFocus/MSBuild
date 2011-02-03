// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009StartOrchestrations.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009StartOrchestrations type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    using Microsoft.Build.Framework;

    /// <summary>
    /// Bts2009StartOrchestrations Class.
    /// </summary>
    public class Bts2009StartOrchestrations : BizTalkApplicationTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the orchestration names.
        /// </summary>
        [Required]
        public ITaskItem[] OrchestrationNames
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
            foreach (ITaskItem orchestrationName in this.OrchestrationNames)
            {
                Log.LogMessage("Starting Orchestration '{0}' for BizTalk application '{1}'.", orchestrationName.ItemSpec, ApplicationName);
                bizTalkApplication.StartOrchestration(orchestrationName.ItemSpec);
            }

            return true;
        }

        #endregion // Methods
    }
}