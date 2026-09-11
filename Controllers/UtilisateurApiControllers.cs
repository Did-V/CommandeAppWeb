using CommandeAppWeb.Data;
using CommandeAppWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommandeAppWeb.Controllers
{
    [ApiController]
    [Route("api/Utilisateur")]

    public class UtilisateurApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtilisateurApiController(AppDbContext context)
        {
            _context = context;
        }
        
        
    }
}

