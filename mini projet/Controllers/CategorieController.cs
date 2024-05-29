using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using mini_projet.Models;
using mini_projet.Models.Repositories;
using NuGet.Protocol.Core.Types;
using mini_projet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mini_projet.Controllers
{
    public class CategorieController : Controller

    {
        readonly ICategorieRepository _repository;


        public CategorieController(ICategorieRepository repository)
        {
            _repository = repository;
        }
        // GET: CategorieController
        public ActionResult Index()
        {
            var c = _repository.GetAll();
            return View(c);
        }

        // GET: CategorieController/Details/5
        public ActionResult Details(int id)
        {
            var c = _repository.GetById(id);
            return View(c);
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categorie c)
        {

            _repository.Add(c);
            return RedirectToAction("Index", new { id = c.CategorieId });


        }

        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
        {

            var category = _repository.GetById(id);

            return View(category);
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie c)
        {
            try
            {
                _repository.Edit(c);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [NonAction]


        // GET: CategorieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategorieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Categorie c)
        {
            try
            {
                c.CategorieId = id;
                _repository.Delete(c);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DetailsCategorie(string cat)
        {
            ViewBag.Categorie = cat;
            var produitListe = _repository.GetProduitsByCateg(cat);
            return View(produitListe);
        }
		public ActionResult Search(string name, int? categorieId)
		{
			var result = _repository.GetAll();
			if (!string.IsNullOrEmpty(name))
				result = _repository.GetCatByName(name);
			
			ViewBag.SchoolID = new SelectList(_repository.GetAll(), "CategorieId", "Categorie");
			return View("Index", result);
		}

	}
}
