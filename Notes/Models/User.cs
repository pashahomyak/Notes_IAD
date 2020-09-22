using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            UserHasNote = new HashSet<UserHasNote>();
        }

        [Key]
        [Column("id_user")]
        public int IdUser { get; set; }
        [Column("id_user_type")]
        public int IdUserType { get; set; }
        [Required]
        [Column("login")]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [ForeignKey(nameof(IdUserType))]
        [InverseProperty(nameof(UserType.User))]
        public virtual UserType IdUserTypeNavigation { get; set; }
        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<UserHasNote> UserHasNote { get; set; }
    }
}
