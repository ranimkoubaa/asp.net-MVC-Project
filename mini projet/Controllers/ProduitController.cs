using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using mini_projet.Models;
using mini_projet.Models.Repositories;
using mini_projet.Models.ViewModels;
using NuGet.Protocol.Core.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace mini_projet.Controllers
{
	public class ProduitController : Controller
	{
		readonly IProduitRepository productRepository;
		readonly ICategorieRepository categorieRepository;
		private readonly IWebHostEnvironment hostingEnvironment;

		public ProduitController(IProduitRepository productRepository,ICategorieRepository categorieRepository, IWebHostEnvironment hostingEnvironment)
		{
			this.productRepository = productRepository;
			this.categorieRepository = categorieRepository;
			this.hostingEnvironment = hostingEnvironment;
		}
		// GET: ProduitController
		public ActionResult Index()
		{
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
            var p = productRepository.GetAll();
			return View(p);
		}

		// GET: ProduitController/Details/5
		public ActionResult Details(int id)
		{
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
            var p = productRepository.GetById(id);
			return View(p);
		}

		// GET: ProduitController/Create
		public ActionResult Create()
		{
			ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
			return View();
		}

		// POST: ProduitController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateViewModelProd model)
		{
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
          
			
            if (ModelState.IsValid)
			{
				string uniqueFileName = null;
				if (model.Image != null)
				{

					string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");

					uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
					string filePath = Path.Combine(uploadsFolder, uniqueFileName);

					model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
				}
                Produit p = new Produit
				{
					Nom = model.Nom,
					Prix = model.Prix,
					CategorieId = model.CategorieId,
					Categorie = categorieRepository.GetById(model.CategorieId),
					Quantite = model.Quantite,
					Image = uniqueFileName
				};
				
                productRepository.Add(p);
                return RedirectToAction(nameof(Index));
            }
        

            return View();
        }

		// GET: ProduitController/Edit/5
		public ActionResult Edit(int id)
		{
			Produit product = productRepository.GetById(id);
            ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
            EditViewModelProd productEditViewModel = new EditViewModelProd
			{
				ProduitId = product.ProduitId,
				Nom = product.Nom,
				Prix = product.Prix,
				CategorieId = product.CategorieId,
				Quantite = product.Quantite,
				ExistingImagePath = product.Image
			};
			return View(productEditViewModel);
		}
            // POST: ProduitController/Edit/5
            [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditViewModelProd model)
        {
			   ViewBag.CategorieId = new SelectList(categorieRepository.GetAll(), "CategorieId", "Nom");
            try
			{ 
                if (ModelState.IsValid)
                {
              
                    Produit product = productRepository.GetById(model.ProduitId);

					product.Nom = model.Nom;
					product.Prix = model.Prix;
					product.CategorieId = model.CategorieId;
					product.Quantite = model.Quantite;
                 
                    if (model.Image != null)
                    {
                        
                        if (model.ExistingImagePath != null)
                        {
                            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                            System.IO.File.Delete(filePath);
                        }
                     
                        product.Image = ProcessUploadedFile(model);
                    }
                   
                    Produit updatedProduct = productRepository.Edit(product);
                    if (updatedProduct != null)
                        return RedirectToAction("Index");
                    else
                        return NotFound();
                }
                return View(model);
            }
            catch
            {
                return View();
            }

        }

		// GET: ProduitController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ProduitController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id,Produit p)
		{
			try
			{
				p.ProduitId=id;
				productRepository.Delete(p);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
        private string ProcessUploadedFile(EditViewModelProd model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
