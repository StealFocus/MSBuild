// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateGuid.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the CreateGuid type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// CreateGuid Task.
    /// </summary>
    public class CreateGuid : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the format for the GUID, "Digits", "DigitsHyphens", "DigitsHyphensBrackets" or "DigitsHyphensParentheses".
        /// </summary>
        public string Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the GUID.
        /// </summary>
        [Output]
        public string Guid
        {
            get;
            private set;
        }

        #endregion // Properties

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            GuidFormat guidFormat;
            try
            {
                guidFormat = (GuidFormat)Enum.Parse(typeof(GuidFormat), this.Format, true);
            }
            catch (ArgumentException)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "Could not convert the provided GUID format of '{0}' to a GuidFormat type.", this.Format);
                Log.LogError(errorMessage);
                return false;
            }

            string format;
            switch (guidFormat)
            {
                case GuidFormat.Digits:
                    format = "N";
                    break;
                case GuidFormat.DigitsHyphens:
                    format = "D";
                    break;
                case GuidFormat.DigitsHyphensBrackets:
                    format = "B";
                    break;
                case GuidFormat.DigitsHyphensParentheses:
                    format = "P";
                    break;
                default:
                    string errorMessage = string.Format(CultureInfo.CurrentCulture, "The supplied GUID format of '{0}' was not supported.", this.Format);
                    Log.LogError(errorMessage);
                    return false;
            }

            this.Guid = System.Guid.NewGuid().ToString(format, CultureInfo.CurrentCulture).ToUpper(CultureInfo.CurrentCulture);
            return true;
        }
    }
}
