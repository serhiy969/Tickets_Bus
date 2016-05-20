using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tickets_Bus.Models;

namespace Tickets_Bus.Controllers
{
    public class Route_Controller : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();

        // GET: Route_
        public ActionResult Index()
        {
            var route_ = db.Route_.Include(r => r.Driver).Include(r => r.Station).Include(r => r.Station1);
            return View(route_.ToList());
        }

        // GET: Route_/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_ route_ = db.Route_.Find(id);
            if (route_ == null)
            {
                return HttpNotFound();
            }
            return View(route_);
        }

        // GET: Route_/Create
        public ActionResult Create()
        {
            ViewBag.ID_Driver = new SelectList(db.Drivers, "ID_Driver", "FirstLastName");
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station");
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station");
            return View();
        }

        // POST: Route_/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Route,Departure,Arrival,Date_departure,Date_arrival,ID_Driver")] Route_ route_)
        {
            if (ModelState.IsValid)
            {
                db.Route_.Add(route_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Driver = new SelectList(db.Drivers, "ID_Driver", "FirstLastName", route_.ID_Driver);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Departure);
            return View(route_);
        }

        // GET: Route_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_ route_ = db.Route_.Find(id);
            if (route_ == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Driver = new SelectList(db.Drivers, "ID_Driver", "FirstLastName", route_.ID_Driver);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Departure);
            return View(route_);
        }

        // POST: Route_/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Route,Departure,Arrival,Date_departure,Date_arrival,ID_Driver")] Route_ route_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Driver = new SelectList(db.Drivers, "ID_Driver", "FirstLastName", route_.ID_Driver);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Departure);
            return View(route_);
        }

        // GET: Route_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_ route_ = db.Route_.Find(id);
            if (route_ == null)
            {
                return HttpNotFound();
            }
            return View(route_);
        }

        // POST: Route_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route_ route_ = db.Route_.Find(id);
            db.Route_.Remove(route_);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
