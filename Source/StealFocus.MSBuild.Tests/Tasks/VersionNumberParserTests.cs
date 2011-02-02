// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionNumberParserTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   VersionNumberParserTests Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tasks;

    /// <summary>
    /// VersionNumberParserTests Class.
    /// </summary>
    [TestClass]
    public class VersionNumberParserTests : MSBuildTests
    {
        /// <summary>
        /// Tests <see cref="VersionNumberParser.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestVersionNumberParserFail()
        {
            const string TargetName = "VersionNumberParserFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="VersionNumberParser.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestVersionNumberParserSuccess()
        {
            const string TargetName = "VersionNumberParserSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }
    }
}
