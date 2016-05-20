﻿using System;
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
        public ActionResult Index([Bind(Include = "ID_Route,Departure,Arrival,Date_departure,")] Route_ route_)
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

        public ActionResult ShowRouts(int? Departure, int? Arrival)
        {
            var a = (from r in db.Route_
                     join rs in db.Route_Station on r.ID_Route equals rs.ID_Route
                     where r.Departure == Departure
                     where r.Arrival == Arrival
                     select new RouteViewModel() { RouteStation = rs, Route = r, ID_Route = r.ID_Route, Station = rs.Station, Daeparture = r.Departure, Date_Departure = r.Date_departure, ID_Station = rs.ID_Station, Date_Arrival = rs.Date_arrival }).ToList();

            return View(a);

        }

        public ActionResult About(int? Departure, int? Arrival)
        {
            ViewBag.Message = "Your application description page.";
            //var route_Station = db.Route_.Include(r => r.Route_Station).Include(r => r.Station);
            //var res = ()
            return View();
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}