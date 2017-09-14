using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SockMarket.DAL;
using SockMarket.Models;

namespace SockMarket.Controllers
{
    public class DealsController : Controller
    {
        private MarketContext db = new MarketContext();

        public ActionResult Index()
        {
            var deals = db.Deals.Include(d => d.Company);
            return View(deals.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Stage,CompanyID")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Deals.Add(deal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", deal.CompanyID);
            return View(deal);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", deal.CompanyID);
            return View(deal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Stage,CompanyID")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name", deal.CompanyID);
            return View(deal);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal deal = db.Deals.Find(id);
            db.Deals.Remove(deal);
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
