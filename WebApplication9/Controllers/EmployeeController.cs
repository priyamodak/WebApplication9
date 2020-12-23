using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication9.Context;

using WebApplication9.Models;

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
        //public ActionResult EmployeeList()
        //{
        //    List<EmployeeViewModel> emplist = dbObj.Admins.Select(x => new EmployeeViewModel
        //    {
        //        ID = x.ID,
        //        EmployeeName = x.EmployeeName,
        //        Client = x.Client,
        //        Amount = x.Amount,
        //        Date = x.Date,
        //        PurchasedItem = x.PurchasedItem,
        //        Bill = x.Bill
        //    }).ToList();


        //    return View(emplist);
        //}
        public void ExportToExcel()
        {
            List<EmployeeViewModel> emplist = dbObj.Admins.Select(x => new EmployeeViewModel
            {
                ID = x.ID,
                EmployeeName = x.EmployeeName,
                Client = x.Client,
                Amount = x.Amount,
                Date = x.Date,
                PurchasedItem = x.PurchasedItem,
                Bill = x.Bill
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "ID";
            ws.Cells["B6"].Value = "EmployeeName";
            ws.Cells["B6"].Value = "Client";
            ws.Cells["C6"].Value = "Amount";
            ws.Cells["D6"].Value = "Date";
            ws.Cells["E6"].Value = "PurchasedItem";
            ws.Cells["F6"].Value = "Bill";

            int rowStart = 7;
            foreach (var item in emplist)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.EmployeeName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Client;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Amount;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Date;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.PurchasedItem;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Bill;

                rowStart++;

            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
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

    