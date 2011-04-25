using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace MOBZystems.RegName
{
  /// <summary>
  /// RegExDirInfo. Directory info object that allows regular expression searches
  /// </summary>
  public class RegExDirInfo
  {
    // The base directory for this object
    string baseDir;
//    // The filter used (in file name regex format, e.g. <base>.exe)
//    string filter;
    // The regex used to match file names
    FileNameRegex regex;

    // A list of file names (of type string)
    ArrayList files;
    // A list of subdirectories (of type RegExDirInfo)
    ArrayList directories;

    /// <summary>
    /// Constructor. Create a new object and fill files and directories
    /// </summary>
    /// <param name="baseDir">The base dir, ending in backslash</param>
    /// <param name="subDirs">If true, also search subdirectories of base directory</param>
    /// <param name="r">The file name regex to use</param>
    public RegExDirInfo(string baseDir, bool subDirs, FileNameRegex r)
    {
      this.baseDir = baseDir;
//      this.filter = filter;
      this.regex = r;

      // Get files in directory, then match
      DirectoryInfo dirInfo = new DirectoryInfo(baseDir);
      FileInfo[] files = dirInfo.GetFiles();

      this.files = new ArrayList();

      for (int i = 0; i < files.Length; i++)
      {
        if (r.IsMatch(files[i].Name))
          this.files.Add(files[i].Name);
      }

      // Set up a list of subdirectories
      this.directories = new ArrayList();

      // Get subdirectories and match those, if necessary
      if (subDirs)
      {
        DirectoryInfo[] directories = dirInfo.GetDirectories();
        foreach (DirectoryInfo directory in directories)
        {
          RegExDirInfo subDir = new RegExDirInfo(directory.FullName + "\\", subDirs, r);
          if (subDir.files.Count != 0)
            this.directories.Add(subDir);
        }
      }
    }

    /// <summary>
    /// List all files in this object and subdirectories (if any)
    /// </summary>
    /// <param name="skipPrefix">Skip this string if it appears at the start of the full name of the file</param>
    public void List(string skipPrefix, bool verbose)
    {
      // Set a prefix to use when listing.
      string prefix = baseDir;
      if (prefix.ToLower().StartsWith(skipPrefix.ToLower()))
        prefix = prefix.Substring(skipPrefix.Length);

      // Loop over all files, printing in verbose mode if necessary
      foreach (string file in this.files)
      {
        if (verbose)
        {
          Console.WriteLine(prefix + file + ":");

          string[] groupnames = this.regex.GetGroupNames();

          // Match the file name again using the same regex:
          Match match = this.regex.Match(file);
          foreach (string groupname in groupnames)
          {
            if (groupname != "0")
              Console.WriteLine("   <" + groupname + "> = \"" + match.Groups[groupname].Value + "\"");
          }
        }
        else
          Console.WriteLine(prefix + file);
      }

      // List subdirectories in the same way
      foreach (RegExDirInfo dir in this.directories)
      {
        dir.List(skipPrefix, verbose);
      }
    }

    /// <summary>
    /// Rename all files in this directory and subdirectories
    /// </summary>
    /// <param name="newNames">The replacement Regex pattern to rename to</param>
    /// <param name="previewOnly">If true, do not rename, only preview</param>
    public void Rename(string skipPrefix, string newNames, bool previewOnly)
    {
      // Set up a prefix for printing
      string prefix = baseDir;
      if (prefix.ToLower().StartsWith(skipPrefix.ToLower()))
        prefix = prefix.Substring(skipPrefix.Length);

      // Handle each file
      foreach (string file in this.files)
      {
        // Match the file name against the original filter
        FileNameRegex r = this.regex; // new FileNameRegex(filter);
        string newFileName = r.ReplaceFileName(file, newNames);
        Console.WriteLine(prefix + file + " --> " + newFileName);
        if (!previewOnly)
        {
          // Rename baseDir + file to baseDir + newFileName
          File.Move(baseDir + file, baseDir + newFileName);
        }
      }

      // Handle subdirectories
      foreach (RegExDirInfo dir in this.directories)
      {
        dir.Rename(skipPrefix, newNames, previewOnly);
      }
    }
  }
}
