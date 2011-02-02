// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateFormatterTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   DateFormatterTests Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tasks;

    /// <summary>
    /// DateFormatterTests Class.
    /// </summary>
    [TestClass]
    public class DateFormatterTests : MSBuildTests
    {
        /// <summary>
        /// Tests <see cref="DateFormatter.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDateFormatterFail()
        {
            const string TargetName = "TestDateFormatterFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="DateFormatter.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDateFormatterSuccess()
        {
            const string TargetName = "TestDateFormatterSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }
    }
}
