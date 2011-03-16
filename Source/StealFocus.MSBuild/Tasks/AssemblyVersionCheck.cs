// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyVersionCheck.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the AssemblyVersionCheck type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Checks the version of assemblies.
    /// </summary>
    public class AssemblyVersionCheck : Task
    {
        /// <summary>
        /// The regex for a version number.
        /// </summary>
        private const string VersionNumberRegex = "[0-9]+.[0-9]+.[0-9]+.[0-9]+";

        #region Properties

        /// <summary>
        /// Gets or sets the assemblies to check.
        /// </summary>
        [Required]
        public ITaskItem[] AssembliesToCheck { get; set; }

        /// <summary>
        /// Gets or sets the minimum major version number.
        /// </summary>
        public int MinimumMajorVersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the minimum minor version number.
        /// </summary>
        public int MinimumMinorVersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the minimum build version number.
        /// </summary>
        public int MinimumBuildVersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the minimum revision version number.
        /// </summary>
        public int MinimumRevisionVersionNumber { get; set; }

        #endregion // Properties

        #region Overrides of Task

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// True if the task successfully executed; otherwise, False.
        /// </returns>
        public override bool Execute()
        {
            foreach (ITaskItem item in this.AssembliesToCheck)
            {
                string assemblyfilePath = item.ItemSpec;
                FileInfo assemblyfileInfo = new FileInfo(assemblyfilePath);
                this.Log.LogMessage(MessageImportance.Normal, "Checking version of assembly '{0}'.", assemblyfileInfo.FullName);
                Assembly assembly = Assembly.LoadFile(assemblyfileInfo.FullName);
                Regex replaceRegex = new Regex(VersionNumberRegex, RegexOptions.None);
                Match match = replaceRegex.Match(assembly.FullName);
                string versionValue = match.Value;
                string[] versionComponents = versionValue.Split('.');
                Version minimumVersion = new Version(this.MinimumMajorVersionNumber, this.MinimumMinorVersionNumber, this.MinimumBuildVersionNumber, this.MinimumRevisionVersionNumber);
                int actualMajorVersionNumber = int.Parse(versionComponents[0], CultureInfo.CurrentCulture);
                int actualMinorVersionNumber = int.Parse(versionComponents[1], CultureInfo.CurrentCulture);
                int actualBuildVersionNumber = int.Parse(versionComponents[2], CultureInfo.CurrentCulture);
                int actualRevisionVersionNumber = int.Parse(versionComponents[3], CultureInfo.CurrentCulture);
                Version actualVersion = new Version(actualMajorVersionNumber, actualMinorVersionNumber, actualBuildVersionNumber, actualRevisionVersionNumber);
                this.Log.LogMessage(MessageImportance.Normal, "Version of assembly '{0}' was '{1}'.", assemblyfileInfo.FullName, actualVersion);
                if (actualVersion < minimumVersion)
                {
                    this.Log.LogError("The assembly '{0}' did not meet the minimum version number of '{1}' (actual version was '{2}').", assemblyfileInfo.FullName, minimumVersion, actualVersion);
                    return false;
                }
            }

            return true;
        }

        #endregion // Overrides of Task
    }
}
