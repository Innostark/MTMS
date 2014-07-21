using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailTemplating.Web.Controllers
{
    public class TemplatesController : Controller
    {
        //
        // GET: /Templates/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid()
        {
            var dataset = new EmailTemplating.SampleData.DataSet();
            return View(dataset.Templates);
        }

        public ActionResult Add()
        {
            return View("ForYouToImplement");
        }
        public ActionResult Edit(int id)
        {
            return View("ForYouToImplement");
        }
        public ActionResult Delete(int id)
        {
            return View("ForYouToImplement");
        }
    }
}
