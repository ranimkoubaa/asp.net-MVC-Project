using System.ComponentModel.DataAnnotations;

namespace mini_projet.Models.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		[Display(Name = "Role")]
		public string RoleName { get; set; }
	}
}
