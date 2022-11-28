using System.ComponentModel.DataAnnotations;

namespace PostStoreApi.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
