// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFileRegexReplace.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   TextFileRegexReplace Class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using Core.Xml.Serialization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// TextFileRegexReplace Class.
    /// </summary>
    public class TextFileRegexReplace : Task
    {
        #region Fields

        /// <summary>
        /// Holds the results.
        /// </summary>
        private readonly Dictionary<Entry, int> results = new Dictionary<Entry, int>();

        /// <summary>
        /// Holds the regular expression options.
        /// </summary>
        private RegexOptions regexOptions;

        /// <summary>
        /// Holds the updates.
        /// </summary>
        private TextFileRegexReplaceDictionary textFileRegexReplaceDictionary;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to apply case-insensitive matching.
        /// </summary>
        /// <value><c>True</c> if [ignore case]; otherwise, <c>false</c>.</value>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use multi-line matching.
        /// </summary>
        /// <value><c>True</c> if multiline; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Changing the meaning of ^ and $ so they match at the beginning and end, respectively, of any line, and not just the beginning and end of the entire string.
        /// </remarks>
        public bool Multiline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use single-line matching. 
        /// </summary>
        /// <value><c>True</c> if singleline; otherwise, <c>false</c>.</value>
        /// <remarks>
        /// Changing the meaning of the dot (.) so it matches every character (instead of every character except \n).
        /// </remarks>
        public bool Singleline { get; set; }

        /// <summary>
        /// Gets or sets the paths of the files to be replaced.
        /// </summary>
        [Required]
        public ITaskItem[] TargetFilePaths { get; set; }

        /// <summary>
        /// Gets or sets the path of dictionary file.
        /// </summary>
        [Required]
        public string DictionaryFilePath { get; set; }

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
            this.regexOptions = this.CreateRegexOptions();
            string dictionaryFileContents = File.ReadAllText(this.DictionaryFilePath);
            this.textFileRegexReplaceDictionary = SimpleXmlSerializer<TextFileRegexReplaceDictionary>.Deserialize(dictionaryFileContents);
            foreach (Entry dictionaryEntry in this.textFileRegexReplaceDictionary.Entry)
            {
                this.results.Add(dictionaryEntry, 0);
            }

            foreach (ITaskItem targetFilePath in this.TargetFilePaths)
            {
                FileInfo file = new FileInfo(targetFilePath.ItemSpec);
                this.EnsureFileExists(file.FullName);
                this.EnsureFileIsWriteable(file.FullName);
                this.UpdateFile(file.FullName);
            }

            StringBuilder errorMessage = new StringBuilder();
            errorMessage.AppendLine(string.Format(CultureInfo.CurrentCulture, "The following regular expressions did not have number of matches specified in the dictionary at '{0}':", this.DictionaryFilePath));
            bool expectedMatchesFailed = false;
            foreach (Entry dictionaryEntry in this.results.Keys)
            {
                int actualMatches = this.results[dictionaryEntry];
                if (actualMatches != dictionaryEntry.ExpectedMatches)
                {
                    expectedMatchesFailed = true;
                    errorMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Regex:'{0}'; Expected matches:{1}; Actual matches:{2}", dictionaryEntry.RegularExpression, dictionaryEntry.ExpectedMatches, actualMatches));
                }
            }

            if (expectedMatchesFailed)
            {
                this.Log.LogError(errorMessage.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Ensures the file is writeable.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void EnsureFileIsWriteable(string fileName)
        {
            if (new FileInfo(fileName).IsReadOnly)
            {
                if (Log != null)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, "Target file path '{0}' is read only.", fileName);
                    Log.LogError(message);
                }
            }
        }

        /// <summary>
        /// Ensures the file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void EnsureFileExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                if (Log != null)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, "Could not find target file path '{0}'.", fileName);
                    Log.LogError(message);
                }
            }
        }

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void UpdateFile(string fileName)
        {
            foreach (Entry dictionaryEntry in this.textFileRegexReplaceDictionary.Entry)
            {
                int matchCount = 0;
                Regex replaceRegex = new Regex(dictionaryEntry.RegularExpression, this.regexOptions);
                string buffer = File.ReadAllText(fileName);

                // Count matches
                Match match = replaceRegex.Match(buffer);
                while (match.Success)
                {
                    matchCount++;
                    match = match.NextMatch();
                }

                // Replace string
                string updatedContent = replaceRegex.Replace(buffer, dictionaryEntry.ReplacementValue);
                File.WriteAllText(fileName, updatedContent);
                this.results[dictionaryEntry] += matchCount;
            }
        }

        /// <summary>
        /// Creates the regular expression options.
        /// </summary>
        /// <returns>The regular expression options.</returns>
        private RegexOptions CreateRegexOptions()
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

            return options;
        }

        #endregion // Methods
    }
}
