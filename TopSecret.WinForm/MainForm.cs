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
        secretsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        secretsDataGridView.MultiSelect = false;

        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
        secretsDataGridView.CellPainting += secretsDataGridView_CellPainting;
        secretsDataGridView.RowEnter += secretsDataGridView_RowEnter;
        secretsDataGridView.CellClick += secretsDataGridView_CellClick;
    }

    /// <summary>
    /// Updates the user interface with the current data from the settings and secrets.
    /// </summary>
    /// <remarks>This method refreshes the display text and record count based on the current settings and
    /// secrets data. It also updates the data source for the secrets data grid view.</remarks>
    public void RefreshUiWithCurrentData()
    {
        this.Text = Settings?.DatabaseFileName != null ? $"Top Secret - {Settings.DatabaseFileName}" : "Top Secret";
        txtRecordCount.Text = Secrets.Count(s => s.Id != null).ToString();

        secretsBindingSource.DataSource = Secrets;
        secretsDataGridView.DataSource = secretsBindingSource;
    }

    /// <summary>
    /// Handles the loading event of the form, initializing UI components and loading application settings.
    /// </summary>
    /// <remarks>This method adds button columns to the DataGridView for adding and deleting items. It
    /// attempts to load application settings from a JSON file and initializes the database connection to retrieve and
    /// display secrets. If the settings file is missing, a setup form is displayed. Errors during database access are
    /// reported to the user via message boxes.</remarks>
    /// <param name="e"></param>
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

        var deleteButtonColumn = new DataGridViewButtonColumn
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
            Name = "DeleteButton",
            Text = "Delete",
            UseColumnTextForButtonValue = false
        };
        secretsDataGridView.Columns.Add(deleteButtonColumn);

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

        SecretsDataProvider secretsDb = new(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
        try
        {
            DatabaseInfo = await secretsDb.GetDatabaseInfoAsync();
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            Secrets.Add(new Secret()); // Add a new empty Secret to the list

            RefreshUiWithCurrentData();
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
    /// Handles the Click event of the Exit menu item, terminating the application.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit menu item.</param>
    /// <param name="e">The event data associated with the Click event.</param>
    private void exitToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Application.Exit(); // Exits the application gracefully regardless of open forms
    }

    /// <summary>
    /// Handles the cell click event for the secrets data grid view, allowing for the addition, update, or deletion of
    /// secrets.
    /// </summary>
    /// <remarks>This method performs different actions based on the column of the clicked cell: <list
    /// type="bullet"> <item><description>If the "AddButton" column is clicked and the row is new, a new secret is
    /// inserted into the database.</description></item> <item><description>If the "AddButton" column is clicked and the
    /// row is existing, the secret is updated in the database.</description></item> <item><description>If the
    /// "DeleteButton" column is clicked, the secret is deleted from the database if it exists.</description></item>
    /// </list> The method refreshes the data grid view after each operation to reflect the current state of the
    /// database.</remarks>
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

            var secretsDb = new SecretsDataProvider(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
            await secretsDb.InsertSecretAsync(secret);

            // Refresh grid
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            Secrets.Add(new Secret()); // Add a new empty Secret to the list
            RefreshUiWithCurrentData();

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

            var secretsDb = new SecretsDataProvider(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
            await secretsDb.UpdateSecretAsync(secret);

            // Refresh grid
            Secrets = [.. await secretsDb.GetAllSecretsAsync()];
            Secrets.Add(new Secret()); // Add a new empty Secret to the list
            RefreshUiWithCurrentData();
        }

        if (e.ColumnIndex == secretsDataGridView.Columns["DeleteButton"].Index && 
            secretsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
        {
            secretsDataGridView.Rows[e.RowIndex].Cells["descriptionDataGridViewTextBoxColumn"].Value = string.Empty;
            secretsDataGridView.Rows[e.RowIndex].Cells["usernameDataGridViewTextBoxColumn"].Value = string.Empty;
            secretsDataGridView.Rows[e.RowIndex].Cells["passwordDataGridViewTextBoxColumn"].Value = string.Empty;
            secretsDataGridView.Rows[e.RowIndex].Cells["notesDataGridViewTextBoxColumn"].Value = string.Empty;
        }
        else if (e.ColumnIndex == secretsDataGridView.Columns["DeleteButton"].Index)
        {
            // Delete the secret from the database
            var row = secretsDataGridView.Rows[e.RowIndex];
            var secretId = (int?)row.Cells["idDataGridViewTextBoxColumn"].Value;
            if (secretId.HasValue)
            {
                if (MessageBox.Show("Are you sure you want to delete this secret?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                var secretsDb = new SecretsDataProvider(Settings.DatabaseFileLocation, Settings.DatabaseFileName);
                await secretsDb.DeleteSecretAsync(secretId.Value);
                
                // Refresh grid
                Secrets = [.. await secretsDb.GetAllSecretsAsync()];
                Secrets.Add(new Secret()); // Add a new empty Secret to the list
                RefreshUiWithCurrentData();
            }
        }
    }

    /// <summary>
    /// Handles the painting of cells in the DataGridView, customizing the appearance of "Add" and "Delete" button
    /// columns.
    /// </summary>
    /// <remarks>This method customizes the painting of cells in the "AddButton" and "DeleteButton" columns of
    /// the DataGridView. It changes the button text based on the presence of an ID in the first cell of the row and
    /// prevents the default button drawing when the cell is not in edit mode.</remarks>
    /// <param name="sender">The source of the event, typically the DataGridView.</param>
    /// <param name="e">A <see cref="DataGridViewCellPaintingEventArgs"/> that contains the event data.</param>
    private void secretsDataGridView_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
            return;

        bool isAddButtonColumn = secretsDataGridView.Columns[e.ColumnIndex].Name == "AddButton";
        bool isDeleteButtonColumn = secretsDataGridView.Columns[e.ColumnIndex].Name == "DeleteButton";
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

        if (isDeleteButtonColumn && noCellInEdit)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Handled = true; // Prevents the default button drawing
        }
        else if (isDeleteButtonColumn)
        {
            secretsDataGridView.Rows[e.RowIndex].Cells["DeleteButton"].Value = hasId ? "Delete" : "Clear";
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

    /// <summary>
    /// Opens the settings dialog for the application.
    /// </summary>
    /// <param name="sender">The source of the event, typically the settings menu item.</param>
    /// <param name="e">The event data associated with the click event.</param>
    private void settingsToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        SettingsForm settingsForm = new()
        {
            Settings = Settings,
            DatabaseInfo = DatabaseInfo
        };
        settingsForm.ShowDialog(this);
    }
}
