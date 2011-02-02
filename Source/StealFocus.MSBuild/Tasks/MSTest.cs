// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSTest.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the MSTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using Core.IO;

    using Microsoft.Build.Framework;

    /// <summary>
    /// MSTest Task.
    /// </summary>
    public class MSTest : FrameworkVersionDependentTask
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Binaries Root Path.
        /// </summary>
        [Required]
        public string BinariesRootPath { get; set; }

        /// <summary>
        /// Gets or sets the Test Run Config File Path.
        /// </summary>
        [Required]
        public string TestRunConfigFilePath { get; set; }

        /// <summary>
        /// Gets or sets the Team Foundation Server Name.
        /// </summary>
        public string TeamFoundationServerName { get; set; }

        /// <summary>
        /// Gets or sets the Build Number.
        /// </summary>
        public string TeamFoundationBuildNumber { get; set; }

        /// <summary>
        /// Gets or sets the Team Project Name.
        /// </summary>
        public string TeamProjectName { get; set; }

        /// <summary>
        /// Gets or sets the Flavor To Build.
        /// </summary>
        [Required]
        public string FlavorToBuild { get; set; }

        /// <summary>
        /// Gets or sets the Platform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the Working Directory.
        /// </summary>
        [Required]
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to publish the Test Results.
        /// </summary>
        public bool PublishTestResults { get; set; }

        /// <summary>
        /// Gets or sets the .
        /// </summary>
        [Required]
        public ITaskItem[] TestContainerSearchFilters { get; set; }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns>Indicating if the task was successful.</returns>
        public override bool Execute()
        {
            if (!Directory.Exists(this.BinariesRootPath))
            {
                Log.LogError("The provided binaries root path of '{0}' does not exist.", this.BinariesRootPath);
            }

            string[] outputDirectories = Directory.GetDirectories(this.BinariesRootPath);
            foreach (string directory in outputDirectories)
            {
                string buildFlavor = GetBuildFlavorFromPath(directory);
                if (string.IsNullOrEmpty(this.FlavorToBuild) || buildFlavor == this.FlavorToBuild)
                {
                    bool success = this.ProcessBuildFlavor(buildFlavor, directory);
                    if (!success)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the build flavor from path.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>The build flavour.</returns>
        private static string GetBuildFlavorFromPath(string directoryPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            return dirInfo.Name;
        }

        /// <summary>
        /// Processes the build flavor.
        /// </summary>
        /// <param name="buildFlavor">The build flavor.</param>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>Whether this was a success.</returns>
        private bool ProcessBuildFlavor(string buildFlavor, string directoryPath)
        {
            Log.LogMessage(MessageImportance.Normal, "Testing Build Flavor: {0}", buildFlavor);
            string[] testContainerPaths = this.GetTestContainerList(directoryPath);
            StringBuilder testContainerArgs = new StringBuilder();
            foreach (string testContainer in testContainerPaths)
            {
                testContainerArgs.AppendFormat(CultureInfo.CurrentCulture, " /testcontainer:\"{0}\"", testContainer);
            }

            if (testContainerArgs.Length > 0)
            {
                string arguments;
                if (this.PublishTestResults)
                {
                    arguments = string.Format(CultureInfo.CurrentCulture, "/runconfig:{0}{1} /publish:{2} /publishBuild:\"{3}\" /teamproject:\"{4}\" /flavor:\"{5}\" /platform:\"{6}\"", this.TestRunConfigFilePath, testContainerArgs, this.TeamFoundationServerName, this.TeamFoundationBuildNumber, this.TeamProjectName, buildFlavor, this.Platform);
                }
                else
                {
                    arguments = string.Format(CultureInfo.CurrentCulture, "/runconfig:{0}{1}", this.TestRunConfigFilePath, testContainerArgs);
                }

                string microsoftTestDotExePath = this.GetMSTestExePath();
                Log.LogMessage(string.Format(CultureInfo.CurrentCulture, "Using MSTest at location '{0}'.", microsoftTestDotExePath));
                Log.LogMessage(string.Format(CultureInfo.CurrentCulture, "Using MSTest with arguments '{0}'.", arguments));
                ProcessStartInfo processStartInfo = new ProcessStartInfo(microsoftTestDotExePath, arguments);
                processStartInfo.WorkingDirectory = this.WorkingDirectory;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                Process process = Process.Start(processStartInfo);
                if (process == null)
                {
                    throw new MSBuildException("There was a problem launching an MSBuild process.");
                }

                string standardOutput = process.StandardOutput.ReadToEnd();
                string standardError = process.StandardError.ReadToEnd();
                process.WaitForExit();
                Log.LogMessage(MessageImportance.High, standardOutput);
                if (process.ExitCode != 0)
                {
                    Log.LogError(standardError);
                    Log.LogError("Test run failed.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the test container list.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>The list of test containers.</returns>
        private string[] GetTestContainerList(string directoryPath)
        {
            Log.LogMessage(MessageImportance.Normal, "Test Containers to be processed:");
            ArrayList allTestContainerPaths = new ArrayList();
            foreach (ITaskItem testContainerSearchFilter in this.TestContainerSearchFilters)
            {
                string[] testContainerPaths = Directory.GetFiles(directoryPath, testContainerSearchFilter.ItemSpec);
                ArrayList filteredTestContainerPaths = new ArrayList();
                foreach (string file in testContainerPaths)
                {
                    if (FileSystem.IsAssembly(file) || file.EndsWith(".loadtest", StringComparison.CurrentCultureIgnoreCase) || file.EndsWith(".webtest", StringComparison.CurrentCultureIgnoreCase))
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        filteredTestContainerPaths.Add(fileInfo.FullName);
                    }
                }

                testContainerPaths = (string[])filteredTestContainerPaths.ToArray(typeof(string));
                foreach (string file in testContainerPaths)
                {
                    Log.LogMessage(MessageImportance.Normal, file);
                }

                allTestContainerPaths.AddRange(testContainerPaths);
            }

            return (string[])allTestContainerPaths.ToArray(typeof(string));
        }

        /// <summary>
        /// Gets the path to mstest.exe.
        /// </summary>
        /// <returns>The path to mstest.exe.</returns>
        private string GetMSTestExePath()
        {
            if (FrameworkVersion == FrameworkVersions.Version20 || FrameworkVersion == FrameworkVersions.Version30)
            {
                return Environment.ExpandEnvironmentVariables("%VS80COMNTOOLS%\\..\\IDE\\mstest.exe");
            }

            if (FrameworkVersion == FrameworkVersions.Version35)
            {
                return Environment.ExpandEnvironmentVariables("%VS90COMNTOOLS%\\..\\IDE\\mstest.exe");
            }

            if (FrameworkVersion == FrameworkVersions.Version40)
            {
                return Environment.ExpandEnvironmentVariables("%VS100COMNTOOLS%\\..\\IDE\\mstest.exe");
            }

            string exceptionMessage = string.Format(CultureInfo.CurrentCulture, "The supplied 'FrameworkVersion' value of '{0}' was not supported.", this.FrameworkVersion);
            throw new MSBuildException(exceptionMessage);
        }

        #endregion // Methods
    }
}
