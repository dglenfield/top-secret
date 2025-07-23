using TopSecret.Common.Data;
using TopSecret.Common.Logging;
using TopSecret.Common.Models;

namespace TopSecret.WinForm;

public partial class MainForm : Form
{
    public ApplicationSettings? Settings { get; set; }
    public DatabaseInfo? DatabaseInfo { get; set; }
    public List<Secret> Secrets { get; set; } = [];

    private FileLogger _logger = new();

    public MainForm()
    {
        InitializeComponent();

        secretsDataGridView.AllowUserToAddRows = false;
        secretsDataGridView.CellPainting += secretsDataGridView_CellPainting;
    }

    /// <summary>
    /// Updates the information controls with the current settings and database information.
    /// </summary>
    /// <remarks>This method sets the text of various UI controls to reflect the current state of the
    /// application settings and database information. It updates the file location, file name, version, and record
    /// count fields, and binds the secrets data to the data grid view.</remarks>
    public void UpdateInfoControls()
    {
        this.Text = Settings?.DatabaseFileName != null ? $"Top Secret - {Settings.DatabaseFileName}" : "Top Secret";
        txtFileLocation.Text = Settings?.DatabaseFileLocation ?? string.Empty;
        txtFileName.Text = Settings?.DatabaseFileName ?? string.Empty;
        txtVersion.Text = DatabaseInfo?.Version.ToString() ?? string.Empty;
        txtRecordCount.Text = Secrets.Count(s => s.Id != null).ToString();

        secretsBindingSource.DataSource = Secrets;
        secretsDataGridView.DataSource = secretsBindingSource;
    }

    /// <summary>
    /// Handles the loading event of the form, initializing UI components and loading application settings.
    /// </summary>
    /// <remarks>This method adds a button column to the DataGridView and sets up an event handler for cell
    /// clicks. It attempts to load application settings from a JSON file. If the settings file is not found, a setup
    /// form is displayed. Upon successful loading of settings, it initializes the database connection and retrieves
    /// secrets, updating the UI accordingly. Displays error messages for file not found or database query
    /// errors.</remarks>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        var addButtonColumn = new DataGridViewButtonColumn
        {
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.Silver,
                ForeColor = Color.Silver,
                SelectionBackColor = Color.Silver,
                SelectionForeColor = Color.Silver
            },
            HeaderText = string.Empty,
            Name = "AddButton",
            Text = "Add",
            UseColumnTextForButtonValue = true
        };
        secretsDataGridView.Columns.Add(addButtonColumn);
        secretsDataGridView.CellClick += secretsDataGridView_CellClick;    

        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            SetupForm setupForm = new();
            setupForm.ShowDialog(this);
            return;
        }

        try
        {
            Settings = await ApplicationSettings.LoadAsync(settingsPath);
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
            Secrets.Add(new Secret()); // Add a new empty Secret to the list

            UpdateInfoControls();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error querying database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Set the current cell to the new row for editing
        secretsDataGridView.Focus();
        secretsDataGridView.CurrentCell = secretsDataGridView.Rows[0].Cells[0];
    }

    /// <summary>
    /// Handles the cell click event for the secrets data grid view, allowing the user to add a new secret entry.
    /// </summary>
    /// <remarks>This method checks if the clicked cell is in the "AddButton" column and if the first cell of
    /// the row is empty. If so, it creates a new <see cref="Secret"/> object from the row's data, inserts it into the
    /// database asynchronously, and refreshes the data grid view to include the new entry. The current cell is then set
    /// to the new row for editing.</remarks>
    /// <param name="sender">The source of the event, typically the secrets data grid view.</param>
    /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
    private async void secretsDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (Settings == null)
            return;

        if (e.ColumnIndex == secretsDataGridView.Columns["AddButton"].Index && 
            secretsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
        {
            var row = secretsDataGridView.Rows[e.RowIndex];
            var secret = new Secret
            {
                Username = row.Cells["usernameDataGridViewTextBoxColumn"].Value?.ToString(),
                Password = row.Cells["passwordDataGridViewTextBoxColumn"].Value?.ToString(),
                Description = row.Cells["descriptionDataGridViewTextBoxColumn"].Value?.ToString(),
                Notes = row.Cells["notesDataGridViewTextBoxColumn"].Value?.ToString()
            };

            var secretsDb = new SecretsDb(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
            await secretsDb.InsertSecretAsync(secret);

            // Refresh grid
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            Secrets.Add(new Secret()); // Add a new empty Secret to the list
            UpdateInfoControls();

            // Set the current cell to the new row for editing
            secretsDataGridView.CurrentCell = secretsDataGridView.Rows[secretsDataGridView.Rows.Count - 1].Cells[1];
        }
    }

    /// <summary>
    /// Handles the cell painting event for the secrets data grid view to customize the appearance of specific cells.
    /// </summary>
    /// <remarks>This method customizes the painting of cells in the "AddButton" column. If the cell in the
    /// first column of the current row is not null, it paints the background of the cell and prevents the default
    /// button drawing.</remarks>
    /// <param name="sender">The source of the event, typically the secrets data grid view.</param>
    /// <param name="e">A <see cref="DataGridViewCellPaintingEventArgs"/> that contains the event data.</param>
    private void secretsDataGridView_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
            return;

        if (secretsDataGridView.Columns[e.ColumnIndex].Name == "AddButton" && 
            secretsDataGridView.Rows[e.RowIndex].Cells[0].Value != null)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Handled = true; // Prevents the button from being drawn
        }
    }
}
