using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    [Table("user_type")]
    public partial class UserType
    {
        public UserType()
        {
            User = new HashSet<User>();
        }

        [Key]
        [Column("id_user_type")]
        public int IdUserType { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("IdUserTypeNavigation")]
        public virtual ICollection<User> User { get; set; }
    }
}
