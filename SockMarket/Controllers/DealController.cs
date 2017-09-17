using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SockMarket.DAL;
using SockMarket.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SockMarket.ViewModels;

namespace SockMarket.Controllers
{
    [Authorize]
    public class DealController : Controller
    {
        private MarketContext db = new MarketContext();

        public ActionResult Index()
        {
            var deals = db.Deals.Include(d => d.Company);
            return View(deals.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Stage,CompanyID")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.CreationTime = DateTime.Now;
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
            return View(deal);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dealToUpdate = db.Deals.Find(id);
            if (TryUpdateModel(dealToUpdate, new string[] { "Stage" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again");
                }
            }
            return View(dealToUpdate);
        }

        public ActionResult Cancel(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Cancel failed. Please Try again";
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
            try
            {
                Deal dealToDelete = db.Deals
                .Include(c => c.Comments)
                .Where(c => c.ID == id)
                .Single();
                db.Deals.Remove(dealToDelete);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Cancel", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddComment(int id, string text)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            var currentUser = userManager.FindById(User.Identity.GetUserId());

            Comment newComment = new Comment
            {
                Text=text,
                CreationTime=DateTime.Now,
                Author = currentUser.Email
            };
            db.Comments.Add(newComment);

            Deal deal = db.Deals.Find(id);
            deal.Comments.Add(newComment);

            db.SaveChanges();

            CommentData commentData = new CommentData
            {
                Text = text,
                CreationTime = newComment.CreationTime.ToString(),
                Author = currentUser.Email
            };

            return Json(commentData, JsonRequestBehavior.AllowGet);
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
