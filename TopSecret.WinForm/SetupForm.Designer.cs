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
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(19, 85);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(91, 23);
            btnSelectFolder.TabIndex = 0;
            btnSelectFolder.Text = "Select Folder";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(116, 86);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.Size = new Size(513, 23);
            txtFolderPath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 61);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 2;
            label1.Text = "Database File Location";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 126);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 3;
            label2.Text = "Database File Name";
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(22, 154);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(210, 23);
            txtFileName.TabIndex = 4;
            txtFileName.Text = "TopSecret.db";
            // 
            // btnCreateDatabaseFile
            // 
            btnCreateDatabaseFile.Location = new Point(25, 203);
            btnCreateDatabaseFile.Name = "btnCreateDatabaseFile";
            btnCreateDatabaseFile.Size = new Size(75, 23);
            btnCreateDatabaseFile.TabIndex = 5;
            btnCreateDatabaseFile.Text = "Create";
            btnCreateDatabaseFile.UseVisualStyleBackColor = true;
            btnCreateDatabaseFile.Click += btnCreateDatabaseFile_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(73, 30);
            label3.TabIndex = 6;
            label3.Text = "Setup";
            // 
            // SetupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 450);
            ControlBox = false;
            Controls.Add(label3);
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
            Text = "Top Secret";
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
        private Label label3;
    }
}
