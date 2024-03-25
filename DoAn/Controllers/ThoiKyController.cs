using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.ViewModel;
using DoAnWeb.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DoAn.Controllers
{
    public class ThoiKyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ThoiKy
        public ActionResult Index()
        {
            return View(db.thoiKies.ToList());
        }

        // GET: ThoiKy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThoiKy thoiKy = db.thoiKies.Find(id);
            if (thoiKy == null)
            {
                return HttpNotFound();
            }
            string[] text = System.IO.File.ReadAllLines(thoiKy.TomTatTK);
            ViewBag.Text = text;
            return View(thoiKy);
        }

        // GET: ThoiKy/Create
        public ActionResult Create()
        {
            var viewNv = new ViewModelTK
            {
                
            };
            return View(viewNv);
        }

        // POST: ThoiKy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdThoiKy,TenThoiKy,NamBatDau,NamKetThuc,TomTatTK")] ViewModelTK view , HttpPostedFileBase text)
        {
            if (ModelState.IsValid)
            {
                    if (text != null)
                    {
                        string TextName = Path.GetFileName(text.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Text/ThoiKy"), TextName);
                        text.SaveAs(_path);
                        view.TomTatTK = _path;

                        var Tk = new ThoiKy
                        {
                            TenThoiKy=view.TenThoiKy,
                            NamBatDau=view.NamBatDau,
                            NamKetThuc=view.NamKetThuc,
                            TomTatTK=view.TomTatTK, 
                        };
                        db.thoiKies.Add(Tk);
                    }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        // GET: ThoiKy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThoiKy thoiKy = db.thoiKies.Find(id);
            if (thoiKy == null)
            {
                return HttpNotFound();
            }
            return View(thoiKy);
        }

        // POST: ThoiKy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdThoiKy,TenThoiKy,NamBatDau,NamKetThuc,TomTatTK")] ThoiKy thoiKy, HttpPostedFileBase text)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thoiKy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thoiKy);
        }

        // GET: ThoiKy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThoiKy thoiKy = db.thoiKies.Find(id);
            if (thoiKy == null)
            {
                return HttpNotFound();
            }
            return View(thoiKy);
        }

        // POST: ThoiKy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            ThoiKy thoiKy = db.thoiKies.Find(id);
            db.thoiKies.Remove(thoiKy);
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
