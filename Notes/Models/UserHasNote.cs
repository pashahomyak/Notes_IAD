using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("user_has_note")]
    public partial class UserHasNote
    {
        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }
        [Key]
        [Column("id_note")]
        public int IdNote { get; set; }

        [ForeignKey(nameof(IdNote))]
        [InverseProperty(nameof(Note.UserHasNote))]
        public virtual Note IdNoteNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(User.UserHasNote))]
        public virtual User IdUserNavigation { get; set; }
    }
}
