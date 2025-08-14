using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AssetStudio.GUI;

partial class About
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;
    
    private Label lblAppName;
    private Label lblVersion;
    private TextBox txtLicense;
    private LinkLabel lnkGitHub;
    private LinkLabel lnkIssues;
    private LinkLabel lnkReleases;

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
        lblAppName = new Label();
        lblVersion = new Label();
        txtLicense = new TextBox();
        lnkGitHub = new LinkLabel();
        lnkIssues = new LinkLabel();
        lnkReleases = new LinkLabel();
        
        components = new Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Text = "About";
        
        lblAppName.AutoSize = true;
        lblAppName.Location = new Point(12, 9);
        lblAppName.Name = "lblAppName";
        lblAppName.Size = new Size(120, 20);
        lblAppName.Text = Application.ProductName;
        
        lblVersion.AutoSize = true;
        lblVersion.Location = new Point(12, 39);
        lblVersion.Name = "lblVersion";
        lblVersion.Size = new Size(60, 13);
        lblVersion.Text = $"Version: {Application.ProductVersion}";
        
        GroupBox licenseGroupBox = new GroupBox();
        licenseGroupBox.Text = "License";
        licenseGroupBox.Location = new Point(10, 80);
        licenseGroupBox.Size = new Size(464, 240);
        
        txtLicense.Multiline = true;
        txtLicense.ReadOnly = true;
        txtLicense.ScrollBars = ScrollBars.Vertical;
        txtLicense.Location = new Point(6, 19);
        txtLicense.Size = new Size(452, 215);
        
        licenseGroupBox.Controls.Add(txtLicense);
        
        lnkGitHub.AutoSize = true;
        lnkGitHub.Location = new Point(12, 350);
        lnkGitHub.Name = "lnkGitHub";
        lnkGitHub.Size = new Size(100, 13);
        lnkGitHub.TabStop = true;
        lnkGitHub.Text = "GitHub repo";
        lnkGitHub.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkGitHub_LinkClicked);
        
        lnkIssues.AutoSize = true;
        lnkIssues.Location = new Point(120, 350);
        lnkIssues.Name = "lnkIssues";
        lnkIssues.Size = new Size(60, 13);
        lnkIssues.TabStop = true;
        lnkIssues.Text = "Issues";
        lnkIssues.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkIssues_LinkClicked);
        
        lnkReleases.AutoSize = true;
        lnkReleases.Location = new Point(200, 350);
        lnkReleases.Name = "lnkReleases";
        lnkReleases.Size = new Size(60, 13);
        lnkReleases.TabStop = true;
        lnkReleases.Text = "Releases";
        lnkReleases.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkReleases_LinkClicked);
        
        ClientSize = new Size(484, 380);
        
        Controls.Add(lnkReleases);
        Controls.Add(lnkIssues);
        Controls.Add(lnkGitHub);
        Controls.Add(lblVersion);
        Controls.Add(lblAppName);
        Controls.Add(licenseGroupBox);
    }

    #endregion
}