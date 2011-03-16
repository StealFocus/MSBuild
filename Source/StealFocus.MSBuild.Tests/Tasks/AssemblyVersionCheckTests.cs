// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyVersionCheckTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the AssemblyVersionCheckTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using StealFocus.MSBuild.Tasks;

    /// <summary>
    /// AssemblyVersionCheckTests Class.
    /// </summary>
    [TestClass]
    public class AssemblyVersionCheckTests : MSBuildTests
    {
        /// <summary>
        /// Tests <see cref="AssemblyVersionCheck.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestAssemblyVersionCheckSuccess()
        {
            const string TargetName = "AssemblyVersionCheckSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }

        /// <summary>
        /// Tests <see cref="AssemblyVersionCheck.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestAssemblyVersionCheckFail()
        {
            const string TargetName = "AssemblyVersionCheckFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }
    }
}
