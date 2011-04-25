namespace MOBZystems.RegName
{
  partial class RegNameForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegNameForm));
      this.label1 = new System.Windows.Forms.Label();
      this.fileList = new System.Windows.Forms.ListView();
      this.originalNameHeader = new System.Windows.Forms.ColumnHeader();
      this.newNameHeader = new System.Windows.Forms.ColumnHeader();
      this.label2 = new System.Windows.Forms.Label();
      this.matchTextBox = new System.Windows.Forms.TextBox();
      this.replaceTextBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.previewButton = new System.Windows.Forms.Button();
      this.renameButton = new System.Windows.Forms.Button();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.resetButton = new System.Windows.Forms.Button();
      this.folderTextBox = new RegName.FolderTextBox();
      this.versionLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Folder:";
      // 
      // fileList
      // 
      this.fileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fileList.CheckBoxes = true;
      this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.originalNameHeader,
            this.newNameHeader});
      this.fileList.FullRowSelect = true;
      this.fileList.Location = new System.Drawing.Point(3, 32);
      this.fileList.Name = "fileList";
      this.fileList.ShowItemToolTips = true;
      this.fileList.Size = new System.Drawing.Size(524, 247);
      this.fileList.TabIndex = 2;
      this.toolTip.SetToolTip(this.fileList, "Uncheck files to exclude them from the rename operation");
      this.fileList.UseCompatibleStateImageBehavior = false;
      this.fileList.View = System.Windows.Forms.View.Details;
      this.fileList.SizeChanged += new System.EventHandler(this.fileList_SizeChanged);
      // 
      // originalNameHeader
      // 
      this.originalNameHeader.Text = "Original name";
      // 
      // newNameHeader
      // 
      this.newNameHeader.Text = "New name";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.Location = new System.Drawing.Point(10, 286);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "&Match pattern:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // matchTextBox
      // 
      this.matchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.matchTextBox.Location = new System.Drawing.Point(106, 285);
      this.matchTextBox.Name = "matchTextBox";
      this.matchTextBox.Size = new System.Drawing.Size(421, 21);
      this.matchTextBox.TabIndex = 4;
      this.toolTip.SetToolTip(this.matchTextBox, "The pattern file names must match");
      // 
      // replaceTextBox
      // 
      this.replaceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.replaceTextBox.Location = new System.Drawing.Point(106, 312);
      this.replaceTextBox.Name = "replaceTextBox";
      this.replaceTextBox.Size = new System.Drawing.Size(421, 21);
      this.replaceTextBox.TabIndex = 6;
      this.toolTip.SetToolTip(this.replaceTextBox, "The replace pattern");
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label3.Location = new System.Drawing.Point(12, 315);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(88, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "&Replace pattern:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // previewButton
      // 
      this.previewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.previewButton.Location = new System.Drawing.Point(369, 339);
      this.previewButton.Name = "previewButton";
      this.previewButton.Size = new System.Drawing.Size(77, 23);
      this.previewButton.TabIndex = 8;
      this.previewButton.Text = "&Preview";
      this.toolTip.SetToolTip(this.previewButton, "Preview the results of the rename operation");
      this.previewButton.UseVisualStyleBackColor = true;
      this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
      // 
      // renameButton
      // 
      this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.renameButton.Location = new System.Drawing.Point(452, 339);
      this.renameButton.Name = "renameButton";
      this.renameButton.Size = new System.Drawing.Size(75, 23);
      this.renameButton.TabIndex = 9;
      this.renameButton.Text = "Re&name!";
      this.toolTip.SetToolTip(this.renameButton, "Rename all checked items");
      this.renameButton.UseVisualStyleBackColor = true;
      this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
      // 
      // resetButton
      // 
      this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.resetButton.Location = new System.Drawing.Point(3, 339);
      this.resetButton.Name = "resetButton";
      this.resetButton.Size = new System.Drawing.Size(77, 23);
      this.resetButton.TabIndex = 7;
      this.resetButton.Text = "&Default";
      this.toolTip.SetToolTip(this.resetButton, "Reset the patterns to default values");
      this.resetButton.UseVisualStyleBackColor = true;
      this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
      // 
      // folderTextBox
      // 
      this.folderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.folderTextBox.Description = "Select the folder to open";
      this.folderTextBox.Location = new System.Drawing.Point(59, 5);
      this.folderTextBox.Name = "folderTextBox";
      this.folderTextBox.Path = "";
      this.folderTextBox.Size = new System.Drawing.Size(468, 21);
      this.folderTextBox.TabIndex = 1;
      this.toolTip.SetToolTip(this.folderTextBox, "Type the name of the folder to open, then press Enter");
      this.folderTextBox.PathSelected += new System.EventHandler(this.folderTextBox_PathSelected);
      // 
      // versionLabel
      // 
      this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.versionLabel.AutoSize = true;
      this.versionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
      this.versionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.versionLabel.ForeColor = System.Drawing.Color.Blue;
      this.versionLabel.Location = new System.Drawing.Point(86, 344);
      this.versionLabel.Name = "versionLabel";
      this.versionLabel.Size = new System.Drawing.Size(50, 13);
      this.versionLabel.TabIndex = 10;
      this.versionLabel.Text = "(version)";
      this.toolTip.SetToolTip(this.versionLabel, "http://www.mobzystems.com/tools/RegName.aspx");
      this.versionLabel.UseMnemonic = false;
      this.versionLabel.Click += new System.EventHandler(this.versionLabel_Click);
      // 
      // RegNameForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(532, 364);
      this.Controls.Add(this.versionLabel);
      this.Controls.Add(this.resetButton);
      this.Controls.Add(this.renameButton);
      this.Controls.Add(this.previewButton);
      this.Controls.Add(this.replaceTextBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.matchTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.fileList);
      this.Controls.Add(this.folderTextBox);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "RegNameForm";
      this.Text = "RegName";
      this.Load += new System.EventHandler(this.RegNameForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private FolderTextBox folderTextBox;
    private System.Windows.Forms.ListView fileList;
    private System.Windows.Forms.ColumnHeader originalNameHeader;
    private System.Windows.Forms.ColumnHeader newNameHeader;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox matchTextBox;
    private System.Windows.Forms.TextBox replaceTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button previewButton;
    private System.Windows.Forms.Button renameButton;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.Label versionLabel;
  }
}