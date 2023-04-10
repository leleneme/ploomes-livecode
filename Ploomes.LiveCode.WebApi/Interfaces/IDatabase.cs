using System.Data;

namespace Ploomes.LiveCode.WebApi.Interfaces;

public interface IDatabase
{
    Task<int> ExecuteNonQueryAsync(string command, params (string, object)[] parameters);

    Task<T> ExecuteQueryAsync<T>(string command, Func<IDataReader, T> serializeDelegate, params (string, object)[] parameters);
}