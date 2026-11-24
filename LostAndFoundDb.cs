using Microsoft.Data.Sqlite;

namespace DatabasesLab;

/// <summary>
/// Saves and loads found items in a SQLite database file.
/// Every method opens its own connection and closes it before it returns.
/// </summary>
public class LostAndFoundDb
{
    private readonly string connectionString;

    /// <summary>Remembers which database file to use. Nothing is opened yet. (Given)</summary>
    public LostAndFoundDb(string path)
    {
        connectionString = "Data Source=" + path;
    }

    // ---------------------------------------------------------------
    // Part A: save it
    // ---------------------------------------------------------------

    // TODO Part A (demo): open a new connection to connectionString and return it.
    private SqliteConnection Open()
    {
        throw new NotImplementedException("TODO Part A: Open");
    }

    // TODO Part A (demo): create the items table if the file does not have it yet.
    public void CreateTable()
    {
        throw new NotImplementedException("TODO Part A: CreateTable");
    }

    // TODO Part A (demo): save a new item and return it with the id the database assigned.
    public FoundItem Add(string description, string location)
    {
        throw new NotImplementedException("TODO Part A: Add");
    }

    // TODO Exercise 1: delete the item with this id. Return true when a row was deleted.
    public bool Claim(int id)
    {
        throw new NotImplementedException("TODO Exercise 1: Claim");
    }

    // ---------------------------------------------------------------
    // Part B: read it back
    // ---------------------------------------------------------------

    // TODO Part B (demo): run the command's SELECT and turn every row into a FoundItem.
    private static List<FoundItem> ReadItems(SqliteCommand command)
    {
        throw new NotImplementedException("TODO Part B: ReadItems");
    }

    // TODO Part B (demo): return every item, oldest first.
    public List<FoundItem> All()
    {
        throw new NotImplementedException("TODO Part B: All");
    }

    // TODO Part B (demo): return how many items are waiting to be claimed.
    public int Count()
    {
        throw new NotImplementedException("TODO Part B: Count");
    }

    // TODO Exercise 2: return items whose description or location contains the word, oldest first.
    public List<FoundItem> Search(string word)
    {
        throw new NotImplementedException("TODO Exercise 2: Search");
    }
}
