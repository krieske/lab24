using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab24.Models;

namespace Lab24.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            lab24Entities ORM = new lab24Entities();
            ViewBag.Items = ORM.Items.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NewItem()
        {
            return View();

        }

        public ActionResult SaveNewItem(item newItem)
        {
            lab24Entities ORM = new lab24Entities();

            //To Do: Validation!

            ORM.Items.Add(newItem);

            ORM.SaveChanges(); // sync with the database
            return RedirectToAction("Index");


        }



        public ActionResult DeleteItem(int itemID)
        {
            lab24Entities ORM = new lab24Entities();

            // for loop to find the id

            // find is a method that is used to find objects by using the primary key
            item ItemToDelete = ORM.Items.Find(itemID);

            // remove
            ORM.Items.Remove(ItemToDelete);

            ORM.SaveChanges(); // To Do: use try catch

            return RedirectToAction("Index");


        }

        public ActionResult ItemDetails(int itemID)
        {
            //this action will show the old data

            lab24Entities ORM = new lab24Entities();

            // find the item
            item ItemToEdit = ORM.Items.Find(itemID);

            if (ItemToEdit ==null)
            {
                return RedirectToAction("Index");
            }


            // send it back to the view
            ViewBag.ItemToEdit = ItemToEdit;

            return View();

        }

        public ActionResult SaveChanges(item UpdatedItem)
        {
            lab24Entities ORM = new lab24Entities();

            // find the old record

            item OldRecord = ORM.Items.Find(UpdatedItem.itemID);

            //To Do: check for null
            if (OldRecord == null)
            {
                return RedirectToAction("Index");
            }

            OldRecord.Name = UpdatedItem.Name;
            OldRecord.Description = UpdatedItem.Description;
            OldRecord.Quantity = UpdatedItem.Quantity;
            OldRecord.Price = UpdatedItem.Price;

            ORM.Entry(OldRecord).State = System.Data.Entity.EntityState.Modified;

            ORM.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult SearchItemByName(string name)
        {
            lab24Entities ORM = new lab24Entities();

            // x.Name.ToLower = Name from Database table
            // contains(name.ToLower() = name from search method in index.cshtml
            ViewBag.Items = ORM.Items.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            return View("Index");

        }
    }
}