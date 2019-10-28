# stylecop-cmd

Command line program which runs the StyleCop linter on specified C# solutions/projects/files. Based on the code from the now seemingly unmaintained [StyleCopCmd](https://sourceforge.net/projects/stylecopcmd/). Changes in this repository add support for non-Windows environments through mono. Furthermore, it updates the outdated StyleCop version used by the project to the latest and targets .NET Framework 4.7.2, making it possible to lint C# source files using more modern syntax.
