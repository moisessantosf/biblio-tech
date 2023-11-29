using Biblio.Application.Interfaces;
using Biblio.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ICustomerService _clientService;

        public ClientsController(ICustomerService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostClient(RequestCreateCustomerDTO customer)
        {
            var createdClient = await _clientService.CreateClient(customer);
            return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetClient(int id)
        {
            var client = await _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetClients()
        {
            var clients = await _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Customer client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            await _clientService.UpdateClient(client);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClient(id);
            return NoContent();
        }
    }

}
