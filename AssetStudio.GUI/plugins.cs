using System;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using super_toolbox;
using UniversalByteRemover;
using UniversalFileExtractor;
using QuickBMSBatchExtractor;

namespace AssetStudio.GUI
{
    public partial class Plugins
    {
        public static ToolStripMenuItem CreateDefaultExtractorMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem21";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "万能二进制提取器";
            menuItem.Visible = true;
            menuItem.Click += 万能二进制提取器ToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateDefaultRemoverMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem22";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "万能字节移除器";
            menuItem.Visible = true;
            menuItem.Click += 万能字节移除器ToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateSofdec2ViewerMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem23";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "USM视频查看工具汉化版";
            menuItem.Visible = true;
            menuItem.Click += Sofdec2ViewerToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateRadVideoMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem24";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "radvideo64.exe";
            menuItem.Visible = true;
            menuItem.Click += RadVideoToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateQuickBmsMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem25";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "quickbmsbatch";
            menuItem.Visible = true;
            menuItem.Click += QuickBmsBatchToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateSuperToolboxMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem26";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "超级工具箱";
            menuItem.Visible = true;
            menuItem.Click += SuperToolboxToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateRioXMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem27";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "RioX汉化版";
            menuItem.Visible = true;
            menuItem.Click += RioXToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreatePakExplorerMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem28";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "LUCA system pak解包器";
            menuItem.Visible = true;
            menuItem.Click += PakExplorerToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreatePSoundMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem29";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "PlayStation音频提取器";
            menuItem.Visible = true;
            menuItem.Click += PSoundToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateWinAsarMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem30";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "WinAsar汉化版";
            menuItem.Visible = true;
            menuItem.Click += WinAsarToolStripMenuItem_Click;
            return menuItem;
        }

        public static ToolStripMenuItem CreateWinPCKMenuItem()
        {
            var menuItem = new ToolStripMenuItem();
            menuItem.Name = "toolStripMenuItem31";
            menuItem.Size = new System.Drawing.Size(180, 22);
            menuItem.Text = "完美世界pck解包工具";
            menuItem.Visible = true;
            menuItem.Click += WinPCKToolStripMenuItem_Click;
            return menuItem;
        }

        private static void 万能二进制提取器ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var formFromExtractor = new FileExtractor();
            formFromExtractor.Show();
        }

        private static void 万能字节移除器ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var formFromByteRemover = new ByteRemover();
            formFromByteRemover.Show();
        }

        private static void SuperToolboxToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var formFromSuperToolbox = new SuperToolbox();
            formFromSuperToolbox.Show();
        }

        private static void QuickBmsBatchToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var formFromQuickBmsBatch = new QuickBMSBatchExtractor.MainForm();
            formFromQuickBmsBatch.Show();
        }

        private static void Sofdec2ViewerToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "USM视频查看工具汉化版.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到USM视频查看工具汉化版.exe，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动USM视频查看工具汉化版.exe出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RadVideoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "radvideo64.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到radvideo64.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动radvideo64.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void FsBankToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "fsbank.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到fsbank.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动fsbank.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RioXToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "RioX汉化版.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到RioX汉化版.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动RioX汉化版.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PakExplorerToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "pak_explorer.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到pak_explorer.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动pak_explorer.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PSoundToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "PSound.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到PSound.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动PSound.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void WinAsarToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "WinAsar汉化版.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到WinAsar汉化版.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动WinAsar汉化版.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void WinPCKToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                string relativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "WinPCK.exe");
                if (File.Exists(relativePath))
                {
                    Process.Start(relativePath);
                }
                else
                {
                    MessageBox.Show("未找到WinPCK.exe文件，请检查路径。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动WinPCK.exe时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void AddMenuItemsToPluginMenu(ToolStripMenuItem pluginMenu)
        {
            var extractorMenuItem = CreateDefaultExtractorMenuItem();
            var removerMenuItem = CreateDefaultRemoverMenuItem();
            var sofdec2ViewerMenuItem = CreateSofdec2ViewerMenuItem();
            var radVideoMenuItem = CreateRadVideoMenuItem();
            var quicbmsbatchMenuItem = CreateQuickBmsMenuItem();
            var superToolboxMenuItem = CreateSuperToolboxMenuItem();
            var rioXMenuItem = CreateRioXMenuItem();
            var pakExplorerMenuItem = CreatePakExplorerMenuItem();
            var pSoundMenuItem = CreatePSoundMenuItem();
            var winAsarMenuItem = CreateWinAsarMenuItem();
            var winPCKMenuItem = CreateWinPCKMenuItem();

            pluginMenu.DropDownItems.Add(extractorMenuItem);
            pluginMenu.DropDownItems.Add(removerMenuItem);
            pluginMenu.DropDownItems.Add(sofdec2ViewerMenuItem);
            pluginMenu.DropDownItems.Add(radVideoMenuItem);
            pluginMenu.DropDownItems.Add(quicbmsbatchMenuItem);
            pluginMenu.DropDownItems.Add(superToolboxMenuItem);
            pluginMenu.DropDownItems.Add(rioXMenuItem);
            pluginMenu.DropDownItems.Add(pakExplorerMenuItem);
            pluginMenu.DropDownItems.Add(pSoundMenuItem);
            pluginMenu.DropDownItems.Add(winAsarMenuItem);
            pluginMenu.DropDownItems.Add(winPCKMenuItem);
        }

        public static void AddMenuItemsToMainForm(MainForm mainForm)
        {
            var pluginToolStripMenuItem = mainForm.MenuStrip1.Items["toolStripMenuItem20"] as ToolStripMenuItem;
            if (pluginToolStripMenuItem != null)
            {
                AddMenuItemsToPluginMenu(pluginToolStripMenuItem);
            }
        }
    }
}