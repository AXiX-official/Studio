using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AssetStudio.GUI
{
    partial class ExportOptions
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
            OKbutton = new Button();
            Cancel = new Button();
            groupBox1 = new GroupBox();
            removeTexNameButton = new Button();
            addTexNameButton = new Button();
            texNameComboBox = new ComboBox();
            label10 = new Label();
            texTypeComboBox = new ComboBox();
            uvTypesComboBox = new ComboBox();
            uvEnabledCheckBox = new CheckBox();
            uvsComboBox = new ComboBox();
            canExportCheckBox = new CheckBox();
            label8 = new Label();
            canParseCheckBox = new CheckBox();
            typesComboBox = new ComboBox();
            label6 = new Label();
            minimalAssetMap = new CheckBox();
            assetGroupOptions = new ComboBox();
            label7 = new Label();
            openAfterExport = new CheckBox();
            restoreExtensionName = new CheckBox();
            key = new NumericUpDown();
            encrypted = new CheckBox();
            convertAudio = new CheckBox();
            panel1 = new Panel();
            totga = new RadioButton();
            tojpg = new RadioButton();
            topng = new RadioButton();
            tobmp = new RadioButton();
            converttexture = new CheckBox();
            collectAnimations = new CheckBox();
            groupBox2 = new GroupBox();
            exportMaterials = new CheckBox();
            exportBlendShape = new CheckBox();
            exportAnimations = new CheckBox();
            scaleFactor = new NumericUpDown();
            label5 = new Label();
            fbxFormat = new ComboBox();
            label4 = new Label();
            fbxVersion = new ComboBox();
            label3 = new Label();
            boneSize = new NumericUpDown();
            label2 = new Label();
            exportSkins = new CheckBox();
            label1 = new Label();
            filterPrecision = new NumericUpDown();
            castToBone = new CheckBox();
            exportAllNodes = new CheckBox();
            eulerFilter = new CheckBox();
            toolTip = new ToolTip(components);
            Reset = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)key).BeginInit();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scaleFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)boneSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)filterPrecision).BeginInit();
            SuspendLayout();
            // 
            // OKbutton
            // 
            OKbutton.Location = new System.Drawing.Point(480, 485);
            OKbutton.Margin = new Padding(4, 5, 4, 5);
            OKbutton.Name = "OKbutton";
            OKbutton.Size = new System.Drawing.Size(88, 29);
            OKbutton.TabIndex = 6;
            OKbutton.Text = "确定";
            OKbutton.UseVisualStyleBackColor = true;
            OKbutton.Click += OKbutton_Click;
            // 
            // Cancel
            // 
            Cancel.DialogResult = DialogResult.Cancel;
            Cancel.Location = new System.Drawing.Point(576, 485);
            Cancel.Margin = new Padding(4, 5, 4, 5);
            Cancel.Name = "Cancel";
            Cancel.Size = new System.Drawing.Size(88, 29);
            Cancel.TabIndex = 7;
            Cancel.Text = "取消";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += Cancel_Click;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(removeTexNameButton);
            groupBox1.Controls.Add(addTexNameButton);
            groupBox1.Controls.Add(texNameComboBox);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(texTypeComboBox);
            groupBox1.Controls.Add(uvTypesComboBox);
            groupBox1.Controls.Add(uvEnabledCheckBox);
            groupBox1.Controls.Add(uvsComboBox);
            groupBox1.Controls.Add(canExportCheckBox);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(canParseCheckBox);
            groupBox1.Controls.Add(typesComboBox);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(minimalAssetMap);
            groupBox1.Controls.Add(assetGroupOptions);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(openAfterExport);
            groupBox1.Controls.Add(restoreExtensionName);
            groupBox1.Controls.Add(key);
            groupBox1.Controls.Add(encrypted);
            groupBox1.Controls.Add(convertAudio);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(converttexture);
            groupBox1.Location = new System.Drawing.Point(14, 17);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new System.Drawing.Size(271, 491);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "导出";
            // 
            // removeTexNameButton
            // 
            removeTexNameButton.Location = new System.Drawing.Point(186, 439);
            removeTexNameButton.Name = "removeTexNameButton";
            removeTexNameButton.Size = new System.Drawing.Size(71, 26);
            removeTexNameButton.TabIndex = 41;
            removeTexNameButton.Text = "删除";
            removeTexNameButton.UseVisualStyleBackColor = true;
            removeTexNameButton.Click += RemoveTexNameButton_Click;
            // 
            // addTexNameButton
            // 
            addTexNameButton.Location = new System.Drawing.Point(199, 406);
            addTexNameButton.Name = "addTexNameButton";
            addTexNameButton.Size = new System.Drawing.Size(42, 26);
            addTexNameButton.TabIndex = 13;
            addTexNameButton.Text = "添加";
            addTexNameButton.UseVisualStyleBackColor = true;
            addTexNameButton.Click += AddTexNameButton_Click;
            // 
            // texNameComboBox
            // 
            texNameComboBox.FormattingEnabled = true;
            texNameComboBox.Location = new System.Drawing.Point(8, 423);
            texNameComboBox.Name = "texNameComboBox";
            texNameComboBox.Size = new System.Drawing.Size(81, 25);
            texNameComboBox.TabIndex = 38;
            texNameComboBox.SelectedIndexChanged += TexNameComboBox_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(9, 402);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(83, 17);
            label10.TabIndex = 36;
            label10.Text = "纹理映射选项:";
            // 
            // texTypeComboBox
            // 
            texTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            texTypeComboBox.FormattingEnabled = true;
            texTypeComboBox.Items.AddRange(new object[] { "漫反射", "法线贴图", "高光", "凹凸", "环境光", "自发光", "反射", "置换" });
            texTypeComboBox.Location = new System.Drawing.Point(95, 423);
            texTypeComboBox.Name = "texTypeComboBox";
            texTypeComboBox.Size = new System.Drawing.Size(79, 25);
            texTypeComboBox.TabIndex = 35;
            texTypeComboBox.SelectedIndexChanged += TexTypeComboBox_SelectedIndexChanged;
            texTypeComboBox.MouseHover += TexTypeComboBox_MouseHover;
            // 
            // uvTypesComboBox
            // 
            uvTypesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            uvTypesComboBox.FormattingEnabled = true;
            uvTypesComboBox.Items.AddRange(new object[] { "漫反射", "法线贴图", "高光", "凹凸", "环境光", "自发光", "反射", "置换" });
            uvTypesComboBox.Location = new System.Drawing.Point(89, 373);
            uvTypesComboBox.Name = "uvTypesComboBox";
            uvTypesComboBox.Size = new System.Drawing.Size(106, 25);
            uvTypesComboBox.TabIndex = 34;
            uvTypesComboBox.SelectedIndexChanged += uvTypesComboBox_SelectedIndexChanged;
            // 
            // uvEnabledCheckBox
            // 
            uvEnabledCheckBox.AutoSize = true;
            uvEnabledCheckBox.Location = new System.Drawing.Point(201, 377);
            uvEnabledCheckBox.Name = "uvEnabledCheckBox";
            uvEnabledCheckBox.Size = new System.Drawing.Size(51, 21);
            uvEnabledCheckBox.TabIndex = 33;
            uvEnabledCheckBox.Text = "导出";
            uvEnabledCheckBox.UseVisualStyleBackColor = true;
            uvEnabledCheckBox.CheckedChanged += uvEnabledCheckBox_CheckedChanged;
            // 
            // uvsComboBox
            // 
            uvsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            uvsComboBox.FormattingEnabled = true;
            uvsComboBox.Items.AddRange(new object[] { "UV0", "UV1", "UV2", "UV3", "UV4", "UV5", "UV6", "UV7" });
            uvsComboBox.Location = new System.Drawing.Point(8, 373);
            uvsComboBox.Name = "uvsComboBox";
            uvsComboBox.Size = new System.Drawing.Size(75, 25);
            uvsComboBox.TabIndex = 32;
            uvsComboBox.SelectedIndexChanged += uvsComboBox_SelectedIndexChanged;
            uvsComboBox.MouseHover += uvsComboBox_MouseHover;
            // 
            // canExportCheckBox
            // 
            canExportCheckBox.AutoSize = true;
            canExportCheckBox.Location = new System.Drawing.Point(209, 323);
            canExportCheckBox.Name = "canExportCheckBox";
            canExportCheckBox.Size = new System.Drawing.Size(51, 21);
            canExportCheckBox.TabIndex = 31;
            canExportCheckBox.Text = "导出";
            canExportCheckBox.UseVisualStyleBackColor = true;
            canExportCheckBox.CheckedChanged += CanExportCheckBox_CheckedChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(8, 303);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(122, 17);
            label8.TabIndex = 30;
            label8.Text = "所选的unity类型可以:";
            // 
            // canParseCheckBox
            // 
            canParseCheckBox.AutoSize = true;
            canParseCheckBox.Location = new System.Drawing.Point(152, 323);
            canParseCheckBox.Name = "canParseCheckBox";
            canParseCheckBox.Size = new System.Drawing.Size(51, 21);
            canParseCheckBox.TabIndex = 29;
            canParseCheckBox.Text = "解析";
            canParseCheckBox.UseVisualStyleBackColor = true;
            canParseCheckBox.CheckedChanged += CanParseCheckBox_CheckedChanged;
            // 
            // typesComboBox
            // 
            typesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typesComboBox.FormattingEnabled = true;
            typesComboBox.Items.AddRange(new object[] { ClassIDType.Animation, ClassIDType.AnimationClip, ClassIDType.Animator, ClassIDType.AnimatorController, ClassIDType.AnimatorOverrideController, ClassIDType.AssetBundle, ClassIDType.AudioClip, ClassIDType.Avatar, ClassIDType.Font, ClassIDType.GameObject, ClassIDType.IndexObject, ClassIDType.Material, ClassIDType.Mesh, ClassIDType.MeshFilter, ClassIDType.MeshRenderer, ClassIDType.MiHoYoBinData, ClassIDType.MonoBehaviour, ClassIDType.MonoScript, ClassIDType.MovieTexture, ClassIDType.PlayerSettings, ClassIDType.RectTransform, ClassIDType.Shader, ClassIDType.SkinnedMeshRenderer, ClassIDType.Sprite, ClassIDType.SpriteAtlas, ClassIDType.TextAsset, ClassIDType.Texture2D, ClassIDType.Transform, ClassIDType.VideoClip, ClassIDType.ResourceManager });
            typesComboBox.Location = new System.Drawing.Point(7, 323);
            typesComboBox.Name = "typesComboBox";
            typesComboBox.Size = new System.Drawing.Size(139, 25);
            typesComboBox.TabIndex = 28;
            typesComboBox.SelectedIndexChanged += TypesComboBox_SelectedIndexChanged;
            typesComboBox.MouseHover += TypesComboBox_MouseHover;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(7, 352);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(76, 17);
            label6.TabIndex = 27;
            label6.Text = "UV映射选项:";
            // 
            // minimalAssetMap
            // 
            minimalAssetMap.AutoSize = true;
            minimalAssetMap.Location = new System.Drawing.Point(7, 146);
            minimalAssetMap.Name = "minimalAssetMap";
            minimalAssetMap.Size = new System.Drawing.Size(99, 21);
            minimalAssetMap.TabIndex = 17;
            minimalAssetMap.Text = "最小资源映射";
            minimalAssetMap.UseVisualStyleBackColor = true;
            // 
            // assetGroupOptions
            // 
            assetGroupOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            assetGroupOptions.FormattingEnabled = true;
            assetGroupOptions.Items.AddRange(new object[] { "类型名称", "容器路径", "源文件名", "不分组" });
            assetGroupOptions.Location = new System.Drawing.Point(7, 272);
            assetGroupOptions.Margin = new Padding(4, 5, 4, 5);
            assetGroupOptions.Name = "assetGroupOptions";
            assetGroupOptions.Size = new System.Drawing.Size(173, 25);
            assetGroupOptions.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(8, 250);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(188, 17);
            label7.TabIndex = 11;
            label7.Text = "按以下方式对导出的资源进行分组";
            // 
            // openAfterExport
            // 
            openAfterExport.AutoSize = true;
            openAfterExport.Checked = true;
            openAfterExport.CheckState = CheckState.Checked;
            openAfterExport.Location = new System.Drawing.Point(7, 88);
            openAfterExport.Margin = new Padding(4, 5, 4, 5);
            openAfterExport.Name = "openAfterExport";
            openAfterExport.Size = new System.Drawing.Size(123, 21);
            openAfterExport.TabIndex = 10;
            openAfterExport.Text = "导出后打开文件夹";
            openAfterExport.UseVisualStyleBackColor = true;
            // 
            // restoreExtensionName
            // 
            restoreExtensionName.AutoSize = true;
            restoreExtensionName.Checked = true;
            restoreExtensionName.CheckState = CheckState.Checked;
            restoreExtensionName.Location = new System.Drawing.Point(7, 27);
            restoreExtensionName.Margin = new Padding(4, 5, 4, 5);
            restoreExtensionName.Name = "restoreExtensionName";
            restoreExtensionName.Size = new System.Drawing.Size(135, 21);
            restoreExtensionName.TabIndex = 9;
            restoreExtensionName.Text = "恢复文本资源扩展名";
            restoreExtensionName.UseVisualStyleBackColor = true;
            // 
            // key
            // 
            key.Hexadecimal = true;
            key.Location = new System.Drawing.Point(186, 117);
            key.Margin = new Padding(4, 3, 4, 3);
            key.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            key.Name = "key";
            key.Size = new System.Drawing.Size(55, 23);
            key.TabIndex = 8;
            key.MouseHover += Key_MouseHover;
            // 
            // encrypted
            // 
            encrypted.AutoSize = true;
            encrypted.Checked = true;
            encrypted.CheckState = CheckState.Checked;
            encrypted.Location = new System.Drawing.Point(7, 118);
            encrypted.Margin = new Padding(4, 3, 4, 3);
            encrypted.Name = "encrypted";
            encrypted.Size = new System.Drawing.Size(159, 21);
            encrypted.TabIndex = 12;
            encrypted.Text = "加密的米哈游二进制数据\r\n";
            encrypted.UseVisualStyleBackColor = true;
            // 
            // convertAudio
            // 
            convertAudio.AutoSize = true;
            convertAudio.Checked = true;
            convertAudio.CheckState = CheckState.Checked;
            convertAudio.Location = new System.Drawing.Point(7, 58);
            convertAudio.Margin = new Padding(4, 5, 4, 5);
            convertAudio.Name = "convertAudio";
            convertAudio.Size = new System.Drawing.Size(182, 21);
            convertAudio.TabIndex = 6;
            convertAudio.Text = "转换AudioClip到WAV(PCM)";
            convertAudio.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(totga);
            panel1.Controls.Add(tojpg);
            panel1.Controls.Add(topng);
            panel1.Controls.Add(tobmp);
            panel1.Location = new System.Drawing.Point(24, 197);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(236, 43);
            panel1.TabIndex = 5;
            // 
            // totga
            // 
            totga.AutoSize = true;
            totga.Location = new System.Drawing.Point(175, 9);
            totga.Margin = new Padding(4, 5, 4, 5);
            totga.Name = "totga";
            totga.Size = new System.Drawing.Size(48, 21);
            totga.TabIndex = 2;
            totga.Text = "Tga";
            totga.UseVisualStyleBackColor = true;
            // 
            // tojpg
            // 
            tojpg.AutoSize = true;
            tojpg.Location = new System.Drawing.Point(113, 9);
            tojpg.Margin = new Padding(4, 5, 4, 5);
            tojpg.Name = "tojpg";
            tojpg.Size = new System.Drawing.Size(54, 21);
            tojpg.TabIndex = 4;
            tojpg.Text = "Jpeg";
            tojpg.UseVisualStyleBackColor = true;
            // 
            // topng
            // 
            topng.AutoSize = true;
            topng.Checked = true;
            topng.Location = new System.Drawing.Point(58, 9);
            topng.Margin = new Padding(4, 5, 4, 5);
            topng.Name = "topng";
            topng.Size = new System.Drawing.Size(48, 21);
            topng.TabIndex = 3;
            topng.TabStop = true;
            topng.Text = "Png";
            topng.UseVisualStyleBackColor = true;
            // 
            // tobmp
            // 
            tobmp.AutoSize = true;
            tobmp.Location = new System.Drawing.Point(4, 9);
            tobmp.Margin = new Padding(4, 5, 4, 5);
            tobmp.Name = "tobmp";
            tobmp.Size = new System.Drawing.Size(53, 21);
            tobmp.TabIndex = 2;
            tobmp.Text = "Bmp";
            tobmp.UseVisualStyleBackColor = true;
            // 
            // converttexture
            // 
            converttexture.AutoSize = true;
            converttexture.Checked = true;
            converttexture.CheckState = CheckState.Checked;
            converttexture.Location = new System.Drawing.Point(7, 173);
            converttexture.Margin = new Padding(4, 5, 4, 5);
            converttexture.Name = "converttexture";
            converttexture.Size = new System.Drawing.Size(110, 21);
            converttexture.TabIndex = 1;
            converttexture.Text = "转换Texture2D";
            converttexture.UseVisualStyleBackColor = true;
            // 
            // collectAnimations
            // 
            collectAnimations.AutoSize = true;
            collectAnimations.Checked = true;
            collectAnimations.CheckState = CheckState.Checked;
            collectAnimations.Location = new System.Drawing.Point(8, 49);
            collectAnimations.Margin = new Padding(4, 3, 4, 3);
            collectAnimations.Name = "collectAnimations";
            collectAnimations.Size = new System.Drawing.Size(75, 21);
            collectAnimations.TabIndex = 24;
            collectAnimations.Text = "收集动画";
            collectAnimations.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.Controls.Add(exportMaterials);
            groupBox2.Controls.Add(collectAnimations);
            groupBox2.Controls.Add(exportBlendShape);
            groupBox2.Controls.Add(exportAnimations);
            groupBox2.Controls.Add(scaleFactor);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(fbxFormat);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(fbxVersion);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(boneSize);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(exportSkins);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(filterPrecision);
            groupBox2.Controls.Add(castToBone);
            groupBox2.Controls.Add(exportAllNodes);
            groupBox2.Controls.Add(eulerFilter);
            groupBox2.Location = new System.Drawing.Point(292, 17);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new System.Drawing.Size(380, 267);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Fbx";
            // 
            // exportMaterials
            // 
            exportMaterials.AutoSize = true;
            exportMaterials.Location = new System.Drawing.Point(154, 109);
            exportMaterials.Margin = new Padding(4, 5, 4, 5);
            exportMaterials.Name = "exportMaterials";
            exportMaterials.Size = new System.Drawing.Size(75, 21);
            exportMaterials.TabIndex = 25;
            exportMaterials.Text = "导出材质";
            exportMaterials.UseVisualStyleBackColor = true;
            // 
            // exportBlendShape
            // 
            exportBlendShape.AutoSize = true;
            exportBlendShape.Checked = true;
            exportBlendShape.CheckState = CheckState.Checked;
            exportBlendShape.Location = new System.Drawing.Point(7, 78);
            exportBlendShape.Margin = new Padding(4, 5, 4, 5);
            exportBlendShape.Name = "exportBlendShape";
            exportBlendShape.Size = new System.Drawing.Size(99, 21);
            exportBlendShape.TabIndex = 22;
            exportBlendShape.Text = "导出混合形状";
            exportBlendShape.UseVisualStyleBackColor = true;
            // 
            // exportAnimations
            // 
            exportAnimations.AutoSize = true;
            exportAnimations.Checked = true;
            exportAnimations.CheckState = CheckState.Checked;
            exportAnimations.Location = new System.Drawing.Point(154, 49);
            exportAnimations.Margin = new Padding(4, 5, 4, 5);
            exportAnimations.Name = "exportAnimations";
            exportAnimations.Size = new System.Drawing.Size(75, 21);
            exportAnimations.TabIndex = 21;
            exportAnimations.Text = "导出动画";
            exportAnimations.UseVisualStyleBackColor = true;
            // 
            // scaleFactor
            // 
            scaleFactor.DecimalPlaces = 2;
            scaleFactor.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            scaleFactor.Location = new System.Drawing.Point(102, 214);
            scaleFactor.Margin = new Padding(4, 5, 4, 5);
            scaleFactor.Name = "scaleFactor";
            scaleFactor.Size = new System.Drawing.Size(59, 23);
            scaleFactor.TabIndex = 20;
            scaleFactor.TextAlign = HorizontalAlignment.Center;
            scaleFactor.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 219);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(56, 17);
            label5.TabIndex = 19;
            label5.Text = "缩放系数";
            // 
            // fbxFormat
            // 
            fbxFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            fbxFormat.FormattingEnabled = true;
            fbxFormat.Items.AddRange(new object[] { "Binary", "Ascii" });
            fbxFormat.Location = new System.Drawing.Point(271, 142);
            fbxFormat.Margin = new Padding(4, 5, 4, 5);
            fbxFormat.Name = "fbxFormat";
            fbxFormat.Size = new System.Drawing.Size(70, 25);
            fbxFormat.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(188, 146);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 17);
            label4.TabIndex = 17;
            label4.Text = "FBX格式";
            // 
            // fbxVersion
            // 
            fbxVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            fbxVersion.FormattingEnabled = true;
            fbxVersion.Items.AddRange(new object[] { "6.1", "7.1", "7.2", "7.3", "7.4", "7.5" });
            fbxVersion.Location = new System.Drawing.Point(271, 178);
            fbxVersion.Margin = new Padding(4, 5, 4, 5);
            fbxVersion.Name = "fbxVersion";
            fbxVersion.Size = new System.Drawing.Size(70, 25);
            fbxVersion.TabIndex = 16;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(188, 182);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(54, 17);
            label3.TabIndex = 15;
            label3.Text = "FBX版本";
            // 
            // boneSize
            // 
            boneSize.Location = new System.Drawing.Point(102, 178);
            boneSize.Margin = new Padding(4, 5, 4, 5);
            boneSize.Name = "boneSize";
            boneSize.Size = new System.Drawing.Size(59, 23);
            boneSize.TabIndex = 11;
            boneSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 182);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 17);
            label2.TabIndex = 10;
            label2.Text = "骨骼大小";
            // 
            // exportSkins
            // 
            exportSkins.AutoSize = true;
            exportSkins.Checked = true;
            exportSkins.CheckState = CheckState.Checked;
            exportSkins.Location = new System.Drawing.Point(154, 19);
            exportSkins.Margin = new Padding(4, 5, 4, 5);
            exportSkins.Name = "exportSkins";
            exportSkins.Size = new System.Drawing.Size(75, 21);
            exportSkins.TabIndex = 8;
            exportSkins.Text = "导出皮肤";
            exportSkins.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 146);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(56, 17);
            label1.TabIndex = 7;
            label1.Text = "过滤精度";
            // 
            // filterPrecision
            // 
            filterPrecision.DecimalPlaces = 2;
            filterPrecision.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            filterPrecision.Location = new System.Drawing.Point(102, 144);
            filterPrecision.Margin = new Padding(4, 5, 4, 5);
            filterPrecision.Name = "filterPrecision";
            filterPrecision.Size = new System.Drawing.Size(59, 23);
            filterPrecision.TabIndex = 6;
            filterPrecision.Value = new decimal(new int[] { 25, 0, 0, 131072 });
            // 
            // castToBone
            // 
            castToBone.AutoSize = true;
            castToBone.Location = new System.Drawing.Point(154, 78);
            castToBone.Margin = new Padding(4, 5, 4, 5);
            castToBone.Name = "castToBone";
            castToBone.Size = new System.Drawing.Size(135, 21);
            castToBone.TabIndex = 5;
            castToBone.Text = "所有节点转换为骨骼";
            castToBone.UseVisualStyleBackColor = true;
            // 
            // exportAllNodes
            // 
            exportAllNodes.AutoSize = true;
            exportAllNodes.Checked = true;
            exportAllNodes.CheckState = CheckState.Checked;
            exportAllNodes.Location = new System.Drawing.Point(7, 109);
            exportAllNodes.Margin = new Padding(4, 5, 4, 5);
            exportAllNodes.Name = "exportAllNodes";
            exportAllNodes.Size = new System.Drawing.Size(99, 21);
            exportAllNodes.TabIndex = 4;
            exportAllNodes.Text = "导出所有节点";
            exportAllNodes.UseVisualStyleBackColor = true;
            // 
            // eulerFilter
            // 
            eulerFilter.AutoSize = true;
            eulerFilter.Checked = true;
            eulerFilter.CheckState = CheckState.Checked;
            eulerFilter.Location = new System.Drawing.Point(8, 19);
            eulerFilter.Margin = new Padding(4, 5, 4, 5);
            eulerFilter.Name = "eulerFilter";
            eulerFilter.Size = new System.Drawing.Size(87, 21);
            eulerFilter.TabIndex = 3;
            eulerFilter.Text = "欧拉过滤器";
            eulerFilter.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            toolTip.AutomaticDelay = 1000;
            toolTip.UseAnimation = false;
            toolTip.UseFading = false;
            // 
            // Reset
            // 
            Reset.Location = new System.Drawing.Point(300, 485);
            Reset.Name = "Reset";
            Reset.Size = new System.Drawing.Size(88, 29);
            Reset.TabIndex = 12;
            Reset.Text = "重置";
            Reset.UseVisualStyleBackColor = true;
            Reset.Click += Reset_Click;
            // 
            // ExportOptions
            // 
            AcceptButton = OKbutton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = Cancel;
            ClientSize = new System.Drawing.Size(677, 529);
            Controls.Add(Reset);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(Cancel);
            Controls.Add(OKbutton);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportOptions";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "导出选项";
            TopMost = true;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)key).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scaleFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)boneSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)filterPrecision).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox converttexture;
        private System.Windows.Forms.RadioButton tojpg;
        private System.Windows.Forms.RadioButton topng;
        private System.Windows.Forms.RadioButton tobmp;
        private System.Windows.Forms.RadioButton totga;
        private System.Windows.Forms.CheckBox convertAudio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown boneSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox exportSkins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown filterPrecision;
        private System.Windows.Forms.CheckBox castToBone;
        private System.Windows.Forms.CheckBox exportAllNodes;
        private System.Windows.Forms.CheckBox eulerFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox fbxVersion;
        private System.Windows.Forms.ComboBox fbxFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown scaleFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox exportBlendShape;
        private System.Windows.Forms.CheckBox exportAnimations;
        private System.Windows.Forms.ComboBox assetGroupOptions;
        private System.Windows.Forms.CheckBox restoreExtensionName;
        private System.Windows.Forms.CheckBox openAfterExport;
        private System.Windows.Forms.CheckBox collectAnimations;
        private System.Windows.Forms.CheckBox encrypted;
        private System.Windows.Forms.NumericUpDown key;
        private System.Windows.Forms.CheckBox minimalAssetMap;
        private System.Windows.Forms.Label label7;
        private Label label6;
        private Label label8;
        private CheckBox canParseCheckBox;
        private ComboBox typesComboBox;
        private CheckBox canExportCheckBox;
        private ComboBox uvTypesComboBox;
        private CheckBox uvEnabledCheckBox;
        private ComboBox uvsComboBox;
        private Label label10;
        private ComboBox texTypeComboBox;
        private ToolTip toolTip;
        private Button Reset;
        private ComboBox texNameComboBox;
        private Button addTexNameButton;
        private Button removeTexNameButton;
        private CheckBox exportMaterials;
    }
}
