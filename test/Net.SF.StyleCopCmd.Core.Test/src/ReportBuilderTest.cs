//-----------------------------------------------------------------------
// <copyright 
//  file="ReportBuilderTest.cs" 
//  company="Schley Andrew Kutz">
//  Copyright (c) Schley Andrew Kutz. All rights reserved.
// </copyright>
// <authors>
//   <author>Schley Andrew Kutz</author>
// </authors>
//-----------------------------------------------------------------------
namespace Net.SF.StyleCopCmd.Core.Test
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;

    /// <summary>
    /// Tests the ReportBuilder class.
    /// </summary>
    [TestFixture]
    public class ReportBuilderTest
    {
        /// <summary>
        /// Tests the WithSolutionFiles method.
        /// </summary>
        [Test]
        public void WithSolutionFilesTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithSolutionsFiles(
                new[]
                {
                    @"c:\foo.txt"
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "SolutionFiles");
            Assert.AreEqual(
                1,
                sf.Count);
            Assert.AreEqual(
                @"c:\foo.txt",
                sf[0]);
        }

        /// <summary>
        /// Tests the WithProjectFiles method.
        /// </summary>
        [Test]
        public void WithProjectFilesTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithProjectFiles(
                new[]
                {
                    @"c:\foo.txt"
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "ProjectFiles");
            Assert.AreEqual(
                1,
                sf.Count);
            Assert.AreEqual(
                @"c:\foo.txt",
                sf[0]);
        }

        /// <summary>
        /// Tests the WithDirectories method.
        /// </summary>
        [Test]
        public void WithDirectoriesTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithDirectories(
                new[]
                {
                    @"c:\windows",
                    @"c:\program files"
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "Directories");
            Assert.AreEqual(
                2,
                sf.Count);
            Assert.AreEqual(
                @"c:\windows",
                sf[0]);
            Assert.AreEqual(
                @"c:\program files",
                sf[1]);
        }

        /// <summary>
        /// Tests the WithFiles method.
        /// </summary>
        [Test]
        public void WithFilesTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithFiles(
                new[]
                {
                    @"c:\windows\foo1.txt",
                    @"c:\program files\foo2.txt"
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "Files");
            Assert.AreEqual(
                2,
                sf.Count);
            Assert.AreEqual(
                @"c:\windows\foo1.txt",
                sf[0]);
            Assert.AreEqual(
                @"c:\program files\foo2.txt",
                sf[1]);
        }

        /// <summary>
        /// Tests the WithIngorePatterns method.
        /// </summary>
        [Test]
        public void WithIgnorePatternsTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithIgnorePatterns(
                new[]
                {
                    @"AssemblyInfo.cs"
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "IgnorePatterns");
            Assert.AreEqual(
                1,
                sf.Count);
            Assert.AreEqual(
                @"AssemblyInfo.cs",
                sf[0]);
        }

        /// <summary>
        /// Tests the WithRecursion method.
        /// </summary>
        [Test]
        public void WithRecursionTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithRecursion();
            var sf = GetPrivateProperty<bool>(
                rb,
                "RecursionEnabled");
            Assert.AreEqual(
                true,
                sf);
        }

        /// <summary>
        /// Tests the WithProcessorSymbols method.
        /// </summary>
        [Test]
        public void WithProcessorSymbols()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithProcessorSymbols(
                new[]
                {
                    "DEBUG",
                    "CODE_ANALYSIS",
                });
            var sf = GetPrivateProperty<IList<string>>(
                rb,
                "ProcessorSymbols");
            Assert.AreEqual(
                2,
                sf.Count);
            Assert.AreEqual(
                "DEBUG",
                sf[0]);
            Assert.AreEqual(
                "CODE_ANALYSIS",
                sf[1]);
        }

        /// <summary>
        /// Tests the WithStyleCopSettingsFile method.
        /// </summary>
        [Test]
        public void WithStyleCopSettingsFileTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithStyleCopSettingsFile("c:\\scs.settings");
            var sf = GetPrivateProperty<string>(
                rb,
                "StyleCopSettingsFile");
            Assert.AreEqual(
                @"c:\scs.settings",
                sf);
        }

        /// <summary>
        /// Tests the WithTransformFile method.
        /// </summary>
        [Test]
        public void WithTransformFileTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            rb.WithTransformFile("c:\\StyleCopReport.xsl");
            var sf = GetPrivateProperty<string>(
                rb,
                "TransformFile");
            Assert.AreEqual(
                @"c:\StyleCopReport.xsl",
                sf);
        }

        /// <summary>
        /// Tests the AddSolutionFile method.
        /// </summary>
        [Test]
        public void AddSolutionFileTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            var sf = GetTestSolutionPath() + @"\StyleCopTestProject.sln";
            
            var t = rb.GetType();
            var mi = t.GetMethod(
                "AddSolutionFile",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            
            // Invoke the method.
            mi.Invoke(
                rb,
                new[] { sf });

            Assert.AreEqual(
                1,
                scr.Solutions.Rows.Count);
            Assert.AreEqual(
                1,
                scr.Projects.Rows.Count);
            Assert.AreEqual(
                3,
                scr.SourceCodeFiles.Rows.Count);
        }

        /// <summary>
        /// Tests the AddProjectFile method.
        /// </summary>
        [Test]
        public void AddProjectFileTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            var sf = GetTestSolutionPath() + 
                @"\StyleCopTestProject\StyleCopTestProject.csproj";

            var t = rb.GetType();
            var mi = t.GetMethod(
                "AddProjectFile",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);

            // Invoke the method.
            mi.Invoke(
                rb,
                new[] { sf, null });

            Assert.AreEqual(
                1,
                scr.Solutions.Rows.Count);
            Assert.AreEqual(
                1,
                scr.Projects.Rows.Count);
            Assert.AreEqual(
                3,
                scr.SourceCodeFiles.Rows.Count);
        }

        /// <summary>
        /// Gets a private property from a ReportBuilder instance.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the property being returned.
        /// </typeparam>
        /// <param name="reportBuilder">
        /// The ReportBuilder instance to get the private property from.
        /// </param>
        /// <param name="propertyName">
        /// The name of the property to get.
        /// </param>
        /// <returns>The property value.</returns>
        private static T GetPrivateProperty<T>(
            ReportBuilder reportBuilder,
            string propertyName)
        {
            var t = reportBuilder.GetType();
            var pi = t.GetProperty(
                propertyName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            return (T) pi.GetValue(reportBuilder, null);
        }

        /// <summary>
        /// Gets the path to the test solution.
        /// </summary>
        /// <returns>
        /// The path to the test solution.
        /// </returns>
        private static string GetTestSolutionPath()
        {
            var d = new DirectoryInfo(".");

            // Move backwards to the root of the solution.
            while (d != null)
            {
                // Is the solution in this directory?
                if (d.GetFiles().FirstOrDefault(f => f.Extension == ".sln") !=
                    null)
                {
                    break;
                }

                d = d.Parent;
            }

            var r = d.FullName +
                    @"\test\Net.SF.StyleCopCmd.Core.Test" +
                    @"\data\StyleCopTestProject";

            return r;
        }
    }
}