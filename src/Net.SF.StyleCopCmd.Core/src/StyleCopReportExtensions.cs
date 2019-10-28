//-----------------------------------------------------------------------
// <copyright 
//  file="StyleCopReportExtensions.cs" 
//  company="Schley Andrew Kutz">
//  Copyright (c) Schley Andrew Kutz. All rights reserved.
// </copyright>
// <authors>
//   <author>Schley Andrew Kutz</author>
// </authors>
//-----------------------------------------------------------------------
namespace Net.SF.StyleCopCmd.Core
{
    /// <summary>
    /// The extensions class for StyleCopReport.
    /// </summary>
    public static class StyleCopReportExtensions
    {
        /// <summary>
        /// Creates a ReportBuilder to assist with building this StyleCopReport.
        /// </summary>
        /// <param name="report">
        /// The StyleCopReport object.
        /// </param>
        /// <returns>
        /// A new ReportBuilder object.
        /// </returns>
        public static ReportBuilder ReportBuilder(
            this StyleCopReport report)
        {
            return new ReportBuilder(report);
        }
    }
}