// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="GacUninstall.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the GacUninstall type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    using StealFocus.Core;

    /// <summary>
    /// GacUninstall Class.
    /// </summary>
    public class GacUninstall : FrameworkVersionDependentTask
    {
        #region Fields

        /// <summary>
        /// Holds the name of "sn.exe".
        /// </summary>
        private const string GacUtilDotExeName = "gacutil.exe";

        /// <summary>
        /// Holds a list of reserve names.
        /// </summary>
        private static readonly string[] reservedAssemblyNames = new[] { "AspNet", "Cpp", "csc", "IE", "Interop", "MFC", "Microsoft", "MQ", "MS", "policy", "Presentation", "System", "UIAutomation", "VB", "VSTO", "VsWebSite", "Windows" };

        /// <summary>
        /// Holds a wildcard.
        /// </summary>
        private const string Wildcard = "*";

        /// <summary>
        /// Holds the minimum length allowed for assembly names.
        /// </summary>
        private const int MinimumAssemblyNameLength = 6;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Assembly name.
        /// </summary>
        [Required]
        public string AssemblyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the public key token.
        /// </summary>
        [Required]
        public string PublicKeyToken
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [Required]
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        [Required]
        public string Locale
        {
            get;
            set;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// True if the task successfully executed; otherwise, False.
        /// </returns>
        public override bool Execute()
        {
            if (this.AssemblyName == Wildcard)
            {
                Log.LogError("Please do not supply a wildcard ({}) alone as the Assembly name, this would removed all Assemblies from the GAC.", Wildcard);
                return false;
            }

            if (this.AssemblyName.Length <= MinimumAssemblyNameLength)
            {
                Log.LogError("Please supply an Assembly name longer than {0} characters to reduce the risk of removing Assemblies un-intentionally.", MinimumAssemblyNameLength);
                return false;
            }

            GlobalAssemblyCacheItem[] gacAssemblies = GlobalAssemblyCache.GetAssemblyList(GlobalAssemblyCacheCategoryTypes.Gac);
            ArrayList assembliesToBeRemovedFromGacList = new ArrayList();
            foreach (GlobalAssemblyCacheItem gacAssembly in gacAssemblies)
            {
                if (this.AssemblyShouldBeRemoved(gacAssembly))
                {
                    foreach (string reservedAssemblyName in reservedAssemblyNames)
                    {
                        if (gacAssembly.Name.StartsWith(reservedAssemblyName, StringComparison.OrdinalIgnoreCase))
                        {
                            Log.LogError("The Assembly name '{0}' is a system Assembly and should not be removed from the GAC.", this.AssemblyName);
                            return false;
                        }
                    }

                    string fullyQualifiedAssemblyName = GetFullyQualifiedAssemblyName(gacAssembly);
                    assembliesToBeRemovedFromGacList.Add(fullyQualifiedAssemblyName);
                }
            }

            if (assembliesToBeRemovedFromGacList.Count > 0)
            {
                Log.LogMessage("Removing Assemblies from the GAC.");
                this.RemoveAssembliesFromGac((string[])assembliesToBeRemovedFromGacList.ToArray(typeof(string)));
            }
            else
            {
                Log.LogMessage("No Assemblies were found in the GAC matching the criteria.");
            }

            return true;
        }

        /// <summary>
        /// Assembly ID component matches.
        /// </summary>
        /// <param name="assemblyIdComponent">The assembly ID component.</param>
        /// <param name="specifiedAssemblyIdComponent">The specified assembly ID component.</param>
        /// <returns>Whether there are any matches.</returns>
        private static bool AssemblyIdComponentMatches(string assemblyIdComponent, string specifiedAssemblyIdComponent)
        {
            bool match = false;
            if (specifiedAssemblyIdComponent.EndsWith(Wildcard, StringComparison.OrdinalIgnoreCase))
            {
                string specifiedAssemblyIdComponentMinusWildcard = specifiedAssemblyIdComponent.Substring(0, specifiedAssemblyIdComponent.Length - 1);
                if (assemblyIdComponent.StartsWith(specifiedAssemblyIdComponentMinusWildcard, StringComparison.OrdinalIgnoreCase))
                {
                    match = true;
                }
            }
            else
            {
                if (assemblyIdComponent == specifiedAssemblyIdComponent)
                {
                    match = true;
                }
            }

            return match;
        }

        /// <summary>
        /// Gets a fully qualified Assembly name.
        /// </summary>
        /// <param name="globalAssemblyCacheItem">The item in the GAC.</param>
        /// <returns>The fully qualified name.</returns>
        private static string GetFullyQualifiedAssemblyName(GlobalAssemblyCacheItem globalAssemblyCacheItem)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0},Version={1},Culture={2},PublicKeyToken={3}", globalAssemblyCacheItem.Name, globalAssemblyCacheItem.Version, globalAssemblyCacheItem.Locale, globalAssemblyCacheItem.PublicKeyToken);
        }

        /// <summary>
        /// Determines if assemblies should be removed.
        /// </summary>
        /// <param name="globalAssemblyCacheItem">The global assembly cache item.</param>
        /// <returns>Indicating if the assemblies should be removed.</returns>
        private bool AssemblyShouldBeRemoved(GlobalAssemblyCacheItem globalAssemblyCacheItem)
        {
            bool shouldBeRemoved = false;
            if (AssemblyIdComponentMatches(globalAssemblyCacheItem.Name, this.AssemblyName) &&
                AssemblyIdComponentMatches(globalAssemblyCacheItem.Version, this.Version) &&
                AssemblyIdComponentMatches(globalAssemblyCacheItem.Locale, this.Locale) &&
                AssemblyIdComponentMatches(globalAssemblyCacheItem.PublicKeyToken, this.PublicKeyToken))
            {
                shouldBeRemoved = true;
            }

            return shouldBeRemoved;
        }

        /// <summary>
        /// Remove Assemblies from the GAC.
        /// </summary>
        /// <param name="assembliesToBeRemovedFromGac">The names of Assemblies to remove.</param>
        private void RemoveAssembliesFromGac(IEnumerable<string> assembliesToBeRemovedFromGac)
        {
            foreach (string assemblyToBeRemovedFromGac in assembliesToBeRemovedFromGac)
            {
                // /u myDll,Version=1.1.0.0,Culture=en,PublicKeyToken=874e23ab874e23ab
                Log.LogMessage(MessageImportance.High, "Removing Assembly '{0}' from the GAC.", assemblyToBeRemovedFromGac);
                string gacUtilArgs = string.Format(CultureInfo.CurrentCulture, "/u {0}", assemblyToBeRemovedFromGac);
                this.ExecuteGacUtilDotExe(gacUtilArgs);
            }
        }

        /// <summary>
        /// Execute gacutil.exe.
        /// </summary>
        /// <param name="gacUtilDotExePathArguments">The arguments.</param>
        private void ExecuteGacUtilDotExe(string gacUtilDotExePathArguments)
        {
            string gacUtilDotExePath = ToolLocationHelper.GetPathToDotNetFrameworkSdkFile(GacUtilDotExeName, GetTargetDotNetFrameworkVersion());
            Log.LogMessage(gacUtilDotExePath + " " + gacUtilDotExePathArguments);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(gacUtilDotExePath, gacUtilDotExePathArguments);
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            Process process = Process.Start(processStartInfo);
            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(standardOutput);
            Log.LogMessage(standardOutput);
            if (process.ExitCode != 0)
            {
                Console.WriteLine(standardError);
                Log.LogError(standardError);
            }
        }

        #endregion // Methods
    }
}