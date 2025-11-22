using System.ComponentModel.DataAnnotations;

namespace ProtocoloRural.Models
{
    public class UserRole
    {
        [Required]
        public string? RoleName { get; set; }
    }
}
