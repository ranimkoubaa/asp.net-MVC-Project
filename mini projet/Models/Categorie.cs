using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mini_projet.Models
{
	public class Categorie
	{
      
        public int CategorieId { get; set; }
		[Required]
		[Display(Name = "Name")]
		public string Nom { get; set; }
		public virtual ICollection<Produit> Produits { get; set; }
		
		
	}
}
