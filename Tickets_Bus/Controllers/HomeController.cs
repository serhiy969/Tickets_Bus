using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Tickets_Bus.Models;

namespace Tickets_Bus.Controllers
{
    public class HomeController : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();
        public ActionResult Index()
        {
            var route_ = db.Route_.Include(r => r.Station).Include(r => r.Station1);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station");
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID_Route,Departure,Arrival,DateArrival")] Route_ route_)
        {
            if (ModelState.IsValid)
            {
                db.Route_.Add(route_);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Departure);
            ViewBag.Date_Route = new SelectList(db.Route_Station, "ID_Route", "ID_Date_Route",
                route_.DateArrival);


            return View(route_);
        }

        public ActionResult ShowRouts(int? Departure, int? Arrival, string DateArrival)
        {
            //var a = (from r in db.Route_
            //         join rs in db.Route_Station on r.ID_Route equals rs.ID_Route
            //         where r.Departure == Departure
            //         where r.Arrival == Arrival

            //         select new RouteViewModel()
            //         {
            //             RouteStation = rs,
            //             Route = r,
            //             ID_Route = r.ID_Route,
            //             Station = rs.Station,
            //             Daeparture = r.Departure,
            //             Date_Departure = r.Date_departure,
            //             ID_Station = rs.ID_Station,
            //             Date_Arrival = rs.Date_arrival
            //         }).ToList();
            DateTime dt = Convert.ToDateTime(DateArrival);
            ;
            var a = (from rou in db.Route_
                join st1 in db.Stations on rou.Departure equals st1.ID_Station
                join st3 in db.Stations on rou.Arrival equals st3.ID_Station
                join rts in db.Route_Station on rou.ID_Route equals rts.ID_Route
                join st2 in db.Stations on rts.ID_Station equals st2.ID_Station
                join dr in db.Drivers on rou.ID_Driver equals dr.ID_Driver
                join bs in db.Buses on dr.ID_bus equals bs.ID_Bus
                where rou.Departure == Departure
                where rts.ID_Station == Arrival
                     where rts.ID_Date_Route == dt
                     select new RouteViewModel()
                {
                    RouteStation = rts,
                    Route = rou,
                    Date_Route = rts.ID_Date_Route,
                    Station1 = st1.Name_Station,
                    Date_Departure = rou.Date_departure,
                    Date_Arrival = rts.Date_arrival,
                    Station2 = st2.Name_Station,
                    Distance_ = rts.Distance,
                    Reliability_ = bs.Reliability,
                    ID_Route = rou.ID_Route,
                    StationD = st1.Name_Station,
                    StationA = st3.Name_Station,
                    Name_buses = bs.Name_Bus
                }).ToList();
           

            return View(a);

        }

        public ActionResult Create(int? routeId, int? departure, int? arrival, string DateArrival)
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
                //var b = (from tk in db.Tickets
                //         join rt in db.Route_ on tk.ID_Route equals rt.ID_Route
                //         join rts in db.Route_Station on rt.ID_Route equals  rts.ID_Route
                //         join dr in db.Drivers on rt.ID_Driver equals dr.ID_Driver
                //         join bs in db.Buses on dr.ID_bus equals bs.ID_Bus
                //         where tk.ID_Route == routeId
                //         where tk.Date_Sale == DateArrival
                //         select new NumbSeats() { Numb_Seat = tk.Numb_Seat, Num_Seats = bs.Num_Seats }).ToList();

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

                int[] result = new int[all.Count()];
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
            //ViewBag.Date_Route = new SelectList(db.Route_Station, "ID_Route", "ID_Date_Route", DateArrival);
            
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

        public ActionResult Details(int? id, int? routeId, int? departure, int? arrival, string DateArrival)
        {
            if (routeId == null)
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

        public ActionResult About(int? Departure, int? Arrival)
        {
            ViewBag.Message = "Your application description page.";
            //var route_Station = db.Route_.Include(r => r.Route_Station).Include(r => r.Station);
            //var res = ()
            return View();
            
        }

        [Authorize]
        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            //return View();
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }
    }
}