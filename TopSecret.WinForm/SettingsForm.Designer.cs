namespace TopSecret.WinForm
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            labelOpenFolder = new Label();
            label3 = new Label();
            txtVersion = new TextBox();
            txtFileLocation = new TextBox();
            label1 = new Label();
            buttonClose = new Button();
            toolTipOpenFolder = new ToolTip(components);
            SuspendLayout();
            // 
            // labelOpenFolder
            // 
            labelOpenFolder.Cursor = Cursors.Hand;
            labelOpenFolder.Image = (Image)resources.GetObject("labelOpenFolder.Image");
            labelOpenFolder.Location = new Point(770, 9);
            labelOpenFolder.Name = "labelOpenFolder";
            labelOpenFolder.Size = new Size(26, 26);
            labelOpenFolder.TabIndex = 14;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 53);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 13;
            label3.Text = "Database Version";
            // 
            // txtVersion
            // 
            txtVersion.Location = new Point(139, 50);
            txtVersion.Name = "txtVersion";
            txtVersion.ReadOnly = true;
            txtVersion.Size = new Size(47, 23);
            txtVersion.TabIndex = 12;
            // 
            // txtFileLocation
            // 
            txtFileLocation.Location = new Point(139, 12);
            txtFileLocation.Name = "txtFileLocation";
            txtFileLocation.ReadOnly = true;
            txtFileLocation.Size = new Size(625, 23);
            txtFileLocation.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 15);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 10;
            label1.Text = "Database File Location";
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(342, 102);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 23);
            buttonClose.TabIndex = 15;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 137);
            Controls.Add(buttonClose);
            Controls.Add(labelOpenFolder);
            Controls.Add(label3);
            Controls.Add(txtVersion);
            Controls.Add(txtFileLocation);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelOpenFolder;
        private Label label3;
        private TextBox txtVersion;
        private TextBox txtFileLocation;
        private Label label1;
        private Button buttonClose;
        private ToolTip toolTipOpenFolder;
    }
}