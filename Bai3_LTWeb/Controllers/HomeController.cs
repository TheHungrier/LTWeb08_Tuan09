using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Bai3_LTWeb.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32.SafeHandles;

namespace Bai3_LTWeb.Controllers
{
    public class HomeController : Controller
    {
        QL_NhanSuN data = new QL_NhanSuN();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DSNhanVien()
        {
            List<tbl_Employee> dsNhanVien = data.tbl_Employee.ToList();
            return View(dsNhanVien);
        }
        [HttpGet]
        public ActionResult ThemMoiNV()
        {
            ViewBag.DepId = new SelectList(data.tbl_Department.OrderBy(n => n.Name), "DepId", "Name");
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ", "Khác" });
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiNV(tbl_Employee em, HttpPostedFileBase fileUpload)
        {
            ViewBag.DepId = new SelectList(data.tbl_Department.ToList().OrderBy(n => n.Name), "DepId", "Name");
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ", "Khác" });
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
                return View();
            }
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Photo"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại!";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                em.Photo = filename;
                data.tbl_Employee.Add(em);
                data.SaveChanges();
            }
            return RedirectToAction("DSNhanVien");
        }
        public ActionResult ChitietNV(int id)
        {
            tbl_Employee em = data.tbl_Employee.SingleOrDefault(e => e.Id == id);
            ViewBag.Id = em.Id;
            if (em == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(em);
        }
        [HttpGet]
        public ActionResult XoaNhanVien(int id)
        {
            tbl_Employee em = data.tbl_Employee.SingleOrDefault(e => e.Id == id);
            ViewBag.Id = em.Id;
            if (em == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(em);
        }
        [HttpPost, ActionName("XoaNhanVien")]
        public ActionResult XacNhanXoa(int id)
        {
            tbl_Employee em = data.tbl_Employee.SingleOrDefault(e => e.Id == id);
            ViewBag.Id = em.Id;
            if (em == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.tbl_Employee.Remove(em);
            data.SaveChanges();
            return RedirectToAction("DSNhanVien");
        }
        [HttpGet]
        public ActionResult SuaNhanVien(int id)
        {
            tbl_Employee em = data.tbl_Employee.SingleOrDefault(n => n.Id == id);
            if (em == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.DepId = new SelectList(data.tbl_Department.ToList().OrderBy(n => n.Name), "DepId", "Name");
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ", "Khác" });
            return View(em);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNhanVien(tbl_Employee em, HttpPostedFileBase fileUpload)
        {
            ViewBag.DepId = new SelectList(data.tbl_Department.ToList().OrderBy(n => n.Name), "DepId", "Name");
            ViewBag.Gender = new SelectList(new[] { "Nam", "Nữ", "Khác" });
            if (ModelState.IsValid)
            {
                var e = data.tbl_Employee.SingleOrDefault(x => x.Id == em.Id);
                if (e == null) return HttpNotFound();

                e.Name = em.Name;
                e.Gender = em.Gender;
                e.City = em.City;
                e.DepId = em.DepId;

                if (fileUpload != null)
                {
                    var filename = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), filename);
                    if (!System.IO.File.Exists(path))
                    {
                        fileUpload.SaveAs(path);
                    }
                    e.Photo = filename;
                }

                data.SaveChanges();
                return RedirectToAction("DSNhanVien");
            }
            ViewBag.ThongBao = "Vui lòng chọn ảnh bìa!";
            return View(em);
        }
        public ActionResult DSNhanVienPhongBan()
        {
            var ds = data.tbl_Employee.ToList();
            var vm = new List<NV_PBViewModel>();
            foreach (var e in ds)
            {
                NV_PBViewModel v = new NV_PBViewModel();
                v.Id = e.Id;
                v.Name = e.Name;
                v.Gender = e.Gender;
                v.City = e.City;
                v.Photo = e.Photo;
                v.DepId = e.DepId;
                v.DepName = e.tbl_Department != null ? e.tbl_Department.Name : "";
                vm.Add(v);
            }
            return View(vm);
        }
        public ActionResult SideBarDept(int? id)
        {
            var dept = data.tbl_Department.ToList();
            return View(dept);
        }
        public ActionResult ShowEmplParameter(int id)
        {
            var em = data.tbl_Employee.Where(e => e.DepId == id).ToList();
            var dept = data.tbl_Department.FirstOrDefault(d => d.DepId == id);
            ViewBag.DepName = dept != null ? dept.Name : "Không xác định";
            return View(em);
        }
        public ActionResult ChitietNVPB(int id)
        {
            tbl_Employee em = data.tbl_Employee.SingleOrDefault(e => e.Id == id);
            ViewBag.Id = em.Id;
            if (em == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(em);
        }
    }
}