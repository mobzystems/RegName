using System;
// using System.Collections.Generic;
// using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;

/*
 * RegName. A console utility to allow regex-like renames.
 * 
 * The following characters are not allowed inside of filenames:
 * 
 * /\:?*<>|"
 * 
 * The pattern for specifying the filespec is built up as follows:
 * 
 * literal | <name:regex> | * | ?
 * 
 * literal: every character that is not one of the above
 * *: zero or more characters, unmatched
 * ?: one single character, unmatched
 * <name:regex>: a named group with an optional regular expression. Default is *.
 * 
 * TODO: add a fixed replacement pattern for day, month, year, hour, minute, second, name of month, name of day, etc
 * TODO: handle directories in some way. Maybe even use the pattern in the directory?
 */
namespace MOBZystems.RegName
{
  /// <summary>
  /// MainClass. The main class of the application.
  /// </summary>
  class MainClass
  {
    /// <summary>
    /// The main entry point for the application. Shows the main form if no arguments are present;
    /// otherwise, uses console.
    /// </summary>
    [STAThread]
    static int Main()
    {
      // Get corrected command line arguments
      string[] args = CommandLineParser.GetCommandLineArgs();

      // If we have no command line arguments, start the UI part of the application: RegNameUI.exe
      if (args.Length <= 1)
      {
        // Get our own startup path
        string appPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        string uiExeName = Path.Combine(appPath, "RegNameUI.exe");

        if (File.Exists(uiExeName))
        {
          // Start RegNameUI
          try
          {
            Process.Start(uiExeName, "\"" + CurrentDir() + "\"");
          }
          catch (Exception ex)
          {
            Console.Error.WriteLine("Error: Failed to spawn '" + uiExeName + "'\r\n" + ex.Message);
          }
        }
        else
        {
          Console.Error.WriteLine("Error: Could not find '" + uiExeName + "'");
        }
        // Done!
        return 0;
      }

      bool optionSubDirs = false;
      bool optionListOnly = true;
      bool optionVerbose = false;

      string origNames = null;
      string newNames = null;

      int i;
      // We expect options to come first
      for (i = 1; i < args.Length; i++)
      {
        if (args[i].StartsWith("-") || args[i].StartsWith("/"))
        {
          switch (args[i].Substring(1).ToLower())
          {
            case "s":
              optionSubDirs = true;
              break;
            case "x":
              optionListOnly = false;
              break;
            case "v":
              optionVerbose = true;
              break;
            case "?":
              Usage("");
              return 1;
            default:
              Usage("Invalid option: " + args[i]);
              return 1;
          }
        }
        else
          // Not an option? Break out of loop!
          break;
      }

      // Calculate number of arguments left
      // int otherargs = args.Length - i + 1;

      // Argument counter
      int argc = 0;
      for (; i < args.Length; i++)
      {
        if (args[i].StartsWith("-") || args[i].StartsWith("/"))
        {
          // This is an option! We don't expect that anymore.
          Usage("Options must be specified first");
          return 1;
        }

        switch (++argc)
        {
          case 1: // First argument: original name OR directory
            origNames = args[i];
            break;
          case 2:
            newNames = args[i];
            break;
          default:
            Usage("Too many arguments");
            return 1;
        }
      }

      try
      {
        switch (argc)
        {
          case 1:
            return DirList(origNames, optionSubDirs, optionVerbose);
          case 2:
            return Rename(origNames, newNames, optionSubDirs, optionListOnly, optionVerbose);
          default:
            Usage("Help!");
            return 1;
        }
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error: " + ex.Message);
        return 1;
      }
    }

    /// <summary>
    /// Return usage instructions.
    /// </summary>
    /// <returns></returns>
    static private void Usage(string message)
    {
      Version version = Assembly.GetExecutingAssembly().GetName().Version;

      if (message.Length > 0)
        Console.Error.WriteLine(message + Environment.NewLine);

      Console.WriteLine(@"MOBZystems' Regular Expression Renamer, version "
        + version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString()
        + @".

Renames files using regular expressions.

Usage:

1. RegName [options] ""<file-pattern>""

   Lists files matching the file-pattern.

2. RegName [options] ""<file-pattern>"" ""<rename-pattern>""

   Renames files matching the file-pattern to the rename-pattern.

Arguments:
   file-pattern: a regular expression
   rename-pattern: the replacement text for matching files

File-patterns contain variables between angle brackets, as in:

   <base>.<ext>

Optionally, a variable name can be followed by a semicolon and a regular
expression:

   <month:[0-9]{2,4}>

The default regular expression is .* which is to say: everything.

A rename-pattern contains the same variables, but always without the
regular expression, e.g. <month>

Options:
   -s: handle files in subdirectories, too
   -x: execute. If not specified, preview only (renaming only)
   -v: verbose output
   -?: show this help text

Examples:

   regname -s ""ex<year>.log""

Lists all files in the current directory and its subdirectories
matching ""ex<year>.log"".

   regname ""<name>.exe"" ""<name>.txt""

Conventional rename. Renames *.exe to *.txt

   regname -s ""<year>-<month>-<day>.log"" ""<day>-<month>-<year>.log""

Renames 2008-02-29.log to 29-02-2008.log etc.");
    }

    /// <summary>
    /// Return the current directory, ALWAYS ENDING IN BACKSLASH
    /// </summary>
    /// <returns></returns>
    static private string CurrentDir()
    {
      string currentDir = Environment.CurrentDirectory;

      if (!currentDir.EndsWith("\\"))
        currentDir += "\\";

      return currentDir;
    }

    /// <summary>
    /// Perform a listing of a directory using the specified filter and options
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="optionSubDirs"></param>
    /// <param name="optionVerbose"></param>
    /// <returns></returns>
    static private int DirList(string filter, bool optionSubDirs, bool optionVerbose)
    {
      // Create a FileNameRegex for the whole file name
      FileNameRegex r = new FileNameRegex("^" + filter + "$");

      if (optionVerbose)
        Console.WriteLine("Listing of " + filter + ":");

      string currentDir = CurrentDir();

      RegExDirInfo dirInfo = new RegExDirInfo(currentDir, optionSubDirs, r);
      dirInfo.List(currentDir, optionVerbose);

      return 0;
    }

    /// <summary>
    /// Perform a rename in a directory using the specified patterns and options
    /// </summary>
    /// <param name="origNames"></param>
    /// <param name="newNames"></param>
    /// <param name="optionSubDirs"></param>
    /// <param name="optionListOnly"></param>
    /// <param name="optionVerbose"></param>
    /// <returns></returns>
    static private int Rename(string origNames, string newNames, bool optionSubDirs, bool optionListOnly, bool optionVerbose)
    {
      if (optionVerbose)
      {
        if (optionListOnly)
          Console.WriteLine("Preview of renaming " + origNames + " to " + newNames + ":");
        else
          Console.WriteLine("Renaming " + origNames + " to " + newNames + ":");
      }

      string currentDir = CurrentDir();

      // Create a FileNameRegex for the whole file name
      FileNameRegex r = new FileNameRegex("^" + origNames + "$");

      RegExDirInfo dirInfo = new RegExDirInfo(currentDir, optionSubDirs, r);
      dirInfo.Rename(currentDir, newNames, optionListOnly);

      return 0;
    }
  }
}