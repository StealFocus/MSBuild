// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileProperties.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   FileProperties Task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System;
    using System.Globalization;
    using System.IO;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// FileProperties Task. Use to manipulate properties of a file.
    /// </summary>
    public class FileProperties : Task
    {
        #region Fields

        /// <summary>
        /// Holds the minimum value that can be assigned to a file.
        /// </summary>
        private readonly System.DateTime minimumFileTime = new System.DateTime(1601, 1, 1);

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the paths to the files.
        /// </summary>
        [Required]
        public ITaskItem[] FilePaths { get; set; }

        /// <summary>
        /// Gets or sets the Creation Time UTC.
        /// </summary>
        public string CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the Last Access Time UTC.
        /// </summary>
        public string LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the Last Write Time UTC.
        /// </summary>
        public string LastWriteTimeUtc { get; set; }

        #endregion // Properties

        /// <summary>
        /// Executes a task.
        /// </summary>
        /// <returns>
        /// true if the task executed successfully; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            foreach (ITaskItem filePath in this.FilePaths)
            {
                FileInfo fileInfo = new FileInfo(filePath.ItemSpec);
                if (!string.IsNullOrEmpty(this.CreationTimeUtc))
                {
                    try
                    {
                        System.DateTime dateTime = System.DateTime.ParseExact(this.CreationTimeUtc, "u", CultureInfo.CurrentCulture);
                        if (this.CheckFileDateTime(dateTime, "CreationTimeUtc"))
                        {
                            fileInfo.CreationTimeUtc = dateTime;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime string of '{0}' given for 'CreationTimeUtc' could not be parsed as a UTC DateTime.", this.CreationTimeUtc);
                        Log.LogError(errorMessage);
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(this.LastAccessTimeUtc))
                {
                    try
                    {
                        System.DateTime dateTime = System.DateTime.ParseExact(this.LastAccessTimeUtc, "u", CultureInfo.CurrentCulture);
                        if (this.CheckFileDateTime(dateTime, "LastAccessTimeUtc"))
                        {
                            fileInfo.LastAccessTimeUtc = dateTime;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime string of '{0}' given for 'LastAccessTimeUtc' could not be parsed as a UTC DateTime.", this.LastAccessTimeUtc);
                        Log.LogError(errorMessage);
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(this.LastWriteTimeUtc))
                {
                    try
                    {
                        System.DateTime dateTime = System.DateTime.ParseExact(this.LastWriteTimeUtc, "u", CultureInfo.CurrentCulture);
                        if (this.CheckFileDateTime(dateTime, "LastWriteTimeUtc"))
                        {
                            fileInfo.LastWriteTimeUtc = dateTime;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime string of '{0}' given for 'LastWriteTimeUtc' could not be parsed as a UTC DateTime.", this.LastWriteTimeUtc);
                        Log.LogError(errorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the DateTime is valid for operating system files.
        /// </summary>
        /// <param name="fileDateTime">The DateTime to be applied.</param>
        /// <param name="parameterName">The name of the parameter in which the DateTime was passed.</param>
        /// <returns>Whether the value is valid.</returns>
        private bool CheckFileDateTime(System.DateTime fileDateTime, string parameterName)
        {
            if (fileDateTime < this.minimumFileTime)
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "The provided DateTime of '{0}' for parameter '{1}' was less than the minimum value of '{2}' allowed by the operating system.", fileDateTime, parameterName, this.minimumFileTime);
                Log.LogError(errorMessage);
                return false;
            }

            return true;
        }
    }
}
