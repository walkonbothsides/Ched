using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ched.Core;
using Ched.Components.Exporter;
using Ched.Localization;

namespace Ched.UI
{
    public partial class SusExportForm : Form
    {
        public SusArgs ExportArgs { get; set; }

        public SusExportForm(SusArgs args)
        {
            InitializeComponent();
            Icon = Properties.Resources.MainIcon;
            ShowInTaskbar = false;
            titleBox.Enabled = false;
            artistBox.Enabled = false;
            notesDesignerBox.Enabled = false;
            hasPaddingBarBox.Visible = false;
            browseButton.Visible = false;
            outputBox.Visible = false;
            AcceptButton = exportButton;

            levelDropDown.Items.AddRange(Enumerable.Range(1, 14).SelectMany(p => new string[] { p.ToString(), p + "+" }).ToArray());
            difficultyDropDown.Items.AddRange(new string[] { "BASIC", "ADVANCED", "EXPERT", "MASTER", "WORLD'S END" });

            ExportArgs = args;

            difficultyDropDown.SelectedIndex = (int)args.PlayDifficulty;
            levelDropDown.Text = args.PlayLevel;
            songIdBox.Text = args.SongId;
            soundFileBox.Text = args.SoundFileName;
            soundOffsetBox.Value = args.SoundOffset;
            jacketFileBox.Text = args.JacketFilePath;

            exportButton.Click += (s, e) =>
            {
                args.PlayDifficulty = (SusArgs.Difficulty)difficultyDropDown.SelectedIndex;
                args.PlayLevel = levelDropDown.Text;
                args.SongId = songIdBox.Text;
                args.SoundFileName = soundFileBox.Text;
                args.SoundOffset = soundOffsetBox.Value;
                args.JacketFilePath = jacketFileBox.Text;
                DialogResult = DialogResult.OK;
                Close();
            };
        }
    }
}
