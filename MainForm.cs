using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RunCleanRhino
{
  /// <summary>
  /// Main application form
  /// </summary>
  public partial class MainForm : Form
  {
    /// <summary>
    /// Public constructor
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// MainForm is about to load
    /// </summary>
    private void MainForm_Load(object sender, EventArgs e)
    {
      for (int i = 0; i < 4; i++)
      {
        RhinoInstall install = null;
        if (i == 0)
          install = new Rhino6Install();
        else if (i == 1)
          install = new Rhino5x64Install();
        else if (i == 2)
          install  = new Rhino5Install();
        else if (i == 3)
          install = new Rhino4Install();

        if (null != install)
        {
          if (install.Find() && install.IsValid())
            comboInstalls.Items.Add(install);
        }
      }

      if (comboInstalls.Items.Count > 0)
      {
        comboInstalls.SelectedIndex = 0;
      }
      else
      {
        MessageBox.Show(this, "No Rhino installations found!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Close();
      }

      this.CenterToScreen();
    }

    /// <summary>
    /// The "Run" button was clicked
    /// </summary>
    private void buttonRun_Click(object sender, EventArgs e)
    {
      RhinoInstall install = (RhinoInstall)comboInstalls.SelectedItem;
      if (null == install)
      {
        // Should never get here...
        MessageBox.Show(this, "Please select a Rhino from the drop-down list.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        comboInstalls.Focus();
        return;
      }

      // Clean up...
      install.Clean();

      // Run Rhino
      try
      {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = install.ExePath;
        startInfo.Arguments = "/Scheme=Clean";
        startInfo.WorkingDirectory = Path.GetDirectoryName(install.ExePath);
        Process.Start(startInfo);
      }
      catch
      {
        string msg = string.Format("Unable to run {0}!", install.Title);
        MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Close();
    }

    /// <summary>
    /// The "Cancel" button was clicked
    /// </summary>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
