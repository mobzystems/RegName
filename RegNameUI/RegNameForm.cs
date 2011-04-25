using System;
// using System.Collections.Generic;
// using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

/*
 * TODO:
 * 
 * Only allow preview if match pattern present
 * Only allow rename if replace pattern present and items checked
 * Ask for confirmation before renaming
 * Set icon for open folder button
 * Add About dialog
 * Check patterns before preview/rename
 * Allow copy instead of move?
 * Use SHFileOperation for move
 * Allow Undo
 */
 
namespace MOBZystems.RegName
{
  /// <summary>
  /// The main form of this application. Allows interactive preview of the rename operation
  /// </summary>
  public partial class RegNameForm : Form
  {
    private FolderItem rootFolder;
    
    /// <summary>
    /// Constructor with a path name:
    /// </summary>
    public RegNameForm(string dir)
    {
      InitializeComponent();
      
      // this.AcceptButton = this.previewButton;
      
      // Link the system image list to the list view
      this.fileList.SmallImageList = FileItem.SmallImageList;

      ResizeHeaders();

      this.folderTextBox.Path = dir;
      
      ResetPatterns();

      OpenFolder(this.folderTextBox.Path);
    }

    /// <summary>
    /// Fill the list view when the path is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void folderTextBox_PathSelected(object sender, EventArgs e)
    {
      OpenFolder(this.folderTextBox.Path);
    }

    private void OpenFolder(string path)
    {
      try
      {
        // Create a folder item
        FolderItem folder = new FolderItem(path, false);
        folder.RetrieveFileIcons();

        this.rootFolder = folder;

        Preview();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, "The folder '" + path + "' could not be opened:\r\n\r\n" + ex.Message, "RegName", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    // Make sure the headers resize when the form resizes
    private void fileList_SizeChanged(object sender, EventArgs e)
    {
      ResizeHeaders();
    }
    
    /// <summary>
    /// Resize the headers of the list view control
    /// </summary>
    private void ResizeHeaders()
    {
      this.originalNameHeader.Width = this.fileList.ClientSize.Width / 2;
      this.newNameHeader.Width = this.fileList.ClientSize.Width / 2;
    }

    /// <summary>
    /// Perform a preview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void previewButton_Click(object sender, EventArgs e)
    {
      Preview();
    }
    
    private void Preview()
    {
      FileNameRegex fnr = new FileNameRegex(this.matchTextBox.Text);

      string replacePattern = this.replaceTextBox.Text;

      // Set up the file list
      this.fileList.Items.Clear();
      // Loop over all file names in the root folder
      foreach (FileItem file in this.rootFolder.Files)
      {
        string oldName = file.Name;
        string newName = fnr.ReplaceFileName(oldName, replacePattern);
        if (newName != null)
        {
          ListViewItem item = this.fileList.Items.Add(oldName, file.SmallIconIndex);
          item.SubItems.Add(newName);

          if (newName != oldName)
          {
            item.Checked = true;
            item.ToolTipText = newName + " --> " + oldName;
          }
          else
          {
            item.Checked = false;
            item.ToolTipText = oldName;
          }
        }
      }
    }

    private void renameButton_Click(object sender, EventArgs e)
    {
      // This will rename all files in the list
      foreach (ListViewItem item in this.fileList.Items)
      {
        if (item.Checked)
        {
          File.Move(Path.Combine(this.rootFolder.FullName(), item.Text), Path.Combine(this.rootFolder.FullName(), item.SubItems[1].Text));
        }
      }
      
      // Re-fill the folder list:
      OpenFolder(this.folderTextBox.Path);
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      ResetPatterns();
      Preview();
    }
    
    private void ResetPatterns()
    {
      this.matchTextBox.Text = "<name>.<ext>";
      this.replaceTextBox.Text = "<name>.<ext>";
    }

    private void RegNameForm_Load(object sender, EventArgs e)
    {
      Version v = new Version(Application.ProductVersion);
      this.versionLabel.Text = "RegName v" 
        + v.Major.ToString() + "." + v.Minor.ToString() + "." + v.Build.ToString() 
        + " (" + (IntPtr.Size * 8).ToString() + "-bit) by MOBZystems";
    }

    private void versionLabel_Click(object sender, EventArgs e)
    {
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        System.Diagnostics.Process.Start("http://www.mobzystems.com/tools/RegName.aspx");
      }
      catch
      {
        MessageBox.Show("There was an error starting the URL");
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }
  }
}