// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GacUninstall.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   GacUninstall Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// GacUninstall Class.
    /// </summary>
    [TestClass]
    public class GacUninstall : MSBuildTests
    {
        /// <summary>
        /// Tests <see cref="GacUninstall.Execute()"/>.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void IntegrationTestGacUninstallSuccess()
        {
            const string TargetName = "GacUninstallSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed when it was not expected to.");
        }
    }
}
