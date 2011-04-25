using System;
using System.IO;
using System.Windows.Forms;

/*
 * RegNameUI. The UI version of RegName.
 */
namespace MOBZystems.RegName
{
	/// <summary>
	/// MainClass. The main class of the application.
	/// </summary>
  class MainClass
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static int Main()
    {
      string[] args = CommandLineParser.GetCommandLineArgs();

      // The directory to open up in. If specified on the command line, use it; if not, use My Documents
      string dir;

      if (args.Length <= 1)
      {
        dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      }
      else 
      {
        dir = Path.GetFullPath(args[1]);
        if (dir.StartsWith("..{"))
        {
          MessageBox.Show("The specified folder does not contain files. Reverting to My Documents.", "RegName", MessageBoxButtons.OK, MessageBoxIcon.Error);
          dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
      }

      // Add a missing backslash if necessary
      if (!dir.EndsWith("\\"))
        dir += "\\";

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RegNameForm(dir));

      return 0;
    }
  }
}
