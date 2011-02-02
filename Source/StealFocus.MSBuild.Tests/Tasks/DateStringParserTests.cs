// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateStringParserTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   DateStringParserTests Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tasks;

    /// <summary>
    /// DateStringParserTests Class.
    /// </summary>
    [TestClass]
    public class DateStringParserTests : MSBuildTests
    {
        /// <summary>
        /// Tests <see cref="DateStringParser.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDateStringParserFail()
        {
            const string TargetName = "DateStringParserFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="DateStringParser.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDateStringParserSuccess()
        {
            const string TargetName = "DateStringParserSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }
    }
}
