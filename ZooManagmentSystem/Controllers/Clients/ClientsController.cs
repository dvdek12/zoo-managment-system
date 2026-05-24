using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.Models.Employee;

namespace ZooManagmentSystem.Controllers.Clients
{
    [Route("client")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // for employees only
        [Route("getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            var clientDtos = clients.Select(client => new ClientDto
            {
                Id = client.id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            }).ToList();

            return Ok(clientDtos);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<ClientDto>> GetClientModel(int id)
        {
            var clientModel = await _context.Clients.FindAsync(id);

            if (clientModel == null)
            {
                return NotFound();
            }

            return Ok(new ClientDto
            {
                Id = clientModel.id,
                FirstName = clientModel.FirstName,
                LastName = clientModel.LastName,
                Email = clientModel.Email,
                PhoneNumber = clientModel.PhoneNumber,
            });
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutClientModel(int id, ClientUpdateDto clientModel)
        {

            if (id != clientModel.Id)
            {
                return BadRequest();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            if (clientModel.FirstName != null)
                client.FirstName = clientModel.FirstName;
            if (clientModel.LastName != null)
                client.LastName = clientModel.LastName;
            if (clientModel.BirthDay != null)
                client.BirthDay = (DateTime)clientModel.BirthDay;
            if (clientModel.PhoneNumber != null)
                client.PhoneNumber = clientModel.PhoneNumber;

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Updated client."});
        }


        // DELETE: api/Clients/5
        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientModel(int id)
        {
            var clientModel = await _context.Clients.FindAsync(id);
            if (clientModel == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(clientModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */

        private bool ClientModelExists(int id)
        {
            return _context.Clients.Any(e => e.id == id);
        }
    }
}
