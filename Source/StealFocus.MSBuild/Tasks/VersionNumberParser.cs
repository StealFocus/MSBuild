// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionNumberParser.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the VersionNumberParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;
    using System.Reflection;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// VersionNumberParser Task.
    /// </summary>
    /// <remarks>
    /// Parses a number with the form factor of 1.0.0.0 into its constituent parts. The task does not validate the 
    /// individual numbers i.e. each number in a genuine <see cref="AssemblyVersionAttribute"/> should be not be 
    /// greater than <see cref="UInt16.MaxValue"/>, this is not validated.
    /// </remarks>
    public class VersionNumberParser : Task
    {
        #region Fields

        /// <summary>
        /// Holds the separator for version numbers.
        /// </summary>
        private const char Separator = '.';

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Version Number e.g. "1.0.0.0".
        /// </summary>
        [Required]
        public string VersionNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Major Version Number.
        /// </summary>
        [Output]
        public int MajorVersionNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Minor Version Number.
        /// </summary>
        [Output]
        public int MinorVersionNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Build Number.
        /// </summary>
        [Output]
        public int BuildNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Revision Number.
        /// </summary>
        [Output]
        public int RevisionNumber
        {
            get;
            private set;
        }

        #endregion // Properties

        #region Method

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            string[] buildNumberComponents = this.VersionNumber.Split(Separator);
            if (buildNumberComponents.Length != 4)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "The supplied Version Number of '{0}' was not a valid Version Number i.e. did not follow the '1.0.0.0' form factor.", this.VersionNumber);
                Log.LogMessage(errorMessage);
                return false;
            }

            this.MajorVersionNumber = int.Parse(buildNumberComponents[0], CultureInfo.CurrentCulture);
            this.MinorVersionNumber = int.Parse(buildNumberComponents[1], CultureInfo.CurrentCulture);
            this.BuildNumber = int.Parse(buildNumberComponents[2], CultureInfo.CurrentCulture);
            this.RevisionNumber = int.Parse(buildNumberComponents[3], CultureInfo.CurrentCulture);
            return true;
        }

        #endregion // Method
    }
}
