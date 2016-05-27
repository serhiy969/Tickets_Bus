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
    public class ReportsController : Controller
    {
        private Tickets_BussEntities db = new Tickets_BussEntities();

        // GET: Reports
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

        public ActionResult Show_Route( string DateArrival)
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
                     where rts.ID_Date_Route == dt   
                     orderby bs.Reliability ascending
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
        //Перегляд Автобусів по кількості місць
        public ActionResult Details_Bus()
        {

            //var buss = db.Buses.Include(r => r.ID_Bus).Include(r => r.Num_Seats).Distinct().ToList();
            ViewBag.Num_Seats = new SelectList(db.Buses.Distinct(), "Num_Seats", "Num_Seats").Distinct().ToList(); 

            //var buss = (from b in db.Buses
            //            select new DriverDetails
            //            {
                            
            //                Num_Seats = b.Num_Seats

            //            })
            //    .ToList();
            //var ok = buss.Distinct();
            //ViewBag.Num_Seats = ok;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ActionName("Details_Bus2")]
        public ActionResult Details_Bus([Bind(Include = "ID_Bus,Name_Bus,Number_Bus,Num_Seats,Date_LastTO,Reliability")] BUS buss)
        {
            if (ModelState.IsValid)
            {
                db.Buses.Add(buss);

                db.SaveChanges();
                return RedirectToAction("Details_Bus");
            }

           
            ViewBag.NumSeats = new SelectList(db.Buses, "ID_Bus", "Num_Seats", buss.Num_Seats);


            return View(buss);
        }

        public ActionResult Show_Bus(BUS model)
        {
            var a = (from r in db.Buses
                     where r.Num_Seats == model.Num_Seats

                     select new DriverDetails()
                     {
                         ID_Bus = r.ID_Bus,
                         Name_Bus = r.Name_Bus,
                         Number_Bus = r.Number_Bus,
                         Num_Seats = r.Num_Seats,
                         Reliability = r.Reliability,
                         Date_LastTO = r.Date_LastTO
                     }).ToList();
            ;
           
            return View(a);

        }
        //Звіт про кількість місць на рейс
        public ActionResult Details_Ticket()
        {
            var rout = db.Route_.Include(r => r.Station).Include(r => r.Station1);
            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station");
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details_Ticket([Bind(Include = "ID_Route,Departure,Arrival,DateArrival")] Route_ rout)
        {
            if (ModelState.IsValid)
            {
                db.Route_.Add(rout);

                db.SaveChanges();
                return RedirectToAction("Details_Ticket");
            }

            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", rout.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", rout.Departure);
            ViewBag.Date_Route = new SelectList(db.Route_Station, "ID_Route", "ID_Date_Route",rout.DateArrival);


            return View(rout);
        }

        public ActionResult Show_Ticket(int? Departure, int? Arrival, string DateArrival)
        {
           
            DateTime dt = Convert.ToDateTime(DateArrival);
            ;
            var a = from tk in db.Tickets
                        //join st1 in db.Stations on tk.Departure equals st1.ID_Station
                        //join st2 in db.Stations on tk.Arrival equals st2.ID_Station
                       //join rou in db.Route_ on tk.ID_Route equals rou.ID_Route
                        //join driv in db.Drivers on rou.ID_Driver equals driv.ID_Driver
                        //join bus in db.Buses on driv.ID_bus equals bus.ID_Bus
                        //join rts in db.Route_Station on tk.ID_Route equals rts.ID_Route
                    //select tk;
                    where tk.Departure == Departure
                    where tk.Arrival == Arrival
                    where tk.Date_Sale == dt
                group tk by tk.Route_
                into tiket
                orderby tiket.Count() descending
                select tiket;
            ViewBag._Daate = dt.Date;
            //select new RouteViewModel()
            //{
            //    ID_Ticket = tk.ID_Ticket,
            //    ID_Route = tk.ID_Route
            //}).ToList();

            //var info = a.Count();
            //foreach (var count in a.OrderBy(x => x.Count))
            //{
            //    var c = count.Count.ToString();
            //}

            return View(a);

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
