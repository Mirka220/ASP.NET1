using System.ComponentModel.DataAnnotations;

namespace Practice1.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fill login")]
        [Display(Name = "User login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Invalid password")]
        [Display(Name = "User password")]
        public string Password { get; set; }

    }
}
