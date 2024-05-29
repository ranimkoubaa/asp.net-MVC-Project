namespace mini_projet.Models.Repositories
{
	public interface IProduitRepository
	{
		IList<Produit> GetAll();
		Produit GetById(int id);
		void Add(Produit p);
		Produit Edit(Produit p);
		void Delete(Produit p);
		IList<Produit> GetProductByPrice(float price);
		IList<Produit> GetProductByName(String s);
	}
}
