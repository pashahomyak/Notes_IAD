using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("note")]
    public partial class Note
    {
        public Note()
        {
            UserHasNote = new HashSet<UserHasNote>();
        }

        [Key]
        [Column("id_note")]
        public int IdNote { get; set; }
        [Column("id_note_category")]
        public int IdNoteCategory { get; set; }
        [Column("header")]
        [StringLength(50)]
        public string Header { get; set; }
        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("is_favorites")]
        public bool IsFavorites { get; set; }
        [Column("image_path")]
        [StringLength(255)]
        public string ImagePath { get; set; }

        [ForeignKey(nameof(IdNoteCategory))]
        [InverseProperty(nameof(NoteCategory.Note))]
        public virtual NoteCategory IdNoteCategoryNavigation { get; set; }
        [InverseProperty("IdNoteNavigation")]
        public virtual ICollection<UserHasNote> UserHasNote { get; set; }
    }
}
