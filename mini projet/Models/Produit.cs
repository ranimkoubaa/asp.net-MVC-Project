using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mini_projet.Models
{
	public class Produit
	{
        public int ProduitId { get; set; }
		[Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2)]
		public string Nom { get; set; }
		[Required]
		[Display(Name = "Price in Dinars")]
		public float Prix { get; set; }
		
		[Display(Name = "Category")]
		public int CategorieId{ get; set; }
        [Display(Name = "Category")]
        public Categorie Categorie { get; set; }
		[Required]
		[Display(Name = "Quantity in unit")]
		public int Quantite { get; set; }
		[Required]
		[Display(Name = "Image")]
		public string Image { get; set; }
	}
}

