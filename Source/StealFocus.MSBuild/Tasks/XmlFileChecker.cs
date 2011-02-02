// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileChecker.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the XmlFileChecker type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tasks
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Xml;

    using Core.Xml.Serialization;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// XmlFileChecker Class.
    /// </summary>
    public class XmlFileChecker : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the files to search in.
        /// </summary>
        [Required]
        public ITaskItem[] FilesToCheck
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path to the configuration file.
        /// </summary>
        [Required]
        public string XmlFileCheckerConfigurationFilePath
        {
            get;
            set;
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
            if (!File.Exists(this.XmlFileCheckerConfigurationFilePath))
            {
                string errorMessage = string.Format(CultureInfo.CurrentCulture, "Could not find the specified configuration file as '{0}'.", this.XmlFileCheckerConfigurationFilePath);
                Log.LogError(errorMessage);
                return false;
            }

            XmlDocument xmlFileCheckerConfigurationXml = new XmlDocument();
            xmlFileCheckerConfigurationXml.Load(this.XmlFileCheckerConfigurationFilePath);
            XmlFileCheckerConfiguration xmlFileCheckerConfiguration = SimpleXmlSerializer<XmlFileCheckerConfiguration>.Deserialize(xmlFileCheckerConfigurationXml.OuterXml);
            bool succeeded = true;
            foreach (ITaskItem taskItem in this.FilesToCheck)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "Testing XPaths for file '{0}' using configuration from '{1}'.", taskItem.ItemSpec, this.XmlFileCheckerConfigurationFilePath);
                Log.LogMessage(MessageImportance.Normal, message);
                XmlDocument fileToCheckXml = new XmlDocument();
                fileToCheckXml.Load(taskItem.ItemSpec);
                XmlNamespaceManager xmlNamespaceManager = InitialiseNamespaceManager(fileToCheckXml, xmlFileCheckerConfiguration.XmlNamespace);
                foreach (XPathToCheck xpathToCheck in xmlFileCheckerConfiguration.XPathToCheck)
                {
                    if (FileShouldBeTestedForXPath(taskItem.ItemSpec, xpathToCheck.FileExclusion))
                    {
                        if (!TestXPath(fileToCheckXml, xpathToCheck.Xpath, xmlNamespaceManager))
                        {
                            string errorMessage = string.Format(CultureInfo.CurrentCulture, "The XPath '{0}' for XML File '{1}' failed. {2}", xpathToCheck.Xpath, taskItem.ItemSpec, xpathToCheck.Advice);
                            Log.LogError(errorMessage);
                            succeeded = false;
                        }
                        else
                        {
                            string successMessage = string.Format(CultureInfo.CurrentCulture, "XPath '{0}' for XML File '{1}' succeeded.", xpathToCheck.Xpath, taskItem.ItemSpec);
                            Log.LogMessage(MessageImportance.Low, successMessage);
                        }
                    }
                }
            }

            return succeeded;
        }

        /// <summary>
        /// Initialise Namespace Manager.
        /// </summary>
        /// <param name="xmlDocument">An <see cref="XmlDocument"/>. The XML.</param>
        /// <param name="xmlNamespaces">The XML namespaces.</param>
        /// <returns>An <see cref="XmlNamespaceManager"/>. The XML Namespace Manager.</returns>
        /// <remarks />
        private static XmlNamespaceManager InitialiseNamespaceManager(XmlDocument xmlDocument, IEnumerable<XmlNamespace> xmlNamespaces)
        {
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            foreach (XmlNamespace xmlNamespace in xmlNamespaces)
            {
                xmlNamespaceManager.AddNamespace(xmlNamespace.Id, xmlNamespace.Value);
            }

            return xmlNamespaceManager;
        }

        /// <summary>
        /// Test an XPath.
        /// </summary>
        /// <param name="xmlDocument">An <see cref="XmlDocument"/>. The XML.</param>
        /// <param name="xpath">The XPath.</param>
        /// <param name="xmlNamespaceManager">An <see cref="XmlNamespaceManager"/>. The XML Namespace Manager.</param>
        /// <returns>Whether the XPath is found.</returns>
        private static bool TestXPath(XmlNode xmlDocument, string xpath, XmlNamespaceManager xmlNamespaceManager)
        {
            XmlNode testNode = xmlDocument.SelectSingleNode(xpath, xmlNamespaceManager);
            if (testNode == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a given file should be tested.
        /// </summary>
        /// <param name="pathOfFileToTest">The path of the file.</param>
        /// <param name="fileExclusions">The exclusions.</param>
        /// <returns>A value indicating if the file should be tested.</returns>
        private static bool FileShouldBeTestedForXPath(string pathOfFileToTest, IEnumerable<string> fileExclusions)
        {
            string qualifiedPathOfFileToTest = Path.GetFullPath(pathOfFileToTest);
            foreach (string fileExclusion in fileExclusions)
            {
                if (qualifiedPathOfFileToTest == Path.GetFullPath(fileExclusion))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
