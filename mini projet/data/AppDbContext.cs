
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mini_projet.Models;

namespace mini_projet.data
{
	public class AppDbContext: IdentityDbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
		{

		}
		public DbSet<Produit> Produits { get; set; }
		public DbSet<Categorie> Categories { get; set; }
		public DbSet<Commande> Commandes { get; set; }

	}
}
