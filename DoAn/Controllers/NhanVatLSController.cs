using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.ViewModel;
using DoAnWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DoAn.Controllers
{
    public class NhanVatLSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NhanVatLS

        public ActionResult Image()
        {
            return View(db.Images.ToList());
        }
        public ActionResult Index()
        {
            return View(db.NhanVatLs.ToList());
        }

        // GET: NhanVatLS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVatLS nhanVatLS = db.NhanVatLs.Find(id);
            if (nhanVatLS == null)
            {
                return HttpNotFound();
            }

            ViewBag.Img = db.Images.Where(x => x.IdNV==nhanVatLS.IdNV).Select(x=>x.UrlImage);

            string[] text = System.IO.File.ReadAllLines(nhanVatLS.NoiDungTomTatNVUrl);
            ViewBag.Data = text;

            return View(nhanVatLS);
        }

        // GET: NhanVatLS/Create
        public ActionResult Create()
        {
            var viewNv = new ViewModelNV
            {
                ThoiKies=db.thoiKies.ToList(),
            };
            return View(viewNv);
        }

        // POST: NhanVatLS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdNV,TenNhanVat,NgaySinh,NgayMat,ImageUrl,NoiDungTomTatNVUrl,IdThoiKy")] ViewModelNV view , HttpPostedFileBase[] images , HttpPostedFileBase text)
        {
            NhanVatLS NV = new NhanVatLS();
            if (ModelState.IsValid)
            {
                List<Image1> imgs = new List<Image1>();
                for(int i =0 ; i < images.Count();i++)
                {
                    var item = images[i];
                    if (item != null && text != null)
                    {
                        string ImageName = Path.GetFileName(item.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Image/NhanVat"), ImageName);
                        item.SaveAs(path);
                        string TextName = Path.GetFileName(text.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Text/NhanVat"), TextName);
                        text.SaveAs(_path);
                        Image1 image1 = new Image1()
                        {
                            UrlImage = item.FileName,
                            IdNV = view.IdNV,
                        };

                        NV.TenNhanVat = view.TenNhanVat;
                        NV.NgaySinh = view.NgaySinh;
                        NV.NgayMat = view.NgayMat;
                        NV.NoiDungTomTatNVUrl = _path;
                        NV.IdThoiKy = view.IdThoiKy;

                        imgs.Add(image1);
                    }
                }
                NV.Images = imgs;
                db.NhanVatLs.Add(NV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        // GET: NhanVatLS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVatLS nhanVatLS = db.NhanVatLs.Find(id);
            ViewBag.Data = db.thoiKies.Select(x=>x).ToList();
            if (nhanVatLS == null)
            {
                return HttpNotFound();
            }
            return View(nhanVatLS);
        }

        // POST: NhanVatLS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdNV,TenNhanVat,NgaySinh,NgayMat,NoiDungTomTatNVUrl,IdThoiKy")] NhanVatLS nhanVatLS, HttpPostedFileBase[] images, HttpPostedFileBase text)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < images.Count(); i++)
                {
                    var image = images[i];
                    if (image != null && text != null)
                    {
                        var img = Path.GetFileName(image.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Image/NhanVat"), img);
                        image.SaveAs(path);
                        string TextName = Path.GetFileName(text.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Text/NhanVat"), TextName);
                        text.SaveAs(_path);
                        nhanVatLS.NoiDungTomTatNVUrl = _path;
                        Image1 image1 = new Image1()
                        {
                            UrlImage = image.FileName,
                            IdNV = nhanVatLS.IdNV,
                        };
                        db.Entry(image1).State = EntityState.Added;
                    }
 
                }
                db.Entry(nhanVatLS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVatLS);
        }

        // GET: NhanVatLS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVatLS nhanVatLS = db.NhanVatLs.Find(id);
            if (nhanVatLS == null)
            {
                return HttpNotFound();
            }
            return View(nhanVatLS);
        }

        // POST: NhanVatLS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVatLS nhanVatLS = db.NhanVatLs.Find(id);
            db.NhanVatLs.Remove(nhanVatLS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteImage(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image1 image1 = db.Images.Find(id);
            if (image1 == null)
            {
                return HttpNotFound();
            }
            return View(image1);
        }
        [HttpPost, ActionName("DeleteImage")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteImage(int id)
        {
            Image1 image1 = db.Images.Find(id);
            db.Images.Remove(image1);
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
