using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MOBZystems.RegName
{
  public partial class FolderTextBox : UserControl
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public FolderTextBox()
    {
      InitializeComponent();
    }

    private string description;

    /// <summary>
    /// Description shown in folder browser
    /// </summary>
    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }

    /// <summary>
    /// PathSelected event handler
    /// </summary>
    public event EventHandler PathSelected;

    private void OnPathSelected(EventArgs e)
    {
      if (PathSelected != null)
      {
        PathSelected(this, e);
      }
    }

    /// <summary>
    /// Button click handler. Shows the folder browser to select the path
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button_Click(object sender, EventArgs e)
    {
      FolderBrowserDialog fbd = new FolderBrowserDialog();
      fbd.Description = this.description;
      // fbd.RootFolder = Environment.SpecialFolder.MyComputer;
      fbd.SelectedPath = textBox.Text;
      fbd.ShowNewFolderButton = false;
      if (fbd.ShowDialog(this) == DialogResult.OK)
      {
        textBox.Text = fbd.SelectedPath;
        OnPathSelected(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Is the selected path valid?
    /// </summary>
    public bool PathIsValid
    {
      get
      {
        try
        {
          return System.IO.Directory.Exists(textBox.Text);
        }
        catch (Exception)
        {
          // Not a valid path
          return false;
        }
      }
    }

    /// <summary>
    /// Get/Set the folder path
    /// </summary>
    public string Path
    {
      get
      {
        return textBox.Text;
      }
      set
      {
        textBox.Text = value;
      }
    }

    private void textBox_SizeChanged(object sender, EventArgs e)
    {
      // Set the client height of the containing user control to the height of the text box
      this.ClientSize = new Size(this.ClientSize.Width, textBox.Height);
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == '\r')
      {
        OnPathSelected(EventArgs.Empty);
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if (keyData.Equals(Keys.Enter))
        return true;
        
      return base.IsInputKey(keyData);
    }
  }
}
