using Microsoft.EntityFrameworkCore;
using mini_projet.data;

namespace mini_projet.Models.Repositories
{
	public class CategorieRepository : ICategorieRepository
	{
		readonly AppDbContext context;
		public CategorieRepository(AppDbContext context)
		{
			this.context = context;
		}
		public IList<Categorie> GetAll()
		{
			return context.Categories.OrderBy(s => s.Nom).ToList();
		}
		public Categorie GetById(int id)
		{
			return context.Categories.Find(id);
		}
		public IList<Categorie> GetCatByName(String s)
		{
			return context.Categories.Where(n => n.Nom.Contains(s)).ToList();
		}
		public Categorie GetProduitsByCateg(string cat)
		{
            return context.Categories.Include("Produits").SingleOrDefault(g => g.Nom == cat);

        }
        public void Add(Categorie c)
		{
			context.Categories.Add(c);
			context.SaveChanges();
		}
		public void Edit(Categorie c)
		{
			var cc = context.Categories.Attach(c);
			cc.State = EntityState.Modified;
			context.SaveChanges();
	
		}
		public void Delete(Categorie c)
		{
			Categorie cc = context.Categories.Find(c.CategorieId);
			if (cc != null)
			{
				context.Categories.Remove(cc);
				context.SaveChanges();
			}
		}
		public IList<Categorie> Search(string keyword)
		{
			return context.Categories.Where(p => p.Nom.Contains(keyword)).ToList();
		}

	}
}
