using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SockMarket.Models;
using SockMarket.DAL;
using SockMarket.ViewModels;

namespace SockMarket.Controllers
{
    public class CompanyController : Controller
    {
        private MarketContext db = new MarketContext();

        public ActionResult Index()
        {
            return View(db.Companies.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,ContactID")] Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Companies.Add(company);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again");
            }
            return View(company);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies
                .Include(c => c.Contacts)
                .Where(c => c.ID == id)
                .Single();
            PopulateContactsData(company);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedContacts)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company companyToUpdate = db.Companies
                .Include(c => c.Contacts)
                .Where(c => c.ID == id)
                .Single();
            if (TryUpdateModel(companyToUpdate, "", new string[] { "Name" }))
            {
                try
                {
                    UpdateCompanyContacts(selectedContacts, companyToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again");
                }
            }
            PopulateContactsData(companyToUpdate);
            return View(companyToUpdate);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please Try again";
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Company company = db.Companies.Find(id);
                db.Companies.Remove(company);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

        private void UpdateCompanyContacts(string[] selectedContacts, Company companyToUpdate)
        {
            if (selectedContacts == null)
            {
                companyToUpdate.Contacts = new List<Contact>();
                return;
            }

            var selectedContactsHS = new HashSet<string>(selectedContacts);
            var companyContacts = new HashSet<int>(companyToUpdate.Contacts.Select(c => c.ID));
            foreach (var contact in db.Contacts)
            {
                if (selectedContactsHS.Contains(contact.ID.ToString()))
                {
                    if (!companyContacts.Contains(contact.ID))
                    {
                        companyToUpdate.Contacts.Add(contact);
                    }
                }
                else
                {
                    if (companyContacts.Contains(contact.ID))
                    {
                        companyToUpdate.Contacts.Remove(contact);
                    }
                }
            }
        }

        private void PopulateContactsData(Company company)
        {
            var allContacts = db.Contacts;
            var companyContacts = new HashSet<int>(company.Contacts.Select(c => c.ID));
            var viewModel = new List<SelectedContactData>();
            foreach (var contact in allContacts)
            {
                viewModel.Add(new SelectedContactData
                {
                    ID = contact.ID,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email,
                    Selected = companyContacts.Contains(contact.ID)
                });
            }
            ViewBag.Contacts = viewModel;
        }
    }
}
