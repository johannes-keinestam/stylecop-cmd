//-----------------------------------------------------------------------
// <copyright 
//  file="Program.cs" 
//  company="Andrew Kutz">
//  Copyright (c) Andrew Kutz. All rights reserved.
// </copyright>
// <authors>
//   <author>Andrew Kutz</author>
// </authors>
//-----------------------------------------------------------------------
namespace Net.SF.StyleCopCmd.Console
{
    using System;
    using System.Reflection;
    using Core;
    using net.sf.dotnetcli;

    /// <summary>
    /// The entry-point class for this application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The command-line options for this application.
        /// </summary>
        private static readonly Options Opts = new Options();

        /// <summary>
        /// The entry-point method for this application.
        /// </summary>
        /// <param name="args">
        /// The command line arguments passed to this method.
        /// </param>
        private static void Main(string[] args)
        {
            // Initialize the command line options.
            InitOptions();

            // Parse the arguments.
            var cl = ProcessArguments(args);
            if (cl == null)
            {
                PrintUsageAndHelp();
                return;
            }

            new StyleCopReport().ReportBuilder()
                .WithStyleCopSettingsFile(cl.GetOptionValue("sc"))
                .WithRecursion(cl.HasOption("r"))
                .WithSolutionsFiles(cl.GetOptionValues("sf"))
                .WithProjectFiles(cl.GetOptionValues("pf"))
                .WithDirectories(cl.GetOptionValues("d"))
                .WithFiles(cl.GetOptionValues("f"))
                .WithIgnorePatterns(cl.GetOptionValues("ifp"))
                .WithTransformFile(cl.GetOptionValue("tf"))
                .WithOutputEventHandler(OutputGenerated)
                .Create(cl.GetOptionValue("of"));
        }

        /// <summary>
        /// Prints the output of the StyleCop processor to the console.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The output message to print.</param>
        private static void OutputGenerated(
            object sender,
            StyleCop.OutputEventArgs e)
        {
            Console.WriteLine(e.Output);
        }

        /// <summary>
        /// Prints this application's usage and help text to StdOut.
        /// </summary>
        private static void PrintUsageAndHelp()
        {
            var hf = new HelpFormatter();
            hf.PrintHelp(
                Assembly.GetExecutingAssembly().GetName().Name,
                Opts,
                true);
        }

        /// <summary>
        /// Initialize the options this program takes.
        /// </summary>
        private static void InitOptions()
        {
            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("solutionFiles")
                    .HasArgs()
                    .WithArgName("filePaths")
                    .WithDescription("Visual Studio solutions files to check")
                    .Create("sf"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("projectFiles")
                    .HasArgs()
                    .WithArgName("filePaths")
                    .WithDescription("Visual Studio project files to check")
                    .Create("pf"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("ignoreFilePattern")
                    .HasArgs()
                    .WithArgName("patterns")
                    .WithDescription(
                    "Regular expression patterns that " +
                    "can be used to ignore files")
                    .Create("ifp"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("directories")
                    .HasArgs()
                    .WithArgName("dirPaths")
                    .WithDescription("Directories to check for CSharp files")
                    .Create("d"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("recurse")
                    .WithDescription("Recursive directory search")
                    .Create("r"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("files")
                    .HasArgs()
                    .WithArgName("filePaths")
                    .WithDescription("Files to check")
                    .Create("f"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("styleCopSettingsFile")
                    .HasArg()
                    .WithArgName("filePath")
                    .WithDescription("Use the given StyleCop settings file")
                    .Create("sc"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("configurationSymbols")
                    .HasArgs()
                    .WithArgName("symbols")
                    .WithDescription(
                    "Configuration symbols to pass to StyleCop " +
                    "(ex. DEBUG, RELEASE)")
                    .Create("cs"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("outputXmlFile")
                    .IsRequired()
                    .HasArg()
                    .WithArgName("filePath")
                    .WithDescription("The file the XML output is written to")
                    .Create("of"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("xslFile")
                    .HasArg()
                    .WithArgName("filePath")
                    .WithDescription("The transform file")
                    .Create("tf"));

            Opts.AddOption(
                OptionBuilder.Factory
                    .WithLongOpt("help")
                    .WithDescription("Print this help screen")
                    .Create("?"));
        }

        /// <summary>
        /// Process the command line arguments against the
        /// expected options.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        /// <returns>
        /// A CommandLine object if successful, otherwise false.
        /// </returns>
        private static CommandLine ProcessArguments(string[] args)
        {
            var pp = new PosixParser();
            CommandLine cl;

            try
            {
                cl = pp.Parse(
                    Opts,
                    args);
            }
            catch (ParseException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            if (cl.HasOption("?"))
            {
                return null;
            }

            var hasInputFiles = cl.HasOption("sf") || cl.HasOption("pf") || cl.HasOption("d") || cl.HasOption("f");
            var hasOutputFile = cl.HasOption("of");
            if (!hasInputFiles || !hasOutputFile)
            {
                return null;
            }

            return cl;
        }
    }
}