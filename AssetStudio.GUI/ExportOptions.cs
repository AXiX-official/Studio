﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AssetStudio.GUI
{
    public partial class ExportOptions : Form
    {
        private Dictionary<ClassIDType, (bool, bool)> types = new Dictionary<ClassIDType, (bool, bool)>();
        private Dictionary<string, (bool, int)> uvs = new Dictionary<string, (bool, int)>();
        private Dictionary<int, string> texs = new Dictionary<int, string>();
        public ExportOptions()
        {
            InitializeComponent();
            assetGroupOptions.SelectedIndex = Properties.Settings.Default.assetGroupOption;
            restoreExtensionName.Checked = Properties.Settings.Default.restoreExtensionName;
            converttexture.Checked = Properties.Settings.Default.convertTexture;
            convertAudio.Checked = Properties.Settings.Default.convertAudio;
            var str = Properties.Settings.Default.convertType.ToString();
            foreach (Control c in panel1.Controls)
            {
                if (c.Text == str)
                {
                    ((RadioButton)c).Checked = true;
                    break;
                }
            }
            openAfterExport.Checked = Properties.Settings.Default.openAfterExport;
            eulerFilter.Checked = Properties.Settings.Default.eulerFilter;
            filterPrecision.Value = Properties.Settings.Default.filterPrecision;
            exportAllNodes.Checked = Properties.Settings.Default.exportAllNodes;
            exportSkins.Checked = Properties.Settings.Default.exportSkins;
            exportAnimations.Checked = Properties.Settings.Default.exportAnimations;
            exportBlendShape.Checked = Properties.Settings.Default.exportBlendShape;
            castToBone.Checked = Properties.Settings.Default.castToBone;
            boneSize.Value = Properties.Settings.Default.boneSize;
            scaleFactor.Value = Properties.Settings.Default.scaleFactor;
            fbxVersion.SelectedIndex = Properties.Settings.Default.fbxVersion;
            fbxFormat.SelectedIndex = Properties.Settings.Default.fbxFormat;
            collectAnimations.Checked = Properties.Settings.Default.collectAnimations;
            encrypted.Checked = Properties.Settings.Default.encrypted;
            key.Value = Properties.Settings.Default.key;
            minimalAssetMap.Checked = Properties.Settings.Default.minimalAssetMap;
            types = JsonConvert.DeserializeObject<Dictionary<ClassIDType, (bool, bool)>>(Properties.Settings.Default.types);
            uvs = JsonConvert.DeserializeObject<Dictionary<string, (bool, int)>>(Properties.Settings.Default.uvs);
            texs = JsonConvert.DeserializeObject<Dictionary<int, string>>(Properties.Settings.Default.texs);
            typesComboBox.SelectedIndex = 0;
            uvsComboBox.SelectedIndex = 0;
            texTypeComboBox.SelectedIndex = 0;
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.assetGroupOption = assetGroupOptions.SelectedIndex;
            Properties.Settings.Default.restoreExtensionName = restoreExtensionName.Checked;
            Properties.Settings.Default.convertTexture = converttexture.Checked;
            Properties.Settings.Default.convertAudio = convertAudio.Checked;
            foreach (Control c in panel1.Controls)
            {
                if (((RadioButton)c).Checked)
                {
                    Properties.Settings.Default.convertType = (ImageFormat)Enum.Parse(typeof(ImageFormat), c.Text);
                    break;
                }
            }
            Properties.Settings.Default.openAfterExport = openAfterExport.Checked;
            Properties.Settings.Default.eulerFilter = eulerFilter.Checked;
            Properties.Settings.Default.filterPrecision = filterPrecision.Value;
            Properties.Settings.Default.exportAllNodes = exportAllNodes.Checked;
            Properties.Settings.Default.exportSkins = exportSkins.Checked;
            Properties.Settings.Default.exportAnimations = exportAnimations.Checked;
            Properties.Settings.Default.exportBlendShape = exportBlendShape.Checked;
            Properties.Settings.Default.castToBone = castToBone.Checked;
            Properties.Settings.Default.boneSize = boneSize.Value;
            Properties.Settings.Default.scaleFactor = scaleFactor.Value;
            Properties.Settings.Default.fbxVersion = fbxVersion.SelectedIndex;
            Properties.Settings.Default.fbxFormat = fbxFormat.SelectedIndex;
            Properties.Settings.Default.collectAnimations = collectAnimations.Checked;
            Properties.Settings.Default.encrypted = encrypted.Checked;
            Properties.Settings.Default.key = (byte)key.Value;
            Properties.Settings.Default.minimalAssetMap = minimalAssetMap.Checked;
            Properties.Settings.Default.types = JsonConvert.SerializeObject(types);
            Properties.Settings.Default.uvs = JsonConvert.SerializeObject(uvs);
            Properties.Settings.Default.texs = JsonConvert.SerializeObject(texs);
            Properties.Settings.Default.Save();
            MiHoYoBinData.Key = (byte)key.Value;
            MiHoYoBinData.Encrypted = encrypted.Checked;
            AssetsHelper.Minimal = Properties.Settings.Default.minimalAssetMap;
            TypeFlags.SetTypes(types);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void TypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox && types.TryGetValue((ClassIDType)comboBox.SelectedItem, out var param))
            {
                canParseCheckBox.Checked = param.Item1;
                canExportCheckBox.Checked = param.Item2;
            }
        }

        private void CanParseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox && types.TryGetValue((ClassIDType)typesComboBox.SelectedItem, out var param))
            {
                param.Item1 = checkBox.Checked;
                types[(ClassIDType)typesComboBox.SelectedItem] = (param.Item1, param.Item2);
            }
        }

        private void CanExportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox && types.TryGetValue((ClassIDType)typesComboBox.SelectedItem, out var param))
            {
                param.Item2 = checkBox.Checked;
                types[(ClassIDType)typesComboBox.SelectedItem] = (param.Item1, param.Item2);
            }
        }

        private void uvsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox && uvs.TryGetValue(comboBox.SelectedItem.ToString(), out var param))
            {
                uvEnabledCheckBox.Checked = param.Item1;
                uvTypesComboBox.SelectedIndex = param.Item2;
            }
        }

        private void uvEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox && uvs.TryGetValue(uvsComboBox.SelectedItem.ToString(), out var param))
            {
                param.Item1 = checkBox.Checked;
                uvs[uvsComboBox.SelectedItem.ToString()] = (param.Item1, param.Item2);
            }
        }

        private void uvTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox && uvs.TryGetValue(uvsComboBox.SelectedItem.ToString(), out var param))
            {
                param.Item2 = comboBox.SelectedIndex;
                uvs[uvsComboBox.SelectedItem.ToString()] = (param.Item1, param.Item2);
            }
        }

        private void TexTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox && texs.TryGetValue(comboBox.SelectedIndex, out var name))
            {
                texNameTextBox.Text = name;
            }
        }

        private void TexNameTextBox_LostFocus(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && texs.ContainsKey(texTypeComboBox.SelectedIndex))
            {
                texs[texTypeComboBox.SelectedIndex] = textBox.Text;
            }
        }

        private void TypesComboBox_MouseHover(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var type in types)
            {
                sb.Append($"{type.Key}: {(type.Value.Item1 ? '\x2713' : '\x2717')}, {(type.Value.Item2 ? '\x2713' : '\x2717')}\n");
            }

            toolTip.ToolTipTitle = "Type options status:";
            toolTip.SetToolTip(typesComboBox, sb.ToString());
        }

        private void uvsComboBox_MouseHover(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var uv in uvs)
            {
                sb.Append($"{uv.Key}: {uvTypesComboBox.Items[uv.Value.Item2]}, {(uv.Value.Item1 ? '\x2713' : '\x2717')}\n");
            }

            toolTip.ToolTipTitle = "UVs options status:";
            toolTip.SetToolTip(uvsComboBox, sb.ToString());
        }

        private void TexTypeComboBox_MouseHover(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var tex in texs)
            {
                sb.Append($"{texTypeComboBox.Items[tex.Key]}: {tex.Value}\n");
            }

            toolTip.ToolTipTitle = "Texture options status:";
            toolTip.SetToolTip(texTypeComboBox, sb.ToString());
        }

        private void Key_MouseHover(object sender, EventArgs e)
        {
            toolTip.ToolTipTitle = "Value";
            toolTip.SetToolTip(key, "Key in Hex");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
