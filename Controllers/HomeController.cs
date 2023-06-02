
using Microsoft.Ajax.Utilities;
using MyFirstApp.Data;
using MyFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DcEntities db = new DcEntities();
            List<EmpModel>dbm = new List<EmpModel>();
            var res=db.Employees.ToList();
            foreach (var item in res)
            {
                dbm.Add(new EmpModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    City = item.City
                }); 
            }
            return View(dbm);  
        }
        public ActionResult AddForm() {
        

            return View();
        }

        [HttpPost]
        public ActionResult AddForm(EmpModel model)
        {
            DcEntities db = new DcEntities();
            Employee emp = new Employee();
            emp.Id = model.Id;
            emp.Name = model.Name;
            emp.City = model.City;
            if(model.Id == 0)
            {
                db.Employees.Add(emp);
                db.SaveChanges();
            }
            else
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index"); 
        }

        public ActionResult Delete(int Id)
        {
            DcEntities db=new DcEntities();
            var del=db.Employees.Where(x => x.Id == Id).First();
            db.Employees.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int Id)
        {
            EmpModel model = new EmpModel();
            
            DcEntities db = new DcEntities();
            var data = db.Employees.Where(x => x.Id == Id).First();
            model.Id = data.Id;
            model.City = data.City;
            model.Name = data.Name;
            return View(model);
        }


        [HttpPost]

        public ActionResult Edit(EmpModel model)
        {
            DcEntities db = new DcEntities();
            Employee emp = new Employee();
            emp.Id = model.Id;
            emp.Name = model.Name;
            emp.City = model.City;
            if (model.Id!= 0)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
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
    }
}