using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carpentry.Core.Models;
using Carpentry.DataAccess.InMemory;

namespace CarpentryShop.WebUI.Controllers
{
    public class CarpenterManagerController : Controller
    {
        CarpenterRepository context;

        public CarpenterManagerController()
        {
            context = new CarpenterRepository();
        }

        // GET: CarpenterManager
        public ActionResult Index()
        {
            List<Carpenter> carpenters = context.Collection().ToList(); 

            return View(carpenters);
        }

        public ActionResult Create()
        {
            Carpenter carpenter = new Carpenter();
            return View(carpenter);
        }
        [HttpPost]
        public ActionResult Create(Carpenter carpenter)
        {
            if (!ModelState.IsValid)
            {
                return View(carpenter);
                }
            else
            {
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
                return View(carpenter);
            }
        }
        [HttpPost]
        public ActionResult Edit(Carpenter carpenter, string Id)
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

                carpenterToEdit.Category = carpenter.Category;
                carpenterToEdit.Description = carpenter.Description;
                carpenterToEdit.Image = carpenter.Image;
                carpenterToEdit.Name = carpenter.Name;
                carpenterToEdit.Price = carpenter.Price;
                carpenterToEdit.PhoneNumber = carpenter.PhoneNumber;
                carpenterToEdit.AssociatedVendor = carpenter.AssociatedVendor;

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