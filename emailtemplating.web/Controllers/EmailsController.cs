using EmailTemplating.Models;
using EmailTemplating.Repository;
using EmailTemplating.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailTemplating.Web.Controllers
{
    public class EmailsController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Email()
        {
            IEnumerable<Email> emails;
            using(UnitOfWork unitOfWork = new UnitOfWork())
            {
                emails = unitOfWork.EmailRepository.GetAllEmails().ToList();                
            }
            return View(emails);
        }

        public ActionResult AddEdit(int? id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                EmailViewModel email = new EmailViewModel();
                var templates = unitOfWork.TemplateRepository.GetAllTemplates();
                email.Templates = templates;
                if (id != null)
                {
                    var emailM = unitOfWork.EmailRepository.Find((int) id);
                    email.Templates = templates;

                    email.SelectedDbSource = emailM.DbSource;
                    email.Email = emailM;
                    email.SelectedTemplate = emailM.TemplateID.ToString();
                    return View(email);
                }
                return View(email);
            }
        }
        [HttpPost]
        public ActionResult AddEdit(EmailViewModel obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //Add New Template
                if (obj.Email.EmailID== 0)
                {
                    AddNewEmail(uow, obj.Email,obj.SelectedDbSource,obj.SelectedTemplate);
                }
                //Edit Template
                else
                {
                    EditEmail(uow, obj.Email, obj.SelectedDbSource, obj.SelectedTemplate);
                }
            }
            return RedirectToAction("Email");
        }

        private void EditEmail(UnitOfWork uow, Email email, int dbSource, string TemplatedID)
        {
            Email emailToEdit = uow.EmailRepository.Find(email.EmailID);
            emailToEdit.Category = email.Category;
            emailToEdit.Title = email.Title;
            emailToEdit.Subject = email.Subject;
            emailToEdit.DbSource = Convert.ToInt32(dbSource);
            emailToEdit.TemplateID = Convert.ToInt32(TemplatedID);
            uow.TemplateRepository.SaveChanges();
        }

        private void AddNewEmail(UnitOfWork uow, Email email, int dbSource, string TemplatedID)
        {
            Email EmailToAdd = new Email
            {
                Category = email.Category,
                Title = email.Title,
                Subject = email.Subject,
                DbSource = (int)(dbSource),
                TemplateID = int.Parse(TemplatedID)
            };

            uow.EmailRepository.Add(EmailToAdd);
            uow.EmailRepository.SaveChanges();
        }
        public ActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Email recordToDelete = uow.EmailRepository.Find((int)id);
                uow.EmailRepository.Delete(recordToDelete);
                uow.EmailRepository.SaveChanges();
            }
            return RedirectToAction("Email");
        }
    }
}
