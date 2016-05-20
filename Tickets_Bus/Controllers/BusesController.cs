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
    public class BusesController : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();

        // GET: Buses
        public ActionResult Index()
        {
            return View(db.Buses.ToList());
        }

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUS bUS = db.Buses.Find(id);
            if (bUS == null)
            {
                return HttpNotFound();
            }
            return View(bUS);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Bus,Name_Bus,Number_Bus,Num_Seats,Date_LastTO,Reliability")] BUS bUS)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(bUS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bUS);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUS bUS = db.Buses.Find(id);
            if (bUS == null)
            {
                return HttpNotFound();
            }
            return View(bUS);
        }

        // POST: Buses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Bus,Name_Bus,Number_Bus,Num_Seats,Date_LastTO,Reliability")] BUS bUS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bUS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bUS);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BUS bUS = db.Buses.Find(id);
            if (bUS == null)
            {
                return HttpNotFound();
            }
            return View(bUS);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BUS bUS = db.Buses.Find(id);
            db.Buses.Remove(bUS);
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
