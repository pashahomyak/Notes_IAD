using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Dto;
using Notes.Models;
using System.Drawing;

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
                    Id = note.IdNote,
                    Header = note.Header,
                    Description = note.Description,
                    IsFavorites = note.IsFavorites,
                    ImagePath = note.ImagePath
                });
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
        }
        
        [HttpPost("addNote")]
        public async Task<ActionResult> AddNote(NoteDto noteDto)
        {
            string token = Request.Headers.Where(p => p.Key == "Authorization").First().Value.ToString().Substring(7);
            JwtSecurityToken decodedToken = GetDecodedToken(token);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);

            if (!String.IsNullOrEmpty(noteDto.ImageData) || !String.IsNullOrEmpty(noteDto.ImageName))
            {
                string imageData = noteDto.ImageData.Substring(noteDto.ImageData.IndexOf(',') + 1);
                byte[] bytes = Convert.FromBase64String(imageData);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                string path = Directory.GetCurrentDirectory();
                if (!path.EndsWith("userImages"))
                {
                    Environment.CurrentDirectory = path + "\\ClientApp\\src\\assets\\userImages";
                }
                image.Save(noteDto.ImageName);
                Environment.CurrentDirectory = path;
            }

            int categoryIndex = _context.NoteCategory.First(p => p.Name == noteDto.CategoryName).IdNoteCategory;
            
            Note note = new Note
            {
                Header = noteDto.Header,
                Description = noteDto.Description,
                IsFavorites = noteDto.IsFavorites,
                IdNoteCategory = categoryIndex
            };
            if (!String.IsNullOrEmpty(noteDto.ImageName))
            {
                note.ImagePath = "assets/userImages/" + noteDto.ImageName.ToString();
            }
            _context.Note.Add(note);

            _context.SaveChanges();
            
            _context.UserHasNote.Add(new UserHasNote
            {
                IdNote = note.IdNote,
                IdUser = id
            });
            
            _context.SaveChanges();
            
            return Ok("ok");
        }
        
        private JwtSecurityToken GetDecodedToken(string inputToken)
        {
            var jwt = inputToken;
            var handler = new JwtSecurityTokenHandler();
            var resultToken = handler.ReadJwtToken(jwt);

            return resultToken;
        }
        
        [HttpPost("deleteNote")]
        public async Task<ActionResult> DeleteNote([FromBody] int noteId)
        {
            var foundNote = await _context.Note.FindAsync(noteId);
            if (foundNote == null)
            {
                return NotFound();
            }

            _context.Note.Remove(foundNote);
            await _context.SaveChangesAsync();
            
            string token = Request.Headers.Where(p => p.Key == "Authorization").First().Value.ToString().Substring(7);
            JwtSecurityToken decodedToken = GetDecodedToken(token);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);
            
            var userHasNotes = _context.UserHasNote.Where(u => u.IdUser == id).ToList();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var userHasNote in userHasNotes)
            {
                Note note = _context.Note.First(p => p.IdNote == userHasNote.IdNote);
                noteDtos.Add(new NoteDto
                {
                    Id = note.IdNote,
                    Header = note.Header,
                    Description = note.Description,
                    IsFavorites = note.IsFavorites,
                    ImagePath = note.ImagePath
                });
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
        }
        
        [HttpPost("getNotesByCategory")]
        public async Task<ActionResult> GetNotesByCategory(TokenDto tokenDto)
        {
            int categoryId = _context.NoteCategory.Where(p => p.Name == tokenDto.Data).FirstOrDefault().IdNoteCategory;
            
            string token = Request.Headers.Where(p => p.Key == "Authorization").First().Value.ToString().Substring(7);
            JwtSecurityToken decodedToken = GetDecodedToken(token);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);
            
            var userHasNotes = _context.UserHasNote.Where(u => u.IdUser == id).ToList();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var userHasNote in userHasNotes)
            {
                Note note = _context.Note.First(p => p.IdNote == userHasNote.IdNote);
                if (note.IdNoteCategory == categoryId)
                {
                    noteDtos.Add(new NoteDto
                    {
                        Id = note.IdNote,
                        Header = note.Header,
                        Description = note.Description,
                        IsFavorites = note.IsFavorites,
                        ImagePath = note.ImagePath
                    });
                }
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
        }
        
        [HttpPost("getFavoritesNotes")]
        public async Task<ActionResult> GetFavoritesNotes()
        {
            string token = Request.Headers.Where(p => p.Key == "Authorization").First().Value.ToString().Substring(7);
            JwtSecurityToken decodedToken = GetDecodedToken(token);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);
            
            var userHasNotes = _context.UserHasNote.Where(u => u.IdUser == id).ToList();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var userHasNote in userHasNotes)
            {
                Note note = _context.Note.First(p => p.IdNote == userHasNote.IdNote);
                if (note.IsFavorites)
                {
                    noteDtos.Add(new NoteDto
                    {
                        Id = note.IdNote,
                        Header = note.Header,
                        Description = note.Description,
                        IsFavorites = note.IsFavorites,
                        ImagePath = note.ImagePath
                    });
                }
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
        }
        
        [HttpPost("changeFavoritesState")]
        public async Task<ActionResult> ChangeFavoritesState(NoteDto noteDto)
        {
            string token = Request.Headers.Where(p => p.Key == "Authorization").First().Value.ToString().Substring(7);
            JwtSecurityToken decodedToken = GetDecodedToken(token);
            int id = Convert.ToInt32(decodedToken.Claims.First(c => c.Type == "nameid").Value);

            //Обновление состояния
            Note inputNote = _context.Note.First(p => p.IdNote == noteDto.Id);
            if (inputNote.IsFavorites)
            {
                inputNote.IsFavorites = false;
            }
            else
            {
                inputNote.IsFavorites = true;
            }
            _context.Update<Note>(inputNote);
            _context.SaveChanges();

            var userHasNotes = _context.UserHasNote.Where(u => u.IdUser == id).ToList();
            List<NoteDto> noteDtos = new List<NoteDto>();
            foreach (var userHasNote in userHasNotes)
            {
                Note note = _context.Note.First(p => p.IdNote == userHasNote.IdNote);
                noteDtos.Add(new NoteDto
                {
                    Id = note.IdNote,
                    Header = note.Header,
                    Description = note.Description,
                    IsFavorites = note.IsFavorites,
                    ImagePath = note.ImagePath
                });
            }
            
            return Ok(new NotesDto{Data = noteDtos.ToArray()});
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
