using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTSWithoutNeron.Transcriptor.User_Interface
{
    public partial class TranscriptorUserInterface : Form
    {
        public TranscriptorUserInterface()
        {
            InitializeComponent();
        }

        private void FileOverviewButton_Click(object sender, EventArgs e)
        {
            openFileDialogLangPath.ShowDialog();
        }

        private void OpenFileDialogLangPath_FileOk(object sender, CancelEventArgs e)
        {
            textBoxWithFilePath.Text = openFileDialogLangPath.FileName;
            Transcriptor.Launch(textBoxWithFilePath.Text);
        }

        private void TranscriptTextButton_Click(object sender, EventArgs e)
        {
            richTextBoxTranscriptedText.Text = Transcriptor.TranscriptCreate(richTextBoxOriginalText.Text);
        }
    }
}