using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication9.Context;


namespace WebApplication9.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        Photo_GalleryEntities dbObj = new Photo_GalleryEntities();
        public ActionResult Employee(Admin obj)
        {
            if (obj != null)
                return View(obj);
            else

            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Admin model)
        {
            Admin obj = new Admin();
            obj.ID = model.ID;
            obj.EmployeeName = model.EmployeeName;
            obj.Client = model.Client;
            obj.Amount = model.Amount;
            obj.Date = model.Date;
            obj.PurchasedItem = model.PurchasedItem;
            obj.Bill = model.Bill;

            if(model.ID==0)
            {

         
            dbObj.Admins.Add(obj);
            dbObj.SaveChanges();
            ////ModelState.Clear();
           
            }
            else
            {
                dbObj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
            }
            return View("Employee");
        }


        public ActionResult EmployeeList()
        {
            var res = dbObj.Admins.ToList();

            return View(res);
        }
        
        public ActionResult Delete(int id)
        {
            var res = dbObj.Admins.Where(x => x.ID == id).First();
            dbObj.Admins.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.Admins.ToList();

            return View("EmployeeList",list);
        }
        
     
    }
}

    