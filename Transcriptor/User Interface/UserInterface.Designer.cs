using TTSWithoutNeron;

namespace TTSWithoutNeron.Transcriptor.User_Interface
{
    partial class TranscriptorUserInterface
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
            this.richTextBoxOriginalText = new System.Windows.Forms.RichTextBox();
            this.richTextBoxTranscriptedText = new System.Windows.Forms.RichTextBox();
            this.TranscriptTextButton = new System.Windows.Forms.Button();
            this.textBoxWithFilePath = new System.Windows.Forms.TextBox();
            this.FileOverviewButton = new System.Windows.Forms.Button();
            this.openFileDialogLangPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // richTextBoxOriginalText
            // 
            this.richTextBoxOriginalText.Location = new System.Drawing.Point(12, 64);
            this.richTextBoxOriginalText.Name = "richTextBoxOriginalText";
            this.richTextBoxOriginalText.Size = new System.Drawing.Size(293, 335);
            this.richTextBoxOriginalText.TabIndex = 1;
            this.richTextBoxOriginalText.Text = "";
            // 
            // richTextBoxTranscriptedText
            // 
            this.richTextBoxTranscriptedText.Location = new System.Drawing.Point(495, 64);
            this.richTextBoxTranscriptedText.Name = "richTextBoxTranscriptedText";
            this.richTextBoxTranscriptedText.Size = new System.Drawing.Size(293, 335);
            this.richTextBoxTranscriptedText.TabIndex = 2;
            this.richTextBoxTranscriptedText.Text = "";
            // 
            // TranscriptTextButton
            // 
            this.TranscriptTextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TranscriptTextButton.Location = new System.Drawing.Point(312, 205);
            this.TranscriptTextButton.Name = "TranscriptTextButton";
            this.TranscriptTextButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TranscriptTextButton.Size = new System.Drawing.Size(177, 53);
            this.TranscriptTextButton.TabIndex = 3;
            this.TranscriptTextButton.Text = "-> Transcript ->";
            this.TranscriptTextButton.UseVisualStyleBackColor = true;
            this.TranscriptTextButton.Click += new System.EventHandler(this.TranscriptTextButton_Click);
            // 
            // textBoxWithFilePath
            // 
            this.textBoxWithFilePath.Location = new System.Drawing.Point(13, 13);
            this.textBoxWithFilePath.Name = "textBoxWithFilePath";
            this.textBoxWithFilePath.Size = new System.Drawing.Size(292, 20);
            this.textBoxWithFilePath.TabIndex = 4;
            // 
            // FileOverviewButton
            // 
            this.FileOverviewButton.Location = new System.Drawing.Point(312, 13);
            this.FileOverviewButton.Name = "FileOverviewButton";
            this.FileOverviewButton.Size = new System.Drawing.Size(75, 20);
            this.FileOverviewButton.TabIndex = 5;
            this.FileOverviewButton.Text = "Обзор";
            this.FileOverviewButton.UseVisualStyleBackColor = true;
            this.FileOverviewButton.Click += new System.EventHandler(this.FileOverviewButton_Click);
            // 
            // openFileDialogLangPath
            // 
            this.openFileDialogLangPath.FileName = "openFileDialogLangPath";
            this.openFileDialogLangPath.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialogLangPath_FileOk);
            // 
            // TranscriptorUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 411);
            this.Controls.Add(this.FileOverviewButton);
            this.Controls.Add(this.textBoxWithFilePath);
            this.Controls.Add(this.TranscriptTextButton);
            this.Controls.Add(this.richTextBoxTranscriptedText);
            this.Controls.Add(this.richTextBoxOriginalText);
            this.MaximumSize = new System.Drawing.Size(816, 450);
            this.MinimumSize = new System.Drawing.Size(816, 450);
            this.Name = "TranscriptorUserInterface";
            this.Text = "Transcriptor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxOriginalText;
        private System.Windows.Forms.RichTextBox richTextBoxTranscriptedText;
        private System.Windows.Forms.Button TranscriptTextButton;
        private System.Windows.Forms.TextBox textBoxWithFilePath;
        private System.Windows.Forms.Button FileOverviewButton;
        private System.Windows.Forms.OpenFileDialog openFileDialogLangPath;
    }
}