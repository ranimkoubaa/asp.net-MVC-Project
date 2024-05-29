using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mini_projet.Models.Help;
using mini_projet.Models.Repositories;
using mini_projet.Models;

namespace mini_projet.Controllers
{
    public class PanierController : Controller
    {
        readonly IProduitRepository produitRepository;
        public PanierController(IProduitRepository produitRepository)
        {
            this.produitRepository = produitRepository;
        }
        public ActionResult Index()
        {
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
        public ActionResult AjouterProduit(int id)
        {
            Produit pp = produitRepository.GetById(id);
            ListeCart.Instance.AddItem(pp);
            ViewBag.Liste = ListeCart.Instance.Items;
            ViewBag.total = ListeCart.Instance.GetSubTotal();
            return View();
        }
      
        [HttpPost]
        public ActionResult PlusProduit(int id)
        {
            Produit pp = produitRepository.GetById(id);
            ListeCart.Instance.AddItem(pp);
            Item trouve = null;
            foreach (Item a in ListeCart.Instance.Items)
            {
                if (a.Prod.ProduitId == pp.ProduitId)
                    trouve = a;
            }
            var results = new
            {
                ct = 1,
                Total = ListeCart.Instance.GetSubTotal(),
                Quatite = trouve.quantite,
                TotalRow = trouve.TotalPrice
            };
            return Json(results);
        }
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(FormCollection collection)
        {
            ListeCart.Instance.Items.Clear();
            ViewBag.Message = "Commande effectuée avec succès";
            return View();
        }
        [HttpPost]
        public ActionResult SupprimerProduit(int id)
        {
            Produit pp = produitRepository.GetById(id);
            ListeCart.Instance.RemoveItem(pp);
            var results = new
            {
                Total = ListeCart.Instance.GetSubTotal(),
            };
            return Json(results);
        }
    }
}

