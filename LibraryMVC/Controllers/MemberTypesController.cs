using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class MemberTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MemberTypes
        public ActionResult Index()
        {
            return View(db.MemberTypes.ToList());
        }

        // GET: MemberTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // GET: MemberTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeId,Name,MaxBooks,MaxDays,Fee,Note")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                db.MemberTypes.Add(memberType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberType);
        }

        // GET: MemberTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // POST: MemberTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeId,Name,MaxBooks,MaxDays,Fee,Note")] MemberType memberType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberType);
        }

        // GET: MemberTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberType memberType = db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return HttpNotFound();
            }
            return View(memberType);
        }

        // POST: MemberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberType memberType = db.MemberTypes.Find(id);
            db.MemberTypes.Remove(memberType);
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
