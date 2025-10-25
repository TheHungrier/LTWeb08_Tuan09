using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bai01.Models;

namespace Bai01.Controllers
{
    public class HomeController : Controller
    {
        BookStoreEF data = new BookStoreEF();
        public ActionResult Index()
        {
            List<THELOAITIN> theloai = data.THELOAITIN.ToList();
            return View(theloai);
        }
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(THELOAITIN theloai)
        {
            data.THELOAITIN.Add(theloai);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var EB_tin = data.THELOAITIN.First(m => m.IDLOAI == id);
            return View(EB_tin);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var Ltin = data.THELOAITIN.First(l => l.IDLOAI == id);
            var E_Loaitin = collection["Tentheloai"];
            Ltin.IDLOAI = id;
            Ltin.TENTHELOAI = E_Loaitin;
            UpdateModel(Ltin);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var Details_tin = data.THELOAITIN.Where(m => m.IDLOAI == id).First();
            return View(Details_tin);
        }
        public ActionResult Delete(int id)
        {
            var D_tin = data.THELOAITIN.First(m => m.IDLOAI == id);
            return View(D_tin);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_tin = data.THELOAITIN.Where(m => m.IDLOAI == id).First();
            data.THELOAITIN.Remove(D_tin);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}