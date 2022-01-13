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
    public class NoteController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public NoteController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public JsonResult Get()
        {
            var notes = _context.Notes;

            return new JsonResult(notes);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var note = _context.Notes.FirstOrDefault(x => x.Id == id);

            if (note == null)
            {
                return new JsonResult(null);
            }

            return new JsonResult(note);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Models.Note note)
        {
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == note.TicketId);

            if (ticket == null)
            {
                return BadRequest();
            }

            _context.Notes.Add(note);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, string content)
        {
            var note = _context.Notes.FirstOrDefault(x => x.Id == id);

            if (note == null)
            {
                return BadRequest();
            }

            note.Content = content;
            _context.Notes.Update(note);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(x => x.Id == id);

            if (note == null)
            {
                return BadRequest();
            }

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == note.TicketId);

            if (ticket == null)
            {
                return BadRequest();
            }

            var person = _context.Persons.FirstOrDefault(x => x.Id == ticket.PersonId);

            if (person.IsAdmin == false)
            {
                return BadRequest();
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();
            return Ok();
        }
    }
}
