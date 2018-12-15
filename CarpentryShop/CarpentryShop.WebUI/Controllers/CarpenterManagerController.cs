using System;
using System.Collections.Generic;
using System.IO;
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
            List<Carpenter> carpenter = context.Collection().ToList(); 

            return View(carpenter);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            
            viewModel.Carpenter = new Carpenter();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Carpenter carpenter, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(carpenter);
                }
            else
            {
                if(file != null)
                {
                    carpenter.Image = carpenter.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//CarpenterImages//") + carpenter.Image);
                }

                context.Insert(carpenter);
                context.Commit();

                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit (String Id)
        {
            Carpenter carpenter = context.Find(Id);
            if (carpenter == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Carpenter = carpenter;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Carpenter carpenter, string Id, HttpPostedFileBase file)
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
                    return View(carpenter);
                }
                if (file != null)
                {
                    carpenter.Image = carpenter.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//CarpenterImages//") + carpenter.Image);
                }

                carpenterToEdit.Category = carpenter.Category;
                carpenterToEdit.Description = carpenter.Description;
                carpenterToEdit.Name = carpenter.Name;
                carpenterToEdit.Price = carpenter.Price;
                

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