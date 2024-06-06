using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    [Authorize(Roles = "Administrator, Book Manager")]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category)
                        .Include(b => b.Author)
                        .Include(b => b.Language)
                        .Include(b => b.Publisher)
                        .ToList();
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.Authors = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.Languages = new SelectList(db.Languages, "LanguageId", "Name");
            ViewBag.Publishers = new SelectList(db.Publishers, "PublisherId", "Name");

            var book = new Book
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = 1
            };

            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookTitle,Price,Pages,CategoryId,AuthorId,LanguageId,PublisherId,PubYear,Description,CreatedDate,UpdatedDate,Status")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var directory = Server.MapPath("~/Images/Books");
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(directory, fileName);
                    ImageFile.SaveAs(path);
                    book.ImageUrl = "/Images/Books/" + fileName;
                }

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.Authors = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.Languages = new SelectList(db.Languages, "LanguageId", "Name");
            ViewBag.Publishers = new SelectList(db.Publishers, "PublisherId", "Name");
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            book.UpdatedDate = DateTime.Now;

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.Authors = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.Languages = new SelectList(db.Languages, "LanguageId", "Name");
            ViewBag.Publishers = new SelectList(db.Publishers, "PublisherId", "Name");
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookTitle,Price,Pages,CategoryId,AuthorId,LanguageId,PublisherId,PubYear,Description,Status")] Book book, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                Book existingBook = db.Books.Find(book.BookId);
                if (existingBook == null)
                {
                    return HttpNotFound();
                }

                // Preserve original CreatedDate
                book.CreatedDate = existingBook.CreatedDate;
                book.UpdatedDate = DateTime.Now;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var directory = Server.MapPath("~/Images/Books");
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var path = Path.Combine(directory, fileName);
                    ImageFile.SaveAs(path);
                    book.ImageUrl = "/Images/Books/" + fileName;
                }
                else
                {
                    book.ImageUrl = existingBook.ImageUrl;
                }

                db.Entry(existingBook).CurrentValues.SetValues(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.Authors = new SelectList(db.Authors, "AuthorId", "Name");
            ViewBag.Languages = new SelectList(db.Languages, "LanguageId", "Name");
            ViewBag.Publishers = new SelectList(db.Publishers, "PublisherId", "Name");
            return View(book);
        }

        [HttpPost]
        public JsonResult ToggleStatus(int id)
        {
            try
            {
                // Find the book
                var book = db.Books.Find(id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found." });
                }

                // Find the category of the book
                var category = db.Categories.Find(book.CategoryId);
                if (category == null)
                {
                    return Json(new { success = false, message = "Category not found." });
                }

                // Check the status of the category
                if (category.Status == 0 && book.Status == 0)
                {
                    return Json(new { success = false, message = "Cannot change the status of the book to 1 because the category status is 0." });
                }

                // Toggle status
                book.Status = book.Status == 1 ? 0 : 1;
                db.SaveChanges();

                return Json(new { success = true, status = book.Status });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
