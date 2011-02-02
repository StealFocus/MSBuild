// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTime.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   DateTime Task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// DateTime Task.
    /// </summary>
    public class DateTime : Task
    {
        /// <summary>
        /// Place holder for the provided DateTime.
        /// </summary>
        private System.DateTime providedDateTime;

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
        /// Gets the Day of the Year.
        /// </summary>
        [Output]
        public int DayOfYear
        {
            get
            {
                return this.providedDateTime.DayOfYear;
            }
        }

        /// <summary>
        /// Gets the Day of the Year.
        /// </summary>
        [Output]
        public string DayOfYearThreeDigits
        {
            get
            {
                return this.providedDateTime.DayOfYear.ToString("000", CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets the Month.
        /// </summary>
        [Output]
        public int Month
        {
            get
            {
                return this.providedDateTime.Month;
            }
        }

        /// <summary>
        /// Gets the Year.
        /// </summary>
        [Output]
        public int Year
        {
            get
            {
                return this.providedDateTime.Year;
            }
        }

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            try
            {
                this.providedDateTime = System.DateTime.ParseExact(this.DateTimeUtcString, "u", CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime string of '{0}' could not be parsed as a UTC DateTime.", this.DateTimeUtcString);
                Log.LogError(errorMessage);
                return false;
            }

            return true;
        }
    }
}
