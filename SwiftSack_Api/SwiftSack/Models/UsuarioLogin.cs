using System.ComponentModel.DataAnnotations;

namespace SwiftSack.Models
{
    public class UsuarioLogin
    {
        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }
    }
}
