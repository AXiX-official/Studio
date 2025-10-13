using System;
using System.Windows.Forms;

namespace AssetStudio.GUI
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadFileToolStripMenuItem = new ToolStripMenuItem();
            loadFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            extractFileToolStripMenuItem = new ToolStripMenuItem();
            extractFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            resetToolStripMenuItem = new ToolStripMenuItem();
            abortStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            displayAll = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            enablePreview = new ToolStripMenuItem();
            enableModelPreview = new ToolStripMenuItem();
            modelsOnly = new ToolStripMenuItem();
            toolStripSeparator11 = new ToolStripSeparator();
            displayInfo = new ToolStripMenuItem();
            enableResolveDependencies = new ToolStripMenuItem();
            allowDuplicates = new ToolStripMenuItem();
            skipContainer = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            toolStripMenuItem14 = new ToolStripMenuItem();
            specifyUnityVersion = new ToolStripTextBox();
            specifyUnityCNKey = new ToolStripMenuItem();
            toolStripSeparator13 = new ToolStripSeparator();
            toolStripMenuItem18 = new ToolStripMenuItem();
            specifyGame = new ToolStripComboBox();
            AutoDetectMultipleBundle = new ToolStripMenuItem();
            toolStripMenuItem19 = new ToolStripMenuItem();
            specifyAIVersion = new ToolStripComboBox();
            showExpOpt = new ToolStripMenuItem();
            modelToolStripMenuItem = new ToolStripMenuItem();
            exportAllObjectssplitToolStripMenuItem1 = new ToolStripMenuItem();
            exportSelectedObjectsToolStripMenuItem = new ToolStripMenuItem();
            exportSelectedObjectsWithAnimationClipToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exportSelectedObjectsmergeToolStripMenuItem = new ToolStripMenuItem();
            exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            exportSelectedNodessplitToolStripMenuItem = new ToolStripMenuItem();
            exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            exportAllAssetsMenuItem = new ToolStripMenuItem();
            exportSelectedAssetsMenuItem = new ToolStripMenuItem();
            exportFilteredAssetsMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            exportAnimatorWithSelectedAnimationClipToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem16 = new ToolStripMenuItem();
            toolStripMenuItem17 = new ToolStripMenuItem();
            toolStripMenuItem24 = new ToolStripMenuItem();
            toolStripMenuItem25 = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem12 = new ToolStripMenuItem();
            toolStripMenuItem13 = new ToolStripMenuItem();
            sceneHierarchy = new ToolStripMenuItem();
            filterTypeToolStripMenuItem = new ToolStripMenuItem();
            allToolStripMenuItem = new ToolStripMenuItem();
            debugMenuItem = new ToolStripMenuItem();
            toolStripMenuItem15 = new ToolStripMenuItem();
            exportClassStructuresMenuItem = new ToolStripMenuItem();
            enableConsole = new ToolStripMenuItem();
            clearConsoleToolStripMenuItem = new ToolStripMenuItem();
            enableFileLogging = new ToolStripMenuItem();
            loggedEventsMenuItem = new ToolStripMenuItem();
            openLogFileToolStripMenuItem = new ToolStripMenuItem();
            miscToolStripMenuItem = new ToolStripMenuItem();
            MapNameComboBox = new ToolStripComboBox();
            buildMapToolStripMenuItem = new ToolStripMenuItem();
            buildBothToolStripMenuItem = new ToolStripMenuItem();
            clearMapToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            assetMapNameTextBox = new ToolStripTextBox();
            buildAssetMapToolStripMenuItem = new ToolStripMenuItem();
            assetMapTypeMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            loadAIToolStripMenuItem = new ToolStripMenuItem();
            loadCABMapToolStripMenuItem = new ToolStripMenuItem();
            assetBrowserToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem20 = new ToolStripMenuItem();
            assetHelpersToolStripMenuItem = new ToolStripMenuItem();
            MapToolStripMenuItem = new ToolStripMenuItem();
            assetMapToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            splitContainer1 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            sceneTreeView = new GOHierarchy();
            treeSearch = new TextBox();
            tabPage2 = new TabPage();
            assetListView = new ListView();
            columnHeaderName = new ColumnHeader();
            columnHeaderContainer = new ColumnHeader();
            columnHeaderType = new ColumnHeader();
            columnHeaderPathID = new ColumnHeader();
            columnHeaderSize = new ColumnHeader();
            listSearch = new TextBox();
            tabPage3 = new TabPage();
            classesListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            progressbarPanel = new Panel();
            progressBar1 = new ProgressBar();
            tabControl2 = new TabControl();
            tabPage4 = new TabPage();
            previewPanel = new Panel();
            assetInfoLabel = new Label();
            FMODpanel = new Panel();
            FMODcopyright = new Label();
            FMODinfoLabel = new Label();
            FMODtimerLabel = new Label();
            FMODstatusLabel = new Label();
            FMODprogressBar = new TrackBar();
            FMODvolumeBar = new TrackBar();
            FMODloopButton = new CheckBox();
            FMODstopButton = new Button();
            FMODpauseButton = new Button();
            FMODplayButton = new Button();
            fontPreviewBox = new RichTextBox();
            glControl = new OpenTK.WinForms.GLControl();
            textPreviewBox = new TextBox();
            classTextBox = new TextBox();
            imgPreviewBox = new PictureBox();
            tabPage5 = new TabPage();
            dumpTextBox = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            timer = new Timer(components);
            openFileDialog1 = new OpenFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            copyToolStripMenuItem = new ToolStripMenuItem();
            exportSelectedAssetsToolStripMenuItem = new ToolStripMenuItem();
            exportAnimatorwithselectedAnimationClipMenuItem = new ToolStripMenuItem();
            goToSceneHierarchyToolStripMenuItem = new ToolStripMenuItem();
            showOriginalFileToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            progressbarPanel.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage4.SuspendLayout();
            previewPanel.SuspendLayout();
            FMODpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)FMODprogressBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FMODvolumeBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgPreviewBox).BeginInit();
            tabPage5.SuspendLayout();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, modelToolStripMenuItem, exportToolStripMenuItem, filterTypeToolStripMenuItem, debugMenuItem, miscToolStripMenuItem, toolStripMenuItem20 });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1264, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadFileToolStripMenuItem, loadFolderToolStripMenuItem, toolStripMenuItem1, extractFileToolStripMenuItem, extractFolderToolStripMenuItem, toolStripSeparator6, resetToolStripMenuItem, abortStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            fileToolStripMenuItem.Text = "文件";
            // 
            // loadFileToolStripMenuItem
            // 
            loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            loadFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            loadFileToolStripMenuItem.Text = "加载文件";
            loadFileToolStripMenuItem.Click += loadFile_Click;
            // 
            // loadFolderToolStripMenuItem
            // 
            loadFolderToolStripMenuItem.Name = "loadFolderToolStripMenuItem";
            loadFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            loadFolderToolStripMenuItem.Text = "加载文件夹";
            loadFolderToolStripMenuItem.Click += loadFolder_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // extractFileToolStripMenuItem
            // 
            extractFileToolStripMenuItem.Name = "extractFileToolStripMenuItem";
            extractFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            extractFileToolStripMenuItem.Text = "提取文件";
            extractFileToolStripMenuItem.Click += extractFileToolStripMenuItem_Click;
            // 
            // extractFolderToolStripMenuItem
            // 
            extractFolderToolStripMenuItem.Name = "extractFolderToolStripMenuItem";
            extractFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            extractFolderToolStripMenuItem.Text = "提取文件夹";
            extractFolderToolStripMenuItem.Click += extractFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            resetToolStripMenuItem.Text = "重置";
            resetToolStripMenuItem.Click += resetToolStripMenuItem_Click;
            // 
            // abortStripMenuItem
            // 
            abortStripMenuItem.Name = "abortStripMenuItem";
            abortStripMenuItem.Size = new System.Drawing.Size(180, 22);
            abortStripMenuItem.Text = "中止";
            abortStripMenuItem.Click += abortStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { displayAll, toolStripSeparator10, enablePreview, enableModelPreview, modelsOnly, toolStripSeparator11, displayInfo, enableResolveDependencies, allowDuplicates, skipContainer, toolStripSeparator12, toolStripMenuItem14, specifyUnityCNKey, toolStripSeparator13, toolStripMenuItem18, AutoDetectMultipleBundle, toolStripMenuItem19, showExpOpt });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            optionsToolStripMenuItem.Text = "选项";
            // 
            // displayAll
            // 
            displayAll.CheckOnClick = true;
            displayAll.Name = "displayAll";
            displayAll.Size = new System.Drawing.Size(184, 22);
            displayAll.Text = "显示所有资源";
            displayAll.ToolTipText = "勾选此选项将显示所有类型的资产,不可提取的资产可以导出原始数据文件.";
            displayAll.CheckedChanged += displayAll_CheckedChanged;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new System.Drawing.Size(181, 6);
            // 
            // enablePreview
            // 
            enablePreview.Checked = true;
            enablePreview.CheckOnClick = true;
            enablePreview.CheckState = CheckState.Checked;
            enablePreview.Name = "enablePreview";
            enablePreview.Size = new System.Drawing.Size(184, 22);
            enablePreview.Text = "启用预览";
            enablePreview.ToolTipText = "切换可读资产如图像、声音、文本等的加载和预览.\r\n如果存在性能或兼容性问题，请禁用预览功能.";
            enablePreview.CheckedChanged += enablePreview_Check;
            // 
            // enableModelPreview
            // 
            enableModelPreview.CheckOnClick = true;
            enableModelPreview.Name = "enableModelPreview";
            enableModelPreview.Size = new System.Drawing.Size(184, 22);
            enableModelPreview.Text = "启用模型预览";
            enableModelPreview.CheckedChanged += enableModelPreview_CheckedChanged;
            // 
            // modelsOnly
            // 
            modelsOnly.CheckOnClick = true;
            modelsOnly.Name = "modelsOnly";
            modelsOnly.Size = new System.Drawing.Size(184, 22);
            modelsOnly.Text = "仅过滤模型";
            modelsOnly.CheckedChanged += modelsOnly_CheckedChanged;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new System.Drawing.Size(181, 6);
            // 
            // displayInfo
            // 
            displayInfo.Checked = true;
            displayInfo.CheckOnClick = true;
            displayInfo.CheckState = CheckState.Checked;
            displayInfo.Name = "displayInfo";
            displayInfo.Size = new System.Drawing.Size(184, 22);
            displayInfo.Text = "显示资源信息";
            displayInfo.ToolTipText = "切换显示每个资产信息的覆盖层，例如图像大小、格式、音频比特率等.";
            displayInfo.CheckedChanged += displayAssetInfo_Check;
            // 
            // enableResolveDependencies
            // 
            enableResolveDependencies.Checked = true;
            enableResolveDependencies.CheckOnClick = true;
            enableResolveDependencies.CheckState = CheckState.Checked;
            enableResolveDependencies.Name = "enableResolveDependencies";
            enableResolveDependencies.Size = new System.Drawing.Size(184, 22);
            enableResolveDependencies.Text = "启用解析依赖项";
            enableResolveDependencies.ToolTipText = "切换加载资源的行为.\r\n禁止在没有依赖项的情况下加载文件.";
            enableResolveDependencies.CheckedChanged += enableResolveDependencies_CheckedChanged;
            // 
            // allowDuplicates
            // 
            allowDuplicates.CheckOnClick = true;
            allowDuplicates.Name = "allowDuplicates";
            allowDuplicates.Size = new System.Drawing.Size(184, 22);
            allowDuplicates.Text = "允许重复";
            allowDuplicates.ToolTipText = "切换导出资产的行为.\r\n允许导出名称重复的资产.";
            allowDuplicates.CheckedChanged += allowDuplicates_CheckedChanged;
            // 
            // skipContainer
            // 
            skipContainer.CheckOnClick = true;
            skipContainer.Name = "skipContainer";
            skipContainer.Size = new System.Drawing.Size(184, 22);
            skipContainer.Text = "跳过容器恢复";
            skipContainer.ToolTipText = "跳过容器恢复步骤.\n处理大量文件时可提升加载速度";
            skipContainer.CheckedChanged += skipContainer_CheckedChanged;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItem14
            // 
            toolStripMenuItem14.DropDownItems.AddRange(new ToolStripItem[] { specifyUnityVersion });
            toolStripMenuItem14.Name = "toolStripMenuItem14";
            toolStripMenuItem14.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem14.Text = "指定Unity版本";
            // 
            // specifyUnityVersion
            // 
            specifyUnityVersion.Name = "specifyUnityVersion";
            specifyUnityVersion.Size = new System.Drawing.Size(100, 23);
            // 
            // specifyUnityCNKey
            // 
            specifyUnityCNKey.Name = "specifyUnityCNKey";
            specifyUnityCNKey.Size = new System.Drawing.Size(184, 22);
            specifyUnityCNKey.Text = "指定UnityCN密钥";
            specifyUnityCNKey.Click += specifyUnityCNKey_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItem18
            // 
            toolStripMenuItem18.DropDownItems.AddRange(new ToolStripItem[] { specifyGame });
            toolStripMenuItem18.Name = "toolStripMenuItem18";
            toolStripMenuItem18.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem18.Text = "指定游戏";
            // 
            // specifyGame
            // 
            specifyGame.DropDownStyle = ComboBoxStyle.DropDownList;
            specifyGame.Name = "specifyGame";
            specifyGame.Size = new System.Drawing.Size(175, 25);
            // 
            // AutoDetectMultipleBundle
            // 
            AutoDetectMultipleBundle.CheckOnClick = true;
            AutoDetectMultipleBundle.Name = "AutoDetectMultipleBundle";
            AutoDetectMultipleBundle.Size = new System.Drawing.Size(184, 22);
            AutoDetectMultipleBundle.Text = "自动检测多个捆绑包";
            AutoDetectMultipleBundle.ToolTipText = "当同一文件中有多个包时启用它.";
            AutoDetectMultipleBundle.CheckedChanged += autoDetectMultipleBundle_CheckedChanged;
            // 
            // toolStripMenuItem19
            // 
            toolStripMenuItem19.DropDownItems.AddRange(new ToolStripItem[] { specifyAIVersion });
            toolStripMenuItem19.Name = "toolStripMenuItem19";
            toolStripMenuItem19.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem19.Text = "指定资源索引版本";
            toolStripMenuItem19.DropDownOpening += toolStripMenuItem19_DropDownOpening;
            // 
            // specifyAIVersion
            // 
            specifyAIVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            specifyAIVersion.Items.AddRange(new object[] { "None" });
            specifyAIVersion.Name = "specifyAIVersion";
            specifyAIVersion.Size = new System.Drawing.Size(121, 25);
            // 
            // showExpOpt
            // 
            showExpOpt.Name = "showExpOpt";
            showExpOpt.Size = new System.Drawing.Size(184, 22);
            showExpOpt.Text = "导出选项";
            showExpOpt.Click += showExpOpt_Click;
            // 
            // modelToolStripMenuItem
            // 
            modelToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportAllObjectssplitToolStripMenuItem1, exportSelectedObjectsToolStripMenuItem, exportSelectedObjectsWithAnimationClipToolStripMenuItem, toolStripSeparator1, exportSelectedObjectsmergeToolStripMenuItem, exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem, toolStripSeparator9, exportSelectedNodessplitToolStripMenuItem, exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem });
            modelToolStripMenuItem.Name = "modelToolStripMenuItem";
            modelToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            modelToolStripMenuItem.Text = "模型";
            // 
            // exportAllObjectssplitToolStripMenuItem1
            // 
            exportAllObjectssplitToolStripMenuItem1.Name = "exportAllObjectssplitToolStripMenuItem1";
            exportAllObjectssplitToolStripMenuItem1.Size = new System.Drawing.Size(261, 22);
            exportAllObjectssplitToolStripMenuItem1.Text = "导出所有对象(拆分)";
            exportAllObjectssplitToolStripMenuItem1.Click += exportAllObjectssplitToolStripMenuItem1_Click;
            // 
            // exportSelectedObjectsToolStripMenuItem
            // 
            exportSelectedObjectsToolStripMenuItem.Name = "exportSelectedObjectsToolStripMenuItem";
            exportSelectedObjectsToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedObjectsToolStripMenuItem.Text = "导出所选对象(拆分)";
            exportSelectedObjectsToolStripMenuItem.Click += exportSelectedObjectsToolStripMenuItem_Click;
            // 
            // exportSelectedObjectsWithAnimationClipToolStripMenuItem
            // 
            exportSelectedObjectsWithAnimationClipToolStripMenuItem.Name = "exportSelectedObjectsWithAnimationClipToolStripMenuItem";
            exportSelectedObjectsWithAnimationClipToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedObjectsWithAnimationClipToolStripMenuItem.Text = "导出所选对象(拆分)+所选动画片段";
            exportSelectedObjectsWithAnimationClipToolStripMenuItem.Click += exportObjectswithAnimationClipMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // exportSelectedObjectsmergeToolStripMenuItem
            // 
            exportSelectedObjectsmergeToolStripMenuItem.Name = "exportSelectedObjectsmergeToolStripMenuItem";
            exportSelectedObjectsmergeToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedObjectsmergeToolStripMenuItem.Text = "导出所选对象(合并)";
            exportSelectedObjectsmergeToolStripMenuItem.Click += exportSelectedObjectsmergeToolStripMenuItem_Click;
            // 
            // exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem
            // 
            exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem.Name = "exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem";
            exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem.Text = "导出所选对象(合并)+所选动画片段";
            exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem.Click += exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(258, 6);
            // 
            // exportSelectedNodessplitToolStripMenuItem
            // 
            exportSelectedNodessplitToolStripMenuItem.Name = "exportSelectedNodessplitToolStripMenuItem";
            exportSelectedNodessplitToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedNodessplitToolStripMenuItem.Text = "导出所选节点(拆分)";
            exportSelectedNodessplitToolStripMenuItem.Click += exportSelectedNodessplitToolStripMenuItem_Click;
            // 
            // exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem
            // 
            exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem.Name = "exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem";
            exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem.Text = "导出所选节点(拆分)+所选动画片段";
            exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem.Click += exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportAllAssetsMenuItem, exportSelectedAssetsMenuItem, exportFilteredAssetsMenuItem, toolStripSeparator3, exportAnimatorWithSelectedAnimationClipToolStripMenuItem, toolStripSeparator4, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem16, toolStripSeparator2, toolStripMenuItem10, sceneHierarchy });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            exportToolStripMenuItem.Text = "导出";
            // 
            // exportAllAssetsMenuItem
            // 
            exportAllAssetsMenuItem.Name = "exportAllAssetsMenuItem";
            exportAllAssetsMenuItem.Size = new System.Drawing.Size(229, 22);
            exportAllAssetsMenuItem.Text = "全部资源";
            exportAllAssetsMenuItem.Click += exportAllAssetsMenuItem_Click;
            // 
            // exportSelectedAssetsMenuItem
            // 
            exportSelectedAssetsMenuItem.Name = "exportSelectedAssetsMenuItem";
            exportSelectedAssetsMenuItem.Size = new System.Drawing.Size(229, 22);
            exportSelectedAssetsMenuItem.Text = "所选资源";
            exportSelectedAssetsMenuItem.Click += exportSelectedAssetsMenuItem_Click;
            // 
            // exportFilteredAssetsMenuItem
            // 
            exportFilteredAssetsMenuItem.Name = "exportFilteredAssetsMenuItem";
            exportFilteredAssetsMenuItem.Size = new System.Drawing.Size(229, 22);
            exportFilteredAssetsMenuItem.Text = "过滤的资源";
            exportFilteredAssetsMenuItem.Click += exportFilteredAssetsMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(226, 6);
            // 
            // exportAnimatorWithSelectedAnimationClipToolStripMenuItem
            // 
            exportAnimatorWithSelectedAnimationClipToolStripMenuItem.Name = "exportAnimatorWithSelectedAnimationClipToolStripMenuItem";
            exportAnimatorWithSelectedAnimationClipToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            exportAnimatorWithSelectedAnimationClipToolStripMenuItem.Text = "动画控制器+所选的动画片段";
            exportAnimatorWithSelectedAnimationClipToolStripMenuItem.Click += exportAnimatorwithAnimationClipMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(226, 6);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(229, 22);
            toolStripMenuItem2.Text = "原始数据";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem4.Text = "全部资源";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem5.Text = "所选资源";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem6.Text = "过滤的资源";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9 });
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(229, 22);
            toolStripMenuItem3.Text = "转储";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem7.Text = "全部资源";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem8.Text = "所选资源";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem9.Text = "过滤的资源";
            toolStripMenuItem9.Click += toolStripMenuItem9_Click;
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem17, toolStripMenuItem24, toolStripMenuItem25 });
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new System.Drawing.Size(229, 22);
            toolStripMenuItem16.Text = "JSON";
            // 
            // toolStripMenuItem17
            // 
            toolStripMenuItem17.Name = "toolStripMenuItem17";
            toolStripMenuItem17.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem17.Text = "全部资源";
            toolStripMenuItem17.Click += toolStripMenuItem17_Click;
            // 
            // toolStripMenuItem24
            // 
            toolStripMenuItem24.Name = "toolStripMenuItem24";
            toolStripMenuItem24.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem24.Text = "所选资源";
            toolStripMenuItem24.Click += toolStripMenuItem24_Click;
            // 
            // toolStripMenuItem25
            // 
            toolStripMenuItem25.Name = "toolStripMenuItem25";
            toolStripMenuItem25.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem25.Text = "过滤的资源";
            toolStripMenuItem25.Click += toolStripMenuItem25_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem11, toolStripMenuItem12, toolStripMenuItem13 });
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new System.Drawing.Size(229, 22);
            toolStripMenuItem10.Text = "资源列表转XML";
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem11.Text = "全部资源";
            toolStripMenuItem11.Click += toolStripMenuItem11_Click;
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem12.Text = "所选资源";
            toolStripMenuItem12.Click += toolStripMenuItem12_Click;
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new System.Drawing.Size(136, 22);
            toolStripMenuItem13.Text = "过滤的资源";
            toolStripMenuItem13.Click += toolStripMenuItem13_Click;
            // 
            // sceneHierarchy
            // 
            sceneHierarchy.Name = "sceneHierarchy";
            sceneHierarchy.Size = new System.Drawing.Size(229, 22);
            sceneHierarchy.Text = "场景层级结构";
            sceneHierarchy.Click += sceneHierarchy_Click;
            // 
            // filterTypeToolStripMenuItem
            // 
            filterTypeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { allToolStripMenuItem });
            filterTypeToolStripMenuItem.Name = "filterTypeToolStripMenuItem";
            filterTypeToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            filterTypeToolStripMenuItem.Text = "过滤器类型";
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.Checked = true;
            allToolStripMenuItem.CheckOnClick = true;
            allToolStripMenuItem.CheckState = CheckState.Checked;
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            allToolStripMenuItem.Text = "全部";
            allToolStripMenuItem.Click += typeToolStripMenuItem_Click;
            // 
            // debugMenuItem
            // 
            debugMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem15, exportClassStructuresMenuItem, enableConsole, clearConsoleToolStripMenuItem, enableFileLogging, loggedEventsMenuItem, openLogFileToolStripMenuItem });
            debugMenuItem.Name = "debugMenuItem";
            debugMenuItem.Size = new System.Drawing.Size(44, 21);
            debugMenuItem.Text = "调式";
            // 
            // toolStripMenuItem15
            // 
            toolStripMenuItem15.Checked = true;
            toolStripMenuItem15.CheckOnClick = true;
            toolStripMenuItem15.CheckState = CheckState.Checked;
            toolStripMenuItem15.Name = "toolStripMenuItem15";
            toolStripMenuItem15.Size = new System.Drawing.Size(180, 22);
            toolStripMenuItem15.Text = "显示错误信息";
            toolStripMenuItem15.Click += toolStripMenuItem15_Click;
            // 
            // exportClassStructuresMenuItem
            // 
            exportClassStructuresMenuItem.Name = "exportClassStructuresMenuItem";
            exportClassStructuresMenuItem.Size = new System.Drawing.Size(180, 22);
            exportClassStructuresMenuItem.Text = "导出类结构";
            exportClassStructuresMenuItem.Click += exportClassStructuresMenuItem_Click;
            // 
            // enableConsole
            // 
            enableConsole.Checked = true;
            enableConsole.CheckOnClick = true;
            enableConsole.CheckState = CheckState.Checked;
            enableConsole.Name = "enableConsole";
            enableConsole.Size = new System.Drawing.Size(180, 22);
            enableConsole.Text = "启用控制台";
            enableConsole.CheckedChanged += enableConsole_CheckedChanged;
            // 
            // clearConsoleToolStripMenuItem
            // 
            clearConsoleToolStripMenuItem.Name = "clearConsoleToolStripMenuItem";
            clearConsoleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            clearConsoleToolStripMenuItem.Text = "清理控制台";
            clearConsoleToolStripMenuItem.Click += clearConsoleToolStripMenuItem_Click;
            // 
            // enableFileLogging
            // 
            enableFileLogging.Checked = true;
            enableFileLogging.CheckOnClick = true;
            enableFileLogging.CheckState = CheckState.Checked;
            enableFileLogging.Name = "enableFileLogging";
            enableFileLogging.Size = new System.Drawing.Size(180, 22);
            enableFileLogging.Text = "启用文件日志记录";
            enableFileLogging.CheckedChanged += enableFileLogging_CheckedChanged;
            // 
            // loggedEventsMenuItem
            // 
            loggedEventsMenuItem.Name = "loggedEventsMenuItem";
            loggedEventsMenuItem.Size = new System.Drawing.Size(180, 22);
            loggedEventsMenuItem.Text = "已记录事件";
            loggedEventsMenuItem.DropDownClosed += loggedEventsMenuItem_DropDownClosed;
            // 
            // openLogFileToolStripMenuItem
            // 
            openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            openLogFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            openLogFileToolStripMenuItem.Text = "打开日志文件";
            openLogFileToolStripMenuItem.Click += openLogFile;
            // 
            // miscToolStripMenuItem
            // 
            miscToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MapNameComboBox, buildMapToolStripMenuItem, buildBothToolStripMenuItem, clearMapToolStripMenuItem, toolStripSeparator7, assetMapNameTextBox, buildAssetMapToolStripMenuItem, assetMapTypeMenuItem, toolStripSeparator8, loadAIToolStripMenuItem, loadCABMapToolStripMenuItem, assetBrowserToolStripMenuItem });
            miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            miscToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            miscToolStripMenuItem.Text = "杂项";
            miscToolStripMenuItem.DropDownOpening += miscToolStripMenuItem_DropDownOpening;
            // 
            // MapNameComboBox
            // 
            MapNameComboBox.Name = "MapNameComboBox";
            MapNameComboBox.Size = new System.Drawing.Size(121, 25);
            MapNameComboBox.ToolTipText = "在此处输入映射名称";
            // 
            // buildMapToolStripMenuItem
            // 
            buildMapToolStripMenuItem.Name = "buildMapToolStripMenuItem";
            buildMapToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            buildMapToolStripMenuItem.Text = "构建映射";
            buildMapToolStripMenuItem.Click += buildMapToolStripMenuItem_Click;
            // 
            // buildBothToolStripMenuItem
            // 
            buildBothToolStripMenuItem.Name = "buildBothToolStripMenuItem";
            buildBothToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            buildBothToolStripMenuItem.Text = "两者都构建";
            buildBothToolStripMenuItem.Click += buildBothToolStripMenuItem_Click;
            // 
            // clearMapToolStripMenuItem
            // 
            clearMapToolStripMenuItem.Name = "clearMapToolStripMenuItem";
            clearMapToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            clearMapToolStripMenuItem.Text = "清理映射";
            clearMapToolStripMenuItem.Click += clearMapToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(178, 6);
            // 
            // assetMapNameTextBox
            // 
            assetMapNameTextBox.Name = "assetMapNameTextBox";
            assetMapNameTextBox.Size = new System.Drawing.Size(100, 23);
            assetMapNameTextBox.ToolTipText = "在此处输入资源映射名称";
            // 
            // buildAssetMapToolStripMenuItem
            // 
            buildAssetMapToolStripMenuItem.Name = "buildAssetMapToolStripMenuItem";
            buildAssetMapToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            buildAssetMapToolStripMenuItem.Text = "构建资源映射";
            buildAssetMapToolStripMenuItem.Click += buildAssetMapToolStripMenuItem_Click;
            // 
            // assetMapTypeMenuItem
            // 
            assetMapTypeMenuItem.Name = "assetMapTypeMenuItem";
            assetMapTypeMenuItem.Size = new System.Drawing.Size(181, 22);
            assetMapTypeMenuItem.Text = "资源映射类型";
            assetMapTypeMenuItem.DropDownItemClicked += assetMapTypeMenuItem_DropDownItemClicked;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(178, 6);
            // 
            // loadAIToolStripMenuItem
            // 
            loadAIToolStripMenuItem.Name = "loadAIToolStripMenuItem";
            loadAIToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            loadAIToolStripMenuItem.Text = "加载资源索引";
            loadAIToolStripMenuItem.Click += loadAIToolStripMenuItem_Click;
            // 
            // loadCABMapToolStripMenuItem
            // 
            loadCABMapToolStripMenuItem.Name = "loadCABMapToolStripMenuItem";
            loadCABMapToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            loadCABMapToolStripMenuItem.Text = "加载CAB映射";
            loadCABMapToolStripMenuItem.Click += loadCABMapToolStripMenuItem_Click;
            // 
            // assetBrowserToolStripMenuItem
            // 
            assetBrowserToolStripMenuItem.Name = "assetBrowserToolStripMenuItem";
            assetBrowserToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            assetBrowserToolStripMenuItem.Text = "资源浏览器";
            assetBrowserToolStripMenuItem.Click += loadAssetMapToolStripMenuItem_Click;
            // 
            // toolStripMenuItem20
            // 
            toolStripMenuItem20.Name = "toolStripMenuItem20";
            toolStripMenuItem20.Size = new System.Drawing.Size(44, 21);
            toolStripMenuItem20.Text = "插件";
            // 
            // assetHelpersToolStripMenuItem
            // 
            assetHelpersToolStripMenuItem.Name = "assetHelpersToolStripMenuItem";
            assetHelpersToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // MapToolStripMenuItem
            // 
            MapToolStripMenuItem.Name = "MapToolStripMenuItem";
            MapToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // assetMapToolStripMenuItem
            // 
            assetMapToolStripMenuItem.Name = "assetMapToolStripMenuItem";
            assetMapToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            splitContainer1.Panel1.Controls.Add(progressbarPanel);
            splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl2);
            splitContainer1.Panel2.Controls.Add(statusStrip1);
            splitContainer1.Panel2MinSize = 400;
            splitContainer1.Size = new System.Drawing.Size(1264, 656);
            splitContainer1.SplitterDistance = 482;
            splitContainer1.TabIndex = 2;
            splitContainer1.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new System.Drawing.Point(17, 3);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(480, 634);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 0;
            tabControl1.Selected += tabPageSelected;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(sceneTreeView);
            tabPage1.Controls.Add(treeSearch);
            tabPage1.Location = new System.Drawing.Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new System.Drawing.Size(472, 604);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "场景层次结构";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // sceneTreeView
            // 
            sceneTreeView.CheckBoxes = true;
            sceneTreeView.Dock = DockStyle.Fill;
            sceneTreeView.HideSelection = false;
            sceneTreeView.Location = new System.Drawing.Point(0, 23);
            sceneTreeView.Name = "sceneTreeView";
            sceneTreeView.Size = new System.Drawing.Size(472, 581);
            sceneTreeView.TabIndex = 1;
            sceneTreeView.AfterCheck += sceneTreeView_AfterCheck;
            // 
            // treeSearch
            // 
            treeSearch.Dock = DockStyle.Top;
            treeSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            treeSearch.Location = new System.Drawing.Point(0, 0);
            treeSearch.Name = "treeSearch";
            treeSearch.PlaceholderText = "搜索(使用Ctrl键检查结果，使用Shift键选择全部，使用Alt键选择父节点)";
            treeSearch.Size = new System.Drawing.Size(472, 23);
            treeSearch.TabIndex = 0;
            treeSearch.TextChanged += treeSearch_TextChanged;
            treeSearch.KeyDown += treeSearch_KeyDown;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(assetListView);
            tabPage2.Controls.Add(listSearch);
            tabPage2.Location = new System.Drawing.Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new System.Drawing.Size(472, 604);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "资源列表";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // assetListView
            // 
            assetListView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderContainer, columnHeaderType, columnHeaderPathID, columnHeaderSize });
            assetListView.Dock = DockStyle.Fill;
            assetListView.FullRowSelect = true;
            assetListView.GridLines = true;
            assetListView.Location = new System.Drawing.Point(0, 23);
            assetListView.Name = "assetListView";
            assetListView.Size = new System.Drawing.Size(472, 581);
            assetListView.TabIndex = 1;
            assetListView.UseCompatibleStateImageBehavior = false;
            assetListView.View = View.Details;
            assetListView.VirtualMode = true;
            assetListView.ColumnClick += assetListView_ColumnClick;
            assetListView.ItemSelectionChanged += selectAsset;
            assetListView.RetrieveVirtualItem += assetListView_RetrieveVirtualItem;
            assetListView.MouseClick += assetListView_MouseClick;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "名称";
            columnHeaderName.Width = 170;
            // 
            // columnHeaderContainer
            // 
            columnHeaderContainer.Text = "容器";
            columnHeaderContainer.Width = 80;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "类型";
            columnHeaderType.Width = 90;
            // 
            // columnHeaderPathID
            // 
            columnHeaderPathID.Text = "PathID";
            // 
            // columnHeaderSize
            // 
            columnHeaderSize.Text = "大小";
            columnHeaderSize.Width = 50;
            // 
            // listSearch
            // 
            listSearch.Dock = DockStyle.Top;
            listSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            listSearch.Location = new System.Drawing.Point(0, 0);
            listSearch.Name = "listSearch";
            listSearch.PlaceholderText = "搜索";
            listSearch.Size = new System.Drawing.Size(472, 23);
            listSearch.TabIndex = 0;
            listSearch.KeyPress += listSearch_KeyPress;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(classesListView);
            tabPage3.Location = new System.Drawing.Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(472, 604);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "资源类别";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // classesListView
            // 
            classesListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            classesListView.Dock = DockStyle.Fill;
            classesListView.FullRowSelect = true;
            classesListView.Location = new System.Drawing.Point(0, 0);
            classesListView.MultiSelect = false;
            classesListView.Name = "classesListView";
            classesListView.Size = new System.Drawing.Size(472, 604);
            classesListView.TabIndex = 0;
            classesListView.UseCompatibleStateImageBehavior = false;
            classesListView.View = View.Details;
            classesListView.ItemSelectionChanged += classesListView_ItemSelectionChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.DisplayIndex = 1;
            columnHeader1.Text = "名称";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.DisplayIndex = 0;
            columnHeader2.Text = "ID";
            columnHeader2.Width = 70;
            // 
            // progressbarPanel
            // 
            progressbarPanel.Controls.Add(progressBar1);
            progressbarPanel.Dock = DockStyle.Bottom;
            progressbarPanel.Location = new System.Drawing.Point(0, 634);
            progressbarPanel.Name = "progressbarPanel";
            progressbarPanel.Padding = new Padding(1, 3, 1, 1);
            progressbarPanel.Size = new System.Drawing.Size(480, 20);
            progressbarPanel.TabIndex = 2;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Bottom;
            progressBar1.Location = new System.Drawing.Point(1, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(478, 17);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 1;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Controls.Add(tabPage5);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new System.Drawing.Point(0, 0);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new System.Drawing.Size(776, 632);
            tabControl2.TabIndex = 4;
            tabControl2.SelectedIndexChanged += tabControl2_SelectedIndexChanged;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(previewPanel);
            tabPage4.Location = new System.Drawing.Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new System.Drawing.Size(768, 602);
            tabPage4.TabIndex = 0;
            tabPage4.Text = "预览";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // previewPanel
            // 
            previewPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            previewPanel.BackgroundImage = Properties.Resources.preview;
            previewPanel.BackgroundImageLayout = ImageLayout.Center;
            previewPanel.Controls.Add(assetInfoLabel);
            previewPanel.Controls.Add(FMODpanel);
            previewPanel.Controls.Add(fontPreviewBox);
            previewPanel.Controls.Add(glControl);
            previewPanel.Controls.Add(textPreviewBox);
            previewPanel.Controls.Add(classTextBox);
            previewPanel.Controls.Add(imgPreviewBox);
            previewPanel.Dock = DockStyle.Fill;
            previewPanel.Location = new System.Drawing.Point(0, 0);
            previewPanel.Name = "previewPanel";
            previewPanel.Size = new System.Drawing.Size(768, 602);
            previewPanel.TabIndex = 1;
            previewPanel.Resize += preview_Resize;
            // 
            // assetInfoLabel
            // 
            assetInfoLabel.AutoSize = true;
            assetInfoLabel.BackColor = System.Drawing.Color.Transparent;
            assetInfoLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            assetInfoLabel.Location = new System.Drawing.Point(4, 7);
            assetInfoLabel.Name = "assetInfoLabel";
            assetInfoLabel.Size = new System.Drawing.Size(0, 17);
            assetInfoLabel.TabIndex = 0;
            // 
            // FMODpanel
            // 
            FMODpanel.BackColor = System.Drawing.SystemColors.ControlDark;
            FMODpanel.Controls.Add(FMODcopyright);
            FMODpanel.Controls.Add(FMODinfoLabel);
            FMODpanel.Controls.Add(FMODtimerLabel);
            FMODpanel.Controls.Add(FMODstatusLabel);
            FMODpanel.Controls.Add(FMODprogressBar);
            FMODpanel.Controls.Add(FMODvolumeBar);
            FMODpanel.Controls.Add(FMODloopButton);
            FMODpanel.Controls.Add(FMODstopButton);
            FMODpanel.Controls.Add(FMODpauseButton);
            FMODpanel.Controls.Add(FMODplayButton);
            FMODpanel.Dock = DockStyle.Fill;
            FMODpanel.Location = new System.Drawing.Point(0, 0);
            FMODpanel.Name = "FMODpanel";
            FMODpanel.Size = new System.Drawing.Size(768, 602);
            FMODpanel.TabIndex = 2;
            FMODpanel.Visible = false;
            // 
            // FMODcopyright
            // 
            FMODcopyright.AutoSize = true;
            FMODcopyright.ForeColor = System.Drawing.SystemColors.ControlLight;
            FMODcopyright.Location = new System.Drawing.Point(214, 337);
            FMODcopyright.Name = "FMODcopyright";
            FMODcopyright.Size = new System.Drawing.Size(268, 17);
            FMODcopyright.TabIndex = 9;
            FMODcopyright.Text = "音频引擎由Firelight Technologies的FMOD提供";
            // 
            // FMODinfoLabel
            // 
            FMODinfoLabel.AutoSize = true;
            FMODinfoLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FMODinfoLabel.Location = new System.Drawing.Point(269, 235);
            FMODinfoLabel.Name = "FMODinfoLabel";
            FMODinfoLabel.Size = new System.Drawing.Size(0, 17);
            FMODinfoLabel.TabIndex = 8;
            // 
            // FMODtimerLabel
            // 
            FMODtimerLabel.AutoSize = true;
            FMODtimerLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FMODtimerLabel.Location = new System.Drawing.Point(460, 235);
            FMODtimerLabel.Name = "FMODtimerLabel";
            FMODtimerLabel.Size = new System.Drawing.Size(89, 17);
            FMODtimerLabel.TabIndex = 7;
            FMODtimerLabel.Text = "0:00.0 / 0:00.0";
            // 
            // FMODstatusLabel
            // 
            FMODstatusLabel.AutoSize = true;
            FMODstatusLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            FMODstatusLabel.Location = new System.Drawing.Point(213, 235);
            FMODstatusLabel.Name = "FMODstatusLabel";
            FMODstatusLabel.Size = new System.Drawing.Size(44, 17);
            FMODstatusLabel.TabIndex = 6;
            FMODstatusLabel.Text = "停止了";
            // 
            // FMODprogressBar
            // 
            FMODprogressBar.AutoSize = false;
            FMODprogressBar.Location = new System.Drawing.Point(213, 253);
            FMODprogressBar.Maximum = 1000;
            FMODprogressBar.Name = "FMODprogressBar";
            FMODprogressBar.Size = new System.Drawing.Size(350, 22);
            FMODprogressBar.TabIndex = 5;
            FMODprogressBar.TickStyle = TickStyle.None;
            FMODprogressBar.Scroll += FMODprogressBar_Scroll;
            FMODprogressBar.MouseDown += FMODprogressBar_MouseDown;
            FMODprogressBar.MouseUp += FMODprogressBar_MouseUp;
            // 
            // FMODvolumeBar
            // 
            FMODvolumeBar.LargeChange = 2;
            FMODvolumeBar.Location = new System.Drawing.Point(460, 280);
            FMODvolumeBar.Name = "FMODvolumeBar";
            FMODvolumeBar.Size = new System.Drawing.Size(104, 45);
            FMODvolumeBar.TabIndex = 4;
            FMODvolumeBar.TickStyle = TickStyle.Both;
            FMODvolumeBar.Value = 8;
            FMODvolumeBar.ValueChanged += FMODvolumeBar_ValueChanged;
            // 
            // FMODloopButton
            // 
            FMODloopButton.Appearance = Appearance.Button;
            FMODloopButton.Location = new System.Drawing.Point(399, 280);
            FMODloopButton.Name = "FMODloopButton";
            FMODloopButton.Size = new System.Drawing.Size(55, 42);
            FMODloopButton.TabIndex = 3;
            FMODloopButton.Text = "循环";
            FMODloopButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            FMODloopButton.UseVisualStyleBackColor = true;
            FMODloopButton.CheckedChanged += FMODloopButton_CheckedChanged;
            // 
            // FMODstopButton
            // 
            FMODstopButton.Location = new System.Drawing.Point(338, 280);
            FMODstopButton.Name = "FMODstopButton";
            FMODstopButton.Size = new System.Drawing.Size(55, 42);
            FMODstopButton.TabIndex = 2;
            FMODstopButton.Text = "停止";
            FMODstopButton.UseVisualStyleBackColor = true;
            FMODstopButton.Click += FMODstopButton_Click;
            // 
            // FMODpauseButton
            // 
            FMODpauseButton.Location = new System.Drawing.Point(277, 280);
            FMODpauseButton.Name = "FMODpauseButton";
            FMODpauseButton.Size = new System.Drawing.Size(55, 42);
            FMODpauseButton.TabIndex = 1;
            FMODpauseButton.Text = "暂停";
            FMODpauseButton.UseVisualStyleBackColor = true;
            FMODpauseButton.Click += FMODpauseButton_Click;
            // 
            // FMODplayButton
            // 
            FMODplayButton.Location = new System.Drawing.Point(216, 280);
            FMODplayButton.Name = "FMODplayButton";
            FMODplayButton.Size = new System.Drawing.Size(55, 42);
            FMODplayButton.TabIndex = 0;
            FMODplayButton.Text = "播放";
            FMODplayButton.UseVisualStyleBackColor = true;
            FMODplayButton.Click += FMODplayButton_Click;
            // 
            // fontPreviewBox
            // 
            fontPreviewBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            fontPreviewBox.Dock = DockStyle.Fill;
            fontPreviewBox.Location = new System.Drawing.Point(0, 0);
            fontPreviewBox.Name = "fontPreviewBox";
            fontPreviewBox.ReadOnly = true;
            fontPreviewBox.Size = new System.Drawing.Size(768, 602);
            fontPreviewBox.TabIndex = 0;
            fontPreviewBox.Text = resources.GetString("fontPreviewBox.Text");
            fontPreviewBox.Visible = false;
            fontPreviewBox.WordWrap = false;
            // 
            // glControl
            // 
            glControl.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl.APIVersion = new Version(3, 3, 0, 0);
            glControl.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            glControl.Dock = DockStyle.Fill;
            glControl.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl.IsEventDriven = true;
            glControl.Location = new System.Drawing.Point(0, 0);
            glControl.Name = "glControl";
            glControl.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl.Size = new System.Drawing.Size(768, 602);
            glControl.TabIndex = 4;
            glControl.Visible = false;
            glControl.Load += glControl_Load;
            glControl.Paint += glControl_Paint;
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseMove += glControl_MouseMove;
            glControl.MouseUp += glControl_MouseUp;
            glControl.MouseWheel += glControl_MouseWheel;
            // 
            // textPreviewBox
            // 
            textPreviewBox.Dock = DockStyle.Fill;
            textPreviewBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            textPreviewBox.Location = new System.Drawing.Point(0, 0);
            textPreviewBox.Multiline = true;
            textPreviewBox.Name = "textPreviewBox";
            textPreviewBox.ReadOnly = true;
            textPreviewBox.ScrollBars = ScrollBars.Both;
            textPreviewBox.Size = new System.Drawing.Size(768, 602);
            textPreviewBox.TabIndex = 2;
            textPreviewBox.Visible = false;
            textPreviewBox.WordWrap = false;
            // 
            // classTextBox
            // 
            classTextBox.Dock = DockStyle.Fill;
            classTextBox.Location = new System.Drawing.Point(0, 0);
            classTextBox.Multiline = true;
            classTextBox.Name = "classTextBox";
            classTextBox.ReadOnly = true;
            classTextBox.ScrollBars = ScrollBars.Both;
            classTextBox.Size = new System.Drawing.Size(768, 602);
            classTextBox.TabIndex = 3;
            classTextBox.Visible = false;
            classTextBox.WordWrap = false;
            // 
            // imgPreviewBox
            // 
            imgPreviewBox.Location = new System.Drawing.Point(0, 0);
            imgPreviewBox.Name = "imgPreviewBox";
            imgPreviewBox.Size = new System.Drawing.Size(768, 605);
            imgPreviewBox.TabIndex = 2;
            imgPreviewBox.TabStop = false;
            imgPreviewBox.Visible = false;
            imgPreviewBox.MouseDown += imgPreviewBox_MouseDown;
            imgPreviewBox.MouseMove += imgPreviewBox_MouseMove;
            imgPreviewBox.MouseUp += imgPreviewBox_MouseUp;
            imgPreviewBox.MouseWheel += imgPreviewBox_MouseWheel;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(dumpTextBox);
            tabPage5.Location = new System.Drawing.Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new System.Drawing.Size(768, 602);
            tabPage5.TabIndex = 1;
            tabPage5.Text = "转储";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // dumpTextBox
            // 
            dumpTextBox.Dock = DockStyle.Fill;
            dumpTextBox.Location = new System.Drawing.Point(0, 0);
            dumpTextBox.Multiline = true;
            dumpTextBox.Name = "dumpTextBox";
            dumpTextBox.ReadOnly = true;
            dumpTextBox.ScrollBars = ScrollBars.Both;
            dumpTextBox.Size = new System.Drawing.Size(768, 602);
            dumpTextBox.TabIndex = 0;
            dumpTextBox.WordWrap = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 632);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(776, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(761, 17);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "准备就绪";
            toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            timer.Interval = 10;
            timer.Tick += timer_Tick;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddExtension = false;
            openFileDialog1.Filter = "所有类型|*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyToolStripMenuItem, exportSelectedAssetsToolStripMenuItem, exportAnimatorwithselectedAnimationClipMenuItem, goToSceneHierarchyToolStripMenuItem, showOriginalFileToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(218, 114);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            copyToolStripMenuItem.Text = "复制文本";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // exportSelectedAssetsToolStripMenuItem
            // 
            exportSelectedAssetsToolStripMenuItem.Name = "exportSelectedAssetsToolStripMenuItem";
            exportSelectedAssetsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            exportSelectedAssetsToolStripMenuItem.Text = "导出所选资源";
            exportSelectedAssetsToolStripMenuItem.Click += exportSelectedAssetsToolStripMenuItem_Click;
            // 
            // exportAnimatorwithselectedAnimationClipMenuItem
            // 
            exportAnimatorwithselectedAnimationClipMenuItem.Name = "exportAnimatorwithselectedAnimationClipMenuItem";
            exportAnimatorwithselectedAnimationClipMenuItem.Size = new System.Drawing.Size(217, 22);
            exportAnimatorwithselectedAnimationClipMenuItem.Text = "导出动画器+所选动画片段";
            exportAnimatorwithselectedAnimationClipMenuItem.Visible = false;
            exportAnimatorwithselectedAnimationClipMenuItem.Click += exportAnimatorwithAnimationClipMenuItem_Click;
            // 
            // goToSceneHierarchyToolStripMenuItem
            // 
            goToSceneHierarchyToolStripMenuItem.Name = "goToSceneHierarchyToolStripMenuItem";
            goToSceneHierarchyToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            goToSceneHierarchyToolStripMenuItem.Text = "转到场景层级结构";
            goToSceneHierarchyToolStripMenuItem.Visible = false;
            goToSceneHierarchyToolStripMenuItem.Click += goToSceneHierarchyToolStripMenuItem_Click;
            // 
            // showOriginalFileToolStripMenuItem
            // 
            showOriginalFileToolStripMenuItem.Name = "showOriginalFileToolStripMenuItem";
            showOriginalFileToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            showOriginalFileToolStripMenuItem.Text = "显示原始文件";
            showOriginalFileToolStripMenuItem.Visible = false;
            showOriginalFileToolStripMenuItem.Click += showOriginalFileToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AllowDrop = true;
            ClientSize = new System.Drawing.Size(1264, 681);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = Properties.Resources._as;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MinimumSize = new System.Drawing.Size(620, 372);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AssetStudio.GUI";
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            KeyDown += AssetStudioForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            progressbarPanel.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            previewPanel.ResumeLayout(false);
            previewPanel.PerformLayout();
            FMODpanel.ResumeLayout(false);
            FMODpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)FMODprogressBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)FMODvolumeBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgPreviewBox).EndInit();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox treeSearch;
        private System.Windows.Forms.TextBox listSearch;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFolderToolStripMenuItem;
        private System.Windows.Forms.ListView assetListView;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllAssetsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedAssetsMenuItem;
        private System.Windows.Forms.Panel previewPanel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel progressbarPanel;
        private System.Windows.Forms.ToolStripMenuItem exportFilteredAssetsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelToolStripMenuItem;
        private System.Windows.Forms.Label assetInfoLabel;
        private System.Windows.Forms.TextBox textPreviewBox;
        private System.Windows.Forms.PictureBox imgPreviewBox;
        private System.Windows.Forms.RichTextBox fontPreviewBox;
        private System.Windows.Forms.Panel FMODpanel;
        private System.Windows.Forms.TrackBar FMODvolumeBar;
        private System.Windows.Forms.CheckBox FMODloopButton;
        private System.Windows.Forms.Button FMODstopButton;
        private System.Windows.Forms.Button FMODpauseButton;
        private System.Windows.Forms.Button FMODplayButton;
        private System.Windows.Forms.TrackBar FMODprogressBar;
        private System.Windows.Forms.Label FMODstatusLabel;
        private System.Windows.Forms.Label FMODtimerLabel;
        private System.Windows.Forms.Label FMODinfoLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayAll;
        private System.Windows.Forms.ToolStripMenuItem enablePreview;
        private System.Windows.Forms.ToolStripMenuItem displayInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem extractFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractFolderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem showExpOpt;
        private GOHierarchy sceneTreeView;
        private System.Windows.Forms.ToolStripMenuItem debugMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView classesListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox classTextBox;
        private System.Windows.Forms.ToolStripMenuItem exportClassStructuresMenuItem;
        private System.Windows.Forms.Label FMODcopyright;
        private OpenTK.WinForms.GLControl glControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showOriginalFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAnimatorwithselectedAnimationClipMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedObjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedObjectsWithAnimationClipToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportAnimatorWithSelectedAnimationClipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllObjectssplitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem goToSceneHierarchyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedObjectsmergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedObjectsmergeWithAnimationClipToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ColumnHeader columnHeaderContainer;
        private System.Windows.Forms.ColumnHeader columnHeaderPathID;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox dumpTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripTextBox specifyUnityVersion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
        private System.Windows.Forms.ToolStripComboBox specifyGame;
        private System.Windows.Forms.ToolStripMenuItem AutoDetectMultipleBundle;
        private System.Windows.Forms.ToolStripComboBox specifyAIVersion;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;
        private System.Windows.Forms.ToolStripMenuItem enableConsole;
        private System.Windows.Forms.ToolStripMenuItem miscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetHelpersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildBothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildAssetMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox MapNameComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox assetMapNameTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem buildMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assetBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedNodessplitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSelectedNodessplitSelectedAnimationClipsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modelsOnly;
        private System.Windows.Forms.ToolStripMenuItem enableModelPreview;
        private System.Windows.Forms.ToolStripMenuItem enableResolveDependencies;
        private System.Windows.Forms.ToolStripMenuItem skipContainer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem specifyUnityCNKey;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem enableFileLogging;
        private System.Windows.Forms.ToolStripMenuItem loggedEventsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sceneHierarchy;
        private System.Windows.Forms.ToolStripMenuItem assetMapTypeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCABMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allowDuplicates;
        private System.Windows.Forms.ToolStripMenuItem openLogFileToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem20;
    }
}

