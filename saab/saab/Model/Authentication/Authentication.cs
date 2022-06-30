using System.ComponentModel.DataAnnotations;

namespace saab.Model.Authentication
{
    public class Authentication
    {
        [Required]
        public string username { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}