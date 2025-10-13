
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static AssetStudio.GUI.Studio;
using OpenTK.Graphics;
using OpenTK.Mathematics;
using System.Text.RegularExpressions;
using OpenTK.Audio.OpenAL;

namespace AssetStudio.GUI
{
    public partial class MainForm : Form
    {
        private AssetItem lastSelectedItem;
        private AssetBrowser assetBrowser;
        private DirectBitmap imageTexture;
        private string tempClipboard;
        public MenuStrip MenuStrip1 => menuStrip1;

        private FMOD.System system;
        private FMOD.Sound sound;
        private FMOD.Channel channel;
        private FMOD.SoundGroup masterSoundGroup;
        private FMOD.MODE loopMode = FMOD.MODE.LOOP_OFF;
        private uint FMODlenms;
        private float FMODVolume = 0.8f;
        private Point MouseDownLocation;

        #region TexControl
        private static char[] textureChannelNames = new[] { 'B', 'G', 'R', 'A' };
        private bool[] textureChannels = new[] { true, true, true, true };
        #endregion

        #region GLControl
        private bool glControlLoaded;
        private int mdx, mdy;
        private bool lmdown, rmdown;
        private int pgmID, pgmColorID, pgmBlackID;
        private int attributeVertexPosition;
        private int attributeNormalDirection;
        private int attributeVertexColor;
        private int uniformModelMatrix;
        private int uniformViewMatrix;
        private int uniformProjMatrix;
        private int vao;
        private OpenTK.Mathematics.Vector3[] vertexData;
        private OpenTK.Mathematics.Vector3[] normalData;
        private OpenTK.Mathematics.Vector3[] normal2Data;
        private OpenTK.Mathematics.Vector4[] colorData;
        private Matrix4 modelMatrixData;
        private Matrix4 viewMatrixData;
        private Matrix4 projMatrixData;
        private int[] indiceData;
        private int wireFrameMode;
        private int shadeMode;
        private int normalMode;
        #endregion

        //asset list sorting
        private int sortColumn = -1;
        private bool reverseSort;

        //tree search
        private int nextGObject;
        private List<TreeNode> treeSrcResults = new List<TreeNode>();

        private string openDirectoryBackup = string.Empty;
        private string saveDirectoryBackup = string.Empty;

        private GUILogger logger;

        public MainForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            InitializeComponent();
            Text = $"AssetStudio v{Application.ProductVersion}";
            InitializeExportOptions();
            InitializeProgressBar();
            InitializeLogger();
            InitalizeOptions();
            FMODinit();
            Plugins.AddMenuItemsToMainForm(this);
        }

        private void InitializeExportOptions()
        {
            enableConsole.Checked = Properties.Settings.Default.enableConsole;
            enableFileLogging.Checked = Properties.Settings.Default.enableFileLogging;
            displayAll.Checked = Properties.Settings.Default.displayAll;
            displayInfo.Checked = Properties.Settings.Default.displayInfo;
            enablePreview.Checked = Properties.Settings.Default.enablePreview;
            enableModelPreview.Checked = Properties.Settings.Default.enableModelPreview;
            modelsOnly.Checked = Properties.Settings.Default.modelsOnly;
            enableResolveDependencies.Checked = Properties.Settings.Default.enableResolveDependencies;
            allowDuplicates.Checked = Properties.Settings.Default.allowDuplicates;
            skipContainer.Checked = Properties.Settings.Default.skipContainer;
            assetsManager.ResolveDependencies = enableResolveDependencies.Checked;
            SkipContainer = Properties.Settings.Default.skipContainer;
            MiHoYoBinData.Encrypted = Properties.Settings.Default.encrypted;
            MiHoYoBinData.Key = Properties.Settings.Default.key;
            AssetsHelper.Minimal = Properties.Settings.Default.minimalAssetMap;
            AutoDetectMultipleBundle.Checked = Properties.Settings.Default.autoDetectMultipleBundle;
        }

        private void InitializeLogger()
        {
            logger = new GUILogger(StatusStripUpdate);
            ConsoleHelper.AllocConsole();
            ConsoleHelper.SetConsoleTitle("Debug Console");
            var handle = ConsoleHelper.GetConsoleWindow();
            if (enableConsole.Checked)
            {
                Logger.Default = new ConsoleLogger();
                ConsoleHelper.ShowWindow(handle, ConsoleHelper.SW_SHOW);
            }
            else
            {
                Logger.Default = logger;
                ConsoleHelper.ShowWindow(handle, ConsoleHelper.SW_HIDE);
            }
            var loggerEventType = (LoggerEvent)Properties.Settings.Default.loggerEventType;
            var loggerEventTypes = Enum.GetValues<LoggerEvent>().ToArray()[1..^1];
            foreach (var loggerEvent in loggerEventTypes)
            {
                var menuItem = new ToolStripMenuItem(loggerEvent.ToString()) { CheckOnClick = true, Checked = loggerEventType.HasFlag(loggerEvent), Tag = (int)loggerEvent };
                loggedEventsMenuItem.DropDownItems.Add(menuItem);
            }
            Logger.Flags = loggerEventType;
            Logger.FileLogging = enableFileLogging.Checked;
        }

        private void InitializeProgressBar()
        {
            Progress.Default = new Progress<int>(SetProgressBarValue);
            Studio.StatusStripUpdate = StatusStripUpdate;
        }

