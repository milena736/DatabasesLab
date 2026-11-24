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
    }

    // ---------------------------------------------------------------
    // Part C: show it
    // ---------------------------------------------------------------

    // TODO Part C (demo): refill ItemsList and CountText from the database.
    private void ShowItems()
    {
        throw new NotImplementedException("TODO Part C: ShowItems");
    }

    // TODO Part C (demo): save what was typed, then clear the boxes and refresh the list.
    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException("TODO Part C: AddButton_Click");
    }

    // TODO Part C (demo): claim the selected item, or explain why it cannot.
    private void ClaimButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException("TODO Part C: ClaimButton_Click");
    }

    // TODO Exercise 3: show only the items that match the search box.
    private void SearchBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        throw new NotImplementedException("TODO Exercise 3: SearchBox_TextChanged");
    }
}
