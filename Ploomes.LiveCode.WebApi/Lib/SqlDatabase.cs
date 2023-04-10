using System.Data;
using System.Data.SqlClient;

namespace Ploomes.LiveCode.WebApi.Lib;

public class SqlDatabase : IDatabase
{
    private IConfiguration _config;

    public SqlDatabase(IConfiguration config)
    {
        _config = config;
    }

    private SqlConnection GetConnection()
    {
        var conn = new SqlConnection(_config.GetConnectionString("SqlServer"));
        return conn;
    }

    public async Task<int> ExecuteNonQueryAsync(string command, params (string, object)[] parameters)
    {
        using var conn = this.GetConnection();
        await conn.OpenAsync();

        using var sqlCommand = new SqlCommand(command, conn);

        foreach ((string name, object value) in parameters)
            sqlCommand.Parameters.AddWithValue(name, value);

        return await sqlCommand.ExecuteNonQueryAsync();
    }

    public async Task<T> ExecuteQueryAsync<T>(string command, Func<IDataReader, T> serializeDelegate, params (string, object)[] parameters)
    {
        using var conn = this.GetConnection();
        await conn.OpenAsync();

        using var sqlCommand = new SqlCommand(command, conn);

        foreach ((string name, object value) in parameters)
            sqlCommand.Parameters.AddWithValue(name, value);

        using var reader = await sqlCommand.ExecuteReaderAsync();
        return serializeDelegate(reader);
    }
}
