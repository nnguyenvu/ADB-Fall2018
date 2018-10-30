using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Areas.Admin.Models;
using X.PagedList;

namespace OpenLibrary.Areas.Admin.Controllers
{
   [Area("Admin")]
    public class NhaXuatBanController : Controller
    {
        public IActionResult Index(int? page, string searchString)
        {
            ViewData["title_table"] = "Nhà Xuất Bản";
            ViewBag.searchString = searchString;
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            List<NhaXuatBan> nhaXuatBans;
            if (searchString != null)
            {
                nhaXuatBans = nhaXuatBanModel.SearchNhaXuatBan(searchString);
            }
            else
            {
                nhaXuatBans = nhaXuatBanModel.DocTatCaNhaXuatBan();
            }
            var pageNumber = page ?? 1;
            int limit = 5;
            var onePageofNhaXuatBans = nhaXuatBans.ToPagedList(pageNumber, limit);

            ViewBag.hasSearchResult = (nhaXuatBans.Count() > 0) ? true : false;
            ViewBag.hasPagination = (nhaXuatBans.Count() > limit) ? true : false;

            return View(onePageofNhaXuatBans);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title_table"] = "Thêm Nhà Xuất Bản";
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddViewModel addViewModel)
        {
            string ten_nha_xuat_ban = addViewModel.TenNhaXuatBan;
            string dia_chi = addViewModel.DiaChiNhaXuatBan;
            string dien_thoai = addViewModel.DienThoaiNhaXuatBan;
            string email = addViewModel.EmailNhaXuatBan;

            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            int res = nhaXuatBanModel.ThemNhaXuatBan(ten_nha_xuat_ban, dia_chi, dien_thoai, email);
            if(res == 1)
            {
                return RedirectToAction("Index");
            }
            return Content("Fail ADD NhaXuatBan");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            @ViewData["title_table"] = "Cập nhật Nhà Xuất Bản";
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            NhaXuatBan nhaXuatBan = nhaXuatBanModel.DocNhaXuatBanTheoID(id).First();

            AddViewModel addViewModel = new AddViewModel();
            addViewModel.TenNhaXuatBan = nhaXuatBan.TenNhaXuatBan;
            addViewModel.DiaChiNhaXuatBan = nhaXuatBan.DiaChi;
            addViewModel.DienThoaiNhaXuatBan = nhaXuatBan.DienThoai;
            addViewModel.EmailNhaXuatBan = nhaXuatBan.Email;
            addViewModel.IdNhaXuatBan = nhaXuatBan._id;
            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddViewModel addViewModel)
        {
            int id = addViewModel.IdNhaXuatBan;
            string ten_nha_xuat = addViewModel.TenNhaXuatBan;
            string dia_chi = addViewModel.DiaChiNhaXuatBan;
            string dien_thioai = addViewModel.DienThoaiNhaXuatBan;
            string email = addViewModel.EmailNhaXuatBan;

            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();
            int res = nhaXuatBanModel.CapNhatNhaXuatBanTheoID(id, ten_nha_xuat, dia_chi, dien_thioai, email);
            if(res == 1)
            {
                return RedirectToAction("Index");
            }
            return Content("Fail Update NhaXuatBan");
        }

        public IActionResult Delete(int id)
        {
            NhaXuatBanModel nhaXuatBanModel = new NhaXuatBanModel();

            int res = nhaXuatBanModel.XoaNhaXuatBanTheoID(id);

            if (res == 1) return RedirectToAction("Index");
            return Content("Fail Delete NhaXuatBAn");
        }
    }
}