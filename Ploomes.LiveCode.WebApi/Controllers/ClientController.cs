using Microsoft.AspNetCore.Mvc;

namespace Ploomes.LiveCode.WebApi.Controllers;

[Route("/api/clients")]
public class ClientController : Controller
{
    private IClientRepository _clientRepo;

    public ClientController(IClientRepository clientRepo)
    {
        _clientRepo = clientRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
        => Ok(await _clientRepo.FetchAllAsync());

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto data)
    {
        if (!ModelState.IsValid) return BadRequest();

        bool result = await _clientRepo.CreateAsync(data);
        // Se o resultado for 'false', a possivel causa é que o usuario não existe
        // então, por simplicidade eu só faço isso:
        return result ? Ok() : NotFound();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetClientById([FromRoute] int id)
    {
        var client = await _clientRepo.GetOneAsync(id);
        return client is not null
            ? Ok(client)
            : NotFound();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteClient([FromRoute] int id)
    {
        bool result = await _clientRepo.DeleteAsync(id);
        return result ? Ok() : NotFound();
    }
}
