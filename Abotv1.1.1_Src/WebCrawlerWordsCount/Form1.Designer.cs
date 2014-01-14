namespace WebCrawlerWordsCount
{
    partial class WebCrawlerWordCount
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
            this.siteURL = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.url = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.words = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filepath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.listBoxLogger = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLinksFound = new System.Windows.Forms.TextBox();
            this.textBoxLinksVisited = new System.Windows.Forms.TextBox();
            this.textBoxLinksSkipped = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // siteURL
            // 
            this.siteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siteURL.Location = new System.Drawing.Point(72, 6);
            this.siteURL.Name = "siteURL";
            this.siteURL.Size = new System.Drawing.Size(312, 20);
            this.siteURL.TabIndex = 0;
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Location = new System.Drawing.Point(390, 3);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(59, 23);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "Go!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(16, 32);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(498, 23);
            this.progressBar.TabIndex = 2;
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.url,
            this.words,
            this.filepath});
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.Location = new System.Drawing.Point(16, 94);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(498, 204);
            this.listViewResults.TabIndex = 3;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            this.listViewResults.SelectedIndexChanged += new System.EventHandler(this.listViewResults_SelectedIndexChanged);
            // 
            // url
            // 
            this.url.Text = "url";
            // 
            // words
            // 
            this.words.Text = "words";
            // 
            // filepath
            // 
            this.filepath.Text = "File Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Site URL:";
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Location = new System.Drawing.Point(455, 3);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(59, 23);
            this.settingsButton.TabIndex = 5;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // listBoxLogger
            // 
            this.listBoxLogger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLogger.FormattingEnabled = true;
            this.listBoxLogger.Location = new System.Drawing.Point(12, 317);
            this.listBoxLogger.Name = "listBoxLogger";
            this.listBoxLogger.Size = new System.Drawing.Size(498, 108);
            this.listBoxLogger.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Links Found";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Links Visited";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Links Skipped";
            // 
            // textBoxLinksFound
            // 
            this.textBoxLinksFound.Location = new System.Drawing.Point(82, 62);
            this.textBoxLinksFound.Name = "textBoxLinksFound";
            this.textBoxLinksFound.ReadOnly = true;
            this.textBoxLinksFound.Size = new System.Drawing.Size(47, 20);
            this.textBoxLinksFound.TabIndex = 11;
            // 
            // textBoxLinksVisited
            // 
            this.textBoxLinksVisited.Location = new System.Drawing.Point(218, 62);
            this.textBoxLinksVisited.Name = "textBoxLinksVisited";
            this.textBoxLinksVisited.ReadOnly = true;
            this.textBoxLinksVisited.Size = new System.Drawing.Size(47, 20);
            this.textBoxLinksVisited.TabIndex = 12;
            // 
            // textBoxLinksSkipped
            // 
            this.textBoxLinksSkipped.Location = new System.Drawing.Point(363, 62);
            this.textBoxLinksSkipped.Name = "textBoxLinksSkipped";
            this.textBoxLinksSkipped.ReadOnly = true;
            this.textBoxLinksSkipped.Size = new System.Drawing.Size(47, 20);
            this.textBoxLinksSkipped.TabIndex = 13;
            // 
            // WebCrawlerWordCount
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(526, 437);
            this.Controls.Add(this.textBoxLinksSkipped);
            this.Controls.Add(this.textBoxLinksVisited);
            this.Controls.Add(this.textBoxLinksFound);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxLogger);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.siteURL);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "WebCrawlerWordCount";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox siteURL;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.ListBox listBoxLogger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader url;
        private System.Windows.Forms.ColumnHeader words;
        private System.Windows.Forms.ColumnHeader filepath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLinksFound;
        private System.Windows.Forms.TextBox textBoxLinksVisited;
        private System.Windows.Forms.TextBox textBoxLinksSkipped;
    }
}

