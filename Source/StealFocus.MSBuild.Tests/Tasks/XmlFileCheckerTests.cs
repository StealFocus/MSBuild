// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileCheckerTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   FileRegexTests Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;
    using System.IO;

    using Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tasks;

    /// <summary>
    /// XmlFileCheckerTests Class.
    /// </summary>
    [TestClass]
    public class XmlFileCheckerTests : MSBuildTests
    {
        #region Fields

        /// <summary>
        /// Holds the TestFileRegexFailFile file name.
        /// </summary>
        private const string TestXmlFileCheckerFailConfiguration = "TestXmlFileCheckerFailConfiguration.xml";

        /// <summary>
        /// Holds the TestFileRegexSuccessFile file name.
        /// </summary>
        private const string TestXmlFileCheckerFailFile = "TestXmlFileCheckerFailFile.xml";

        /// <summary>
        /// Holds the TestFileRegexFailFile file name.
        /// </summary>
        private const string TestXmlFileCheckerSuccessConfiguration = "TestXmlFileCheckerSuccessConfiguration.xml";

        /// <summary>
        /// Holds the TestFileRegexSuccessFile file name.
        /// </summary>
        private const string TestXmlFileCheckerSuccessFile = "TestXmlFileCheckerSuccessFile.xml";

        #endregion // Fields

        #region Initialize & Cleanup

        /// <summary>
        /// Initialise the test.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestXmlFileCheckerFailConfiguration.xml", TestXmlFileCheckerFailConfiguration);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestXmlFileCheckerFailFile.xml", TestXmlFileCheckerFailFile);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestXmlFileCheckerSuccessConfiguration.xml", TestXmlFileCheckerSuccessConfiguration);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestXmlFileCheckerSuccessFile.xml", TestXmlFileCheckerSuccessFile);
        }

        /// <summary>
        /// Clean-up the test.
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            File.Delete(TestXmlFileCheckerFailConfiguration);
            File.Delete(TestXmlFileCheckerFailFile);
            File.Delete(TestXmlFileCheckerSuccessConfiguration);
            File.Delete(TestXmlFileCheckerSuccessFile);
        }

        #endregion // Initialize & Cleanup

        /// <summary>
        /// Tests <see cref="XmlFileChecker.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestXmlFileCheckerSuccess()
        {
            const string TargetName = "TestXmlFileCheckerSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="XmlFileChecker.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestXmlFileCheckerFail()
        {
            const string TargetName = "TestXmlFileCheckerFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }
    }
}
