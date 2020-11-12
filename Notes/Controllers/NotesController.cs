using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Dto;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }
        
        [HttpPost("getMainNotes")]
        public async Task<ActionResult> GetMainNotes(TokenDto tokenDto)
        {
            JwtSecurityToken decodedToken = GetDecodedToken(tokenDto.Data);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);
            
            var userHasNotes = _context.UserHasNote.Where(u => u.IdUser == id).ToList();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var userHasNote in userHasNotes)
            {
                Note note = _context.Note.First(p => p.IdNote == userHasNote.IdNote);
                noteDtos.Add(new NoteDto
                {
                    Header = note.Header,
                    Description = note.Description,
                    IsFavorites = note.IsFavorites,
                    ImagePath = note.ImagePath
                });
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
        }
        
        private JwtSecurityToken GetDecodedToken(string inputToken)
        {
            var jwt = inputToken;
            var handler = new JwtSecurityTokenHandler();
            var resultToken = handler.ReadJwtToken(jwt);

            return resultToken;
        }

        /*// GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Note.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // PUT: api/Notes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.IdNote)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Notes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.IdNote }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> DeleteNote(int id)
        {
            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return note;
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.IdNote == id);
        }*/
    }
}
