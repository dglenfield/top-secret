using TopSecret.Common.Data;
using TopSecret.Common.Models;

namespace TopSecret.WinForm;

public partial class MainForm : Form
{
    public ApplicationSettings? Settings { get; set; }
    public DatabaseInfo? DatabaseInfo { get; set; }

    public MainForm()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Updates the database information controls with the current database version.
    /// </summary>
    /// <remarks>If the <see cref="DatabaseInfo"/> property is not null, the version information  is displayed
    /// in the associated text control. Otherwise, the text control is cleared.</remarks>
    public void UpdateDatabaseInfoControls()
    {
        if (DatabaseInfo != null)
        {
            txtVersion.Text = DatabaseInfo.Version.ToString();
        }
        else
        {
            txtVersion.Text = string.Empty;
        }
    }

    /// <summary>
    /// Updates the settings controls to reflect the current values of the <see cref="Settings"/> object.
    /// </summary>
    /// <remarks>If the <see cref="Settings"/> object is not null, the method populates the controls with the 
    /// database file location and file name from the <see cref="Settings"/> object. If <see cref="Settings"/>  is null,
    /// the controls are cleared.</remarks>
    public void UpdateSettingsControls()
    {
        if (Settings != null)
        {
            txtFileLocation.Text = Settings.DatabaseFileLocation;
            txtFileName.Text = Settings.DatabaseFileName;
        }
        else
        {
            txtFileLocation.Text = string.Empty;
            txtFileName.Text = string.Empty;
        }
    }

    /// <summary>
    /// Handles the load event of the form, initializing application settings or prompting the user to configure them if
    /// missing.
    /// </summary>
    /// <remarks>This method checks for the existence of the application settings file. If the file is
    /// missing,  it displays a setup dialog to the user. If the file exists, it attempts to load the settings
    /// asynchronously  and updates the form controls accordingly. If the settings file cannot be found during the
    /// loading process,  an error message is displayed to the user.</remarks>
    /// <param name="e">An <see cref="EventArgs"/> object containing the event data.</param>
    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            var setupForm = new SetupForm();
            setupForm.ShowDialog(this);
            return;
        }

        try
        {
            Settings = await ApplicationSettings.LoadAsync(settingsPath);
            UpdateSettingsControls();
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show($"Settings file not found: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (Settings != null)
        {
            SecretsDb secretsDb = new(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
            try
            {
                DatabaseInfo = await secretsDb.GetDatabaseInfoAsync();
                UpdateDatabaseInfoControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load database info: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
