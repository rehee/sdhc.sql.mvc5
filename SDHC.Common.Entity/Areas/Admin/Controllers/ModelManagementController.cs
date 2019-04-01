using SDHC.Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSQL.Areas.Admin.Controllers
{
  public class ModelManagementController : Controller
    {
        // GET: Admin/ModelManagement
        public ActionResult Index(string id)
        {
            var type = ModelManager.GetModelType(id);
            if (type == null)
            {
                goto GoToDashBoard;
            }
            if (type.IsSingleRecordTable())
            {
                var singleRecord = ModelManager.Read<IInt64Key>(type, b => true).FirstOrDefault();
                if (singleRecord == null)
                {
                    return RedirectToAction("Create", "ModelManagement", new { @id = id, @area = G.AdminPath });
                }
                return RedirectToAction("Edit", "ModelManagement", new { @type = id, @id = singleRecord.Id, @area = G.AdminPath });
            }
            ViewBag.id = id;
            var model = ModelManager.GetContentTableHtmlView(type);
            return View(model);
            GoToDashBoard:
            return RedirectToAction("Index", "DashBoard", new { @area = G.AdminPath });
        }

        public ActionResult Create(string id)
        {
            var type = ModelManager.GetModelType(id);
            if (type == null)
            {
                goto GoToDashBoard;
            }

            var passModel = ModelManager.GetModelPostModelByType(type);
            if (passModel == null)
            {
                goto GoToDashBoard;
            }
            return View(passModel);
            GoToDashBoard:
            return RedirectToAction("Index", "DashBoard", new { @area = G.AdminPath });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ModelPostModel model)
        {
            ModelManager.Create(model);
            var key = ModelManager.GetMapperKey(model.FullType);
            if (String.IsNullOrEmpty(key))
            {
                return RedirectToAction("Index", "DashBoard", new { @area = G.AdminPath, @id = key });
            }
            return RedirectToAction("Index", "ModelManagement", new { @area = G.AdminPath, @id = key });
        }
        public ActionResult Edit(string type, long id)
        {
            if (string.IsNullOrEmpty(type))
            {
                return RedirectToAction("Index", "DashBoard", new { @area = G.AdminPath });
            }
            var model = ModelManager.Find(type, id, out ISave repo);
            return View(model.ConvertModelToModelPostModel());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ModelPostModel model)
        {
            var key = ModelManager.GetMapperKey(model.FullType);
            try
            {
                ModelManager.Update(model);
            }
            catch { }
            return RedirectToAction("Index", "ModelManagement", new { @area = G.AdminPath, @id = key });
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(string deleteId, string type)
        {
            var start = DateTime.Now;
            ModelManager.Delete(type, deleteId.MyTryConvert<long>());
            var end = DateTime.Now;
            var ms = (end - start).Milliseconds;
            if (ms < 1000)
            {
                System.Threading.Thread.Sleep(1000 - ms);
            }
            return RedirectToAction("Index", "ModelManagement", new { @area = G.AdminPath, @id = type });
        }
    }
}