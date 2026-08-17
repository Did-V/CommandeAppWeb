using Microsoft.EntityFrameworkCore;
using CommandeApp.Models;

namespace CommandeApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produit> Produits { get; set; }
    public DbSet<Commande> Commandes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=commandeapp;Username=postgres;Password=***SECRET_SUPPRIME***");
    }
}