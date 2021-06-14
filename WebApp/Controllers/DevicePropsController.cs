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
    public class DevicePropsController : Controller
    {
        private MContext db = new MContext();

        // GET: DeviceProps
        public ActionResult Index()
        {
            var deviceProps = db.DeviceProps.Include(d => d.Device).Include(d => d.Property);
            return View(deviceProps.ToList());
        }

        // GET: DeviceProps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProp deviceProp = db.DeviceProps.Find(id);
            if (deviceProp == null)
            {
                return HttpNotFound();
            }
            return View(deviceProp);
        }

        // GET: DeviceProps/Create
        public ActionResult Create(int? id)
        {
            
                //ViewBag.D_Id = new SelectList(db.Devices, "Id", "DeviceName");
                //ViewBag.P_Id = new SelectList(db.Properties, "Id", "Descraption");

                //new
                Device device = db.Devices.Find(id);
                Category category = db.Categories.Find(device.cat_id);
                ViewBag.device = device;
                ViewBag.deviceId = device.Id;
                ViewBag.catProp = category.Properties.ToList();

                return View(/*db.DeviceProps.ToList()*/);
            

        }

        // POST: DeviceProps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(/*List<DeviceProp> deviceProps*//*string prop*/ DeviceProp deviceProp)
        {

            //if (ModelState.IsValid)
            //{
            //    if (deviceProps == null || deviceProps.Count() == 0)
            //    {
            //        ViewData["message"] = "please select at least one image";
            //    }
            //    foreach(var devProp in deviceProps)
            //    {
            //        db.DeviceProps.Add(devProp);
            //        db.SaveChanges();

            //    }

            // return View(db.DeviceProps.ToList());

            if (ModelState.IsValid)
            {

                db.DeviceProps.Add(deviceProp);
                db.SaveChanges();
                return RedirectToAction("Index","Devices");
            }
            return View();
        }

            //////ViewBag.D_Id = new SelectList(db.Devices, "Id", "DeviceName", deviceProp.D_Id);
            ///////ViewBag.P_Id = new SelectList(db.Properties, "Id", "Descraption", deviceProp.P_Id);
        //    return View(deviceProps);
        //}

        // GET: DeviceProps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProp deviceProp = db.DeviceProps.Find(id);
            if (deviceProp == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_Id = new SelectList(db.Devices, "Id", "DeviceName", deviceProp.D_Id);
            ViewBag.P_Id = new SelectList(db.Properties, "Id", "Descraption", deviceProp.P_Id);
            return View(deviceProp);
        }

        // POST: DeviceProps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "D_Id,P_Id,value")] DeviceProp deviceProp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deviceProp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_Id = new SelectList(db.Devices, "Id", "DeviceName", deviceProp.D_Id);
            ViewBag.P_Id = new SelectList(db.Properties, "Id", "Descraption", deviceProp.P_Id);
            return View(deviceProp);
        }

        // GET: DeviceProps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeviceProp deviceProp = db.DeviceProps.Find(id);
            if (deviceProp == null)
            {
                return HttpNotFound();
            }
            return View(deviceProp);
        }

        // POST: DeviceProps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeviceProp deviceProp = db.DeviceProps.Find(id);
            db.DeviceProps.Remove(deviceProp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
