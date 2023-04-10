namespace Ploomes.LiveCode.WebApi.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateAsync(UserCreateDto data);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<User>> FetchAllAsync();

    Task<User?> GetOneAsync(int id);
}
