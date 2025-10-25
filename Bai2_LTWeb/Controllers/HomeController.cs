using Bai2_LTWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bai2_LTWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        QLHH data = new QLHH();
        public ActionResult DanhMucSach()
        {
            List<SACH> sachList = data.SACHes.ToList();
            return View(sachList);
        }
        [HttpGet]
        public ActionResult ThemMoiSach()
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiSach(SACH sach, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.ANHBIA = filename;
                data.SACHes.Add(sach);
                data.SaveChanges();
            }
            return RedirectToAction("DanhMucSach");
        }
        public ActionResult ChitietSach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MASACH == id);
            ViewBag.MaSach = sach.MASACH;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult XoaSach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MASACH == id);
            ViewBag.MaSach = sach.MASACH;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("XoaSach")]
        public ActionResult XacNhanXoa(int id)
        {
            SACH s = data.SACHes.SingleOrDefault(e => e.MASACH == id);
            ViewBag.Id = s.MASACH;
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SACHes.Remove(s);
            data.SaveChanges();
            return RedirectToAction("DanhMucSach");
        }
        [HttpGet]
        public ActionResult SuaSach(int id)
        {
            SACH s = data.SACHes.SingleOrDefault(n => n.MASACH == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");
            return View(s);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSach(SACH s, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");
            if (ModelState.IsValid)
            {
                var b = data.SACHes.SingleOrDefault(x => x.MASACH == s.MASACH);
                if (b == null) return HttpNotFound();
                b.TENSACH = s.TENSACH;
                b.GIABAN = s.GIABAN;
                b.MOTA = s.MOTA;
                b.NGAYCAPNHAT = DateTime.Now;
                b.MANXB = s.MANXB;
                b.MACD = s.MACD;
                if (fileUpload != null)
                {
                    var filename = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), filename);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    b.ANHBIA = filename;
                }
                data.SaveChanges();
                return RedirectToAction("DanhMucSach");
            }
            ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
            return View(s);
        }
    }
}