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
        SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection;
    }

    // TODO Part A (demo): create the items table if the file does not have it yet.
    public void CreateTable()
    {
        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS items (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                description TEXT NOT NULL,
                location TEXT NOT NULL
            )
            """;
            
        command.ExecuteNonQuery();
    }

    // TODO Part A (demo): save a new item and return it with the id the database assigned.
    public FoundItem Add(string description, string location)
    {
        FoundItem draft = new FoundItem(0, description, location);

        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO items (description, location) 
            VALUES (@description, @location);
            RETURNING id
            """;
        command.Parameters.AddWithValue("@description", draft.Description);
        command.Parameters.AddWithValue("@location", draft.Location);

        int id = Convert.ToInt32(command.ExecuteScalar());
        return new FoundItem(id, draft.Description, draft.Location);
    }

    // TODO Exercise 1: delete the item with this id. Return true when a row was deleted.
    public bool Claim(int id)
    {
        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM items WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        int rowsDeleted = command.ExecuteNonQuery();
        return rowsDeleted == 1;    
    }

    // ---------------------------------------------------------------
    // Part B: read it back
    // ---------------------------------------------------------------

    // TODO Part B (demo): run the command's SELECT and turn every row into a FoundItem.
    private static List<FoundItem> ReadItems(SqliteCommand command)
    {
        List<FoundItem> items = new List<FoundItem>();
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string description = reader.GetString(1);
            string location = reader.GetString(2);
            items.Add(new FoundItem(id, description, location));
        }
        return items;
    }

    // TODO Part B (demo): return every item, oldest first.
    public List<FoundItem> All()
    {
        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, description, location 
            FROM items 
            ORDER BY id
            """;
            
        return ReadItems(command);
    }

    // TODO Part B (demo): return how many items are waiting to be claimed.
    public int Count()
    {
        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            SELECT COUNT(*) 
            FROM items
            """;
            
        return Convert.ToInt32(command.ExecuteScalar());
    }

    // TODO Exercise 2: return items whose description or location contains the word, oldest first.
    public List<FoundItem> Search(string word)
    {
        using SqliteConnection connection = Open();
        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, description, location 
            FROM items 
            WHERE description LIKE @pattern OR location LIKE @pattern
            ORDER BY id
            """;
        command.Parameters.AddWithValue("@pattern", "%"+ word.Trim() + "%");

        return ReadItems(command);
    }
}
