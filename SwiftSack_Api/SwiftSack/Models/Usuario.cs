using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SwiftSack.Models
{
    [Table("users")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }

        [Required]
        [MaxLength(100)]
        public string telefono { get; set; }

        public bool active { get; set; }

        public DateTime? confirmed_at { get; set; }

        [ForeignKey("Rol")] 
        public int roleId { get; set; } 
    }
}
