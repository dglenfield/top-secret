namespace TopSecret.WinForm
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
            label1 = new Label();
            label2 = new Label();
            txtFileLocation = new TextBox();
            txtFileName = new TextBox();
            txtVersion = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 21);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 0;
            label1.Text = "Database File Location";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 59);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 1;
            label2.Text = "Database File Name";
            // 
            // txtFileLocation
            // 
            txtFileLocation.Location = new Point(148, 18);
            txtFileLocation.Name = "txtFileLocation";
            txtFileLocation.Size = new Size(432, 23);
            txtFileLocation.TabIndex = 2;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(148, 56);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(235, 23);
            txtFileName.TabIndex = 3;
            // 
            // txtVersion
            // 
            txtVersion.Location = new Point(148, 95);
            txtVersion.Name = "txtVersion";
            txtVersion.Size = new Size(100, 23);
            txtVersion.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 98);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 5;
            label3.Text = "Database Version";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(txtVersion);
            Controls.Add(txtFileName);
            Controls.Add(txtFileLocation);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Top Secret";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtFileLocation;
        private TextBox txtFileName;
        private TextBox txtVersion;
        private Label label3;
    }
}