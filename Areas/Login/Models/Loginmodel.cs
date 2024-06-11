using System.ComponentModel.DataAnnotations;

namespace UMS.Areas.Login.Models
{
    public class Loginmodel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
