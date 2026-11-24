using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DatabasesLab;

/// <summary>
/// The lost and found desk. The window asks the database and shows what it says.
/// It never keeps its own copy of the items.
/// </summary>
public partial class MainWindow : Window
{
    private readonly LostAndFoundDb db;

    /// <summary>Opens the desk on lostandfound.db, next to the program. (Given)</summary>
    public MainWindow() : this("lostandfound.db")
    {
    }

    /// <summary>Opens the desk on any database file. The lab check uses this one. (Given, plus Part C)</summary>
    public MainWindow(string databasePath)
    {
        InitializeComponent();
        db = new LostAndFoundDb(databasePath);

        // TODO Part C (demo): create the table, then show the items.
        db.CreateTable();
        ShowItems();
    }

    // ---------------------------------------------------------------
    // Part C: show it
    // ---------------------------------------------------------------

    // TODO Part C (demo): refill ItemsList and CountText from the database.
    private void ShowItems()
    {
        ItemsList.Items.Clear();
        foreach (FoundItem item in db.Search(SearchBox.Text ?? ""))
        {
            ItemsList.Items.Add(item);
        }
        CountText.Text = "Waiting to be claimed: " + db.Count();
    }

    // TODO Part C (demo): save what was typed, then clear the boxes and refresh the list.
    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            FoundItem item = db.Add(DescriptionBox.Text ?? "", 
                             LocationBox.Text ?? "");
            DescriptionBox.Text = "";
            LocationBox.Text = "";
            StatusText.Text = "Added: " + item;
            ShowItems();
        }
        catch (ArgumentException ex)
        {
            StatusText.Text = ex.Message;
        }
    }

    // TODO Part C (demo): claim the selected item, or explain why it cannot.
    private void ClaimButton_Click(object? sender, RoutedEventArgs e)
    {
        FoundItem? item = ItemsList.SelectedItem as FoundItem;

        if (item == null)
        {
            StatusText.Text = "Select an item to claim.";
        }
        else if (db.Claim(item.Id))
        {
            StatusText.Text = "Claimed: " + item;
            ShowItems();
        }
        else
        {
            StatusText.Text = "That item was already claimed.";
            ShowItems();
        }
    }

    // TODO Exercise 3: show only the items that match the search box.
    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        ShowItems();
    }
}
