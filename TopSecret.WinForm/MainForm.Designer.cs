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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new Label();
            txtFileLocation = new TextBox();
            txtVersion = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtRecordCount = new TextBox();
            secretsDataGridView = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            usernameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            passwordDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            notesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            createdOnDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            updatedOnDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            secretsBindingSource = new BindingSource(components);
            labelOpenFolder = new Label();
            toolTipOpenFolder = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)secretsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)secretsBindingSource).BeginInit();
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
            // txtFileLocation
            // 
            txtFileLocation.Location = new Point(148, 18);
            txtFileLocation.Name = "txtFileLocation";
            txtFileLocation.ReadOnly = true;
            txtFileLocation.Size = new Size(625, 23);
            txtFileLocation.TabIndex = 2;
            // 
            // txtVersion
            // 
            txtVersion.Location = new Point(919, 18);
            txtVersion.Name = "txtVersion";
            txtVersion.ReadOnly = true;
            txtVersion.Size = new Size(47, 23);
            txtVersion.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(817, 21);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 5;
            label3.Text = "Database Version";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(62, 57);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 6;
            label4.Text = "Record Count";
            // 
            // txtRecordCount
            // 
            txtRecordCount.Location = new Point(148, 54);
            txtRecordCount.Name = "txtRecordCount";
            txtRecordCount.ReadOnly = true;
            txtRecordCount.Size = new Size(100, 23);
            txtRecordCount.TabIndex = 7;
            // 
            // secretsDataGridView
            // 
            secretsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            secretsDataGridView.AutoGenerateColumns = false;
            secretsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            secretsDataGridView.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, usernameDataGridViewTextBoxColumn, passwordDataGridViewTextBoxColumn, notesDataGridViewTextBoxColumn, createdOnDataGridViewTextBoxColumn, updatedOnDataGridViewTextBoxColumn });
            secretsDataGridView.DataSource = secretsBindingSource;
            secretsDataGridView.Location = new Point(17, 100);
            secretsDataGridView.Name = "secretsDataGridView";
            secretsDataGridView.Size = new Size(949, 338);
            secretsDataGridView.TabIndex = 8;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Silver;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Width = 40;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.MinimumWidth = 100;
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            usernameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            usernameDataGridViewTextBoxColumn.DataPropertyName = "Username";
            usernameDataGridViewTextBoxColumn.HeaderText = "Username";
            usernameDataGridViewTextBoxColumn.MinimumWidth = 80;
            usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            passwordDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            passwordDataGridViewTextBoxColumn.MinimumWidth = 80;
            passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            // 
            // notesDataGridViewTextBoxColumn
            // 
            notesDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            notesDataGridViewTextBoxColumn.FillWeight = 200F;
            notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            notesDataGridViewTextBoxColumn.MinimumWidth = 100;
            notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // createdOnDataGridViewTextBoxColumn
            // 
            createdOnDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            createdOnDataGridViewTextBoxColumn.DataPropertyName = "CreatedOn";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.Silver;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            createdOnDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            createdOnDataGridViewTextBoxColumn.HeaderText = "CreatedOn";
            createdOnDataGridViewTextBoxColumn.Name = "createdOnDataGridViewTextBoxColumn";
            createdOnDataGridViewTextBoxColumn.ReadOnly = true;
            createdOnDataGridViewTextBoxColumn.Width = 120;
            // 
            // updatedOnDataGridViewTextBoxColumn
            // 
            updatedOnDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updatedOnDataGridViewTextBoxColumn.DataPropertyName = "UpdatedOn";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.Silver;
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            updatedOnDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            updatedOnDataGridViewTextBoxColumn.HeaderText = "UpdatedOn";
            updatedOnDataGridViewTextBoxColumn.Name = "updatedOnDataGridViewTextBoxColumn";
            updatedOnDataGridViewTextBoxColumn.ReadOnly = true;
            updatedOnDataGridViewTextBoxColumn.Width = 120;
            // 
            // secretsBindingSource
            // 
            secretsBindingSource.DataSource = typeof(Common.Models.Secret);
            // 
            // labelOpenFolder
            // 
            labelOpenFolder.Cursor = Cursors.Hand;
            labelOpenFolder.Image = (Image)resources.GetObject("labelOpenFolder.Image");
            labelOpenFolder.Location = new Point(779, 15);
            labelOpenFolder.Name = "labelOpenFolder";
            labelOpenFolder.Size = new Size(26, 26);
            labelOpenFolder.TabIndex = 9;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 450);
            Controls.Add(labelOpenFolder);
            Controls.Add(secretsDataGridView);
            Controls.Add(txtRecordCount);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtVersion);
            Controls.Add(txtFileLocation);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Top Secret";
            ((System.ComponentModel.ISupportInitialize)secretsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)secretsBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtFileLocation;
        private TextBox txtVersion;
        private Label label3;
        private Label label4;
        private TextBox txtRecordCount;
        private DataGridView secretsDataGridView;
        private BindingSource secretsBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn createdOnDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn updatedOnDataGridViewTextBoxColumn;
        private Label labelOpenFolder;
        private ToolTip toolTipOpenFolder;
    }
}