        private void InitalizeOptions()
        {
            var assetMapType = (ExportListType)Properties.Settings.Default.assetMapType;
            var assetMapTypes = Enum.GetValues<ExportListType>().ToArray()[1..];
            foreach (var mapType in assetMapTypes)
            {
                var menuItem = new ToolStripMenuItem(mapType.ToString()) { CheckOnClick = true, Checked = assetMapType.HasFlag(mapType), Tag = (int)mapType };
                assetMapTypeMenuItem.DropDownItems.Add(menuItem);
            }

            specifyGame.Items.AddRange(GameManager.GetGames());
            specifyGame.SelectedIndex = Properties.Settings.Default.selectedGame;
            specifyGame.SelectedIndexChanged += new EventHandler(specifyGame_SelectedIndexChanged);
            Studio.Game = GameManager.GetGame(Properties.Settings.Default.selectedGame);
            TypeFlags.SetTypes(JsonConvert.DeserializeObject<Dictionary<ClassIDType, (bool, bool)>>(Properties.Settings.Default.types));
            Logger.Info($"目标游戏类型是{Studio.Game.Type}");

            if (Studio.Game.Type.IsUnityCN())
            {
                UnityCNManager.SetKey(Properties.Settings.Default.selectedUnityCNKey);
            }

            MapNameComboBox.SelectedIndexChanged += new EventHandler(specifyNameComboBox_SelectedIndexChanged);
            if (!string.IsNullOrEmpty(Properties.Settings.Default.selectedCABMapName))
            {
                if (!AssetsHelper.LoadCABMapInternal(Properties.Settings.Default.selectedCABMapName))
                {
                    Properties.Settings.Default.selectedCABMapName = "";
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MapNameComboBox.Text = Properties.Settings.Default.selectedCABMapName;
                }
            }
        }
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length > 0)
            {
                LoadPaths(paths);
            }
        }

        public async void LoadPaths(params string[] paths)
        {
            ResetForm();
            assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
            assetsManager.Game = Studio.Game;
            if (paths.Length == 1 && Directory.Exists(paths[0]))
            {
                await Task.Run(() => assetsManager.LoadFolder(paths[0]));
            }
            else
            {
                await Task.Run(() => assetsManager.LoadFiles(paths));
            }
            BuildAssetStructures();
        }

        private async void loadFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = openDirectoryBackup;
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var paths = openFileDialog1.FileNames;
                ResetForm();
                openDirectoryBackup = Path.GetDirectoryName(paths[0]);
                assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
                assetsManager.Game = Studio.Game;
                if (paths.Length == 1 && File.Exists(paths[0]) && Path.GetExtension(paths[0]) == ".txt")
                {
                    paths = File.ReadAllLines(paths[0]);
                }
                await Task.Run(() => assetsManager.LoadFiles(paths));
                BuildAssetStructures();
            }
        }

        private async void loadFolder_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.InitialFolder = openDirectoryBackup;
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                ResetForm();
                openDirectoryBackup = openFolderDialog.Folder;
                assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
                assetsManager.Game = Studio.Game;
                await Task.Run(() => assetsManager.LoadFolder(openFolderDialog.Folder));
                BuildAssetStructures();
            }
        }

        private async void extractFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.Title = "选择保存文件夹";
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var fileNames = openFileDialog1.FileNames;
                    var savePath = saveFolderDialog.Folder;
                    var extractedCount = await Task.Run(() => ExtractFile(fileNames, savePath));
                    StatusStripUpdate($"已完成提取{extractedCount}个文件.");
                }
            }
        }

        private async void extractFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new OpenFolderDialog();
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.Title = "选择保存文件夹";
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var path = openFolderDialog.Folder;
                    var savePath = saveFolderDialog.Folder;
                    var extractedCount = await Task.Run(() => ExtractFolder(path, savePath));
                    StatusStripUpdate($"已完成提取{extractedCount}个文件.");
                }
            }
        }

        private async void BuildAssetStructures()
        {
            if (assetsManager.assetsFileList.Count == 0)
            {
                StatusStripUpdate("无法加载Unity文件.");
                return;
            }

            (var productName, var treeNodeCollection) = await Task.Run(BuildAssetData);
            var typeMap = await Task.Run(BuildClassStructure);

            if (string.IsNullOrEmpty(productName))
            {
                if (!Studio.Game.Type.IsNormal())
                {
                    productName = Studio.Game.Name;
                }
                else if (Studio.Game.Type.IsUnityCN() && UnityCNManager.TryGetEntry(Properties.Settings.Default.selectedUnityCNKey, out var unityCN))
                {
                    productName = unityCN.Name;
                }
                else
                {
                    productName = "no productName";
                }
            }

            Text = $"AssetStudio v{Application.ProductVersion} - {productName} - {assetsManager.assetsFileList[0].unityVersion} - {assetsManager.assetsFileList[0].m_TargetPlatform}";

            assetListView.VirtualListSize = visibleAssets.Count;

            sceneTreeView.BeginUpdate();
            sceneTreeView.Nodes.AddRange(treeNodeCollection.ToArray());
            sceneTreeView.EndUpdate();
            treeNodeCollection.Clear();

            classesListView.BeginUpdate();
            foreach (var version in typeMap)
            {
                var versionGroup = new ListViewGroup(version.Key);
                classesListView.Groups.Add(versionGroup);

                foreach (var uclass in version.Value)
                {
                    uclass.Value.Group = versionGroup;
                    classesListView.Items.Add(uclass.Value);
                }
            }
            typeMap.Clear();
            classesListView.EndUpdate();

            var types = exportableAssets.Select(x => x.Type).Distinct().OrderBy(x => x.ToString()).ToArray();
            foreach (var type in types)
            {
                var typeItem = new ToolStripMenuItem
                {
                    CheckOnClick = true,
                    Name = type.ToString(),
                    Size = new Size(180, 22),
                    Text = type.ToString()
                };
                typeItem.Click += typeToolStripMenuItem_Click;
                filterTypeToolStripMenuItem.DropDownItems.Add(typeItem);
            }
            allToolStripMenuItem.Checked = true;
            var log = $"已完成加载{assetsManager.assetsFileList.Count}个文件,其中包含{assetListView.Items.Count}个可导出资源";
            var m_ObjectsCount = assetsManager.assetsFileList.Sum(x => x.m_Objects.Count);
            var objectsCount = assetsManager.assetsFileList.Sum(x => x.Objects.Count);
            if (m_ObjectsCount != objectsCount)
            {
                log += $" and {m_ObjectsCount - objectsCount}个资源读取失败";
            }
            StatusStripUpdate(log);
        }

        private void typeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var typeItem = (ToolStripMenuItem)sender;
            if (typeItem != allToolStripMenuItem)
            {
                allToolStripMenuItem.Checked = false;
            }
            else if (allToolStripMenuItem.Checked)
            {
                for (var i = 1; i < filterTypeToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (ToolStripMenuItem)filterTypeToolStripMenuItem.DropDownItems[i];
                    item.Checked = false;
                }
            }
            FilterAssetList();
        }

        private void AssetStudioForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (glControl.Visible)
            {
                if (e.Control)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.W:
                            //Toggle WireFrame
                            wireFrameMode = (wireFrameMode + 1) % 3;
                            glControl.Invalidate();
                            break;
                        case Keys.S:
                            //Toggle Shade
                            shadeMode = (shadeMode + 1) % 2;
                            glControl.Invalidate();
                            break;
                        case Keys.N:
                            //Normal mode
                            normalMode = (normalMode + 1) % 2;
                            CreateVAO();
                            glControl.Invalidate();
                            break;
                    }
                }
            }
            else if (previewPanel.Visible)
            {
                if (e.Control)
                {
                    var need = false;
                    switch (e.KeyCode)
                    {
                        case Keys.B:
                            textureChannels[0] = !textureChannels[0];
                            need = true;
                            break;
                        case Keys.G:
                            textureChannels[1] = !textureChannels[1];
                            need = true;
                            break;
                        case Keys.R:
                            textureChannels[2] = !textureChannels[2];
                            need = true;
                            break;
                        case Keys.A:
                            textureChannels[3] = !textureChannels[3];
                            need = true;
                            break;
                    }
                    if (need)
                    {
                        if (lastSelectedItem != null)
                        {
                            PreviewAsset(lastSelectedItem);
                            assetInfoLabel.Text = lastSelectedItem.InfoText;
                        }
                    }
                }
            }
        }

        private void exportClassStructuresMenuItem_Click(object sender, EventArgs e)
        {
            if (classesListView.Items.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var savePath = saveFolderDialog.Folder;
                    var count = classesListView.Items.Count;
                    int i = 0;
                    Progress.Reset();
                    foreach (TypeTreeItem item in classesListView.Items)
                    {
                        var versionPath = Path.Combine(savePath, item.Group.Header);
                        Directory.CreateDirectory(versionPath);

                        var saveFile = $"{versionPath}{Path.DirectorySeparatorChar}{item.SubItems[1].Text} {item.Text}.txt";
                        File.WriteAllText(saveFile, item.ToString());

                        Progress.Report(++i, count);
                    }

                    StatusStripUpdate("已完成类结构的导出");
                }
            }
        }

        private void displayAll_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.displayAll = displayAll.Checked;
            Properties.Settings.Default.Save();
        }
        
        private void autoDetectMultipleBundle_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoDetectMultipleBundle = AutoDetectMultipleBundle.Checked;
            assetsManager.autoDetectMultipleBundle = AutoDetectMultipleBundle.Checked;
            Studio.AutoDetectMultipleBundle = AutoDetectMultipleBundle.Checked;
            Properties.Settings.Default.Save();
        }

        private void enablePreview_Check(object sender, EventArgs e)
        {
            if (lastSelectedItem != null)
            {
                switch (lastSelectedItem.Type)
                {
                    case ClassIDType.Texture2D:
                    case ClassIDType.Sprite:
                        {
                            if (enablePreview.Checked && imageTexture != null)
                            {
                                imgPreviewBox.Image = imageTexture.Bitmap;
                            }
                            else
                            {
                                imgPreviewBox.Image = null;
                            }
                        }
                        break;
                    case ClassIDType.Shader:
                    case ClassIDType.TextAsset:
                    case ClassIDType.MonoBehaviour:
                    case ClassIDType.MiHoYoBinData:
                        textPreviewBox.Visible = !textPreviewBox.Visible;
                        break;
                    case ClassIDType.Font:
                        fontPreviewBox.Visible = !fontPreviewBox.Visible;
                        break;
                    case ClassIDType.AudioClip:
                        {
                            FMODpanel.Visible = !FMODpanel.Visible;

                            if (sound != null && channel != null)
                            {
                                var result = channel.isPlaying(out var playing);
                                if (result == FMOD.RESULT.OK && playing)
                                {
                                    channel.stop();
                                    FMODreset();
                                }
                            }
                            else if (FMODpanel.Visible)
                            {
                                PreviewAsset(lastSelectedItem);
                            }

                            break;
                        }

                }

            }
            else if (lastSelectedItem != null && enablePreview.Checked)
            {
                PreviewAsset(lastSelectedItem);
            }

            Properties.Settings.Default.enablePreview = enablePreview.Checked;
            Properties.Settings.Default.Save();
        }
        private void displayAssetInfo_Check(object sender, EventArgs e)
        {
            if (displayInfo.Checked && assetInfoLabel.Text != null)
            {
                assetInfoLabel.Visible = true;
            }
            else
            {
                assetInfoLabel.Visible = false;
            }

            Properties.Settings.Default.displayInfo = displayInfo.Checked;
            Properties.Settings.Default.Save();
        }

        private void showExpOpt_Click(object sender, EventArgs e)
        {
            var exportOpt = new ExportOptions();
            if (exportOpt.ShowDialog(this) == DialogResult.OK && exportOpt.Resetted)
            {
                InitializeExportOptions();
                InitializeLogger();
                InitalizeOptions();
            }
        }

        private void assetListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = visibleAssets[e.ItemIndex];
        }

        private void tabPageSelected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    treeSearch.Select();
                    break;
                case 1:
                    listSearch.Select();
                    break;
            }
        }

        private void treeSearch_TextChanged(object sender, EventArgs e)
        {
            treeSrcResults.Clear();
            nextGObject = 0;
        }

        private void treeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(treeSearch.Text))
            {
                if (treeSrcResults.Count == 0)
                {
                    try
                    {
                        Regex.Match("", treeSearch.Text, RegexOptions.IgnoreCase);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("无效的正则表达式.\n" + ex.Message);
                        return;
                    }
                    var regex = new Regex(treeSearch.Text, RegexOptions.IgnoreCase);
                    foreach (TreeNode node in sceneTreeView.Nodes)
                    {
                        TreeNodeSearch(regex, node);
                    }
                }
                if (treeSrcResults.Count > 0)
                {
                    if (e.Shift)
                    {
                        foreach (var node in treeSrcResults)
                        {
                            var tempNode = node;
                            if (e.Alt)
                            {
                                while (tempNode.Parent != null)
                                {
                                    tempNode = tempNode.Parent;
                                }
                            }
                            tempNode.EnsureVisible();
                            tempNode.Checked = e.Control;
                        }
                        sceneTreeView.SelectedNode = treeSrcResults[0];
                    }
                    else
                    {
                        if (nextGObject >= treeSrcResults.Count)
                        {
                            nextGObject = 0;
                        }
                        var node = treeSrcResults[nextGObject];
                        if (e.Alt)
                        {
                            while (node.Parent != null)
                            {
                                node = node.Parent;
                            }
                        }

                        node.EnsureVisible();
                        node.Checked = e.Control;
                        sceneTreeView.SelectedNode = treeSrcResults[nextGObject];
                        nextGObject++;
                    }
                }
            }
        }

        private void TreeNodeSearch(Regex regex, TreeNode treeNode)
        {
            if (regex.IsMatch(treeNode.Text))
            {
                treeSrcResults.Add(treeNode);
            }

            foreach (TreeNode node in treeNode.Nodes)
            {
                TreeNodeSearch(regex, node);
            }
        }

        private void sceneTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode childNode in e.Node.Nodes)
            {
                childNode.Checked = e.Node.Checked;
            }
        }

        private void sceneHierarchy_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog() { FileName = "scene.json", Filter = "场景层次结构转储| *.json" };
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var path = saveFileDialog.FileName;
                var nodes = new Dictionary<string, object>();
                foreach (TreeNode node in sceneTreeView.Nodes)
                {
                    var value = GetNode(node);
                    nodes.Add(node.Text, value);
                }
                var json = JsonConvert.SerializeObject(nodes, Formatting.Indented);
                File.WriteAllText(path, json);
                Logger.Info("场景层次结构已成功转储!!");
            }
        }

        private object GetNode(TreeNode treeNode)
        {
            var nodes = new Dictionary<string, object>();
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (HasGameObjectNode(node))
                {
                    nodes.TryAdd(node.Text, GetNode(node));
                }
            }
            return nodes.Count == 0 ? string.Empty : nodes;
        }

        private bool HasGameObjectNode(TreeNode treeNode)
        {
            if (treeNode is GameObjectTreeNode gameObjectNode && !(bool)gameObjectNode.gameObject.m_Transform?.m_Father.IsNull)
            {
                return gameObjectNode.gameObject.m_Animator != null;
            }
            else
            {
                foreach (TreeNode node in treeNode.Nodes)
                {
                    return HasGameObjectNode(node);
                }
                return false;
            }
        }

        private void listSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Invoke(new Action(FilterAssetList));
            }
        }

        private void assetListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (sortColumn != e.Column)
            {
                reverseSort = false;
            }
            else
            {
                reverseSort = !reverseSort;
            }
            sortColumn = e.Column;
            assetListView.BeginUpdate();
            assetListView.SelectedIndices.Clear();
            if (sortColumn == 4) //FullSize
            {
                visibleAssets.Sort((a, b) =>
                {
                    var asf = a.FullSize;
                    var bsf = b.FullSize;
                    return reverseSort ? bsf.CompareTo(asf) : asf.CompareTo(bsf);
                });
            }
            else if (sortColumn == 3) // PathID
            {
                visibleAssets.Sort((x, y) =>
                {
                    long pathID_X = x.m_PathID;
                    long pathID_Y = y.m_PathID;
                    return reverseSort ? pathID_Y.CompareTo(pathID_X) : pathID_X.CompareTo(pathID_Y);
                });
            }
            else
            {
                visibleAssets.Sort((a, b) =>
                {
                    var at = a.SubItems[sortColumn].Text;
                    var bt = b.SubItems[sortColumn].Text;
                    return reverseSort ? bt.CompareTo(at) : at.CompareTo(bt);
                });
            }
            assetListView.EndUpdate();
        }

        private void selectAsset(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            imgPreviewBox.Image = null;
            classTextBox.Visible = false;
            assetInfoLabel.Visible = false;
            assetInfoLabel.Text = null;
            textPreviewBox.Visible = false;
            imgPreviewBox.Visible = false;
            fontPreviewBox.Visible = false;
            FMODpanel.Visible = false;
            glControl.Visible = false;
            StatusStripUpdate("");

            FMODreset();

            lastSelectedItem = (AssetItem)e.Item;

            if (e.IsSelected)
            {
                if (tabControl2.SelectedIndex == 1)
                {
                    dumpTextBox.Text = DumpAsset(lastSelectedItem.Asset);
                }
                if (enablePreview.Checked)
                {
                    PreviewAsset(lastSelectedItem);
                    if (displayInfo.Checked && lastSelectedItem.InfoText != null)
                    {
                        assetInfoLabel.Text = lastSelectedItem.InfoText;
                        assetInfoLabel.Visible = true;
                    }
                }
            }
        }

        private void classesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            classTextBox.Visible = true;
            assetInfoLabel.Visible = false;
            assetInfoLabel.Text = null;
            textPreviewBox.Visible = false;
            imgPreviewBox.Visible = false;
            fontPreviewBox.Visible = false;
            FMODpanel.Visible = false;
            glControl.Visible = false;
            StatusStripUpdate("");
            if (e.IsSelected)
            {
                classTextBox.Text = ((TypeTreeItem)classesListView.SelectedItems[0]).ToString();
            }
        }

        private void preview_Resize(object sender, EventArgs e)
        {
            if (glControlLoaded && glControl.Visible)
            {
                ChangeGLSize(glControl.Size);
                glControl.Invalidate();
            }
        }

        private void PreviewAsset(AssetItem assetItem)
        {
            if (assetItem == null)
                return;
            try
            {
                switch (assetItem.Asset)
                {
                    case GameObject m_GameObject when Properties.Settings.Default.enableModelPreview:
                        PreviewGameObject(m_GameObject);
                        break;
                    case Texture2D m_Texture2D:
                        PreviewTexture2D(assetItem, m_Texture2D);
                        break;
                    case AudioClip m_AudioClip:
                        PreviewAudioClip(assetItem, m_AudioClip);
                        break;
                    case Shader m_Shader:
                        PreviewShader(m_Shader);
                        break;
                    case TextAsset m_TextAsset:
                        PreviewTextAsset(m_TextAsset);
                        break;
                    case MonoBehaviour m_MonoBehaviour:
                        PreviewMonoBehaviour(m_MonoBehaviour);
                        break;
                    case Font m_Font:
                        PreviewFont(m_Font);
                        break;
                    case Mesh m_Mesh:
                        PreviewMesh(m_Mesh);
                        break;
                    case VideoClip _:
                    case MovieTexture _:
                        StatusStripUpdate("仅支持导出.");
                        break;
                    case Sprite m_Sprite:
                        PreviewSprite(assetItem, m_Sprite);
                        break;
                    case Animator m_Animator when Properties.Settings.Default.enableModelPreview:
                        //StatusStripUpdate("Can be exported to FBX file.");
                        PreviewAnimator(m_Animator);
                        break;
                    case AnimationClip m_AnimationClip:
                        PreviewAnimationClip(m_AnimationClip);
                        break;
                    case MiHoYoBinData m_MiHoYoBinData:
                        PreviewText(m_MiHoYoBinData.AsString);
                        StatusStripUpdate("如果数据是有效的JSON，则可以将其导出/预览为JSON格式(检查XOR).");
                        break;
                    default:
                        var str = assetItem.Asset.Dump();
                        if (str != null)
                        {
                            textPreviewBox.Text = str;
                            textPreviewBox.Visible = true;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"预览{assetItem.Type}:{assetItem.Text}出错\r\n{e.Message}\r\n{e.StackTrace}");
            }
        }

        private void PreviewTexture2D(AssetItem assetItem, Texture2D m_Texture2D)
        {
            var image = m_Texture2D.ConvertToImage(true);
            if (image != null)
            {
                var bitmap = new DirectBitmap(image.ConvertToBytes(), m_Texture2D.m_Width, m_Texture2D.m_Height);
                image.Dispose();
                assetItem.InfoText = $"宽度: {m_Texture2D.m_Width}\n高度: {m_Texture2D.m_Height}\n格式: {m_Texture2D.m_TextureFormat}";
                switch (m_Texture2D.m_TextureSettings.m_FilterMode)
                {
                    case 0: assetItem.InfoText += "\n过滤模式:点"; break;
                    case 1: assetItem.InfoText += "\n过滤模式:双线性"; break;
                    case 2: assetItem.InfoText += "\n过滤模式:三线性"; break;
                }
                assetItem.InfoText += $"\n各向异性级别: {m_Texture2D.m_TextureSettings.m_Aniso}\n多级渐远纹理偏移: {m_Texture2D.m_TextureSettings.m_MipBias}";
                switch (m_Texture2D.m_TextureSettings.m_WrapMode)
                {
                    case 0: assetItem.InfoText += "\n环绕模式:重复"; break;
                    case 1: assetItem.InfoText += "\n环绕模式:钳位"; break;
                }
                assetItem.InfoText += "\n通道: ";
                int validChannel = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (textureChannels[i])
                    {
                        assetItem.InfoText += textureChannelNames[i];
                        validChannel++;
                    }
                }
                if (validChannel == 0)
                    assetItem.InfoText += "无";
                if (validChannel != 4)
                {
                    var bytes = bitmap.Bits;
                    for (int i = 0; i < bitmap.Height; i++)
                    {
                        int offset = Math.Abs(bitmap.Stride) * i;
                        for (int j = 0; j < bitmap.Width; j++)
                        {
                            bytes[offset] = textureChannels[0] ? bytes[offset] : validChannel == 1 && textureChannels[3] ? byte.MaxValue : byte.MinValue;
                            bytes[offset + 1] = textureChannels[1] ? bytes[offset + 1] : validChannel == 1 && textureChannels[3] ? byte.MaxValue : byte.MinValue;
                            bytes[offset + 2] = textureChannels[2] ? bytes[offset + 2] : validChannel == 1 && textureChannels[3] ? byte.MaxValue : byte.MinValue;
                            bytes[offset + 3] = textureChannels[3] ? bytes[offset + 3] : byte.MaxValue;
                            offset += 4;
                        }
                    }
                }
                PreviewTexture(bitmap);

                StatusStripUpdate("'按Ctrl'+'R'/'G'/'B'/'A'可切换通道");
            }
            else
            {
                StatusStripUpdate("不支持用于预览的图像");
            }
        }

        private void PreviewAudioClip(AssetItem assetItem, AudioClip m_AudioClip)
        {
            //Info
            assetItem.InfoText = "压缩格式: ";
            if (m_AudioClip.version.Major < 5)
            {
                switch (m_AudioClip.m_Type)
                {
                    case FMODSoundType.ACC:
                        assetItem.InfoText += "Acc";
                        break;
                    case FMODSoundType.AIFF:
                        assetItem.InfoText += "AIFF";
                        break;
                    case FMODSoundType.IT:
                        assetItem.InfoText += "Impulse tracker";
                        break;
                    case FMODSoundType.MOD:
                        assetItem.InfoText += "Protracker / Fasttracker MOD";
                        break;
                    case FMODSoundType.MPEG:
                        assetItem.InfoText += "MP2/MP3 MPEG";
                        break;
                    case FMODSoundType.OGGVORBIS:
                        assetItem.InfoText += "Ogg vorbis";
                        break;
                    case FMODSoundType.S3M:
                        assetItem.InfoText += "ScreamTracker 3";
                        break;
                    case FMODSoundType.WAV:
                        assetItem.InfoText += "Microsoft WAV";
                        break;
                    case FMODSoundType.XM:
                        assetItem.InfoText += "FastTracker 2 XM";
                        break;
                    case FMODSoundType.XMA:
                        assetItem.InfoText += "Xbox360 XMA";
                        break;
                    case FMODSoundType.VAG:
                        assetItem.InfoText += "PlayStation Portable ADPCM";
                        break;
                    case FMODSoundType.AUDIOQUEUE:
                        assetItem.InfoText += "iPhone";
                        break;
                    default:
                        assetItem.InfoText += "Unknown";
                        break;
                }
            }
            else
            {
                switch (m_AudioClip.m_CompressionFormat)
                {
                    case AudioCompressionFormat.PCM:
                        assetItem.InfoText += "PCM";
                        break;
                    case AudioCompressionFormat.Vorbis:
                        assetItem.InfoText += "Vorbis";
                        break;
                    case AudioCompressionFormat.ADPCM:
                        assetItem.InfoText += "ADPCM";
                        break;
                    case AudioCompressionFormat.MP3:
                        assetItem.InfoText += "MP3";
                        break;
                    case AudioCompressionFormat.PSMVAG:
                        assetItem.InfoText += "PlayStation Portable ADPCM";
                        break;
                    case AudioCompressionFormat.HEVAG:
                        assetItem.InfoText += "PSVita ADPCM";
                        break;
                    case AudioCompressionFormat.XMA:
                        assetItem.InfoText += "Xbox360 XMA";
                        break;
                    case AudioCompressionFormat.AAC:
                        assetItem.InfoText += "AAC";
                        break;
                    case AudioCompressionFormat.GCADPCM:
                        assetItem.InfoText += "Nintendo 3DS/Wii DSP";
                        break;
                    case AudioCompressionFormat.ATRAC9:
                        assetItem.InfoText += "PSVita ATRAC9";
                        break;
                    default:
                        assetItem.InfoText += "Unknown";
                        break;
                }
            }

            var m_AudioData = m_AudioClip.m_AudioData.GetData();
            if (m_AudioData == null || m_AudioData.Length == 0)
                return;
            var exinfo = new FMOD.CREATESOUNDEXINFO();

            exinfo.cbsize = Marshal.SizeOf(exinfo);
            exinfo.length = (uint)m_AudioClip.m_Size;

            var result = system.createSound(m_AudioData, FMOD.MODE.OPENMEMORY | loopMode, ref exinfo, out sound);
            if (ERRCHECK(result)) return;

            sound.getNumSubSounds(out var numsubsounds);

            if (numsubsounds > 0)
            {
                result = sound.getSubSound(0, out var subsound);
                if (result == FMOD.RESULT.OK)
                {
                    sound = subsound;
                }
            }

            result = sound.getLength(out FMODlenms, FMOD.TIMEUNIT.MS);
            if (ERRCHECK(result)) return;

            result = system.playSound(sound, null, true, out channel);
            if (ERRCHECK(result)) return;

            FMODpanel.Visible = true;

            result = channel.getFrequency(out var frequency);
            if (ERRCHECK(result)) return;

            FMODinfoLabel.Text = frequency + " Hz";
            FMODtimerLabel.Text = $"0:0.0 / {FMODlenms / 1000 / 60}:{FMODlenms / 1000 % 60}.{FMODlenms / 10 % 100}";
        }

        private void PreviewShader(Shader m_Shader)
        {
            if (m_Shader.byteSize > 0xFFFFFFF)
            {
                PreviewText("着色器太大，无法解析");
                return;
            }

            var str = m_Shader.Convert();
            PreviewText(str == null ? "无法读取序列化着色器" : str.Replace("\n", "\r\n"));
        }

        private void PreviewTextAsset(TextAsset m_TextAsset)
        {
            var text = Encoding.UTF8.GetString(m_TextAsset.m_Script);
            text = text.Replace("\n", "\r\n").Replace("\0", "");
            PreviewText(text);
        }

        private void PreviewMonoBehaviour(MonoBehaviour m_MonoBehaviour)
        {
            var obj = m_MonoBehaviour.ToType();
            if (obj == null)
            {
                var type = MonoBehaviourToTypeTree(m_MonoBehaviour);
                obj = m_MonoBehaviour.ToType(type);
            }
            var str = JsonConvert.SerializeObject(obj, Formatting.Indented);
            PreviewText(str);
        }

        private void PreviewFont(Font m_Font)
        {
            if (m_Font.m_FontData != null)
            {
                var data = Marshal.AllocCoTaskMem(m_Font.m_FontData.Length);
                Marshal.Copy(m_Font.m_FontData, 0, data, m_Font.m_FontData.Length);

                uint cFonts = 0;
                var re = FontHelper.AddFontMemResourceEx(data, (uint)m_Font.m_FontData.Length, IntPtr.Zero, ref cFonts);
                if (re != IntPtr.Zero)
                {
                    using (var pfc = new PrivateFontCollection())
                    {
                        pfc.AddMemoryFont(data, m_Font.m_FontData.Length);
                        Marshal.FreeCoTaskMem(data);
                        if (pfc.Families.Length > 0)
                        {
                            fontPreviewBox.SelectionStart = 0;
                            fontPreviewBox.SelectionLength = 80;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 16, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 81;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 12, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 138;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 18, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 195;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 24, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 252;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 36, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 309;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 48, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 366;
                            fontPreviewBox.SelectionLength = 56;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 60, FontStyle.Regular);
                            fontPreviewBox.SelectionStart = 423;
                            fontPreviewBox.SelectionLength = 55;
                            fontPreviewBox.SelectionFont = new System.Drawing.Font(pfc.Families[0], 72, FontStyle.Regular);
                            fontPreviewBox.Visible = true;
                        }
                    }
                    return;
                }
            }
            StatusStripUpdate("不支持用于预览的字体.尝试导出.");
        }

        private void PreviewMesh(Mesh m_Mesh)
        {
            if (m_Mesh.m_VertexCount > 0)
            {
                viewMatrixData = Matrix4.CreateRotationY(-(float)Math.PI / 4) * Matrix4.CreateRotationX(-(float)Math.PI / 6);
                #region Vertices
                if (m_Mesh.m_Vertices == null || m_Mesh.m_Vertices.Length == 0)
                {
                    StatusStripUpdate("Mesh can't be previewed.");
                    return;
                }
                int count = 3;
                if (m_Mesh.m_Vertices.Length == m_Mesh.m_VertexCount * 4)
                {
                    count = 4;
                }
                vertexData = new OpenTK.Mathematics.Vector3[m_Mesh.m_VertexCount];
                // Calculate Bounding
                float[] min = new float[3];
                float[] max = new float[3];
                for (int i = 0; i < 3; i++)
                {
                    min[i] = m_Mesh.m_Vertices[i];
                    max[i] = m_Mesh.m_Vertices[i];
                }
                for (int v = 0; v < m_Mesh.m_VertexCount; v++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        min[i] = Math.Min(min[i], m_Mesh.m_Vertices[v * count + i]);
                        max[i] = Math.Max(max[i], m_Mesh.m_Vertices[v * count + i]);
                    }
                    vertexData[v] = new OpenTK.Mathematics.Vector3(
                        m_Mesh.m_Vertices[v * count],
                        m_Mesh.m_Vertices[v * count + 1],
                        m_Mesh.m_Vertices[v * count + 2]);
                }

                // Calculate modelMatrix
                var dist = OpenTK.Mathematics.Vector3.One;
                var offset = OpenTK.Mathematics.Vector3.Zero;
                for (int i = 0; i < 3; i++)
                {
                    dist[i] = max[i] - min[i];
                    offset[i] = (max[i] + min[i]) / 2;
                }
                float d = Math.Max(1e-5f, dist.Length);
                modelMatrixData = Matrix4.CreateTranslation(-offset) * Matrix4.CreateScale(2f / d);
                #endregion
                #region Indicies
                indiceData = new int[m_Mesh.m_Indices.Count];
                for (int i = 0; i < m_Mesh.m_Indices.Count; i = i + 3)
                {
                    indiceData[i] = (int)m_Mesh.m_Indices[i];
                    indiceData[i + 1] = (int)m_Mesh.m_Indices[i + 1];
                    indiceData[i + 2] = (int)m_Mesh.m_Indices[i + 2];
                }
                #endregion
                #region Normals
                if (m_Mesh.m_Normals != null && m_Mesh.m_Normals.Length > 0)
                {
                    if (m_Mesh.m_Normals.Length == m_Mesh.m_VertexCount * 3)
                        count = 3;
                    else if (m_Mesh.m_Normals.Length == m_Mesh.m_VertexCount * 4)
                        count = 4;
                    normalData = new OpenTK.Mathematics.Vector3[m_Mesh.m_VertexCount];
                    for (int n = 0; n < m_Mesh.m_VertexCount; n++)
                    {
                        normalData[n] = new OpenTK.Mathematics.Vector3(
                            m_Mesh.m_Normals[n * count],
                            m_Mesh.m_Normals[n * count + 1],
                            m_Mesh.m_Normals[n * count + 2]);
                    }
                }
                else
                    normalData = null;
                // calculate normal by ourself
                normal2Data = new OpenTK.Mathematics.Vector3[m_Mesh.m_VertexCount];
                int[] normalCalculatedCount = new int[m_Mesh.m_VertexCount];
                for (int i = 0; i < m_Mesh.m_VertexCount; i++)
                {
                    normal2Data[i] = OpenTK.Mathematics.Vector3.Zero;
                    normalCalculatedCount[i] = 0;
                }
                for (int i = 0; i < m_Mesh.m_Indices.Count; i = i + 3)
                {
                    var dir1 = vertexData[indiceData[i + 1]] - vertexData[indiceData[i]];
                    var dir2 = vertexData[indiceData[i + 2]] - vertexData[indiceData[i]];
                    var normal = OpenTK.Mathematics.Vector3.Cross(dir1, dir2);
                    normal.Normalize();
                    for (int j = 0; j < 3; j++)
                    {
                        normal2Data[indiceData[i + j]] += normal;
                        normalCalculatedCount[indiceData[i + j]]++;
                    }
                }
                for (int i = 0; i < m_Mesh.m_VertexCount; i++)
                {
                    if (normalCalculatedCount[i] == 0)
                        normal2Data[i] = new OpenTK.Mathematics.Vector3(0, 1, 0);
                    else
                        normal2Data[i] /= normalCalculatedCount[i];
                }
                #endregion
                #region Colors
                if (m_Mesh.m_Colors != null && m_Mesh.m_Colors.Length == m_Mesh.m_VertexCount * 3)
                {
                    colorData = new OpenTK.Mathematics.Vector4[m_Mesh.m_VertexCount];
                    for (int c = 0; c < m_Mesh.m_VertexCount; c++)
                    {
                        colorData[c] = new OpenTK.Mathematics.Vector4(
                            m_Mesh.m_Colors[c * 3],
                            m_Mesh.m_Colors[c * 3 + 1],
                            m_Mesh.m_Colors[c * 3 + 2],
                            1.0f);
                    }
                }
                else if (m_Mesh.m_Colors != null && m_Mesh.m_Colors.Length == m_Mesh.m_VertexCount * 4)
                {
                    colorData = new OpenTK.Mathematics.Vector4[m_Mesh.m_VertexCount];
                    for (int c = 0; c < m_Mesh.m_VertexCount; c++)
                    {
                        colorData[c] = new OpenTK.Mathematics.Vector4(
                        m_Mesh.m_Colors[c * 4],
                        m_Mesh.m_Colors[c * 4 + 1],
                        m_Mesh.m_Colors[c * 4 + 2],
                        m_Mesh.m_Colors[c * 4 + 3]);
                    }
                }
                else
                {
                    colorData = new OpenTK.Mathematics.Vector4[m_Mesh.m_VertexCount];
                    for (int c = 0; c < m_Mesh.m_VertexCount; c++)
                    {
                        colorData[c] = new OpenTK.Mathematics.Vector4(0.5f, 0.5f, 0.5f, 1.0f);
                    }
                }
                #endregion
                glControl.Visible = true;
                CreateVAO();
                StatusStripUpdate("使用的OpenGL版本: " + GL.GetString(StringName.Version) + "\n"
                                  + "'鼠标左键'=旋转 | '鼠标右键'=移动 | '鼠标滚轮'=缩放 \n"
                                  + "'Ctrl W'=线框模式 | 'Ctrl S'=阴影 | 'Ctrl N'=重新归一化 ");
            }
            else
            {
                StatusStripUpdate("无法预览此网格");
            }
        }

        private void PreviewGameObject(GameObject m_GameObject)
        {
            var options = new ModelConverter.Options()
            {
                imageFormat = Properties.Settings.Default.convertType,
                game = Studio.Game,
                collectAnimations = Properties.Settings.Default.collectAnimations,
                exportMaterials = false,
                materials = new HashSet<Material>(),
                uvs = JsonConvert.DeserializeObject<Dictionary<string, (bool, int)>>(Properties.Settings.Default.uvs),
                texs = JsonConvert.DeserializeObject<Dictionary<string, int>>(Properties.Settings.Default.texs),
            };
            var model = new ModelConverter(m_GameObject, options, Array.Empty<AnimationClip>());
            PreviewModel(model);
        }
        private void PreviewAnimator(Animator m_Animator)
        {
            var options = new ModelConverter.Options()
            {
                imageFormat = Properties.Settings.Default.convertType,
                game = Studio.Game,
                collectAnimations = Properties.Settings.Default.collectAnimations,
                exportMaterials = false,
                materials = new HashSet<Material>(),
                uvs = JsonConvert.DeserializeObject<Dictionary<string, (bool, int)>>(Properties.Settings.Default.uvs),
                texs = JsonConvert.DeserializeObject<Dictionary<string, int>>(Properties.Settings.Default.texs),
            };
            var model = new ModelConverter(m_Animator, options, Array.Empty<AnimationClip>());
            PreviewModel(model);
        }

        private void PreviewAnimationClip(AnimationClip clip)
        {
            var str = clip.Convert();
            if (string.IsNullOrEmpty(str))
                str = "传统动画不受支持";
            PreviewText(str.Replace("\n", "\r\n"));
        }

        private void PreviewModel(ModelConverter model)
        {
            if (model.MeshList.Count > 0)
            {
                viewMatrixData = Matrix4.CreateRotationY(-(float)Math.PI / 4) * Matrix4.CreateRotationX(-(float)Math.PI / 6);
                #region Vertices
                vertexData = model.MeshList.SelectMany(x => x.VertexList).Select(x => new OpenTK.Mathematics.Vector3(x.Vertex.X, x.Vertex.Y, x.Vertex.Z)).ToArray();
                // Calculate Bounding
                var min = vertexData.Aggregate(OpenTK.Mathematics.Vector3.ComponentMin);
                var max = vertexData.Aggregate(OpenTK.Mathematics.Vector3.ComponentMax);

                // Calculate modelMatrix
                var dist = max - min;
                var offset = (max - min) / 2;
                float d = Math.Max(1e-5f, dist.Length);
                modelMatrixData = Matrix4.CreateTranslation(-offset) * Matrix4.CreateScale(2f / d);
                #endregion
                #region Indicies
                int meshOffset = 0;
                var indices = new List<int>();
                foreach (var mesh in model.MeshList)
                {
                    foreach (var submesh in mesh.SubmeshList)
                    {
                        foreach (var face in submesh.FaceList)
                        {
                            foreach (var index in face.VertexIndices)
                            {
                                indices.Add(submesh.BaseVertex + index + meshOffset);
                            }
                        }
                    }
                    meshOffset += mesh.VertexList.Count;
                }
                indiceData = indices.ToArray();
                #endregion
                #region Normals
                normalData = model.MeshList.SelectMany(x => x.VertexList).Select(x => new OpenTK.Mathematics.Vector3(x.Normal.X, x.Normal.Y, x.Normal.Z)).ToArray();
                // calculate normal by ourself
                normal2Data = new OpenTK.Mathematics.Vector3[vertexData.Length];
                int[] normalCalculatedCount = new int[vertexData.Length];
                Array.Fill(normal2Data, OpenTK.Mathematics.Vector3.Zero);
                Array.Fill(normalCalculatedCount, 0);
                for (int j = 0; j < indiceData.Length; j += 3)
                {
                    var dir1 = vertexData[indiceData[j + 1]] - vertexData[indiceData[j]];
                    var dir2 = vertexData[indiceData[j + 2]] - vertexData[indiceData[j]];
                    var normal = OpenTK.Mathematics.Vector3.Cross(dir1, dir2);
                    normal.Normalize();
                    for (int k = 0; k < 3; k++)
                    {
                        normal2Data[indiceData[j + k]] += normal;
                        normalCalculatedCount[indiceData[j + k]]++;
                    }
                }
                for (int j = 0; j < vertexData.Length; j++)
                {
                    if (normalCalculatedCount[j] == 0)
                        normal2Data[j] = new OpenTK.Mathematics.Vector3(0, 1, 0);
                    else
                        normal2Data[j] /= normalCalculatedCount[j];
                }
                #endregion
                #region Colors
                colorData = model.MeshList.SelectMany(x => x.VertexList).Select(x => new OpenTK.Mathematics.Vector4(x.Color.R, x.Color.G, x.Color.B, x.Color.A)).ToArray();
                #endregion
                glControl.Visible = true;
                CreateVAO();
                StatusStripUpdate("使用的OpenGL版本: " + GL.GetString(StringName.Version) + "\n"
                                  + "'鼠标左键'=旋转 | '鼠标右键'=移动 | '鼠标滚轮'=缩放 \n"
                                  + "'Ctrl W'=线框模式 | 'Ctrl S'=阴影 | 'Ctrl N'=重新归一化 ");
            }
            else
            {
                StatusStripUpdate("无法预览此模型");
            }
        }

        private void PreviewSprite(AssetItem assetItem, Sprite m_Sprite)
        {
            var image = m_Sprite.GetImage();
            if (image != null)
            {
                var bitmap = new DirectBitmap(image.ConvertToBytes(), image.Width, image.Height);
                image.Dispose();
                assetItem.InfoText = $"宽度: {bitmap.Width}\n高度: {bitmap.Height}\n";
                PreviewTexture(bitmap);
            }
            else
            {
                StatusStripUpdate("不支持用于预览的精灵.");
            }
        }

        private void PreviewTexture(DirectBitmap bitmap)
        {
            imgPreviewBox.Visible = true;
            imgPreviewBox.Size = new Size(768, 605);
            imgPreviewBox.Location = new Point(0, 0);
            imageTexture?.Dispose();
            imageTexture = bitmap;
            imgPreviewBox.Image = imageTexture.Bitmap;
            imgPreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
        
        private void imgPreviewBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void imgPreviewBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgPreviewBox.Left = e.X + imgPreviewBox.Left - MouseDownLocation.X;
                imgPreviewBox.Top = e.Y + imgPreviewBox.Top - MouseDownLocation.Y;
            }
        }

        private void imgPreviewBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                // Zoom in
                imgPreviewBox.Width = (int)(imgPreviewBox.Width * 1.25);
                imgPreviewBox.Height = (int)(imgPreviewBox.Height * 1.25);
            }
            else
            {
                // Zoom out
                imgPreviewBox.Width = (int)(imgPreviewBox.Width / 1.25);
                imgPreviewBox.Height = (int)(imgPreviewBox.Height / 1.25);
            }
        }
        
        private void imgPreviewBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                imgPreviewBox.Location = new Point(e.X + imgPreviewBox.Left - MouseDownLocation.X, e.Y + imgPreviewBox.Top - MouseDownLocation.Y);
            }
        }

        private void PreviewText(string text)
        {
            textPreviewBox.Text = text;
            textPreviewBox.Visible = true;
        }

        private void SetProgressBarValue(int value)
        {
            if (InvokeRequired)
            {
                
                var result = BeginInvoke(new Action(() => { progressBar1.Value = value; }));
                result.AsyncWaitHandle.WaitOne();
            }
            else
            {
                progressBar1.Value = value;
            }
        }

        private void StatusStripUpdate(string statusText)
        {
            if (InvokeRequired)
            {
                var result = BeginInvoke(() => { toolStripStatusLabel1.Text = statusText; });
                result.AsyncWaitHandle.WaitOne();
            }
            else
            {
                toolStripStatusLabel1.Text = statusText;
            }
        }

        public void ResetForm()
        {
            Text = $"Studio v{Application.ProductVersion}";
            assetsManager.Clear();
            assemblyLoader.Clear();
            exportableAssets.Clear();
            visibleAssets.Clear();
            sceneTreeView.Nodes.Clear();
            assetListView.VirtualListSize = 0;
            assetListView.Items.Clear();
            classesListView.Items.Clear();
            classesListView.Groups.Clear();
            imgPreviewBox.Image = null;
            imageTexture?.Dispose();
            imageTexture = null;
            assetInfoLabel.Visible = false;
            assetInfoLabel.Text = null;
            textPreviewBox.Visible = false;
            imgPreviewBox.Visible = false;
            fontPreviewBox.Visible = false;
            glControl.Visible = false;
            lastSelectedItem = null;
            sortColumn = -1;
            reverseSort = false;
            listSearch.Text = string.Empty;

            var count = filterTypeToolStripMenuItem.DropDownItems.Count;
            for (var i = 1; i < count; i++)
            {
                filterTypeToolStripMenuItem.DropDownItems.RemoveAt(1);
            }

            FMODreset();
            StatusStripUpdate("重置成功!!");
        }

        private void assetListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && assetListView.SelectedIndices.Count > 0)
            {
                goToSceneHierarchyToolStripMenuItem.Visible = false;
                showOriginalFileToolStripMenuItem.Visible = false;
                exportAnimatorwithselectedAnimationClipMenuItem.Visible = false;

                if (assetListView.SelectedIndices.Count == 1)
                {
                    goToSceneHierarchyToolStripMenuItem.Visible = true;
                    showOriginalFileToolStripMenuItem.Visible = true;
                }
                if (assetListView.SelectedIndices.Count >= 1)
                {
                    var selectedAssets = GetSelectedAssets();
                    if (selectedAssets.Any(x => x.Type == ClassIDType.Animator) && selectedAssets.Any(x => x.Type == ClassIDType.AnimationClip))
                    {
                        exportAnimatorwithselectedAnimationClipMenuItem.Visible = true;
                    }
                }

                tempClipboard = assetListView.HitTest(new Point(e.X, e.Y)).SubItem.Text;
                contextMenuStrip1.Show(assetListView, e.X, e.Y);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(tempClipboard);
        }

        private void exportSelectedAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Selected, ExportType.Convert);
        }

        private void showOriginalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectasset = (AssetItem)assetListView.Items[assetListView.SelectedIndices[0]];
            var args = $"/select, \"{selectasset.SourceFile.originalPath ?? selectasset.SourceFile.fullName}\"";
            var pfi = new ProcessStartInfo("explorer.exe", args);
            Process.Start(pfi);
        }

        private void exportAnimatorwithAnimationClipMenuItem_Click(object sender, EventArgs e)
        {
            AssetItem animator = null;
            List<AssetItem> animationList = new List<AssetItem>();
            var selectedAssets = GetSelectedAssets();
            foreach (var assetPreloadData in selectedAssets)
            {
                if (assetPreloadData.Type == ClassIDType.Animator)
                {
                    animator = assetPreloadData;
                }
                else if (assetPreloadData.Type == ClassIDType.AnimationClip)
                {
                    animationList.Add(assetPreloadData);
                }
            }

            if (animator != null)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    var exportPath = Path.Combine(saveFolderDialog.Folder, "Animator") + Path.DirectorySeparatorChar;
                    ExportAnimatorWithAnimationClip(animator, animationList, exportPath);
                }
            }
        }

        private void exportSelectedObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportObjects(false);
        }

        private void exportObjectswithAnimationClipMenuItem_Click(object sender, EventArgs e)
        {
            ExportObjects(true);
        }

        private void ExportObjects(bool animation)
        {
            if (sceneTreeView.Nodes.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    var exportPath = Path.Combine(saveFolderDialog.Folder, "GameObject") + Path.DirectorySeparatorChar;
                    List<AssetItem> animationList = null;
                    if (animation)
                    {
                        animationList = GetSelectedAssets().Where(x => x.Type == ClassIDType.AnimationClip).ToList();
                        if (animationList.Count == 0)
                        {
                            animationList = null;
                        }
                    }
                    ExportObjectsWithAnimationClip(exportPath, sceneTreeView.Nodes, animationList);
                }
            }
            else
            {
                StatusStripUpdate("没有可供导出的对象");
            }
        }

        private void exportSelectedObjectsmergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportMergeObjects(false);
        }

        private void exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportMergeObjects(true);
        }

        private void ExportMergeObjects(bool animation)
        {
            if (sceneTreeView.Nodes.Count > 0)
            {
                var gameObjects = new List<GameObject>();
                GetSelectedParentNode(sceneTreeView.Nodes, gameObjects);
                if (gameObjects.Count > 0)
                {
                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = gameObjects[0].m_Name + " (merge).fbx";
                    saveFileDialog.AddExtension = false;
                    saveFileDialog.Filter = "Fbx文件(*.fbx)|*.fbx";
                    saveFileDialog.InitialDirectory = saveDirectoryBackup;
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        saveDirectoryBackup = Path.GetDirectoryName(saveFileDialog.FileName);
                        var exportPath = saveFileDialog.FileName;
                        List<AssetItem> animationList = null;
                        if (animation)
                        {
                            animationList = GetSelectedAssets().Where(x => x.Type == ClassIDType.AnimationClip).ToList();
                            if (animationList.Count == 0)
                            {
                                animationList = null;
                            }
                        }
                        ExportObjectsMergeWithAnimationClip(exportPath, gameObjects, animationList);
                    }
                }
                else
                {
                    StatusStripUpdate("没有选择要导出的对象.");
                }
            }
        }

        private void exportSelectedNodessplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportNodes(false);
        }

        private void exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportNodes(true);
        }

        private void ExportNodes(bool animation)
        {
            if (sceneTreeView.Nodes.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    var exportPath = Path.Combine(saveFolderDialog.Folder, "GameObject") + Path.DirectorySeparatorChar;
                    var roots = sceneTreeView.Nodes.Cast<TreeNode>().Where(x => x.Level == 0 && x.Checked).ToList();
                    if (roots.Count == 0)
                    {
                        Logger.Info("未找到所选的根节点.");
                        return;
                    }
                    List<AssetItem> animationList = null;
                    if (animation)
                    {
                        animationList = GetSelectedAssets().Where(x => x.Type == ClassIDType.AnimationClip).ToList();
                        if (animationList.Count == 0)
                        {
                            animationList = null;
                        }
                    }
                    ExportNodesWithAnimationClip(exportPath, roots, animationList);
                }
            }
        }

        private void goToSceneHierarchyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectasset = (AssetItem)assetListView.Items[assetListView.SelectedIndices[0]];
            if (selectasset.TreeNode != null)
            {
                sceneTreeView.SelectedNode = selectasset.TreeNode;
                tabControl1.SelectedTab = tabPage1;
            }
        }

        private void exportAllAssetsMenuItem_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.All, ExportType.Convert);
        }

        private void exportSelectedAssetsMenuItem_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Selected, ExportType.Convert);
        }

        private void exportFilteredAssetsMenuItem_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Filtered, ExportType.Convert);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.All, ExportType.Raw);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Selected, ExportType.Raw);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Filtered, ExportType.Raw);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.All, ExportType.Dump);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Selected, ExportType.Dump);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Filtered, ExportType.Dump);
        }
        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.All, ExportType.JSON);
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Selected, ExportType.JSON);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            ExportAssets(ExportFilter.Filtered, ExportType.JSON);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ExportAssetsList(ExportFilter.All);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            ExportAssetsList(ExportFilter.Selected);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            ExportAssetsList(ExportFilter.Filtered);
        }

        private void exportAllObjectssplitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (sceneTreeView.Nodes.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    var savePath = saveFolderDialog.Folder + Path.DirectorySeparatorChar;
                    ExportSplitObjects(savePath, sceneTreeView.Nodes);
                }
            }
            else
            {
                StatusStripUpdate("没有可供导出的对象");
            }
        }

        private List<AssetItem> GetSelectedAssets()
        {
            var selectedAssets = new List<AssetItem>(assetListView.SelectedIndices.Count);
            foreach (int index in assetListView.SelectedIndices)
            {
                selectedAssets.Add((AssetItem)assetListView.Items[index]);
            }

            return selectedAssets;
        }

        private void FilterAssetList()
        {
            assetListView.BeginUpdate();
            assetListView.SelectedIndices.Clear();
            var show = new List<ClassIDType>();
            if (!allToolStripMenuItem.Checked)
            {
                for (var i = 1; i < filterTypeToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (ToolStripMenuItem)filterTypeToolStripMenuItem.DropDownItems[i];
                    if (item.Checked)
                    {
                        show.Add((ClassIDType)Enum.Parse(typeof(ClassIDType), item.Text));
                    }
                }
                visibleAssets = exportableAssets.FindAll(x => show.Contains(x.Type));
            }
            else
            {
                visibleAssets = exportableAssets;
            }
            if (Properties.Settings.Default.modelsOnly)
            {
                var models = visibleAssets.FindAll(x => x.Type == ClassIDType.Animator || x.Type == ClassIDType.GameObject);
                foreach (var model in models)
                {
                    var hasModel = model.Asset switch
                    {
                        GameObject m_GameObject => m_GameObject.HasModel(),
                        Animator m_Animator => m_Animator.m_GameObject.TryGet(out var gameObject) && gameObject.HasModel(),
                        _ => throw new NotImplementedException()
                    };
                    if (!hasModel)
                    {
                        visibleAssets.Remove(model);
                    }
                }
            }
            if (!string.IsNullOrEmpty(listSearch.Text))
            {
                try
                {
                    Regex.Match("", listSearch.Text, RegexOptions.IgnoreCase);
                }
                catch (Exception ex)
                {
                    Logger.Error("无效的正则表达式.\n" + ex.Message);
                    listSearch.Text = "";
                }
                var regex = new Regex(listSearch.Text, RegexOptions.IgnoreCase);
                visibleAssets = visibleAssets.FindAll(
                    x => regex.IsMatch(x.Text) ||
                    regex.IsMatch(x.SubItems[1].Text) ||
                    regex.IsMatch(x.SubItems[3].Text));
            }
            assetListView.VirtualListSize = visibleAssets.Count;
            assetListView.EndUpdate();
        }

        private async void ExportAssets(ExportFilter type, ExportType exportType)
        {
            if (exportableAssets.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    timer.Stop();
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    List<AssetItem> toExportAssets = null;
                    switch (type)
                    {
                        case ExportFilter.All:
                            toExportAssets = exportableAssets;
                            break;
                        case ExportFilter.Selected:
                            toExportAssets = GetSelectedAssets();
                            break;
                        case ExportFilter.Filtered:
                            toExportAssets = visibleAssets;
                            break;
                    }
                    await Studio.ExportAssets(saveFolderDialog.Folder, toExportAssets, exportType, Properties.Settings.Default.openAfterExport);
                }
            }
            else
            {
                StatusStripUpdate("未加载可导出的资源");
            }
        }

        private void ExportAssetsList(ExportFilter type)
        {
            // XXX: Only exporting as XML for now, but would JSON(/CSV/other) be useful too?

            if (exportableAssets.Count > 0)
            {
                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    timer.Stop();
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    List<AssetItem> toExportAssets = null;
                    switch (type)
                    {
                        case ExportFilter.All:
                            toExportAssets = exportableAssets;
                            break;
                        case ExportFilter.Selected:
                            toExportAssets = GetSelectedAssets();
                            break;
                        case ExportFilter.Filtered:
                            toExportAssets = visibleAssets;
                            break;
                    }
                    Studio.ExportAssetsList(saveFolderDialog.Folder, toExportAssets, ExportListType.XML);
                }
            }
            else
            {
                StatusStripUpdate("未加载可导出的资源");
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            logger.ShowErrorMessage = toolStripMenuItem15.Checked;
        }
        private async void toolStripMenuItem19_DropDownOpening(object sender, EventArgs e)
        {
            if (specifyAIVersion.Enabled && await AIVersionManager.FetchVersions())
            {
                UpdateVersionList();
            }
        }

        private void miscToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (miscToolStripMenuItem.Enabled)
            {
                MapNameComboBox.Items.Clear();
                MapNameComboBox.Items.AddRange(AssetsHelper.GetMaps());
            }
        }

        private async void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (specifyAIVersion.SelectedIndex == 0)
            {
                return;
            }
            if (skipContainer.Checked)
            {
                Logger.Info("跳过容器已启用，正在中止...");
                return;
            }
            optionsToolStripMenuItem.DropDown.Visible = false;
            var version = specifyAIVersion.SelectedItem.ToString();

            if (version.Contains(' '))
            {
                version = version.Split(' ')[0];
            }

            Logger.Info($"加载资源索引v{version}");
            InvokeUpdate(specifyAIVersion, false);
            var path = await AIVersionManager.FetchAI(version);
            await Task.Run(() => ResourceIndex.FromFile(path));
            UpdateContainers();
            UpdateVersionList();
            InvokeUpdate(specifyAIVersion, true);
        }

        private void UpdateVersionList()
        {
            var selectedIndex = specifyAIVersion.SelectedIndex;
            specifyAIVersion.Items.Clear();
            specifyAIVersion.Items.Add("None");

            var versions = AIVersionManager.GetVersions();
            foreach (var version in versions)
            {
                specifyAIVersion.Items.Add(version.Item1 + (version.Item2 ? " (cached)" : ""));
            }

            specifyAIVersion.SelectedIndexChanged -= new EventHandler(toolStripComboBox1_SelectedIndexChanged);
            specifyAIVersion.SelectedIndex = selectedIndex;
            specifyAIVersion.SelectedIndexChanged += new EventHandler(toolStripComboBox1_SelectedIndexChanged);
        }

        private void UpdateContainers()
        {
            if (exportableAssets.Count > 0)
            {
                Logger.Info("正在更新容器...");
                assetListView.BeginUpdate();
                foreach (var asset in exportableAssets)
                {
                    if (int.TryParse(asset.Container, out var value))
                    {
                        var last = unchecked((uint)value);
                        var name = Path.GetFileNameWithoutExtension(asset.SourceFile.originalPath);
                        if (uint.TryParse(name, out var id))
                        {
                            var path = ResourceIndex.GetContainer(id, last);
                            if (!string.IsNullOrEmpty(path))
                            {
                                asset.Container = path;
                                asset.SubItems[1].Text = path;
                                if (asset.Type == ClassIDType.MiHoYoBinData)
                                {
                                    asset.Text = Path.GetFileNameWithoutExtension(path);
                                }
                            }
                        }
                    }
                }
                assetListView.EndUpdate();
                Logger.Info("已更新!!");
            }
        }

        private void InvokeUpdate(ToolStripItem item, bool value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => { item.Enabled = value; }));
            }
            else
            {
                item.Enabled = value;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 1 && lastSelectedItem != null)
            {
                dumpTextBox.Text = DumpAsset(lastSelectedItem.Asset);
            }
        }
        private void enableResolveDependencies_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.enableResolveDependencies = enableResolveDependencies.Checked;
            Properties.Settings.Default.Save();

            assetsManager.ResolveDependencies = enableResolveDependencies.Checked;
        }
        private void allowDuplicates_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.allowDuplicates = allowDuplicates.Checked;
            Properties.Settings.Default.Save();
        }
        private void skipContainer_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.skipContainer = skipContainer.Checked;
            Properties.Settings.Default.Save();

            SkipContainer = skipContainer.Checked;
        }
        private void assetMapTypeMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var assetMapType = Properties.Settings.Default.assetMapType;
            if (e.ClickedItem is ToolStripMenuItem item)
            {
                if (item.Checked)
                {
                    assetMapType -= (int)item.Tag;
                }
                else
                {
                    assetMapType += (int)item.Tag;
                }

                Properties.Settings.Default.assetMapType = assetMapType;
                Properties.Settings.Default.Save();
            }

        }
        private void modelsOnly_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.modelsOnly = modelsOnly.Checked;
            Properties.Settings.Default.Save();

            if (visibleAssets.Count > 0)
            {
                FilterAssetList();
            }
        }
        private void enableModelPreview_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.enableModelPreview = enableModelPreview.Checked;
            Properties.Settings.Default.Save();
        }

        private void specifyGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            optionsToolStripMenuItem.DropDown.Visible = false;
            Properties.Settings.Default.selectedGame = specifyGame.SelectedIndex;
            Properties.Settings.Default.Save();

            ResetForm();

            Studio.Game = GameManager.GetGame(Properties.Settings.Default.selectedGame);
            Logger.Info($"目标游戏是{Studio.Game.Name}");

            if (Studio.Game.Type.IsUnityCN())
            {
                UnityCNManager.SetKey(Properties.Settings.Default.selectedUnityCNKey);
            }

            assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
            assetsManager.Game = Studio.Game;
        }

        private async void specifyNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;
            InvokeUpdate(miscToolStripMenuItem, false);

            ResetForm();

            var name = MapNameComboBox.SelectedItem.ToString();
            await Task.Run(() =>
            {
                if (AssetsHelper.LoadCABMapInternal(name))
                {
                    Properties.Settings.Default.selectedCABMapName = name;
                    Properties.Settings.Default.Save();
                }
            });

            assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
            assetsManager.Game = Studio.Game;

            InvokeUpdate(miscToolStripMenuItem, true);
        }

        private async void buildMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;
            InvokeUpdate(miscToolStripMenuItem, false);

            var input = MapNameComboBox.Text;
            var selectedText = MapNameComboBox.SelectedText;
            var name = "";

            if (!string.IsNullOrEmpty(selectedText))
            {
                name = selectedText;
            }
            else if (!string.IsNullOrEmpty(input))
            {
                if (input.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    Logger.Warning("名称包含无效字符!!");
                    InvokeUpdate(miscToolStripMenuItem, true);
                    return;
                }

                name = input;
            }
            else
            {
                Logger.Error("映射名称为空，请在上方组合框中输入任意名称");
                InvokeUpdate(miscToolStripMenuItem, true);
                return;
            }

            if (File.Exists(Path.Combine(AssetsHelper.MapName, $"{name}.bin")))
            {
                var acceptOverride = MessageBox.Show("映射已存在，你想覆盖它吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (acceptOverride != DialogResult.Yes)
                {
                    InvokeUpdate(miscToolStripMenuItem, true);
                    return;
                }
            }

            var version = specifyUnityVersion.Text;
            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "选择游戏文件夹";
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                Logger.Info("正在扫描文件...");
                var files = Directory.GetFiles(openFolderDialog.Folder, "*.*", SearchOption.AllDirectories).ToArray();
                Logger.Info($"找到{files.Length}个文件");
                AssetsHelper.SetUnityVersion(version);
                await Task.Run(() => AssetsHelper.BuildCABMap(files, name, openFolderDialog.Folder, Studio.Game));
            }
            InvokeUpdate(miscToolStripMenuItem, true);
        }

        private async void buildBothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;
            InvokeUpdate(miscToolStripMenuItem, false);

            var input = MapNameComboBox.Text;
            var selectedText = MapNameComboBox.SelectedText;
            var exportListType = (ExportListType)assetMapTypeMenuItem.DropDownItems.Cast<ToolStripMenuItem>().Select(x => x.Checked ? (int)x.Tag : 0).Sum();
            var name = "";

            if (!string.IsNullOrEmpty(selectedText))
            {
                name = selectedText;
            }
            else if (!string.IsNullOrEmpty(input))
            {
                if (input.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    Logger.Warning("名称包含无效字符!!");
                    InvokeUpdate(miscToolStripMenuItem, true);
                    return;
                }

                name = input;
            }
            else
            {
                Logger.Error("映射名称为空，请在上方组合框中输入任意名称");
                InvokeUpdate(miscToolStripMenuItem, true);
                return;
            }

            if (File.Exists(Path.Combine(AssetsHelper.MapName, $"{name}.bin")))
            {
                var acceptOverride = MessageBox.Show("映射已存在，你想覆盖它吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (acceptOverride != DialogResult.Yes)
                {
                    InvokeUpdate(miscToolStripMenuItem, true);
                    return;
                }
            }

            var version = specifyUnityVersion.Text;
            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "选择游戏文件夹";
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                Logger.Info("正在扫描文件...");
                var files = Directory.GetFiles(openFolderDialog.Folder, "*.*", SearchOption.AllDirectories).ToArray();
                Logger.Info($"找到{files.Length}个文件");

                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                saveFolderDialog.Title = "选择输出文件夹";
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    saveDirectoryBackup = saveFolderDialog.Folder;
                    AssetsHelper.SetUnityVersion(version);
                    await Task.Run(() => AssetsHelper.BuildBoth(files, name, openFolderDialog.Folder, Studio.Game, saveFolderDialog.Folder, exportListType));
                }
            }
            InvokeUpdate(miscToolStripMenuItem, true);
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;
            InvokeUpdate(miscToolStripMenuItem, false);

            var acceptDelete = MessageBox.Show("映射将被删除，此操作无法撤销，是否继续?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (acceptDelete != DialogResult.Yes)
            {
                InvokeUpdate(miscToolStripMenuItem, true);
                return;
            }

            var name = MapNameComboBox.Text.ToString();
            var path = Path.Combine(AssetsHelper.MapName, $"{name}.bin");
            if (File.Exists(path))
            {
                File.Delete(path);
                Logger.Info($"{name}删除成功!!");
                MapNameComboBox.SelectedIndexChanged -= new EventHandler(specifyNameComboBox_SelectedIndexChanged);
                MapNameComboBox.SelectedIndex = 0;
                MapNameComboBox.SelectedIndexChanged += new EventHandler(specifyNameComboBox_SelectedIndexChanged);
            }

            InvokeUpdate(miscToolStripMenuItem, true);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetForm();
            AssetsHelper.Clear();
            assetBrowser?.Clear();
            assetsManager.SpecifyUnityVersion = specifyUnityVersion.Text;
            assetsManager.Game = Studio.Game;
        }

        private void enableConsole_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.enableConsole = enableConsole.Checked;
            Properties.Settings.Default.Save();

            var handle = ConsoleHelper.GetConsoleWindow();
            if (enableConsole.Checked)
            {
                Logger.Default = new ConsoleLogger();
                ConsoleHelper.ShowWindow(handle, ConsoleHelper.SW_SHOW);
            }
            else
            {
                Logger.Default = logger;
                ConsoleHelper.ShowWindow(handle, ConsoleHelper.SW_HIDE);
            }
        }

        private void enableFileLogging_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.enableFileLogging = enableFileLogging.Checked;
            Properties.Settings.Default.Save();

            Logger.FileLogging = enableFileLogging.Checked;
        }

        private void openLogFile(object sender, EventArgs e)
        {
            if (Logger.FileLogging && Logger.File is FileLogger fileLogger)
            {
                if (File.Exists(fileLogger.logPath))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = fileLogger.logPath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show("未找到日志文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("文件日志记录未启用", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loggedEventsMenuItem_DropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        private void loggedEventsMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.loggerEventType = loggedEventsMenuItem.DropDownItems.Cast<ToolStripMenuItem>().Select(x => x.Checked ? (int)x.Tag : 0).Sum();
            Properties.Settings.Default.Save();

            Logger.Flags = (LoggerEvent)Properties.Settings.Default.loggerEventType;
        }

        private void abortStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Info("正在中止....");
            assetsManager.tokenSource.Cancel();
            AssetsHelper.tokenSource.Cancel();
        }

        private async void loadAIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (skipContainer.Checked)
            {
                Logger.Info("跳过容器已启用，正在中止...");
                return;
            }
            miscToolStripMenuItem.DropDown.Visible = false;

            var openFileDialog = new OpenFileDialog() { Multiselect = false, Filter = "资源索引JSON文件|*.json" };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var path = openFileDialog.FileName;
                Logger.Info($"加载资源索引...");
                InvokeUpdate(loadAIToolStripMenuItem, false);
                await Task.Run(() => ResourceIndex.FromFile(path));
                UpdateContainers();
                InvokeUpdate(loadAIToolStripMenuItem, true);
            }
        }

        private async void loadCABMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;

            var openFileDialog = new OpenFileDialog() { Multiselect = false, Filter = "CAB映射文件|*.bin" };
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var path = openFileDialog.FileName;
                InvokeUpdate(loadCABMapToolStripMenuItem, false);
                await Task.Run(() => AssetsHelper.LoadCABMap(path));
                InvokeUpdate(loadCABMapToolStripMenuItem, true);
            }
        }

        private void clearConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Clear();
        }

        private async void buildAssetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miscToolStripMenuItem.DropDown.Visible = false;
            InvokeUpdate(miscToolStripMenuItem, false);

            var input = assetMapNameTextBox.Text;
            var exportListType = (ExportListType)assetMapTypeMenuItem.DropDownItems.Cast<ToolStripMenuItem>().Select(x => x.Checked ? (int)x.Tag : 0).Sum();
            var name = "assets_map";

            if (!string.IsNullOrEmpty(input))
            {
                if (input.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    Logger.Warning("名称包含无效字符!!");
                    InvokeUpdate(miscToolStripMenuItem, true);
                    return;
                }

                name = input;
            }

            var version = specifyUnityVersion.Text;
            var openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = $"Select Game Folder";
            if (openFolderDialog.ShowDialog(this) == DialogResult.OK)
            {
                Logger.Info("正在扫描文件...");
                var files = Directory.GetFiles(openFolderDialog.Folder, "*.*", SearchOption.AllDirectories).ToArray();
                Logger.Info($"找到 {files.Length}个文件");

                var saveFolderDialog = new OpenFolderDialog();
                saveFolderDialog.InitialFolder = saveDirectoryBackup;
                saveFolderDialog.Title = "选择输出文件夹";
                if (saveFolderDialog.ShowDialog(this) == DialogResult.OK)
                {
                    AssetsHelper.SetUnityVersion(version);
                    await Task.Run(() => AssetsHelper.BuildAssetMap(files, name, Studio.Game, saveFolderDialog.Folder, exportListType));
                }
            }
            InvokeUpdate(miscToolStripMenuItem, true);
        }

        private void loadAssetMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            assetBrowser = new AssetBrowser(this);
            assetBrowser.Show();
        }

        private void specifyUnityCNKey_Click(object sender, EventArgs e)
        {
            var unitycn = new UnityCNForm();
            unitycn.Show();
        }

        #region FMOD
        private void FMODinit()
        {
            FMODreset();

            var result = FMOD.Factory.System_Create(out system);
            if (ERRCHECK(result)) { return; }

            result = system.getVersion(out var version);
            ERRCHECK(result);
            if (version < FMOD.VERSION.number)
            {
                Logger.Error($"错误!你正在使用旧版本的FMOD{version:X}.这个程序需要{FMOD.VERSION.number:X}.");
                Application.Exit();
            }

            result = system.init(2, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
            if (ERRCHECK(result)) { return; }

            result = system.getMasterSoundGroup(out masterSoundGroup);
            if (ERRCHECK(result)) { return; }

            result = masterSoundGroup.setVolume(FMODVolume);
            if (ERRCHECK(result)) { return; }
        }

        private void FMODreset()
        {
            timer.Stop();
            FMODprogressBar.Value = 0;
            FMODtimerLabel.Text = "0:00.0 / 0:00.0";
            FMODstatusLabel.Text = "已停止";
            FMODinfoLabel.Text = "";

            if (sound != null && sound.isValid())
            {
                var result = sound.release();
                ERRCHECK(result);
                sound = null;
            }
        }

        private void FMODplayButton_Click(object sender, EventArgs e)
        {
            if (sound != null && channel != null)
            {
                timer.Start();
                var result = channel.isPlaying(out var playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                if (playing)
                {
                    result = channel.stop();
                    if (ERRCHECK(result)) { return; }

                    result = system.playSound(sound, null, false, out channel);
                    if (ERRCHECK(result)) { return; }

                    FMODpauseButton.Text = "暂停";
                }
                else
                {
                    result = system.playSound(sound, null, false, out channel);
                    if (ERRCHECK(result)) { return; }
                    FMODstatusLabel.Text = "播放";

                    if (FMODprogressBar.Value > 0)
                    {
                        uint newms = FMODlenms / 1000 * (uint)FMODprogressBar.Value;

                        result = channel.setPosition(newms, FMOD.TIMEUNIT.MS);
                        if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                        {
                            if (ERRCHECK(result)) { return; }
                        }

                    }
                }
            }
        }

        private void FMODpauseButton_Click(object sender, EventArgs e)
        {
            if (sound != null && channel != null)
            {
                var result = channel.isPlaying(out var playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                if (playing)
                {
                    result = channel.getPaused(out var paused);
                    if (ERRCHECK(result)) { return; }
                    result = channel.setPaused(!paused);
                    if (ERRCHECK(result)) { return; }

                    if (paused)
                    {
                        FMODstatusLabel.Text = "播放";
                        FMODpauseButton.Text = "暂停";
                        timer.Start();
                    }
                    else
                    {
                        FMODstatusLabel.Text = "暂停";
                        FMODpauseButton.Text = "恢复";
                        timer.Stop();
                    }
                }
            }
        }

        private void FMODstopButton_Click(object sender, EventArgs e)
        {
            if (channel != null)
            {
                var result = channel.isPlaying(out var playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                if (playing)
                {
                    result = channel.stop();
                    if (ERRCHECK(result)) { return; }
                    //channel = null;
                    //don't FMODreset, it will nullify the sound
                    timer.Stop();
                    FMODprogressBar.Value = 0;
                    FMODtimerLabel.Text = "0:00.0 / 0:00.0";
                    FMODstatusLabel.Text = "已停止";
                    FMODpauseButton.Text = "暂停";
                }
            }
        }

        private void FMODloopButton_CheckedChanged(object sender, EventArgs e)
        {
            FMOD.RESULT result;

            loopMode = FMODloopButton.Checked ? FMOD.MODE.LOOP_NORMAL : FMOD.MODE.LOOP_OFF;

            if (sound != null)
            {
                result = sound.setMode(loopMode);
                if (ERRCHECK(result)) { return; }
            }

            if (channel != null)
            {
                result = channel.isPlaying(out var playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                result = channel.getPaused(out var paused);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                if (playing || paused)
                {
                    result = channel.setMode(loopMode);
                    if (ERRCHECK(result)) { return; }
                }
            }
        }

        private void FMODvolumeBar_ValueChanged(object sender, EventArgs e)
        {
            FMODVolume = Convert.ToSingle(FMODvolumeBar.Value) / 10;

            var result = masterSoundGroup.setVolume(FMODVolume);
            if (ERRCHECK(result)) { return; }
        }

        private void FMODprogressBar_Scroll(object sender, EventArgs e)
        {
            if (channel != null)
            {
                uint newms = FMODlenms / 1000 * (uint)FMODprogressBar.Value;
                FMODtimerLabel.Text = $"{newms / 1000 / 60}:{newms / 1000 % 60}.{newms / 10 % 100}/{FMODlenms / 1000 / 60}:{FMODlenms / 1000 % 60}.{FMODlenms / 10 % 100}";
            }
        }

        private void FMODprogressBar_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Stop();
        }

        private void FMODprogressBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (channel != null)
            {
                uint newms = FMODlenms / 1000 * (uint)FMODprogressBar.Value;

                var result = channel.setPosition(newms, FMOD.TIMEUNIT.MS);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }


                result = channel.isPlaying(out var playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    if (ERRCHECK(result)) { return; }
                }

                if (playing) { timer.Start(); }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            uint ms = 0;
            bool playing = false;
            bool paused = false;

            if (channel != null)
            {
                var result = channel.getPosition(out ms, FMOD.TIMEUNIT.MS);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    ERRCHECK(result);
                }

                result = channel.isPlaying(out playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    ERRCHECK(result);
                }

                result = channel.getPaused(out paused);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE))
                {
                    ERRCHECK(result);
                }
            }

            FMODtimerLabel.Text = $"{ms / 1000 / 60}:{ms / 1000 % 60}.{ms / 10 % 100} / {FMODlenms / 1000 / 60}:{FMODlenms / 1000 % 60}.{FMODlenms / 10 % 100}";
            FMODprogressBar.Value = (int)(ms * 1000 / FMODlenms);
            FMODstatusLabel.Text = paused ? "已停止" : playing ? "播放" : "已停止";

            if (system != null && channel != null)
            {
                system.update();
            }
        }

        private bool ERRCHECK(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                FMODreset();
                StatusStripUpdate($"FMOD错误! {result} - {FMOD.Error.String(result)}");
                return true;
            }
            return false;
        }
        #endregion

        #region GLControl
        private void InitOpenTK()
        {
            ChangeGLSize(glControl.Size);
            GL.ClearColor(System.Drawing.Color.CadetBlue);
            pgmID = GL.CreateProgram();
            LoadShader("vs", ShaderType.VertexShader, pgmID, out int vsID);
            LoadShader("fs", ShaderType.FragmentShader, pgmID, out int fsID);
            GL.LinkProgram(pgmID);

            pgmColorID = GL.CreateProgram();
            LoadShader("vs", ShaderType.VertexShader, pgmColorID, out vsID);
            LoadShader("fsColor", ShaderType.FragmentShader, pgmColorID, out fsID);
            GL.LinkProgram(pgmColorID);

            pgmBlackID = GL.CreateProgram();
            LoadShader("vs", ShaderType.VertexShader, pgmBlackID, out vsID);
            LoadShader("fsBlack", ShaderType.FragmentShader, pgmBlackID, out fsID);
            GL.LinkProgram(pgmBlackID);

            attributeVertexPosition = GL.GetAttribLocation(pgmID, "vertexPosition");
            attributeNormalDirection = GL.GetAttribLocation(pgmID, "normalDirection");
            attributeVertexColor = GL.GetAttribLocation(pgmColorID, "vertexColor");
            uniformModelMatrix = GL.GetUniformLocation(pgmID, "modelMatrix");
            uniformViewMatrix = GL.GetUniformLocation(pgmID, "viewMatrix");
            uniformProjMatrix = GL.GetUniformLocation(pgmID, "projMatrix");
        }

        private static void LoadShader(string filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            var str = (string)Properties.Resources.ResourceManager.GetObject(filename);
            GL.ShaderSource(address, str);
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            GL.DeleteShader(address);
        }

        private static void CreateVBO(out int vboAddress, OpenTK.Mathematics.Vector3[] data, int address)
        {
            GL.GenBuffers(1, out vboAddress);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboAddress);
            GL.BufferData(BufferTarget.ArrayBuffer,
                                    (IntPtr)(data.Length * OpenTK.Mathematics.Vector3.SizeInBytes),
                                    data,
                                    BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(address, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(address);
        }

        private static void CreateVBO(out int vboAddress, OpenTK.Mathematics.Vector4[] data, int address)
        {
            GL.GenBuffers(1, out vboAddress);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboAddress);
            GL.BufferData(BufferTarget.ArrayBuffer,
                                    (IntPtr)(data.Length * OpenTK.Mathematics.Vector4.SizeInBytes),
                                    data,
                                    BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(address, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(address);
        }

        private static void CreateVBO(out int vboAddress, Matrix4 data, int address)
        {
            GL.GenBuffers(1, out vboAddress);
            GL.UniformMatrix4(address, false, ref data);
        }

        private static void CreateEBO(out int address, int[] data)
        {
            GL.GenBuffers(1, out address);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, address);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                            (IntPtr)(data.Length * sizeof(int)),
                            data,
                            BufferUsageHint.StaticDraw);
        }

        private void CreateVAO()
        {
            GL.DeleteVertexArray(vao);
            GL.GenVertexArrays(1, out vao);
            GL.BindVertexArray(vao);
            CreateVBO(out var vboPositions, vertexData, attributeVertexPosition);
            if (normalMode == 0)
            {
                CreateVBO(out var vboNormals, normal2Data, attributeNormalDirection);
            }
            else
            {
                if (normalData != null)
                    CreateVBO(out var vboNormals, normalData, attributeNormalDirection);
            }
            CreateVBO(out var vboColors, colorData, attributeVertexColor);
            CreateVBO(out var vboModelMatrix, modelMatrixData, uniformModelMatrix);
            CreateVBO(out var vboViewMatrix, viewMatrixData, uniformViewMatrix);
            CreateVBO(out var vboProjMatrix, projMatrixData, uniformProjMatrix);
            CreateEBO(out var eboElements, indiceData);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        private void ChangeGLSize(Size size)
        {
            GL.Viewport(0, 0, size.Width, size.Height);

            if (size.Width <= size.Height)
            {
                float k = 1.0f * size.Width / size.Height;
                projMatrixData = Matrix4.CreateScale(1, k, 1);
            }
            else
            {
                float k = 1.0f * size.Height / size.Width;
                projMatrixData = Matrix4.CreateScale(k, 1, 1);
            }
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            InitOpenTK();
            glControlLoaded = true;
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            glControl.MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.BindVertexArray(vao);
            if (wireFrameMode == 0 || wireFrameMode == 2)
            {
                GL.UseProgram(shadeMode == 0 ? pgmID : pgmColorID);
                GL.UniformMatrix4(uniformModelMatrix, false, ref modelMatrixData);
                GL.UniformMatrix4(uniformViewMatrix, false, ref viewMatrixData);
                GL.UniformMatrix4(uniformProjMatrix, false, ref projMatrixData);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                GL.DrawElements(PrimitiveType.Triangles, indiceData.Length, DrawElementsType.UnsignedInt, 0);
            }
            //Wireframe
            if (wireFrameMode == 1 || wireFrameMode == 2)
            {
                GL.Enable(EnableCap.PolygonOffsetLine);
                GL.PolygonOffset(-1, -1);
                GL.UseProgram(pgmBlackID);
                GL.UniformMatrix4(uniformModelMatrix, false, ref modelMatrixData);
                GL.UniformMatrix4(uniformViewMatrix, false, ref viewMatrixData);
                GL.UniformMatrix4(uniformProjMatrix, false, ref projMatrixData);
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                GL.DrawElements(PrimitiveType.Triangles, indiceData.Length, DrawElementsType.UnsignedInt, 0);
                GL.Disable(EnableCap.PolygonOffsetLine);
            }
            GL.BindVertexArray(0);
            GL.Flush();
            glControl.SwapBuffers();
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (glControl.Visible)
            {
                viewMatrixData *= Matrix4.CreateScale(1 + e.Delta / 1000f);
                glControl.Invalidate();
            }
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            mdx = e.X;
            mdy = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                lmdown = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                rmdown = true;
            }
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (lmdown || rmdown)
            {
                float dx = mdx - e.X;
                float dy = mdy - e.Y;
                mdx = e.X;
                mdy = e.Y;
                if (lmdown)
                {
                    dx *= 0.01f;
                    dy *= 0.01f;
                    viewMatrixData *= Matrix4.CreateRotationX(dy);
                    viewMatrixData *= Matrix4.CreateRotationY(dx);
                }
                if (rmdown)
                {
                    dx *= 0.003f;
                    dy *= 0.003f;
                    viewMatrixData *= Matrix4.CreateTranslation(-dx, dy, 0);
                }
                glControl.Invalidate();
            }
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lmdown = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                rmdown = false;
            }
        }
        #endregion
    }
}
