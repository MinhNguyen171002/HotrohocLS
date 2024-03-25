using DoAn.Models;
using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DoAn.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index(string search , int SKID = 0)
        {
            ViewBag.Keyword = search;
            ViewBag.Img = db.Images.ToList();
            var sk = db.SuKien.Select(x => x);
            if (!String.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                sk = sk.Where(b => b.TenNoiDung.ToLower().Contains(search) & b.IdThoiKy == SKID);
            }
            else
            {
                ViewData["Error"] = "Vui long nhap tu khoa";
            }
            if(SKID != 0)
            {
                sk = sk.Where(c=>c.IdThoiKy==SKID);
            }
            else
            {
                ViewData["Error"] = "Vui long chon thoi ky";
            }
            ViewBag.skID = new SelectList(db.thoiKies, "IdThoiKy", "TenThoiKy");
            return View(sk.ToList());
        }
    }
}