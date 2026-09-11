using Microsoft.EntityFrameworkCore;
using CommandeAppWeb.Models;

namespace CommandeAppWeb.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ } //Constructeur sans paramètre pour permettre la récupération des identifiants de connexion à la base
    public DbSet<Produit> Produits { get; set; }
    public DbSet<Commande> Commandes { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql("Host=localhost;Database=commandeapp;Username=postgres;Password=***SECRET_SUPPRIME***"); 
    //     // optionsBuilder.UseNpgsql("Host=localhost;Database=commandeapp;Username=postgres;Password=***SECRET_SUPPRIME***"); 
    // }
}