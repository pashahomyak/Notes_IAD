using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("note_category")]
    public partial class NoteCategory
    {
        public NoteCategory()
        {
            Note = new HashSet<Note>();
        }

        [Key]
        [Column("id_note_category")]
        public int IdNoteCategory { get; set; }
        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [InverseProperty("IdNoteCategoryNavigation")]
        public virtual ICollection<Note> Note { get; set; }
    }
}
