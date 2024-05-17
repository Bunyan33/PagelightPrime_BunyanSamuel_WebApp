using PagelightPrime_BunyanSamuel_WebApp.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace PagelightPrime_BunyanSamuel_WebApp.Models
{
    public class User : BaseModel
    {
        
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNum { get; set; }
        

    }
}
