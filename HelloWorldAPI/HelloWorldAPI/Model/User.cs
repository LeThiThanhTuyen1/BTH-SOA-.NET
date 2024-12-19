using System.ComponentModel.DataAnnotations;

namespace HelloWorldAPI.Model
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
