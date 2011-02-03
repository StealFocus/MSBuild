// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSBuildTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   MSBuildTests Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Base Class for testing MSBuild tasks.
    /// </summary>
    [TestClass]
    public abstract class MSBuildTests
    {
        /// <summary>
        /// Holds the file name.
        /// </summary>
        protected const string MSBuildProjectFileName = "StealFocus.MSBuild.Tests.Tasks.msbuild";

        /// <summary>
        /// Holds the path to MSBuild.
        /// </summary>
        private static readonly string microsoftBuildPath = Environment.ExpandEnvironmentVariables(@"%systemdrive%\WINDOWS\Microsoft.NET\Framework\v3.5\MSBuild.exe");

        /// <summary>
        /// Initialise the test.
        /// </summary>
        [TestInitialize]
        public virtual void Initialize()
        {
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.Tests.msbuild", MSBuildProjectFileName);
        }

        /// <summary>
        /// Clean-up the test.
        /// </summary>
        [TestCleanup]
        public virtual void Cleanup()
        {
            File.Delete(MSBuildProjectFileName);
        }

        /// <summary>
        /// Run MSBuild.
        /// </summary>
        /// <param name="msbuildExeArguments">The arguments.</param>
        /// <returns>Result of the call.</returns>
        protected static int RunMSBuild(string msbuildExeArguments)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(".");
            return RunMSBuild(msbuildExeArguments, directoryInfo.FullName);
        }

        /// <summary>
        /// Run MSBuild.
        /// </summary>
        /// <param name="msbuildExeArguments">The arguments.</param>
        /// <param name="workingDirectory">The working directory.</param>
        /// <returns>Result of the call.</returns>
        protected static int RunMSBuild(string msbuildExeArguments, string workingDirectory)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(microsoftBuildPath, msbuildExeArguments);
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.WorkingDirectory = workingDirectory;
            Process process = Process.Start(processStartInfo);
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                Console.WriteLine(process.StandardError.ReadToEnd());
            }

            return process.ExitCode;
        }
    }
}