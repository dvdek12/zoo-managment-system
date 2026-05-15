using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagmentSystem.Data;
using ZooManagmentSystem.DTOs;
using ZooManagmentSystem.Models.Client;

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
        public async Task<ActionResult<IEnumerable<TicketsAllDto>>> GetTickets()
        {
            var tickets = await _context.Tickets.Include(t => t.EntryTypes).ToListAsync();
            var ticketList = new List<TicketsAllDto>();

            foreach (var ticket in tickets)
            {
                var ticketDto = new TicketsAllDto
                {
                    Id = ticket.id,
                    ClientId = ticket.ClientId,
                };
                foreach (var entry in ticket.EntryTypes)
                {
                    var entryType = await _context.EntryTypes.FindAsync(entry.EntryTypeId);
                    ticketDto.EntryTypes.Add(entryType.TypeName, entry.Quantity);
                }
                ticketList.Add(ticketDto);
            }

            return Ok(ticketList);
        }
            
        // GET: /tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDetailsDto>> GetTicket(int id)
        {
            var ticketModel = await _context.Tickets.Include(t => t.EntryTypes).FirstOrDefaultAsync(t => t.id == id);

            if (ticketModel == null)
            {
                return NotFound();
            }

            var ticketDetails = new TicketDetailsDto
            {
                ClientId = ticketModel.ClientId,
                PurchaseDate = ticketModel.PurchaseDate,
                ValidUntil = ticketModel.ValidUntil,
                Price = ticketModel.Price,
                EntryTypes = new Dictionary<string, int>()
            };
            foreach(TicketEntryTypeModel ticketEntry in ticketModel.EntryTypes)
            {
                var entryType = await _context.EntryTypes.FindAsync(ticketEntry.EntryTypeId);
                ticketDetails.EntryTypes.Add(entryType.TypeName, ticketEntry.Quantity);
            }

            return Ok(ticketDetails);
        }

        // GET: /tickets/forClient/5
        [HttpGet("forClient/{clientId}")]
        public async Task<ActionResult<IEnumerable<TicketsForClientDto>>> GetTicketsForClient(int clientId)
        {
            var tickets = await _context.Tickets
                .Where(t => t.ClientId == clientId)
                .Include(t => t.EntryTypes)
                .ToListAsync();

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound();
            }

            var ticketList = new List<TicketsAllDto>();

            foreach(TicketModel ticket in tickets)
            {
                var ticketDto = new TicketsAllDto
                {
                    Id = ticket.id,
                };
                foreach (TicketEntryTypeModel entry in ticket.EntryTypes)
                {
                    var entryType = await _context.EntryTypes.FindAsync(entry.EntryTypeId);
                    ticketDto.EntryTypes.Add(entryType.TypeName, entry.Quantity);
                }
                ticketList.Add(ticketDto);
            }

            return Ok(ticketList);
        }

        // POST: tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketModel>> CreateTicket(TicketDto ticketModel)
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

        [Route("entryType")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntryTypeModel>>> GetEntryTypes()
        {
            return Ok(await _context.EntryTypes.ToListAsync());
        }

        [Route("entryType/new")]
        [HttpPost]
        public async Task<ActionResult<EntryTypeModel>> CreateEntryType(EntryTypeModel entryTypeModel)
        {
            _context.EntryTypes.Add(entryTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created entry type successfully!" });
        }

        [Route("entryType/delete/{id}")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteEntryType(int id)
        {
            var entryTypeModel = await _context.EntryTypes.FindAsync(id);
            if (entryTypeModel == null)
            {
                return NotFound();
            }

            _context.EntryTypes.Remove(entryTypeModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Entry type deleted successfully!" });
        }


        private bool TicketModelExists(int id)
        {
            return _context.Tickets.Any(e => e.id == id);
        }
    }
}
