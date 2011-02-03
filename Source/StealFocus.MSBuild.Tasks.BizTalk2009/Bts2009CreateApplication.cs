// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009CreateApplication.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009CreateApplication type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009
{
    using Core.BizTalk2009;

    /// <summary>
    /// Bts2009CreateApplication Class.
    /// </summary>
    public class Bts2009CreateApplication : BizTalkApplicationTask
    {
        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            BizTalkCatalogExplorer bizTalkCatalogExplorer = new BizTalkCatalogExplorer(ManagementDatabaseConnectionString);
            if (bizTalkCatalogExplorer.ApplicationExists(ApplicationName))
            {
                Log.LogMessage("A BizTalk application with the name '{0}' already exists.", ApplicationName);
                return false;
            }

            Log.LogMessage("Creating a BizTalk application with the name '{0}'.", ApplicationName);
            bizTalkCatalogExplorer.CreateApplication(this.ApplicationName);
            return true;
        }

        #endregion // Methods
    }
}