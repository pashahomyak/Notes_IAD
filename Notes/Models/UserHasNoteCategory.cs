using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("user_has_note_category")]
    public partial class UserHasNoteCategory
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }
        [Key]
        [Column("id_note_category")]
        public int IdNoteCategory { get; set; }

        [ForeignKey(nameof(IdNoteCategory))]
        [InverseProperty(nameof(NoteCategory.UserHasNoteCategory))]
        public virtual NoteCategory IdNoteCategoryNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(User.UserHasNoteCategory))]
        public virtual User IdUserNavigation { get; set; }
    }
}
