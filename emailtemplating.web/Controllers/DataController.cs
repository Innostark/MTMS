using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailTemplating.Web.Controllers
{
    public class DataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Employees.OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }

        public ActionResult Customers()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Customers.OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }

        public ActionResult Invoices()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Invoices.OrderByDescending(m => m.Date));
        }

        public ActionResult InvoicesVM()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.InvoicesViewModel.OrderByDescending(m => m.Date));
        }

    }
}
