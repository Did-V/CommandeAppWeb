using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommandeAppWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            return Ok(new { 
                usernameRecu = request.Username, 
                passwordRecu = request.Password,
                usernameEgal = request.Username == "admin",
                passwordEgal = request.Password == "motdepasse123"
            });
            
            // if (request.Username != "admin" || request.Password != "motdepasse123")
            // {
            //     return Unauthorized();
            // }

            // var jwtKey = _configuration["Jwt:Key"]
            //     ?? throw new InvalidOperationException("La clé JWT 'Jwt:Key' est absente de la configuration.");
            
            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.Name, request.Username)
            // };

            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // var token = new JwtSecurityToken(
            //     claims: claims,
            //     expires: DateTime.Now.AddHours(2),
            //     signingCredentials: creds
            // );

            // return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }


    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}