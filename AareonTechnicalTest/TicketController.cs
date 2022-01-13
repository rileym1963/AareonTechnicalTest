using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TicketController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/<TicketController>
        [HttpGet]
        public JsonResult Get()
        {
            var tickets = _context.Tickets;

            var ticketDetails = new List<TicketDetail>();
            foreach (var ticket in tickets)
            {
                var person = _context.Persons.FirstOrDefault(x => x.Id == ticket.PersonId);

                var notes = _context.Notes.Where(x => x.TicketId == ticket.Id);

                ticketDetails.Add(
                    new TicketDetail
                    {
                        Ticket = ticket,
                        Person = person,
                        Notes = notes
                    });
            }

            return new JsonResult(ticketDetails);
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);

            if (ticket == null)
            {
                return new JsonResult(null);
            }

            var person = _context.Persons.FirstOrDefault(x => x.Id == ticket.PersonId);

            var notes = _context.Notes.Where(x => x.TicketId == ticket.Id);

            var ticketDetail = new TicketDetail
            {
                Ticket = ticket,
                Person = person,
                Notes = notes
            };

            return new JsonResult(ticketDetail);
        }

        // POST api/<TicketController>
        [HttpPost]
        public IActionResult Post([FromBody] Models.Ticket ticket)
        {
            var person = _context.Persons.FirstOrDefault(x => x.Id == ticket.PersonId);

            if (person == null)
            {
                return BadRequest();
            }

            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, string content)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);

            if (ticket == null)
            {
                return BadRequest();
            }

            ticket.Content = content;
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == id);

            if (ticket == null)
            {
                return BadRequest();
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return Ok();
        }
    }
}
