using TopSecret.Common.Data;
using TopSecret.Common.Models;

namespace TopSecret.WinForm;

public partial class MainForm : Form
{
    public ApplicationSettings? Settings { get; set; }
    public DatabaseInfo? DatabaseInfo { get; set; }
    public List<Secret> Secrets { get; set; } = [];

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

        txtRecordCount.Text = Secrets.Count.ToString();
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
            this.Text = $"Top Secret - {Settings.DatabaseFileName}";
            txtFileLocation.Text = Settings.DatabaseFileLocation;
            txtFileName.Text = Settings.DatabaseFileName;
        }
        else
        {
            this.Text = "Top Secret";
            txtFileLocation.Text = string.Empty;
            txtFileName.Text = string.Empty;
        }
    }

    /// <summary>
    /// Handles the loading process for the form, initializing application settings and database information.
    /// </summary>
    /// <remarks>This method performs the following steps: <list type="bullet"> <item> Checks for the
    /// existence of the application settings file. If the file is missing, it displays a setup form to the user.
    /// </item> <item> Attempts to load the application settings asynchronously. If the settings file is not found or
    /// cannot be loaded, an error message is displayed. </item> <item> If settings are successfully loaded, initializes
    /// the database connection and retrieves database information and secrets asynchronously.  If an error occurs
    /// during database operations, an error message is displayed. </item> </list> This method overrides <see
    /// cref="Control.OnLoad"/> and is invoked when the form is loaded.</remarks>
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

        if (Settings == null)
            return;

        SecretsDb secretsDb = new(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
        try
        {
            DatabaseInfo = await secretsDb.GetDatabaseInfoAsync();
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            UpdateDatabaseInfoControls();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error querying database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }
}
