using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommandeAppWeb.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public required string NomUtilisateur { get; set; }
        public required string HashMotDePasse { get; set; }
    }
}