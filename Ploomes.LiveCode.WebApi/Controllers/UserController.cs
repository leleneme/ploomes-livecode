using Microsoft.AspNetCore.Mvc;

namespace Ploomes.LiveCode.WebApi.Controllers;

[Route("/api/users")]
public class UserController : Controller
{
    private IUserRepository _userRepo;
    private IClientRepository _clientRepo;

    public UserController(IUserRepository userRepo, IClientRepository clientRepo)
    {
        _userRepo = userRepo;
        _clientRepo = clientRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
        => Ok(await _userRepo.FetchAllAsync());

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto data)
    {
        if (!ModelState.IsValid) return BadRequest();

        bool result = await _userRepo.CreateAsync(data);
        return result ? Ok() : BadRequest();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _userRepo.GetOneAsync(id);
        return user is not null
            ? Ok(user)
            : NotFound();
    }

    [HttpGet]
    [Route("{id:int}/clients")]
    public async Task<IActionResult> GetUserClients([FromRoute] int id)
    {
        var clients = await _clientRepo.GetAllFromUser(id);
        return Ok(clients);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        bool result = await _userRepo.DeleteAsync(id);
        return result ? Ok() : NotFound();
    }
}
