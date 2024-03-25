using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.ViewModel;
using DoAnWeb.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DoAn.Controllers
{
    public class SuKienController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SuKien
        public ActionResult Index()
        {
            return View(db.SuKien.ToList());
        }

        // GET: SuKien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKien.Find(id);
            string[] Nv = suKien.IdNv.Split(',');
            int[] nv = Nv.Select(int.Parse).ToArray();
            ViewBag.lstItem = db.NhanVatLs.ToList();
            ViewBag.Img = db.Images.Where(x => x.IdNoiDung == suKien.IdNoiDung).Select(x => x.UrlImage);
            ViewBag.Id = nv;
            string[] text = System.IO.File.ReadAllLines(suKien.NoiDungSK);
            ViewBag.Data = text;
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // GET: SuKien/Create
        public ActionResult Create()
        {
            var viewModel = new ViewModelSK
            {
                thoiKy = db.thoiKies.ToList(),
                nhanVat = db.NhanVatLs.ToList(),
            };

            return View(viewModel);
        }

        // POST: SuKien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdNoiDung,TenNoiDung,NoiDungSK,IdThoiKy,IdNV,TomTatSK")] ViewModelSK view , HttpPostedFileBase text, HttpPostedFileBase[] images)
        {
            SuKien sk = new SuKien();
            if (ModelState.IsValid)
            {
                List<Image1> img = new List<Image1>();
                for (int i = 0; i < images.Count(); i++)
                {
                    var item = images[i];
                    if (item != null && text != null)
                    {
                        string ImageName = Path.GetFileName(item.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Image/SuKien"), ImageName);
                        item.SaveAs(path);
                        string TextName = Path.GetFileName(text.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Text/SuKien"), TextName);
                        text.SaveAs(_path);
                        Image1 image1 = new Image1()
                        {
                            UrlImage = item.FileName,
                            IdNoiDung = view.IdNoiDung,
                        };

                        sk.TenNoiDung=view.TenNoiDung;
                        sk.NoiDungSK = _path;
                        sk.TomTatSK = view.TomTatSK;
                        sk.IdThoiKy = view.IdThoiKy;
                        sk.IdNv = string.Join(",", view.IdNV);

                        img.Add(image1);
                    }
                }
                sk.Images = img;
                db.SuKien.Add(sk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        // GET: SuKien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewModelSK sk = new ViewModelSK();
            ViewBag.Data = db.thoiKies.Select(x => x).ToList();
            ViewBag.Char = db.NhanVatLs.Select(x => x).ToList();
            ViewBag.Id = sk;
            SuKien suKien = db.SuKien.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdNoiDung,TenNoiDung,TomTatSK,IdThoiKy,IdNv,NoiDungSK")] SuKien suKien , HttpPostedFileBase text , HttpPostedFileBase[] images , int[]nv)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < images.Count(); i++)
                {
                    var image = images[i];
                    if (image != null && text != null)
                    {
                        var img = Path.GetFileName(image.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Image/SuKien"), img);
                        image.SaveAs(path);
                        string TextName = Path.GetFileName(text.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/Text/SuKien"), TextName);
                        text.SaveAs(_path);
                        suKien.NoiDungSK = _path;
     
                        Image1 image1 = new Image1()
                        {
                            UrlImage = image.FileName,
                            IdNoiDung = suKien.IdNoiDung,
                        };
                        db.Entry(image1).State = EntityState.Added;
                    }

                }
                db.Entry(suKien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suKien);
        }

        // GET: SuKien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKien.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }

        // POST: SuKien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            SuKien suKien = db.SuKien.Find(id);
            db.SuKien.Remove(suKien);
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
