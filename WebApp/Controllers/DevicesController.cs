using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DevicesController : Controller
    {
        private MContext db = new MContext();

        // GET: Devices
        public ActionResult Index()
        {
          // int sizeCategories = db.Categories.Count();
            ViewBag.categorys = db.Categories.ToList();
            return View(db.Devices.ToList());
        }

        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);

            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.category = db.Categories.Find(device.cat_id);
            var props = new List<Property>();
            int numofProp=db.DeviceProps.Where(n => n.D_Id == device.Id).Count();
            var test = db.DeviceProps.Where(n => n.D_Id == device.Id).ToList();
            for (int i = 0; i < numofProp; i++)
            {
                props.Add(db.Properties.Find(test[i].P_Id));
            }
            ViewBag.prop = db.DeviceProps.Where(n=> n.D_Id == device.Id);
            ViewBag.propertiesOfDevice = props;

            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            ViewBag.category = db.Categories.ToList();
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Device device, HttpPostedFileBase img)
        {
            ViewBag.category = db.Categories.ToList();
            if (ModelState.IsValid)
            {
                if(img == null)
                {
                    
                    device.img = "default.png";
                }
                else {
                    string imgName = img.FileName;
                    img.SaveAs(Server.MapPath($"~/Images/{img.FileName}"));
                    device.img = imgName;
                }
                
                db.Devices.Add(device);
                db.SaveChanges();
                
                return RedirectToAction("Create", "DeviceProps", new { id = device.Id });
            }

            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.category = db.Categories.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DeviceName,Memo,AcquisitionDate,cat_id")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Devices.Find(id);
            db.Devices.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
