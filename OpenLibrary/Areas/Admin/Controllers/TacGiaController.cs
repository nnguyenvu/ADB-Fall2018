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
    public class TacGiaController : Controller
    {
        public IActionResult Index(int? page, string searchString)
        {
            ViewData["title_table"] = "Tác Giả";
            ViewBag.searchString = searchString;
            TacGiaModel tacGiaModel = new TacGiaModel();
            List<TacGia> tacGias;
            if (searchString != null)
            {
                tacGias = tacGiaModel.SearchTacGia(searchString);
            }
            else
            {
                tacGias = tacGiaModel.DocTatCaTacGia();
            }
            var pageNumber = page ?? 1;
            int limit = 5;
            var onePageofTacGias = tacGias.ToPagedList(pageNumber, limit);

            ViewBag.hasSearchResult = (tacGias.Count() > 0) ? true : false;
            ViewBag.hasPagination = (tacGias.Count() > limit) ? true : false;

            return View(onePageofTacGias);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["title_table"] = "Thêm Tác Giả";
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddViewModel addViewModel)
        {
            ViewData["title_table"] = "Thêm Tác Giả";
            string ten_tac_gia = addViewModel.TenTacGia;
            string ngay_sinh = addViewModel.NgaySinhTacGia;
            string gioi_thieu = addViewModel.GioiThieu;

            TacGiaModel tacGiaModel = new TacGiaModel();
            int res = tacGiaModel.ThemTacGia(ten_tac_gia, ngay_sinh, gioi_thieu);
            if (res == 1) return RedirectToAction("Index");

            return Content("Fail add TacGia");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            @ViewData["title_table"] = "Cập nhật Tác Giả";
            TacGiaModel tacGiaModel = new TacGiaModel();
            TacGia tacGia = tacGiaModel.DocTacGiaTheoId(id).First();
            AddViewModel addViewModel = new AddViewModel();
            addViewModel.TenTacGia = tacGia.TenTacGia;
            addViewModel.NgaySinhTacGia = tacGia.NgaySinh;
            addViewModel.GioiThieu = tacGia.GioiThieu;
            addViewModel.IdTacGia = tacGia._id;
            return View(addViewModel);
        }

        public IActionResult Edit(AddViewModel addViewModel)
        {
            int id = addViewModel.IdTacGia;
            string ten_tac_gia = addViewModel.TenTacGia;
            string ngay_sinh = addViewModel.NgaySinhTacGia;
            string gioi_thieu = addViewModel.GioiThieu;

            TacGiaModel tacGiaModel = new TacGiaModel();
            int res = tacGiaModel.CapNhatTacGiaTheoID(id, ten_tac_gia, ngay_sinh, gioi_thieu);
            if(res > 0)
            {
                return RedirectToAction("Index");
            }
            return Content("Fail to Update Tacgia");
        }

        public IActionResult Delete(int id)
        {
            TacGiaModel tacGiaModel = new TacGiaModel();

            int res = tacGiaModel.XoaTacGia(id);

            if (res == 1) return RedirectToAction("Index");
            return Content("Fail Delete TacGia");
        }
    }
}