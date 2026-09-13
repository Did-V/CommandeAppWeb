using CommandeAppWeb.Data;
using CommandeAppWeb.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //Code pour créer l'utilisateur admin si nécessaire
        // [HttpPost("seed")]
        // public IActionResult SeedAdmin()
        // {
        //     var lehaseurDeMdp = new PasswordHasher<Utilisateur>();
        //     var utilisateur = new Utilisateur
        //     {
        //         NomUtilisateur = "admin",
        //         HashMotDePasse = lehaseurDeMdp.HashPassword(null!, ""),
        //         Role = "Admin"
        //     };
        //     _context.Utilisateurs.Add(utilisateur);
        //     _context.SaveChanges();
        //     return Ok("Utilisateur admin créé.");
        // }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            
            var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == request.NomUtilisateur);
            if (utilisateur == null) return Unauthorized();

            var hasher = new PasswordHasher<Utilisateur>();
            var result = hasher.VerifyHashedPassword(utilisateur, utilisateur.HashMotDePasse, request.MotDePasse);

            if (result == PasswordVerificationResult.Failed) return Unauthorized();

            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("La clé JWT 'Jwt:Key' est absente de la configuration.");
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.NomUtilisateur),
                new Claim(ClaimTypes.Role, utilisateur.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public class LoginRequest
    {
        public required string NomUtilisateur { get; set; }
        public required string MotDePasse { get; set; }
    }
}