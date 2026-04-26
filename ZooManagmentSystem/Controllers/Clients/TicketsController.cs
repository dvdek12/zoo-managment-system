using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.Models.Client;
using ZooManagmentSystem.ViewModels;

namespace ZooManagmentSystem.Controllers.Clients
{
    [Route("tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: tickets
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetTickets()
        {
            return Ok(await _context.Tickets.ToListAsync());
        }
            
        // GET: /tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> GetTicket(int id)
        {
            var ticketModel = await _context.Tickets.FindAsync(id);

            if (ticketModel == null)
            {
                return NotFound();
            }

            return Ok(ticketModel);
        }

        // GET: /tickets/forClient/5
        [HttpGet("forClient/{clientId}")]
        public async Task<ActionResult<IEnumerable<TicketModel>>> GetTicketsForClient(int clientId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.ClientId == clientId)
                .Include(t => t.EntryTypes)
                .ToListAsync();

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound();
            }

            return Ok(tickets);
        }

        // POST: tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketModel>> CreateTicket(TicketViewModel ticketModel)
        {
            var entryTypesIds = ticketModel.EntryTypeIds.Select(et => et.Key).ToList();
            var entryTypes = await _context.EntryTypes
                .Where(et => entryTypesIds.Contains(et.id))
                .ToListAsync();

            if(entryTypes.Count != ticketModel.EntryTypeIds.Count)
                return BadRequest(new { message = "One or more entry types are invalid." });

            decimal totalPrice = entryTypes.Sum(et => et.Price);

            var ticket = new TicketModel
            {
                ClientId = ticketModel.ClientId,
                PurchaseDate = DateTime.Now,
                ValidUntil = DateTime.Now.AddDays(30),
                Price = totalPrice,
                EntryTypes = entryTypes.Select(et => new TicketEntryTypeModel {
                   EntryTypeId = et.id, Quantity = ticketModel.EntryTypeIds[et.id] 
                }).ToList()
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created ticket successfully!" });              
        }

        // DELETE: tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketModel(int id)
        {
            var ticketModel = await _context.Tickets.FindAsync(id);
            if (ticketModel == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticketModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Ticket deleted successfully!" });
        }

        private bool TicketModelExists(int id)
        {
            return _context.Tickets.Any(e => e.id == id);
        }
    }
}
