using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;
using Microsoft.AspNet.Identity;
using System.Security.Policy;

namespace LibraryMVC.Controllers
{
    public class BorrowingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Borrowings
        public ActionResult Index()
        {
            var borrowings = db.Borrowing.ToList();

            var borrowingViewModels = borrowings.Select(borrowing => new BorrowIndexViewModel
            {
                BorrowId = borrowing.BorrowId,
                MemberName = db.Members.Find(borrowing.MemberId)?.Name,
                StaffName =  borrowing.StaffId,
                ReleaseDate = borrowing.ReleaseDate,
                DueDate = borrowing.DueDate,
                Status = borrowing.Status,
        }).ToList();

            return View(borrowingViewModels);
        }

        // GET: Borrowings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrowing borrowing = db.Borrowing.Find(id);

            if (borrowing == null)
            {
                return HttpNotFound();
            }

            List<BorrowDetail> borrowDetails = db.BorrowDetail.Where(b => b.BorrowId == borrowing.BorrowId).ToList();

            var viewModel = new BorrowingDetailViewModel
            {
                Borrowing = borrowing,
                BorrowDetails = borrowDetails
            };

            return View(viewModel);
        }


        // GET: Borrowings/Create
        public ActionResult Create()
        {
            var model = new BorrowingViewModel
            {
                AvailableBooks = db.Books.Where(b => b.Status == 1).ToList()

            };
            var books = db.Books.ToList();
            ViewBag.Books = books;

            return View(model);
        }

        [HttpPost]
        public JsonResult GetMemberDetails(string phone)
        {
            var member = db.Members.FirstOrDefault(m => m.Phone == phone);
            var memberType = db.MemberTypes.FirstOrDefault(m => m.TypeId == member.MemberTypeId);
            if (member == null)
            {
                return Json(new { success = false, message = "Member not found." });
            }

            var memberDetails = new
            {
                MemberId = member.MemberId,
                MemberName = member.Name,
                MemberTypeName = memberType.Name,
                MaxBooks = memberType.MaxBooks,
                MaxDays = memberType.MaxDays
            };

            return Json(new { success = true, data = memberDetails });
        }

        // POST: Borrowing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowingViewModel model)
        {
            if (ModelState.IsValid)
            {
                int totalBookCount = model.Books.Sum(book => book.Count);

                if (totalBookCount > model.MaxBooks)
                {
                    ModelState.AddModelError("", $"Reader can borrow a maximum of {model.MaxBooks} books in total.");
                    model.AvailableBooks = db.Books.Where(b => b.Status == 1).ToList();
                    return View(model);
                }

                var borrowing = new Borrowing
                {
                    MemberId = model.MemberId,
                    StaffId = User.Identity.GetUserId(),
                    ReleaseDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(model.MaxDays),
                    Status = 1
                };

                db.Borrowing.Add(borrowing);
                db.SaveChanges();

                foreach (var book in model.Books)
                {
                    var borrowDetail = new BorrowDetail
                    {
                        BorrowId = borrowing.BorrowId,
                        BookId = book.BookId,
                        Count = book.Count
                    };

                    db.BorrowDetail.Add(borrowDetail);
                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            model.AvailableBooks = db.Books.Where(b => b.Status == 1).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id)
        {
            var borrowing = db.Borrowing.Find(id);
            if (borrowing == null)
            {
                return Json(new { success = false, message = "Borrowing record not found." });
            }
            if (borrowing.Status == 1 || borrowing.Status == 3)
            {
                borrowing.Status = 2;
            }    
            
            db.Entry(borrowing).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }


        // GET: Borrowings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrowing borrowing = db.Borrowing.Find(id);
            if (borrowing == null)
            {
                return HttpNotFound();
            }
            return View(borrowing);
        }

        // POST: Borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowId,MemberId,StaffId,ReleaseDate,DueDate,Status")] Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrowing);
        }

        // GET: Borrowings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Borrowing borrowing = db.Borrowing.Find(id);
            if (borrowing == null)
            {
                return HttpNotFound();
            }
            return View(borrowing);
        }

        // POST: Borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Borrowing borrowing = db.Borrowing.Find(id);
            db.Borrowing.Remove(borrowing);
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
