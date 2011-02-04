// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="Bts2009Tests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the Bts2009Tests type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks.BizTalk2009.Tests
{
    using System.Globalization;

    using Core.BizTalk2009;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tests.Tasks;

    using StealFocus.Core;

    /// <summary>
    /// Tests for BizTalk 2006 MSBuild Tasks.
    /// </summary>
    [TestClass]
    public class Bts2009Tests : MSBuildTests
    {
        /// <summary>
        /// Holds the MSBuild project file name.
        /// </summary>
        public new const string MSBuildProjectFileName = "StealFocus.MSBuild.Tasks.BizTalk2009.Tests.msbuild";

        /// <summary>
        /// Tests Create and Remove.
        /// </summary>
        [TestMethod]
        public void IntegrationTestCreateAndRemove()
        {
            const string TargetName = "CreateAndRemove";
            const string ManagementDatabaseConnectionString = "server=.\\MSSqlSvr2008;database=BizTalkMgmtDb;integrated security=sspi;";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1} /p:ManagementDatabaseConnectionString=\"{2}\"", MSBuildProjectFileName, TargetName, ManagementDatabaseConnectionString);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed.");
        }

        /// <summary>
        /// Tests Host manipulation.
        /// </summary>
        [TestMethod]
        public void IntegrationTestHostsManipulation()
        {
            const string HostName = "BTS_StealFocusMSBuildTestHostA";

            // Make sure the Handlers do not exist
            ReceiveHandler.Delete("FILE", HostName);
            SendHandler.Delete("FILE", HostName);
            try
            {
                // Make sure the Host does not exist
                Host.Delete(HostName);
            }
            catch (CoreException)
            {
                // Do nothing
            }

            const string TargetName = "HostsManipulation";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed.");
        }

        /// <summary>
        /// Test Initialize.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            Assert.IsNotNull(typeof(BizTalkApplicationTask)); // Needs to be here to ensure "StealFocus.MSBuild.Tasks.BizTalk2009.dll" is copied to Tests directory.
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tasks.BizTalk2009.Tests", "StealFocus.MSBuild.Tasks.BizTalk2009.Tests.Resources.Tests.msbuild", MSBuildProjectFileName);
        }

        /// <summary>
        /// Test Cleanup.
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
        }
    }
}
