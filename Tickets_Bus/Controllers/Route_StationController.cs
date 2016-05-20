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
    public class Route_StationController : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();

        // GET: Route_Station
        public ActionResult Index()
        {
            var route_Station = db.Route_Station.Include(r => r.Route_).Include(r => r.Station);
            return View(route_Station.ToList());
        }

        // GET: Route_Station/Details/5
        public ActionResult Details(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Station route_Station = db.Route_Station.Find(id2,id);
            if (route_Station == null)
            {
                return HttpNotFound();
            }
            return View(route_Station);
        }

        // GET: Route_Station/Create
        public ActionResult Create()
        {
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route");
            ViewBag.ID_Station = new SelectList(db.Stations, "ID_Station", "Name_Station");
            return View();
        }

        // POST: Route_Station/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Station,ID_Route,Date_departure,Date_arrival,Distance,Numof_Order")] Route_Station route_Station)
        {
            if (ModelState.IsValid)
            {
                db.Route_Station.Add(route_Station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", route_Station.ID_Route);
            ViewBag.ID_Station = new SelectList(db.Stations, "ID_Station", "Name_Station", route_Station.ID_Station);
            return View(route_Station);
        }

        // GET: Route_Station/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Station route_Station = db.Route_Station.Find(id2,id);
            if (route_Station == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", route_Station.ID_Route);
            ViewBag.ID_Station = new SelectList(db.Stations, "ID_Station", "Name_Station", route_Station.ID_Station);
            return View(route_Station);
        }

        // POST: Route_Station/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Station,ID_Route,Date_departure,Date_arrival,Distance,Numof_Order")] Route_Station route_Station)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route_Station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", route_Station.ID_Route);
            ViewBag.ID_Station = new SelectList(db.Stations, "ID_Station", "Name_Station", route_Station.ID_Station);
            return View(route_Station);
        }

        // GET: Route_Station/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Station route_Station = db.Route_Station.Find(id2,id);
            if (route_Station == null)
            {
                return HttpNotFound();
            }
            return View(route_Station);
        }

        // POST: Route_Station/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, int? id2)
        {
            Route_Station route_Station = db.Route_Station.Find(id);
            db.Route_Station.Remove(route_Station);
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
