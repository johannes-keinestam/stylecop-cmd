//-----------------------------------------------------------------------
// <copyright 
//  file="StyleCopReportExtensionsTest.cs" 
//  company="Schley Andrew Kutz">
//  Copyright (c) Schley Andrew Kutz. All rights reserved.
// </copyright>
// <authors>
//   <author>Schley Andrew Kutz</author>
// </authors>
//-----------------------------------------------------------------------
namespace Net.SF.StyleCopCmd.Core.Test
{
    using Core;
    using NUnit.Framework;

    /// <summary>
    /// A test fixture for the StyleCopReportExtensions class.
    /// </summary>
    [TestFixture]
    public class StyleCopReportExtensionsTest
    {
        /// <summary>
        /// Tests the ReportBuilder method.
        /// </summary>
        [Test]
        public void ReportBuilderTest()
        {
            var scr = new StyleCopReport();
            var rb = scr.ReportBuilder();
            Assert.IsNotNull(rb != null);
        }
    }
}