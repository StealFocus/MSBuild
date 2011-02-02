// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateStringParser.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateStringParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// DateStringParser Task.
    /// </summary>
    public class DateStringParser : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the string holding the date e.g. "20091231" (for 31/12/2009).
        /// </summary>
        [Required]
        public string InputDateTimeString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the string holding the date format e.g. "yyyyMMdd".
        /// </summary>
        [Required]
        public string InputDateTimeFormatString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the DateTime in UTC format.
        /// </summary>
        [Output]
        public string DateTimeUtcString
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
            System.DateTime dateTime;
            try
            {
                dateTime = System.DateTime.ParseExact(this.InputDateTimeString, this.InputDateTimeFormatString, CultureInfo.CurrentCulture);
            }
            catch (FormatException)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "Could not parse the provided DateTime string of '{0}' with the provided format string of '{1}'.", this.InputDateTimeString, this.InputDateTimeFormatString);
                Log.LogMessage(errorMessage);
                return false;
            }

            this.DateTimeUtcString = dateTime.ToString("u", CultureInfo.CurrentCulture);
            return true;
        }

        #endregion // Methods
    }
}
