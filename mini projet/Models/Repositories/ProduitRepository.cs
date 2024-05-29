using Microsoft.EntityFrameworkCore;
using mini_projet.data;

namespace mini_projet.Models.Repositories
{
	public class ProduitRepository : IProduitRepository
	{
		readonly AppDbContext context;
		public ProduitRepository(AppDbContext context)
		{
			this.context = context;
		}
		public IList<Produit> GetAll()
		{
			return context.Produits.OrderBy(s => s.Nom).ToList();
		}
		public Produit GetById(int id)
		{
			return context.Produits.Find(id);
		}
		public IList<Produit> GetProductByPrice(float price)
		{
			return context.Produits.Where(p => p.Prix == price).ToList();
		}
		public IList<Produit> GetProductByName(String s)
		{
			return context.Produits.Where(n => n.Nom.Contains(s)).ToList();
		}
		
		public void Add(Produit p)
		{
			context.Produits.Add(p);
			context.SaveChanges();
		}
		public Produit Edit(Produit p)
		{
		
               var produit = context.Produits.Attach(p);
               produit.State = EntityState.Modified;
               context.SaveChanges();
				return p;
		}
		public void Delete(Produit p)
		{
			Produit pp = context.Produits.Find(p.ProduitId);
			if (pp != null)
			{
				context.Produits.Remove(pp);
				context.SaveChanges();
			}
		}
		
	}
}

