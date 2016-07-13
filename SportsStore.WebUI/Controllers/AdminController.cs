using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using System.Linq;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductsRepository repository;

        public AdminController(IProductsRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong in the data values
                return View(product);
            }
        }
    }
}