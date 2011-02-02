// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateFormatter.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateFormatter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// DateFormatter Task.
    /// </summary>
    public class DateFormatter : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the DateTime in UTC format.
        /// </summary>
        [Required]
        public string DateTimeUtcString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the string holding the date format e.g. "yMMdd".
        /// </summary>
        [Required]
        public string OutputDateTimeFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the DateTime string.
        /// </summary>
        [Output]
        public string DateTimeString
        {
            get;
            private set;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            System.DateTime providedDateTime;
            try
            {
                providedDateTime = System.DateTime.ParseExact(this.DateTimeUtcString, "u", CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime string of '{0}' could not be parsed as a UTC DateTime.", this.DateTimeUtcString);
                Log.LogError(errorMessage);
                return false;
            }

            this.DateTimeString = providedDateTime.ToString(this.OutputDateTimeFormatString, CultureInfo.CurrentCulture);
            return true;
        }

        #endregion // Methods
    }
}
