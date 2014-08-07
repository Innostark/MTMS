using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmailTemplating.Models;
using EmailTemplating.Repository;
using EmailTemplating.Repository.Interfaces;
using EmailTemplating.Repository.Repositories;

namespace EmailTemplating.Web.Controllers
{
    public class MergeTagMapsController : Controller
    {
        //
        // GET: /MergeTagMaps/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Grid()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            var mergeVarmaps = unitOfWork.MergerVarMapRepository.GetAllMergeVarMap();
            //var dataset = new EmailTemplating.SampleData.DataSet();
            return View(mergeVarmaps);
        }

        public ActionResult AddEdit(int? id)
        {
            if (id != null)
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                MergeVarMap recordFromDb = unitOfWork.MergerVarMapRepository.Find((int)id);
                return View(recordFromDb);
            }
            return View();
        }

        private void AddMergeVarMap(UnitOfWork uow, MergeVarMap obj)
        {
            MergeVarMap map = new MergeVarMap
            {
                Name = obj.Name,
                MapItems = new List<MergeVarMapItem>()
            };
            if (obj.MapItems != null)
            {
                foreach (var mergeVarMapItem in obj.MapItems)
                {
                    //Add New map Item
                    map.MapItems.Add(new MergeVarMapItem
                    {
                        VariableName = mergeVarMapItem.VariableName,
                        PropertyName = mergeVarMapItem.PropertyName
                    });
                }
            }
            uow.MergerVarMapRepository.Add(map);
            uow.MergerVarMapRepository.SaveChanges();
        }

        private void EditMergeVarMap(UnitOfWork uow, MergeVarMap obj)
        {
            MergeVarMap mergeVarMapToUpdate = uow.MergerVarMapRepository.Find(obj.MergeVarMapID);
            mergeVarMapToUpdate.Name = obj.Name;
            uow.MergerVarMapRepository.SaveChanges();
            if (obj.MapItems != null)
            {
                foreach (var mergeVarMapItem in obj.MapItems)
                {
                    //Edit MergeVarMapItem
                    if (mergeVarMapItem.MergeVarMapItemID > 0)
                    {
                        MergeVarMapItem mergeVarMapItemToUpdate =
                            uow.MergerVarMapItemRepository.Find(mergeVarMapItem.MergeVarMapItemID);
                        mergeVarMapItemToUpdate.VariableName = mergeVarMapItem.VariableName;
                        mergeVarMapItemToUpdate.PropertyName = mergeVarMapItem.PropertyName;
                    }
                    //Add new MergeVarMapItem
                    else
                    {
                        MergeVarMapItem newItemToAdd = new MergeVarMapItem
                        {
                            MergeVarMapID = obj.MergeVarMapID,
                            VariableName = mergeVarMapItem.VariableName,
                            PropertyName = mergeVarMapItem.PropertyName
                        };
                        uow.MergerVarMapItemRepository.Add(newItemToAdd);
                    }
                }
                uow.MergerVarMapItemRepository.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult AddEdit(MergeVarMap obj)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                //New MergeVarMap
                if (obj.MergeVarMapID == 0)
                {
                    AddMergeVarMap(uow, obj);
                }
                //Update MergeVarMap
                else
                {
                    EditMergeVarMap(uow, obj);
                }
            }
            return RedirectToAction("Grid");
        }

        public ActionResult Edit(int id)
        {
            return View("ForYouToImplement");
        }
        public ActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                MergeVarMap recordToDelete = uow.MergerVarMapRepository.Find((int)id);
                uow.MergerVarMapRepository.Delete(recordToDelete);
                uow.MergerVarMapRepository.SaveChanges();
            }
            return RedirectToAction("Grid");
        }

    }
}

