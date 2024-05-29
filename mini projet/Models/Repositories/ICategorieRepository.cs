namespace mini_projet.Models.Repositories
{
	public interface ICategorieRepository
	{
		IList<Categorie> GetAll();
		Categorie GetById(int id);
		IList<Categorie> GetCatByName(String s);
		Categorie GetProduitsByCateg(string cat);
		void Add(Categorie c);
		void Edit(Categorie c);
		void Delete(Categorie c);
		IList<Categorie> Search(string keyword);
	}
}
