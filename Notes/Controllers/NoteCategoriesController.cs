using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Dto;
using Notes.Models;

namespace Notes.Controllers
{
    [Route("noteCategories")]
    [ApiController]
    public class NoteCategoriesController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public NoteCategoriesController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }


        [HttpPost("getNoteCategories")]
        public async Task<ActionResult> GetNoteCategory(TokenDto tokenDto)
        {
            NoteCategoryDto noteCategoryDtos = await _authRepo.GetNoteCategories(tokenDto.Data);

            return Ok(noteCategoryDtos);
        }

        /*// GET: api/NoteCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteCategory>> GetNoteCategory(int id)
        {
            var noteCategory = await _context.NoteCategory.FindAsync(id);

            if (noteCategory == null)
            {
                return NotFound();
            }

            return noteCategory;
        }

        // PUT: api/NoteCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteCategory(int id, NoteCategory noteCategory)
        {
            if (id != noteCategory.IdNoteCategory)
            {
                return BadRequest();
            }

            _context.Entry(noteCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteCategoryExists(id))
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

        // POST: api/NoteCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NoteCategory>> PostNoteCategory(NoteCategory noteCategory)
        {
            _context.NoteCategory.Add(noteCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteCategory", new { id = noteCategory.IdNoteCategory }, noteCategory);
        }

        // DELETE: api/NoteCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NoteCategory>> DeleteNoteCategory(int id)
        {
            var noteCategory = await _context.NoteCategory.FindAsync(id);
            if (noteCategory == null)
            {
                return NotFound();
            }

            _context.NoteCategory.Remove(noteCategory);
            await _context.SaveChangesAsync();

            return noteCategory;
        }

        private bool NoteCategoryExists(int id)
        {
            return _context.NoteCategory.Any(e => e.IdNoteCategory == id);
        }*/
    }
}
