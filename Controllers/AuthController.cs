using EVA1_BackEnd.Model;
using EVA1_BackEnd.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EVA1_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(AppDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO user)
        {

            User usuario = new User();
            usuario.Username=user.Username;
            usuario.Email=user.Email;
            usuario.Name = user.Name;
            usuario.Role=user.Role;

            CreatePasswordHash(user.Password,out byte[] hash, out byte[] salt);
            usuario.PasswordHash=hash;
            usuario.PasswordSalt=salt;

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string username,string password)
        {
            var user = _context.TblUsers.Where(u=>u.Username==username).FirstOrDefault();
            if (user!=null)
            {
                if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                {
                    string token = CreateToken(user);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Password Incorrect");
                }
            }
            else
            {
                return BadRequest("User not found");
            }
        }


        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            using (var hmac=new HMACSHA512(passwordSalt))
            {
                var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> datos = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:TokenKey").Value));
            var Credential =new SigningCredentials(Key,SecurityAlgorithms.HmacSha512Signature);
            var Token = new JwtSecurityToken(
                claims: datos,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: Credential);
            var JWT=new JwtSecurityTokenHandler().WriteToken(Token);
            return JWT;
        }
    }
}
