using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailTemplating.Models;
using EmailTemplating.Repository;
using EmailTemplating.Web.Models;

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
            UnitOfWork unitOfWork = new UnitOfWork();
            var templates = unitOfWork.TemplateRepository.GetAllTemplates();
            return View(templates);
        }

        public ActionResult AddEdit(int? id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            TemplateViewModel template = new TemplateViewModel();
            var mergeVarMaps = unitOfWork.MergerVarMapRepository.GetAllMergeVarMap();
            template.MergeVarMaps = mergeVarMaps;
            if (id != null)
            {
                var templates = unitOfWork.TemplateRepository.FindTemplate((int)id);
                template.Template = templates;
                return View(template);
            }
            return View(template);
        }
        [HttpPost]
        public ActionResult AddEdit(TemplateViewModel obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //Add New Template
                if (obj.Template.TemplateID == 0)
                {
                    AddNewTemplate(uow, obj.Template, obj.SelectedMergeVarMap);
                }
                //Edit Template
                else
                {
                    EditTemplate(uow,obj.Template, obj.SelectedMergeVarMap);
                }
            }
            
            return RedirectToAction("Grid");
        }

        private void AddNewTemplate(UnitOfWork uow, Template template, string selectedMergeVarMap)
        {
            Template templateToAdd = new Template
                                     {
                                         Body = template.Body,
                                         Description = template.Description,
                                         MergeVarMapID = Convert.ToInt32(selectedMergeVarMap),
                                         Name = template.Name
                                     };
            uow.TemplateRepository.Add(templateToAdd);
            uow.TemplateRepository.SaveChanges();
        }

        private void EditTemplate(UnitOfWork uow, Template template, string selectedMergeVarMap)
        {
            Template templateToEdit = uow.TemplateRepository.Find(template.TemplateID);
            templateToEdit.Name = template.Name;
            templateToEdit.Description= template.Description;
            templateToEdit.Body = template.Body;
            templateToEdit.MergeVarMapID = Convert.ToInt32(selectedMergeVarMap);
            uow.TemplateRepository.SaveChanges();
        }

        public ActionResult Edit(int id)
        {
            return View("ForYouToImplement");
        }
        public ActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Template recordToDelete = uow.TemplateRepository.Find((int)id);
                uow.TemplateRepository.Delete(recordToDelete);
                uow.TemplateRepository.SaveChanges();
            }
            return RedirectToAction("Grid");
        }
    }
}
