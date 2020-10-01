using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHasNotesController : ControllerBase
    {
        private readonly NotesContext _context;

        public UserHasNotesController(NotesContext context)
        {
            _context = context;
        }

        // GET: api/UserHasNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHasNote>>> GetUserHasNote()
        {
            return await _context.UserHasNote.ToListAsync();
        }

        // GET: api/UserHasNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHasNote>> GetUserHasNote(int id)
        {
            var userHasNote = await _context.UserHasNote.FindAsync(id);

            if (userHasNote == null)
            {
                return NotFound();
            }

            return userHasNote;
        }

        // PUT: api/UserHasNotes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHasNote(int id, UserHasNote userHasNote)
        {
            if (id != userHasNote.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(userHasNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHasNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserHasNotes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserHasNote>> PostUserHasNote(UserHasNote userHasNote)
        {
            _context.UserHasNote.Add(userHasNote);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserHasNoteExists(userHasNote.IdUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserHasNote", new { id = userHasNote.IdUser }, userHasNote);
        }

        // DELETE: api/UserHasNotes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserHasNote>> DeleteUserHasNote(int id)
        {
            var userHasNote = await _context.UserHasNote.FindAsync(id);
            if (userHasNote == null)
            {
                return NotFound();
            }

            _context.UserHasNote.Remove(userHasNote);
            await _context.SaveChangesAsync();

            return userHasNote;
        }

        private bool UserHasNoteExists(int id)
        {
            return _context.UserHasNote.Any(e => e.IdUser == id);
        }
    }
}
