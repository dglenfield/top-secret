using TopSecret.Common.Models;

namespace TopSecret.WinForm;

public partial class SettingsForm : Form
{
    public ApplicationSettings? Settings { get; set; }
    public DatabaseInfo? DatabaseInfo { get; set; }

    public SettingsForm()
    {
        InitializeComponent();

        toolTipOpenFolder.SetToolTip(labelOpenFolder, "Open the folder containing the database file");

        buttonClose.Click += (s, e) => Close();
        labelOpenFolder.Click += labelOpenFolder_Click;
    }

    /// <summary>
    /// Handles the load event of the form, initializing UI elements with settings and database information.
    /// </summary>
    /// <remarks>This method sets the text of the file location textbox based on the current settings, and
    /// displays the database version if available.</remarks>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!string.IsNullOrEmpty(Settings?.DatabaseFileLocation) && !string.IsNullOrEmpty(Settings?.DatabaseFileName))
        {
            txtFileLocation.Text = Path.Combine(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
        }
        else
        {
            txtFileLocation.Text = string.Empty;
        }
        txtVersion.Text = DatabaseInfo?.Version.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Handles the click event for the label to open the folder containing the database file.
    /// </summary>
    /// <remarks>This method attempts to open the folder specified by the <see
    /// cref="Settings.DatabaseFileLocation"/> property. If the location is not set, the method exits without performing
    /// any action. An error message is displayed if the folder cannot be opened.</remarks>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void labelOpenFolder_Click(object? sender, EventArgs e)
    {
        if (Settings?.DatabaseFileLocation == null)
            return;

        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = Settings.DatabaseFileLocation,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error opening folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
