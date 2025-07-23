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
        secretsDataGridView.RowEnter += secretsDataGridView_RowEnter;
        secretsDataGridView.CellClick += secretsDataGridView_CellClick;
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
            UseColumnTextForButtonValue = false
        };
        secretsDataGridView.Columns.Add(addButtonColumn);

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
    /// Handles the cell click event for the secrets data grid view, allowing for the insertion or update of secret
    /// entries.
    /// </summary>
    /// <remarks>If the clicked cell is in the "AddButton" column and the first cell of the row is empty, a
    /// new secret is inserted into the database. Otherwise, if the clicked cell is in the "AddButton" column, the
    /// existing secret in the row is updated in the database. The method refreshes the grid and sets the current cell
    /// to the new row for editing after an insertion.</remarks>
    /// <param name="sender">The source of the event, typically the secrets data grid view.</param>
    /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data, including the index of the
    /// clicked cell.</param>
    private async void secretsDataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (Settings == null)
            return;

        if (e.ColumnIndex == secretsDataGridView.Columns["AddButton"].Index &&
            secretsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
        {
            // Insert a new secret into the database
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
        else if (e.ColumnIndex == secretsDataGridView.Columns["AddButton"].Index)
        {
            // Update the secret in the database
            var row = secretsDataGridView.Rows[e.RowIndex];
            var secret = new Secret
            {
                Id = (int?)row.Cells["idDataGridViewTextBoxColumn"].Value,
                Username = row.Cells["usernameDataGridViewTextBoxColumn"].Value?.ToString(),
                Password = row.Cells["passwordDataGridViewTextBoxColumn"].Value?.ToString(),
                Description = row.Cells["descriptionDataGridViewTextBoxColumn"].Value?.ToString(),
                Notes = row.Cells["notesDataGridViewTextBoxColumn"].Value?.ToString()
            };

            var secretsDb = new SecretsDb(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
            await secretsDb.UpdateSecretAsync(secret);

            // Refresh grid
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            Secrets.Add(new Secret()); // Add a new empty Secret to the list
            UpdateInfoControls();
        }
    }

    /// <summary>
    /// Customizes the painting of cells in the DataGridView, specifically handling the appearance of the "AddButton"
    /// column.
    /// </summary>
    /// <remarks>This method alters the default painting behavior for cells in the "AddButton" column.  If the
    /// cell is not in edit mode, it paints the background and prevents the default button drawing. If the cell is in
    /// edit mode, it sets the cell's value to "Save" or "Add" based on the presence of an ID in the first cell of the
    /// row.</remarks>
    /// <param name="sender">The source of the event, typically the DataGridView.</param>
    /// <param name="e">A <see cref="DataGridViewCellPaintingEventArgs"/> that contains the event data.</param>
    private void secretsDataGridView_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
            return;

        bool isAddButtonColumn = secretsDataGridView.Columns[e.ColumnIndex].Name == "AddButton";
        bool hasId = secretsDataGridView.Rows[e.RowIndex].Cells[0].Value != null;
        bool noCellInEdit = secretsDataGridView.CurrentCell == null || secretsDataGridView.CurrentCell.RowIndex != e.RowIndex;

        if (isAddButtonColumn && noCellInEdit)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Handled = true; // Prevents the default button drawing
        }
        else if (isAddButtonColumn)
        {
            secretsDataGridView.Rows[e.RowIndex].Cells["AddButton"].Value = hasId ? "Save" : "Add";
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            e.Handled = true; // Prevents the default button drawing
        }
    }

    /// <summary>
    /// Handles the RowEnter event of the secretsDataGridView control.
    /// </summary>
    /// <remarks>This method forces a repaint of the secretsDataGridView to update the appearance of buttons
    /// when a new row is entered.</remarks>
    /// <param name="sender">The source of the event, typically the secretsDataGridView control.</param>
    /// <param name="e">A DataGridViewCellEventArgs that contains the event data.</param>
    private void secretsDataGridView_RowEnter(object? sender, DataGridViewCellEventArgs e)
    {
        secretsDataGridView.Invalidate(); // Force a repaint to update the button appearance
    }
}
