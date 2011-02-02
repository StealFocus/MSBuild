// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRegex.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the FileRegex type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// FileRegex Class.
    /// </summary>
    public class FileRegex : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the files to search in.
        /// </summary>
        [Required]
        public ITaskItem[] Files
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        [Required]
        public string RegularExpression
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use case-insensitive matching.
        /// </summary>
        public bool IgnoreCase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use multi-line matching.
        /// </summary>
        /// <remarks>
        /// Changing the meaning of ^ and $ so they match at the beginning and end, respectively, of any line, and not just the beginning and end of the entire string.
        /// </remarks>
        public bool Multiline
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use single-line matching.
        /// </summary>
        /// <summary>
        /// Changing the meaning of the dot (.) so it matches every character (instead of every character except \n). 
        /// </summary>
        public bool Singleline
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the maximum number of times the replacement can occur.
        /// </summary>
        [Output]
        public int MatchCount
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            RegexOptions options = RegexOptions.None;
            if (this.IgnoreCase)
            {
                options |= RegexOptions.IgnoreCase;
            }

            if (this.Multiline)
            {
                options |= RegexOptions.Multiline;
            }

            if (this.Singleline)
            {
                options |= RegexOptions.Singleline;
            }

            this.MatchCount = 0;
            Regex replaceRegex = new Regex(this.RegularExpression, options);
            Log.LogMessage(MessageImportance.Low, string.Format(CultureInfo.CurrentCulture, "Searching {0} file(s).", this.Files.Length));
            foreach (ITaskItem item in this.Files)
            {
                string fileName = item.ItemSpec;
                Log.LogMessage(MessageImportance.Low, "Searching file '{0}'.", fileName);
                if (!File.Exists(fileName))
                {
                    Log.LogError("File '{0}' does not exist.", fileName);
                    return false;
                }

                int matchCount = 0;
                string buffer = File.ReadAllText(fileName);
                Match match = replaceRegex.Match(buffer);
                while (match.Success)
                {
                    matchCount++;
                    match = match.NextMatch();
                }

                this.MatchCount += matchCount;
                Log.LogMessage(MessageImportance.Normal, "Regular expression '{0}' matched {1} in file '{2}'.", this.RegularExpression, matchCount, fileName);
            }

            return true;
        }
    }
}
