using Asp.NetMvc_MvcGrid.Models;
using Asp.NetMvc_MvcGrid.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.NetMvc_MvcGrid.Controllers
{
    public class GridController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var model = db.Employees.ToList();
            return View(model);
        }
        public ActionResult Add()
        {
            List<SelectListItem> countries =
                (from i in db.Countries.ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();

            ViewBag.Countries = countries;

            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            var _vrCountry = db.Countries.Where(w => w.Id == emp.Country.Id).FirstOrDefault();
            emp.Country = _vrCountry;

            db.Employees.Add(emp);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var _vrEmployee = db.Countries.Where(w => w.Id == id).FirstOrDefault();

            List<SelectListItem> countries =
                (from i in db.Countries.ToList()
                 select new SelectListItem
                 {
                     Text = i.Name,
                     Value = i.Id.ToString()
                 }).ToList();

            ViewBag.Countries = countries;

            return View();
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            var _vrEmployee = db.Employees.Where(w => w.Id == emp.Id).FirstOrDefault();
            _vrEmployee.firstName = emp.firstName;
            _vrEmployee.lastName = emp.lastName;
            _vrEmployee.Age = emp.Age;


            var _vrCountry = db.Countries.Where(w => w.Id == emp.Country.Id).FirstOrDefault();
            emp.Country = _vrCountry;

            db.SaveChanges();


            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id!=null)
            {
                var _vrEmployee = db.Employees.Where(w => w.Id == id).FirstOrDefault();
                if (_vrEmployee!=null)
                {
                    db.Employees.Remove(_vrEmployee);
                    int _intValue = db.SaveChanges();
                    if (_intValue>0)
                    {
                        //Database Add
                    }
                    else
                    {
                        // Database Doesnt Add
                    }
                }
            }
            return View();
        }
    }
}