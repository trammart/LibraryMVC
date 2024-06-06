using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles ="Administrator, Reader Manager")]
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Members
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.MemberTypes = new SelectList(db.MemberTypes, "TypeId", "Name");
            var mem = new Member
            {
                RegistrationDate = DateTime.Now,
                Status = 1
            };
            return View(mem);
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,Name,Birth,Gender,Email,Phone,RegistrationDate,ExpireDate,MemberTypeId,Address,Status")] Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Calculate the total based on RegistrationDate, ExpireDate, and MemberType fee
                    var memberType = db.MemberTypes.Find(member.MemberTypeId);
                    if (memberType != null)
                    {
                        var months = ((member.ExpireDate.Value.Year - member.RegistrationDate.Value.Year) * 12) + member.ExpireDate.Value.Month - member.RegistrationDate.Value.Month;
                        months = Math.Max(0, months); 
                        member.Total = months * memberType.Fee;
                    }

                    db.Members.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException?.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        // Handle unique constraint violation error
                        ModelState.AddModelError("Phone", "This phone number is already in use.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "MemberTypeId", "TypeName", member.MemberTypeId);
            return View(member);
        }

        [HttpGet]
        public JsonResult GetMemberTypeFee(int memberTypeId)
        {
            var memberType = db.MemberTypes.Find(memberTypeId);
            if (memberType == null)
            {
                return Json(new { success = false, message = "Member type not found." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, fee = memberType.Fee }, JsonRequestBehavior.AllowGet);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,Name,Birth,Gender,Email,Phone,RegistrationDate,ExpireDate,MemberTypeId,Address,Status")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
