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

    /// <summary>
    /// Handles the click event for the "Create Database File" button, initiating the process of creating a new database
    /// file.
    /// </summary>
    /// <remarks>This method validates the input for the database file name and prompts the user for
    /// confirmation before proceeding. It saves the application settings to a configuration file and creates a new
    /// database file at the specified location. If the operation is successful, it updates the main form with the new
    /// database information and secrets.</remarks>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void btnCreateDatabaseFile_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFileName.Text))
        {
            MessageBox.Show("Please specify the database file name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show($"Error saving settings: {task.Exception?.GetBaseException().Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"** ERROR SAVING SETTINGS: {ex.Message} **", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error creating database: {ex.Message} **", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (Owner is MainForm mainForm)
        {
            try
            {
                mainForm.DatabaseInfo = secretsDb.GetDatabaseInfoAsync().Result;
                mainForm.Secrets = [.. secretsDb.GetAllSecretsAsync().Result];
                mainForm.Secrets.Add(new Secret()); // Add a new empty Secret to the list
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error querying database: {ex.Message} **", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            mainForm.Settings = settings;
            mainForm.UpdateInfoControls();
        }

        this.Close();
    }

    /// <summary>
    /// Handles the click event for the "Select Folder" button, allowing the user to choose a folder.
    /// </summary>
    /// <remarks>Opens a folder browser dialog for the user to select a folder. If a folder is selected,  the
    /// selected path is displayed in the associated text box.</remarks>
    /// <param name="sender">The source of the event, typically the button that was clicked.</param>
    /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
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
}
