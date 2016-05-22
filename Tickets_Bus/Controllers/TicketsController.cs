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
    public class TicketsController : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Route_).Include(t => t.Station).Include(t => t.Station1);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create(int? routeId, int? departure, int? arrival)
        {
            if (routeId != null)
            {
                var route = db.Route_.Find(routeId);
                var a = (from r in db.Route_Station
                         where r.ID_Route == routeId
                         where r.ID_Station == arrival
                         select new RouteStInfo() { Distance = r.Distance }).ToList();

                double sum = a[0].Distance;

                ViewBag.Distance = (sum * 0.7).ToString();

                var b = (from tk in db.Tickets
                         join rt in db.Route_ on tk.ID_Route equals rt.ID_Route
                         join dr in db.Drivers on rt.ID_Driver equals dr.ID_Driver
                         join bs in db.Buses on dr.ID_bus equals bs.ID_Bus
                         where tk.ID_Route == routeId
                         select new NumbSeats() { Numb_Seat = tk.Numb_Seat, Num_Seats = bs.Num_Seats }).ToList();

                int[] seats = new int[b.Count];
                for (int r = 0; r < b.Count; r++)
                {
                    seats[r] = b[r].Numb_Seat;
                }

                int[] all = new int[b[0].Num_Seats];
                for (int i = 0; i < b[0].Num_Seats; i++)
                {
                    all[i] = i + 1;
                }

                //int[] result = new int[all.Count()];
                foreach (var tr in all)
                {
                    if (seats.Contains(tr))
                    {
                        all = all.Where(w => w != tr).ToArray();
                    }
                }
                ViewBag.Free = new SelectList(all);
            }
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", routeId);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", departure);

            return View();
        }


        // POST: Tickets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Ticket,ID_Route,Departure,Arrival,Numb_Seat,Price,Name_Surname,Date_Sale")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", ticket.ID_Route);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Departure);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", ticket.ID_Route);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Departure);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Ticket,ID_Route,Departure,Arrival,Numb_Seat,Price,Name_Surname,Date_Sale")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Route = new SelectList(db.Route_, "ID_Route", "ID_Route", ticket.ID_Route);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", ticket.Departure);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
