namespace MOBZystems.RegName
{
  partial class FolderTextBox
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.textBox = new System.Windows.Forms.TextBox();
      this.button = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox
      // 
      this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox.Location = new System.Drawing.Point(0, 0);
      this.textBox.Name = "textBox";
      this.textBox.Size = new System.Drawing.Size(317, 20);
      this.textBox.TabIndex = 0;
      this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
      this.textBox.SizeChanged += new System.EventHandler(this.textBox_SizeChanged);
      // 
      // button
      // 
      this.button.Dock = System.Windows.Forms.DockStyle.Right;
      this.button.Location = new System.Drawing.Point(317, 0);
      this.button.Name = "button";
      this.button.Size = new System.Drawing.Size(33, 21);
      this.button.TabIndex = 1;
      this.button.Text = "...";
      this.button.UseVisualStyleBackColor = true;
      this.button.Click += new System.EventHandler(this.button_Click);
      // 
      // FolderTextBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.textBox);
      this.Controls.Add(this.button);
      this.Name = "FolderTextBox";
      this.Size = new System.Drawing.Size(350, 21);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox;
    private System.Windows.Forms.Button button;
  }
}
