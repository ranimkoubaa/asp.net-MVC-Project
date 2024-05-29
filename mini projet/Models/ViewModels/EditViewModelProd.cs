using System.ComponentModel.DataAnnotations;

namespace mini_projet.Models.ViewModels
{
    public class EditViewModelProd:CreateViewModelProd
    {
        public int ProduitId { get; set; }
        public string ExistingImagePath { get; set; }
    }
}
