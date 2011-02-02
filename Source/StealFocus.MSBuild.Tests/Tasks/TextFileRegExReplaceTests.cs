// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextFileRegexReplaceTests.cs" company="StealFocus">
//   Copyright StealFocus. All rights reserved.
// </copyright>
// <summary>
//   Defines the TextFileRegexReplaceTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StealFocus.MSBuild.Tests.Tasks
{
    using System.Globalization;
    using System.IO;

    using Core;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MSBuild.Tasks;

    /// <summary>
    /// TextFileRegexReplaceTests Class.
    /// </summary>
    [TestClass]
    public class TextFileRegexReplaceTests : MSBuildTests
    {
        #region Fields

        /// <summary>
        /// Holds the TestTextFileRegexReplaceSuccessDictionary file name.
        /// </summary>
        private const string TestTextFileRegexReplaceSuccessDictionary = "TestTextFileRegexReplaceSuccessDictionary.xml";

        /// <summary>
        /// Holds the TestTextFileRegexReplaceSuccessFile file name.
        /// </summary>
        private const string TestTextFileRegexReplaceSuccessFile = "TestTextFileRegexReplaceSuccessFile.txt";

        /// <summary>
        /// Holds the TestTextFileRegexReplaceSuccessDictionary file name.
        /// </summary>
        private const string TestTextFileRegexReplaceFailDictionary = "TestTextFileRegexReplaceFailDictionary.xml";

        /// <summary>
        /// Holds the TestTextFileRegexReplaceSuccessFile file name.
        /// </summary>
        private const string TestTextFileRegexReplaceFailFile = "TestTextFileRegexReplaceFailFile.txt";

        #endregion // Fields

        #region Initialize & Cleanup

        /// <summary>
        /// Initialise the test.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestTextFileRegexReplaceSuccessDictionary.xml", TestTextFileRegexReplaceSuccessDictionary);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestTextFileRegexReplaceSuccessFile.txt", TestTextFileRegexReplaceSuccessFile);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestTextFileRegexReplaceFailDictionary.xml", TestTextFileRegexReplaceFailDictionary);
            Resource.GetFileAndWriteToPath("StealFocus.MSBuild.Tests", "StealFocus.MSBuild.Tests.Tasks.Resources.TestTextFileRegexReplaceFailFile.txt", TestTextFileRegexReplaceFailFile);
        }

        /// <summary>
        /// Clean-up the test.
        /// </summary>
        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            File.Delete(TestTextFileRegexReplaceSuccessDictionary);
            File.Delete(TestTextFileRegexReplaceSuccessFile);
            File.Delete(TestTextFileRegexReplaceFailDictionary);
            File.Delete(TestTextFileRegexReplaceFailFile);
        }

        #endregion // Initialize & Cleanup

        /// <summary>
        /// Tests <see cref="TextFileRegexReplace.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestDirectTextFileRegexReplaceSuccess()
        {
            TextFileRegexReplace textFileRegexReplace = new TextFileRegexReplace();
            textFileRegexReplace.TargetFilePaths = new ITaskItem[] { new TaskItem(TestTextFileRegexReplaceSuccessFile) };
            textFileRegexReplace.DictionaryFilePath = TestTextFileRegexReplaceSuccessDictionary;
            bool result = textFileRegexReplace.Execute();
            Assert.IsTrue(result, "The MSBuild Task failed.");
            string fileContent = File.ReadAllText(TestTextFileRegexReplaceSuccessFile);
            Assert.AreEqual("Content\r\nMyReplacementValue1\r\nContent\r\nMyReplacementValue2\r\nContent\r\nMyReplacementValue2\r\nContent\r\nMyReplacementValue2\r\n", fileContent);
        }

        /// <summary>
        /// Tests <see cref="TextFileRegexReplace.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestScriptTextFileRegexReplaceSuccess()
        {
            const string TargetName = "TestTextFileRegexReplaceSuccess";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode == 0, "The MSBuild Task failed.");
            string fileContent = File.ReadAllText(TestTextFileRegexReplaceSuccessFile);
            Assert.AreEqual("Content\r\nMyReplacementValue1\r\nContent\r\nMyReplacementValue2\r\nContent\r\nMyReplacementValue2\r\nContent\r\nMyReplacementValue2\r\n", fileContent);
        }

        /// <summary>
        /// Tests <see cref="TextFileRegexReplace.Execute()"/>.
        /// </summary>
        [TestMethod]
        public void IntegrationTestScriptTextFileRegexReplaceFail()
        {
            const string TargetName = "TestTextFileRegexReplaceFail";
            string arguments = string.Format(CultureInfo.CurrentCulture, "{0} /t:{1}", MSBuildProjectFileName, TargetName);
            int exitCode = RunMSBuild(arguments);
            Assert.IsTrue(exitCode != 0, "The MSBuild Task succeeded when it was not expected to.");
        }
    }
}
