using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{

    public class RegisterDto //We've created this DTO in order not use a query string while registering the user and send the username and password paramaters inside of the body 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)] // min = 4, max = 8
        public string Password { get; set; }
    }

}