﻿using Microsoft.Win32;
using System;
using System.IO;

namespace RunCleanRhino
{
  /// <summary>
  /// Class to track Rhino installs, used in form's combo box.
  /// </summary>
  internal class RhinoInstall
  {
    /// <summary>
    /// Public constructor
    /// </summary>
    public RhinoInstall()
    {
      Title = null;
      RegSubKey = null;
      ExeFile = null;
      ExePath = null;
      Is64Bit = false;
    }

    public string Title { get; protected set; }
    public string RegSubKey { get; protected set; }
    public string ExeFile { get; protected set; }
    public string ExePath { get; set; }
    public bool Is64Bit { get; protected set; }

    public bool IsValid()
    {
      bool rc = !string.IsNullOrEmpty(Title);
      if (rc)
        rc = !string.IsNullOrEmpty(RegSubKey);
      if (rc)
        rc = !string.IsNullOrEmpty(ExeFile);
      if (rc)
        rc = !string.IsNullOrEmpty(ExePath);
      return rc;
    }

    public override string ToString()
    {
      return Title;
    }

    /// <summary>
    /// Cleans up an installation scheme
    /// </summary>
    public virtual void Clean()
    {
      const string scheme = "Scheme: Clean";

      string keyName = RegSubKey + "\\" + scheme;
      bool bScheme = false;

      // Look for a scheme named "Clean"
      try
      {
        RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
        if (null != baseKey)
        {
          RegistryKey key = baseKey.OpenSubKey(keyName);
          if (null != key)
          {
            bScheme = true;
            key.Close();
          }
          baseKey.Close();
        }
      }
      catch
      {
      }

      // If found, delete the scheme named "Clean"
      if (bScheme)
      {
        try
        {
          RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
          if (null != baseKey)
          {
            RegistryKey key = baseKey.OpenSubKey(RegSubKey, true);
            if (null != key)
            {
              key.DeleteSubKeyTree(scheme, false);
              key.Close();
            }
            baseKey.Close();
          }
        }
        catch
        {
        }
      }
    }

    /// <summary>
    /// Finds a Rhino installation
    /// </summary>
    public virtual bool Find()
    {
      string keyName = RegSubKey + "\\Install";
      const string valueName = "Path";
      string value = null;

      try
      {
        RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Is64Bit ? RegistryView.Registry64 : RegistryView.Registry32);
        if (null != baseKey)
        {
          RegistryKey key = baseKey.OpenSubKey(keyName);
          if (null != key)
          {
            value = (string)key.GetValue(valueName);
            key.Close();
          }
          baseKey.Close();
        }
      }
      catch
      {
      }

      if (string.IsNullOrEmpty(value))
        return false;

      string filePath = value + ExeFile;
      if (!File.Exists(filePath))
        return false;

      ExePath = filePath;
      
      return true;
    }
  }

  /// <summary>
  /// Rhino 4.0 install
  /// </summary>
  internal class Rhino4Install : RhinoInstall
  {
    public Rhino4Install()
    {
      Title = "Rhinoceros 4.0";
      RegSubKey = "Software\\McNeel\\Rhinoceros\\4.0";
      ExeFile = "Rhino4.exe";
      Is64Bit = false;
    }

   /// <summary>
    /// Finds a Rhino installation
    /// </summary>
    public override bool Find()
    {
      string keyName = RegSubKey;
      string valueName = "MostRecent";
      string value = null;

      try
      {
        RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        if (null != baseKey)
        {
          RegistryKey key = baseKey.OpenSubKey(keyName);
          if (null != key)
          {
            value = (string)key.GetValue(valueName);
            key.Close();
          }
          baseKey.Close();
        }
      }
      catch
      {
      }

      if (string.IsNullOrEmpty(value))
        return false;

      keyName = string.Format("{0}\\{1}\\Install", keyName, value);
      valueName = "Path";
      value = null;

      try
      {
        RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        if (null != baseKey)
        {
          RegistryKey key = baseKey.OpenSubKey(keyName);
          if (null != key)
          {
            value = (string)key.GetValue(valueName);
            key.Close();
          }
          baseKey.Close();
        }
      }
      catch
      {
      }

      if (string.IsNullOrEmpty(value))
        return false;

      string filePath = value + ExeFile;
      if (!File.Exists(filePath))
        return false;

      ExePath = filePath;

      return true;
    }
  }

  /// <summary>
  /// Rhino 5 32-bit install
  /// </summary>
  internal class Rhino5Install : RhinoInstall
  {
    public Rhino5Install()
    {
      Title = "Rhinoceros 5 (32-bit)";
      RegSubKey = "Software\\McNeel\\Rhinoceros\\5.0";
      ExeFile = "Rhino4.exe";
      Is64Bit = false;
    }
  }

  /// <summary>
  /// Rhino 5 64-bit install
  /// </summary>
  internal class Rhino5x64Install : RhinoInstall
  {
    public Rhino5x64Install()
    {
      Title = "Rhinoceros 5 (64-bit)";
      RegSubKey = "Software\\McNeel\\Rhinoceros\\5.0x64";
      ExeFile = "Rhino.exe";
      Is64Bit = true;
    }
  }

  /// <summary>
  /// Rhino 6 install
  /// </summary>
  internal class Rhino6Install : RhinoInstall
  {
    public Rhino6Install()
    {
      Title = "Rhinoceros 6";
      RegSubKey = "Software\\McNeel\\Rhinoceros\\6.0";
      ExeFile = "Rhino.exe";
      Is64Bit = true;
    }

    public override void Clean()
    {
      base.Clean();

      string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      path += "\\McNeel\\Rhinoceros\\6.0\\settings";

      if (Directory.Exists(path))
      {
        try
        {
          string filename = path + "\\settings-Scheme__Clean.xml";
          File.Delete(filename);

          filename = path + "\\window_positions-Scheme__Clean.xml";
          File.Delete(filename);
        }
        catch (Exception)
        {
        }
      }
    }
  }

}
