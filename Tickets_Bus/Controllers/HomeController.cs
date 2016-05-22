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
        public ActionResult Index([Bind(Include = "ID_Route,Departure,Arrival")] Route_ route_)
        {
            if (ModelState.IsValid)
            {
                db.Route_.Add(route_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Arrival = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Arrival);
            ViewBag.Departure = new SelectList(db.Stations, "ID_Station", "Name_Station", route_.Departure);
            
            return View(route_);
        }

        public ActionResult ShowRouts(int? Departure, int? Arrival, DateTime? Date_Route)
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
            var a = (from rou in db.Route_
                join st1 in db.Stations on rou.Departure equals st1.ID_Station
                join st3 in db.Stations on rou.Arrival equals st3.ID_Station
                join rts in db.Route_Station on rou.ID_Route equals rts.ID_Route
                join st2 in db.Stations on rts.ID_Station equals st2.ID_Station
                join dr in db.Drivers on rou.ID_Driver equals dr.ID_Driver
                join bs in db.Buses on dr.ID_bus equals bs.ID_Bus
                where rou.Departure == Departure
                where rts.ID_Station == Arrival
                where rts.ID_Date_Route == Date_Route
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

        [Authorize(Roles = "admin")]
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