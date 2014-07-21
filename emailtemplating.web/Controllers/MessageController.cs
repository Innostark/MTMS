﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailTemplating.Web.Controllers
{
    public class MessageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send()
        {
            return View("ForYouToImplement");
        }

        public ActionResult Password()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Customers.OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }


        public ActionResult Promotion(string id)
        {
            if (string.IsNullOrEmpty(id)) { id = "Unknown?"; }
            ViewBag.City = id;

            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Customers.Where(m => string.Equals(m.City, id, StringComparison.CurrentCultureIgnoreCase)).OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }


        public ActionResult Parking()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Employees.OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }

        public ActionResult Sales()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            var max_date = dataset.Invoices.Max(m => m.Date);
            max_date = max_date.AddDays(-1 * max_date.Day);     //end of last month
            var min_date = new DateTime(max_date.Year, max_date.Month, 1);
            string period = min_date.ToString("MMMM, yyyy");

            ViewBag.Period = period;
            return View(dataset.BuildEmployeeSalesSummaryViewModel(min_date, max_date, period).OrderBy(m => m.LastName).ThenBy(m => m.FirstName));
        }


    }
}
