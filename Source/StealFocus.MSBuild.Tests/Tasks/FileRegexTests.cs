// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRegexTests.cs" company="StealFocus">
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
    /// FileRegexTests Class.
    /// </summary>
    [TestClass]
    public class FileRegexTests : MSBuildTests
    {
        #region Fields

        /// <summary>
        /// Holds the TestFileRegexFailFile file name.
        /// </summary>
        private const string TestFileRegexFailFile = "TestFileRegexFailFile.txt";

        /// <summary>
        /// Holds the TestFileRegexSuccessFile file name.
        /// </summary>
        private const string TestFileRegexSuccessFile = "TestFileRegexSuccessFile.txt";

        #endregion // Fields

        #region Initialize & Cleanup

        /// <summary>
        /// Initialise the test.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestFileRegexFailFile.txt", TestFileRegexFailFile);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestFileRegexSuccessFile.txt", TestFileRegexSuccessFile);
        }

        /// <summary>
        /// Clean-up the test.
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            File.Delete(TestFileRegexFailFile);
            File.Delete(TestFileRegexSuccessFile);
        }

        #endregion // Initialize & Cleanup

        /// <summary>
        /// Tests <see cref="FileRegex.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestScriptFileRegexFail()
        {
            const string TargetName = "TestFileRegexFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="FileRegex.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestScriptFileRegexSuccess()
        {
            const string TargetName = "TestFileRegexSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }
    }
}
