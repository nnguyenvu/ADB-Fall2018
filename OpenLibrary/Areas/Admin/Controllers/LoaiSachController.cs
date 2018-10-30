using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Areas.Admin.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using X.PagedList;

namespace OpenLibrary.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiSachController : Controller
    {
        public IActionResult Index(int? page, string searchString)
        {
            ViewData["title_table"] = "Loại Sách";
            ViewBag.searchString = searchString;
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            List<LoaiSach> loaiSaches;
            if (searchString != null)
            {
                loaiSaches = loaiSachModel.SearchLoaiSach(searchString);
            }
            else
            {
                loaiSaches = loaiSachModel.DocTatCaLoaiSach();
            }
            var pageNumber = page ?? 1;
            int limit = 5;
            var onePageofLoaiSachs = loaiSaches.ToPagedList(pageNumber, limit);

            ViewBag.hasSearchResult = (loaiSaches.Count() > 0) ? true : false;
            ViewBag.hasPagination = (loaiSaches.Count() > limit) ? true : false;

            return View(onePageofLoaiSachs);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title_table"] = "Thêm Loại Sách";
            AddViewModel addViewModel = new AddViewModel();
            LoaiSachModel loaiSachModel = new LoaiSachModel();

            addViewModel.listLoaiSach = loaiSachModel.DocTatCaLoaiSach();
            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel addViewModel)
        {
            string ten_loai_sach = addViewModel.TenLoaiSach;
            int id_loai_cha = addViewModel.IdLoaiSachCha;
            int trang_thai = (addViewModel.TrangThai) ? 1 : 0;

            LoaiSachModel loaiSachModel = new LoaiSachModel();
            int res = loaiSachModel.ThemLoaiSach(ten_loai_sach, id_loai_cha, trang_thai);

            if (res == 1)
            {
                return RedirectToAction("Index");
            }
            return Content("Add LoaiSach Fail!");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["title_table"] = "Cập Nhật Loại Sách";
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            LoaiSach loaiSach = loaiSachModel.DocLoaiSachTheoId(id).First();

            AddViewModel addViewModel = new AddViewModel();
            addViewModel.listLoaiSach = loaiSachModel.DocTatCaLoaiSach();
            addViewModel.TenLoaiSach = loaiSach.TenLoaiSach;
            addViewModel.IdLoaiSachCha = loaiSach.IdLoaiCha;
            addViewModel.TrangThai = (loaiSach.TrangThai == 1) ? true : false;
            addViewModel.IdLoaiSach = loaiSach._id;
            return View(addViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddViewModel addViewModel)
        {
            int id = addViewModel.IdLoaiSach;
            string ten_loai_sach = addViewModel.TenLoaiSach;
            int id_loai_cha = addViewModel.IdLoaiSachCha;
            int trang_thai = (addViewModel.TrangThai) ? 1 : 0;

            LoaiSachModel loaiSachModel = new LoaiSachModel();
            int res = loaiSachModel.CapNhatLoaiSachTheoID(id, ten_loai_sach, id_loai_cha, trang_thai);
            if(res == 1)
            {
                return RedirectToAction("Index");
            }
            return Content("Fail Update LoaiSach");
        }


        public IActionResult Delete(int id)
        {
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            if (hasChild(id))
            {
                int deleteMany = loaiSachModel.XoaLoaiSachTheoIdCha(id);
            }
            int res = loaiSachModel.XoaLoaiSachTheoID(id);
            if (res == 1)
                return RedirectToAction("Index");
            else
            {
                return Content("Fail Delete LoaiSach");
            }

        }
        public bool hasChild(int id)
        {
            LoaiSachModel loaiSachModel = new LoaiSachModel();
            return (loaiSachModel.DocLoaiSachTheoIdCha(id).Count() > 0) ? true : false;
        }

    }
}