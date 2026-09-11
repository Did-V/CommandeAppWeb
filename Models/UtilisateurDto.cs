namespace CommandeAppWeb.Models
{
    public class UtilisateurDto
    {    
        public int Id { get; set; }
        public required string NomUtilisateur { get; set;}
        public required string Role { get; set;}
    }

    public class CreerUtilisateurRequest
    {
        public required string NomUtilisateur { get; set; }
        public required string MotDePasse { get; set; }
        public required string Role { get; set; }
    }
    
    public class ModifierUtilisateurRequest
    {
        public required string NomUtilisateur { get; set; }
        public string? MotDePasse { get; set; } //optionnel : ne change que si fourni
        public required string Role { get; set; }
    }
}