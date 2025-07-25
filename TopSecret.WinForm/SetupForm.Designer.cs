namespace TopSecret.WinForm
{
    partial class SetupForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelectFolder = new Button();
            txtFolderPath = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtFileName = new TextBox();
            btnCreateDatabaseFile = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(531, 48);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(91, 23);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(12, 45);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(513, 23);
            txtFolderPath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 2;
            label1.Text = "Database File Location";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 80);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 3;
            label2.Text = "Database File Name";
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(12, 107);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(210, 23);
            txtFileName.TabIndex = 4;
            txtFileName.Text = "TopSecret.db";
            // 
            // btnCreateDatabaseFile
            // 
            btnCreateDatabaseFile.Location = new Point(12, 155);
            btnCreateDatabaseFile.Name = "btnCreateDatabaseFile";
            btnCreateDatabaseFile.Size = new Size(75, 23);
            btnCreateDatabaseFile.TabIndex = 5;
            btnCreateDatabaseFile.Text = "Create";
            btnCreateDatabaseFile.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(147, 155);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 7;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 197);
            ControlBox = false;
            Controls.Add(btnExit);
            Controls.Add(btnCreateDatabaseFile);
            Controls.Add(txtFileName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtFolderPath);
            Controls.Add(btnSelectFolder);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetupForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Top Secret Setup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelectFolder;
        private TextBox txtFolderPath;
        private Label label1;
        private Label label2;
        private TextBox txtFileName;
        private Button btnCreateDatabaseFile;
        private Button btnExit;
    }
}
