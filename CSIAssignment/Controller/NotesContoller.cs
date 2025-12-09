using Microsoft.AspNetCore.Mvc;
using CSIAssignment.Data;
using CSIAssignment.Models;
using Microsoft.AspNetCore.Authorization;


namespace CSIAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _context;

        public NotesController(NotesDbContext context)
        {
            _context = context;
        }

        // GET: api/notes
        [HttpGet]
        [AllowAnonymous] // explicitly allow requests without auth
        public IActionResult GetNotes()
        {
            var notes = _context.Notes.ToList();
            return Ok(notes);
        }

        // GET: api/notes/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetNoteById(int id)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        // POST: api/notes
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateNote([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            _context.Notes.Add(note);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // PUT: api/notes/{id}
        [HttpPut("{id}")]
        [AllowAnonymous]
        public IActionResult UpdateNote(int id, [FromBody] Note updatedNote)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            note.Title = updatedNote.Title;
            note.Content = updatedNote.Content;
            _context.SaveChanges();

            return Ok(note);
        }

        // DELETE: api/notes/{id}
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public IActionResult DeleteNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
