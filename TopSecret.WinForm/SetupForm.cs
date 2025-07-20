using TopSecret.Common.Data;
using TopSecret.Common.Models;

namespace TopSecret.WinForm;

public partial class SetupForm : Form
{
    public SetupForm()
    {
        InitializeComponent();

        txtFolderPath.Text = AppContext.BaseDirectory;
    }

    private void btnSelectFolder_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog folderDialog = new() 
        {
            Description = "Select a folder to save the database file",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            UseDescriptionForTitle = true
        };

        if (folderDialog.ShowDialog() == DialogResult.OK)
        {
            txtFolderPath.Text = folderDialog.SelectedPath;
        }
    }

    private void btnCreateDatabaseFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFileName.Text))
        {
            MessageBox.Show("Please specify the database file name.", "Input Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.ActiveControl = txtFileName;
            return;
        }

        ApplicationSettings settings = new()
        {
            DatabaseFileLocation = txtFolderPath.Text,
            DatabaseFileName = txtFileName.Text
        };

        string databaseFilePath = Path.Combine(settings.DatabaseFileLocation, settings.DatabaseFileName);

        var result = MessageBox.Show($"Create the database file in this location?\n{databaseFilePath}", "Confirmation", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.No)
            return;

        // Save the settings to appsettings.json
        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        try
        {
            settings.SaveAsync(settingsPath).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show($"Error saving settings: {task.Exception?.GetBaseException().Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"** ERROR SAVING SETTINGS: {ex.Message} **", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Create the database
        SecretsDb secretsDb = new(settings.DatabaseFileLocation, settings.DatabaseFileName);
        try
        {
            secretsDb.CreateAsync().ContinueWith(createTask =>
            {
                if (createTask.IsFaulted)
                {
                    MessageBox.Show($"Error creating database: {createTask.Exception?.GetBaseException().Message}", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Database created successfully.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"** ERROR CREATING DATABASE: {ex.Message} **", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
