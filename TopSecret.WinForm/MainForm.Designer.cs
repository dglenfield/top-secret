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
            label4 = new Label();
            txtRecordCount = new TextBox();
            secretsDataGridView = new DataGridView();
            secretsBindingSource = new BindingSource(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            idColumn = new DataGridViewTextBoxColumn();
            descriptionColumn = new DataGridViewTextBoxColumn();
            usernameColumn = new DataGridViewTextBoxColumn();
            passwordColumn = new DataGridViewTextBoxColumn();
            notesColumn = new DataGridViewTextBoxColumn();
            createdOnColumn = new DataGridViewTextBoxColumn();
            updatedOnColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)secretsDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)secretsBindingSource).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 39);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 6;
            label4.Text = "Record Count";
            // 
            // txtRecordCount
            // 
            txtRecordCount.Location = new Point(103, 36);
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
            secretsDataGridView.Columns.AddRange(new DataGridViewColumn[] { idColumn, descriptionColumn, usernameColumn, passwordColumn, notesColumn, createdOnColumn, updatedOnColumn });
            secretsDataGridView.DataSource = secretsBindingSource;
            secretsDataGridView.Location = new Point(17, 76);
            secretsDataGridView.Name = "secretsDataGridView";
            secretsDataGridView.Size = new Size(949, 408);
            secretsDataGridView.TabIndex = 8;
            // 
            // secretsBindingSource
            // 
            secretsBindingSource.DataSource = typeof(Common.Models.Secret);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(978, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(125, 22);
            settingsToolStripMenuItem.Text = "Settings...";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(125, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // idColumn
            // 
            idColumn.DataPropertyName = "Id";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Silver;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            idColumn.DefaultCellStyle = dataGridViewCellStyle1;
            idColumn.HeaderText = "Id";
            idColumn.Name = "idColumn";
            idColumn.ReadOnly = true;
            idColumn.Visible = false;
            idColumn.Width = 40;
            // 
            // descriptionColumn
            // 
            descriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            descriptionColumn.DataPropertyName = "Description";
            descriptionColumn.HeaderText = "Description";
            descriptionColumn.MinimumWidth = 100;
            descriptionColumn.Name = "descriptionColumn";
            // 
            // usernameColumn
            // 
            usernameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            usernameColumn.DataPropertyName = "Username";
            usernameColumn.HeaderText = "Username";
            usernameColumn.MinimumWidth = 80;
            usernameColumn.Name = "usernameColumn";
            // 
            // passwordColumn
            // 
            passwordColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            passwordColumn.DataPropertyName = "Password";
            passwordColumn.HeaderText = "Password";
            passwordColumn.MinimumWidth = 80;
            passwordColumn.Name = "passwordColumn";
            // 
            // notesColumn
            // 
            notesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            notesColumn.DataPropertyName = "Notes";
            notesColumn.FillWeight = 200F;
            notesColumn.HeaderText = "Notes";
            notesColumn.MinimumWidth = 100;
            notesColumn.Name = "notesColumn";
            // 
            // createdOnColumn
            // 
            createdOnColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            createdOnColumn.DataPropertyName = "CreatedOn";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.Silver;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            createdOnColumn.DefaultCellStyle = dataGridViewCellStyle2;
            createdOnColumn.HeaderText = "CreatedOn";
            createdOnColumn.Name = "createdOnColumn";
            createdOnColumn.ReadOnly = true;
            createdOnColumn.Width = 120;
            // 
            // updatedOnColumn
            // 
            updatedOnColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updatedOnColumn.DataPropertyName = "UpdatedOn";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.Silver;
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            updatedOnColumn.DefaultCellStyle = dataGridViewCellStyle3;
            updatedOnColumn.HeaderText = "UpdatedOn";
            updatedOnColumn.Name = "updatedOnColumn";
            updatedOnColumn.ReadOnly = true;
            updatedOnColumn.Width = 120;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 496);
            Controls.Add(secretsDataGridView);
            Controls.Add(txtRecordCount);
            Controls.Add(label4);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Top Secret";
            ((System.ComponentModel.ISupportInitialize)secretsDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)secretsBindingSource).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private TextBox txtRecordCount;
        private DataGridView secretsDataGridView;
        private BindingSource secretsBindingSource;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private DataGridViewTextBoxColumn idColumn;
        private DataGridViewTextBoxColumn descriptionColumn;
        private DataGridViewTextBoxColumn usernameColumn;
        private DataGridViewTextBoxColumn passwordColumn;
        private DataGridViewTextBoxColumn notesColumn;
        private DataGridViewTextBoxColumn createdOnColumn;
        private DataGridViewTextBoxColumn updatedOnColumn;
    }
}