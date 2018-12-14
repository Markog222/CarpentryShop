using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carpentry.Core.Contracts;
using Carpentry.Core.Models;
using Carpentry.Core.ViewModels;
using Carpentry.DataAccess.InMemory;

namespace CarpentryShop.WebUI.Controllers
{
    public class CarpenterManagerController : Controller
    {
        IRepository<Carpenter> context;
        IRepository<ProductCategory> productCategories;

        public CarpenterManagerController(IRepository<Carpenter> carpenterContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = carpenterContext;
            productCategories = productCategoryContext;
        }

        // GET: CarpenterManager
        public ActionResult Index()
        {
            List<Carpenter> carpenters = context.Collection().ToList(); 

            return View(carpenters);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            
            viewModel.Carpenter = new Carpenter();
            viewModel.productCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Carpenter carpenters)
        {
            if (!ModelState.IsValid)
            {
                return View(carpenters);
                }
            else
            {
                context.Insert(carpenters);
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit (String Id)
        {
            Carpenter carpenters = context.Find(Id);
            if (carpenters == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Carpenter = carpenters;
                viewModel.productCategories = productCategories.Collection();

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Carpenter carpenters, string Id)
        {
            Carpenter carpenterToEdit = context.Find(Id);
            if (carpenterToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(carpenters);
                }

                carpenterToEdit.Category = carpenters.Category;
                carpenterToEdit.Description = carpenters.Description;
                carpenterToEdit.Image = carpenters.Image;
                carpenterToEdit.Name = carpenters.Name;
                carpenterToEdit.Price = carpenters.Price;
                

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(String Id)
        {
            Carpenter carpenterToDelete = context.Find(Id);
            if (carpenterToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(carpenterToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Carpenter carpenterToDelete = context.Find(Id);
            if (carpenterToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}