﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkVersionDependentTask.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   FrameworkVersionDependentTask Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// FrameworkVersionDependentTask Class.
    /// </summary>
    public abstract class FrameworkVersionDependentTask : Task
    {
        /// <summary>
        /// Gets or sets the .NET Framework version.
        /// </summary>
        [Required]
        public string FrameworkVersion { get; set; }

        /// <summary>
        /// Holds a set of valid .NET Framework versions.
        /// </summary>
        protected static class FrameworkVersions
        {
            /// <summary>
            /// Version 2.0.
            /// </summary>
            public const string Version20 = "2.0";

            /// <summary>
            /// Version 3.0.
            /// </summary>
            public const string Version30 = "3.0";

            /// <summary>
            /// Version 3.5.
            /// </summary>
            public const string Version35 = "3.5";

            /// <summary>
            /// Version 3.5.
            /// </summary>
            public const string Version40 = "4.0";
        }
    }
}
