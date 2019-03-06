using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RunCleanRhino
{
  /// <summary>
  ///   Main application form
  /// </summary>
  public partial class MainForm : Form
  {
    /// <summary>
    ///   Public constructor
    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    }

    /// <summary>
    ///   MainForm is about to load
    /// </summary>
    private void MainForm_Load(object sender, EventArgs e)
    {
      for (var i = 0; i < 5; i++)
      {
        RhinoInstall install = null;
        if (i == 0)
          install = new Rhino7Install();
        else if (i == 1)
          install = new Rhino6Install();
        else if (i == 2)
          install = new Rhino5x64Install();
        else if (i == 3)
          install = new Rhino5Install();
        else if (i == 4)
          install = new Rhino4Install();

        if (null != install)
          if (install.Find() && install.IsValid())
            comboInstalls.Items.Add(install);
      }

      if (comboInstalls.Items.Count > 0)
      {
        comboInstalls.SelectedIndex = 0;
      }
      else
      {
        MessageBox.Show(this, "No Rhino installations found!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
      }

      CenterToScreen();
    }

    /// <summary>
    ///   The "Run" button was clicked
    /// </summary>
    private void buttonRun_Click(object sender, EventArgs e)
    {
      var install = (RhinoInstall) comboInstalls.SelectedItem;
      if (null == install)
      {
        // Should never get here...
        MessageBox.Show(this, "Please select a Rhino from the drop-down list.", Text, MessageBoxButtons.OK,
          MessageBoxIcon.Information);
        comboInstalls.Focus();
        return;
      }

      // Clean up...
      install.Clean();

      // Run Rhino
      try
      {
        var start_info = new ProcessStartInfo
        {
          FileName = install.ExePath,
          Arguments = "/Scheme=Clean",
          WorkingDirectory = Path.GetDirectoryName(install.ExePath)
        };
        Process.Start(start_info);
      }
      catch
      {
        var msg = $"Unable to run {install.Title}!";
        MessageBox.Show(this, msg, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      Close();
    }

    /// <summary>
    ///   The "Cancel" button was clicked
    /// </summary>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}