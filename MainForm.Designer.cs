namespace RunCleanRhino
{
  partial class MainForm
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
      this.labelPrompt = new System.Windows.Forms.Label();
      this.comboInstalls = new System.Windows.Forms.ComboBox();
      this.buttonRun = new System.Windows.Forms.Button();
      this.buttonCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // labelPrompt
      // 
      this.labelPrompt.AutoSize = true;
      this.labelPrompt.Location = new System.Drawing.Point(13, 13);
      this.labelPrompt.Name = "labelPrompt";
      this.labelPrompt.Size = new System.Drawing.Size(101, 13);
      this.labelPrompt.TabIndex = 0;
      this.labelPrompt.Text = "Select Rhino to run:";
      // 
      // comboInstalls
      // 
      this.comboInstalls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboInstalls.FormattingEnabled = true;
      this.comboInstalls.Location = new System.Drawing.Point(16, 30);
      this.comboInstalls.Name = "comboInstalls";
      this.comboInstalls.Size = new System.Drawing.Size(256, 21);
      this.comboInstalls.TabIndex = 1;
      // 
      // buttonRun
      // 
      this.buttonRun.Location = new System.Drawing.Point(51, 57);
      this.buttonRun.Name = "buttonRun";
      this.buttonRun.Size = new System.Drawing.Size(75, 23);
      this.buttonRun.TabIndex = 2;
      this.buttonRun.Text = "Run";
      this.buttonRun.UseVisualStyleBackColor = true;
      this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
      // 
      // buttonCancel
      // 
      this.buttonCancel.Location = new System.Drawing.Point(155, 57);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new System.Drawing.Size(75, 23);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 97);
      this.Controls.Add(this.buttonCancel);
      this.Controls.Add(this.buttonRun);
      this.Controls.Add(this.comboInstalls);
      this.Controls.Add(this.labelPrompt);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Text = "Run Clean Rhino";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label labelPrompt;
    private System.Windows.Forms.ComboBox comboInstalls;
    private System.Windows.Forms.Button buttonRun;
    private System.Windows.Forms.Button buttonCancel;
  }
}

