using System;
using System.Text.RegularExpressions;

namespace MOBZystems.RegName
{
	/// <summary>
	/// FileNameRegex. Regex-variant for file names.
	/// </summary>
	public class FileNameRegex: Regex
	{
    /// <summary>
    /// Constructor. Translates the supplied pattern.
    /// </summary>
    /// <param name="pattern">The file name pattern</param>
    public FileNameRegex(string pattern) :
      base(ToFilePattern(pattern))
    {
    }

    /// <summary>
    /// Perform a regex-like replace, only in file name mode, i.e. translate the supplied pattern to a file name replace pattern.
    /// </summary>
    /// <param name="filename">The filename to rename</param>
    /// <param name="pattern">The pattern to use for the new name</param>
    /// <returns>The new name of the file, or null if it does not match the pattern</returns>
    public string ReplaceFileName(string filename, string pattern)
    {
      if (!base.IsMatch(filename))
        return null;
      return base.Replace(filename, ToReplacePattern(pattern));
    }

    /// <summary>
    /// Convert a file filter to a regex pattern.
    /// </summary>
    /// <param name="pattern">The file name pattern, e.g. &lt;base&gt;.exe</param>
    /// <returns>A regex-usable pattern, e.g. (?&lt;base&gt;.*)</returns>
    private static string ToFilePattern(string pattern)
    {
      const string CAPTURE = "<(?<name>[a-zA-Z]+[a-zA-Z0-9_]*)>";
      const string CAPTUREWITHPATTERN = "<(?<name>[a-zA-Z]+[a-zA-Z0-9]*):(?<pattern>.+?)>";

      // Match the original pattern using a Regex

      // Split the string into captures and literals:
      Regex r;
      r = new Regex("(?<pattern>" + CAPTURE + "|" + CAPTUREWITHPATTERN + ")");
      MatchCollection matches = r.Matches(pattern);
      
      // Detect the literals in between the patterns:
      int startpos = 0;
      // The structure of a pattern is literal/match/literal/match.../match/literal
      // where each literal may be empty
      string[] literals = new string[matches.Count + 1];
      int index = 0;

      foreach (Match match in matches)
      {
        if (match.Index > startpos)
        {
          // We have a little bit left!
          literals[index] = pattern.Substring(startpos, match.Index - startpos);
          // Console.WriteLine("DEBUG: literal = \"" + literal + "\"");
        }
        else
          literals[index] = "";

        // On to the next literal
        index++;

        // Then move on to after this match
        startpos = match.Index + match.Length;
      }
      // If we have a little bit left at the end, deal with that:
      if (startpos < pattern.Length)
      {
        literals[index] = pattern.Substring(startpos);
        // Console.WriteLine("DEBUG: literal = \"" + literal + "\"");
      }
      else
        literals[index] = "";

      // Loop over all literals, replacing characters:
      // Replace . with [.], ? with .? and * with .*
      for (int i = 0; i < literals.Length; i++)
      {
        if (literals[i].Length > 0)
        {
          literals[i] = literals[i].Replace(".", "[.]"); // . --> [.] (real dot instead of wildcard)
          literals[i] = literals[i].Replace("?", ".");   // ? --> . (single wildcard)
          literals[i] = literals[i].Replace("*", ".*"); // * --> .* (multiple wildcard). To use lazy matching, replace with .*?

          // TODO: If we have any more ?'s or *'s or <'s or >'s inside the literals, complain
          // if (literals[i].IndexOfAny(new char[] { '<', '>', 
        }
      }

      // Now merge the total pattern back together:
      pattern = "";
      for (int i = 0; i < matches.Count; i++)
      {
        pattern += literals[i] + matches[i].Value;
      }
      pattern += literals[matches.Count];

      // Replace simple captures e.g. <base> or <date>. These default to the capture string .*, e.g. everything
      r = new Regex(CAPTURE);
      pattern = r.Replace(pattern, "(?<${name}>.*)"); // for lazy matching, use .*?
      
      // Then replace captures with a pattern, e.g. <name:[0-9]{1-6}>
      r = new Regex(CAPTUREWITHPATTERN);
      pattern = r.Replace(pattern, "(?<${name}>${pattern})");

      return pattern;
    }

    /// <summary>
    /// Convert a normal pattern to a replace pattern, e.g. &lt;test&gt; to ${test}
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    private static string ToReplacePattern(string pattern)
    {
#if DEBUG
      string p = pattern;
#endif

      Regex r = new Regex("<(?<name>[a-zA-Z]+[a-zA-Z0-9_]*)>");
      pattern = r.Replace(pattern, "$${${name}}");

#if DEBUG
      Console.WriteLine("DEBUG: translate " + p + " --> " + pattern);
#endif

      return pattern;
    }
	}
}
