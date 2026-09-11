using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CommandeAppWeb.Data;
using CommandeAppWeb.Models;

namespace CommandeAppWeb.Controllers
{
    [ApiController]
    [Route("api/Utilisateurs")]
    [Authorize(Roles = "Admin")]
    public class UtilisateursApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtilisateursApiController(AppDbContext context)
        {
            _context = context;
        }
        
        //GET: api/Utilisateurs
        [HttpGet]
        public IActionResult GetAll()
        {
            var utilisateurs = _context.Utilisateurs
                .Select(u => new UtilisateurDto
                {
                    Id = u.Id,
                    NomUtilisateur = u.NomUtilisateur,
                    Role = u.Role
                })
                .ToList();
            return Ok(utilisateurs);
        }

        //GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var utilisateur = _context.Utilisateurs.Find(id);
            if (utilisateur == null) return NotFound();

            return Ok(new UtilisateurDto
            {
                Id = utilisateur.Id,
                NomUtilisateur = utilisateur.NomUtilisateur,
                Role = utilisateur.Role
            });
        }

        //POST: api/Utilisateurs
        [HttpPost]
        public IActionResult Create(CreerUtilisateurRequest request)
        {
            if (_context.Utilisateurs.Any(u => u.NomUtilisateur == request.NomUtilisateur))
                return Conflict("Ce nom d'utilisateur existe déjà.");

            var utilisateur = new Utilisateur
            {
                NomUtilisateur = request.NomUtilisateur,
                Role = request.Role,
                HashMotDePasse = "" //placeholder, hashé juste après
            };

            var leHasheurDeMdp = new PasswordHasher<Utilisateur>();
            utilisateur.HashMotDePasse = leHasheurDeMdp.HashPassword(utilisateur, request.MotDePasse);

            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();

            var dto = new UtilisateurDto
            {
                Id = utilisateur.Id,
                NomUtilisateur = utilisateur.NomUtilisateur,
                Role = utilisateur.Role
            };

            return CreatedAtAction(nameof(GetById), new { id = utilisateur.Id }, dto);
        }

        //PUT: api/Utilisateurs/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ModifierUtilisateurRequest request)
        {
            var utilisateur = _context.Utilisateurs.Find(id);
            if (utilisateur == null) return NotFound();

            utilisateur.NomUtilisateur = request.NomUtilisateur;
            utilisateur.Role = request.Role;

            //Ne Rehasher que si un nouveau mot de passe est fourni
            if (!string.IsNullOrWhiteSpace(request.MotDePasse))
            {
                var leHasheurDeMdp = new PasswordHasher<Utilisateur>();
                utilisateur.HashMotDePasse = leHasheurDeMdp.HashPassword(utilisateur, request.MotDePasse);
            }

            _context.SaveChanges();
            return NoContent();
        }

        //DELETE: api/Utilisateurs/5
        [HttpDelete()]
        
    }
}

