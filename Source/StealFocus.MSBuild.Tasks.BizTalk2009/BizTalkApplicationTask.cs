// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="BizTalkApplicationTask.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the BizTalkApplicationTask type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// BizTalkApplicationTask Class.
    /// </summary>
    public abstract class BizTalkApplicationTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the management database connection string.
        /// </summary>
        [Required]
        public string ManagementDatabaseConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        [Required]
        public string ApplicationName
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
        public abstract override bool Execute();

        #endregion // Methods
    }
}