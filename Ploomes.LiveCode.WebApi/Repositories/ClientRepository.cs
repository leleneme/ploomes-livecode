using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Ploomes.LiveCode.WebApi.Repositories;

public class SqlClientRepository : IClientRepository
{
    private IDatabase _db;

    public SqlClientRepository(IDatabase db)
    {
        _db = db;
    }

    public async Task<bool> CreateAsync(ClientCreateDto data)
    {
        string query =
            """
            INSERT INTO cliente(user_id, name)
            VALUES(@user_id, @name)
            """;

        try
        {
            int result = await _db.ExecuteNonQueryAsync(query, ("@user_id", data.UserId), ("@name", data.Name));
            return result > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string query =
            """
            DELETE FROM cliente
            WHERE id = @id
            """;

        int result = await _db.ExecuteNonQueryAsync(query, ("@id", id));
        return result > 0;
    }

    public async Task<IEnumerable<Client>> FetchAllAsync()
    {
        string query = "SELECT * FROM cliente";

        List<Client> clients = await _db.ExecuteQueryAsync(query, reader =>
        {
            List<Client> clients = new();

            while (reader.Read())
                clients.Add(ClientFromReader(reader));

            return clients;
        });

        return clients;
    }

    public async Task<IEnumerable<Client>> GetAllFromUser(int userId)
    {
        string query =
            """
            SELECT * FROM cliente
            WHERE user_id = @user_id
            """;

        List<Client> clients = await _db.ExecuteQueryAsync(query, reader =>
        {
            List<Client> clients = new();

            while (reader.Read())
                clients.Add(ClientFromReader(reader));

            return clients;
        }, ("@user_id", userId));

        return clients;
    }

    public async Task<Client?> GetOneAsync(int id)
    {
        string query =
            """
            SELECT * FROM cliente
            WHERE id = @id
            """;

        Client? client = await _db.ExecuteQueryAsync(query, reader =>
        {
            if (!reader.Read()) return null;
            else return ClientFromReader(reader);
        }, ("@id", id));

        return client;
    }

    private Client ClientFromReader(IDataReader reader)
    {
        return new Client
        {
            Id = Convert.ToInt32(reader["id"]),
            Name = Convert.ToString(reader["name"]),
            CreatedAt = Convert.ToDateTime(reader["created_at"]),
            UserId = Convert.ToInt32(reader["user_id"]),
        };
    }
}
