using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AssetStudio.GUI;

public partial class About : Form
{
    public About(Form mainForm)
    {
        InitializeComponent();

        Icon = mainForm.Icon;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        LoadLicense();
    }
    
    private void LoadLicense()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "AssetStudio.GUI.LICENSE";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException("LICENSE Not found！");

            using StreamReader reader = new StreamReader(stream);
            txtLicense.Text = reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to load License: {ex.Message}");
        }
    }
    
    private void lnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenUrl("https://github.com/AXiX-official/Studio");
    }

    private void lnkIssues_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenUrl("https://github.com/AXiX-official/Studio/issues");
    }
    
    private void lnkReleases_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        OpenUrl("https://github.com/AXiX-official/Studio/releases");
    }

    private void OpenUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed to open {ex.Message}");
        }
    }
}