using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailTemplating.Models;
using EmailTemplating.Repository;

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

        public ActionResult AddEdit(int? id)
        {
            //UnitOfWork unitOfWork = new UnitOfWork();
            //MergeVarMap recordFromDb = unitOfWork.MergerVarMapRepository.Find((int)id);
            //return View(recordFromDb);
            UnitOfWork unitOfWork = new UnitOfWork();
            return View();
        }
        public ActionResult AddEdit(Template obj)
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
