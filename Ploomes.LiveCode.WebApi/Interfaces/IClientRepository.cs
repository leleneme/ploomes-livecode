namespace Ploomes.LiveCode.WebApi.Interfaces;

public interface IClientRepository
{
    Task<bool> CreateAsync(ClientCreateDto data);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<Client>> FetchAllAsync();

    Task<IEnumerable<Client>> GetAllFromUser(int userId);

    Task<Client?> GetOneAsync(int id);
}
