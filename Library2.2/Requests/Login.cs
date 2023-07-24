using System.ComponentModel.DataAnnotations;

namespace Library2._2.Requests
{
    public class Login
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
