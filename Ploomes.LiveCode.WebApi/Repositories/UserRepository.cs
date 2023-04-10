using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Ploomes.LiveCode.WebApi.Repositories;

public class SqlUserRepository : IUserRepository
{
    private IDatabase _db;

    public SqlUserRepository(IDatabase db)
    {
        _db = db;
    }

    public async Task<bool> CreateAsync(UserCreateDto data)
    {
        string query =
            """
            INSERT INTO usuario(name, email)
            VALUES(@name, @email)
            """;

        int result = await _db.ExecuteNonQueryAsync(query, ("@name", data.Name), ("@email", data.Email));
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        string query =
            """
            DELETE FROM usuario
            WHERE id = @id
            """;

        int result = await _db.ExecuteNonQueryAsync(query, ("@id", id));
        return result > 0;
    }

    public async Task<IEnumerable<User>> FetchAllAsync()
    {
        string query = "SELECT * FROM usuario";

        List<User> users = await _db.ExecuteQueryAsync(query, reader =>
        {
            List<User> users = new();

            while (reader.Read())
                users.Add(UserFromReader(reader));

            return users;
        });

        return users;
    }

    public async Task<User?> GetOneAsync(int id)
    {
        string query =
            """
            SELECT * FROM usuario
            WHERE id = @id
            """;

        User? user = await _db.ExecuteQueryAsync(query, reader =>
        {
            if (!reader.Read()) return null;
            else return UserFromReader(reader);
        }, ("@id", id));

        return user;
    }

    private User UserFromReader(IDataReader reader)
    {
        return new User
        {
            Id = Convert.ToInt32(reader["id"]),
            Name = Convert.ToString(reader["name"]),
            Email = Convert.ToString(reader["email"])
        };
    }
}
