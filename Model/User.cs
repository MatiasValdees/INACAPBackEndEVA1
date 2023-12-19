using System.ComponentModel.DataAnnotations;

namespace EVA1_BackEnd.Model
{
    public class User
    {
        [Key]
        public  Guid Id { get; set; } //sdasdasd-asdaasdas-asdadas-sadasdas
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;}

    }
}
