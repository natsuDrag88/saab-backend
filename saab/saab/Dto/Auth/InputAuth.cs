using System.ComponentModel.DataAnnotations;

namespace saab.Dto.Auth
{
    public class InputAuth
    {
        [Required]
        public string username { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}