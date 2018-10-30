using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Models;

namespace OpenLibrary.Controllers
{
    public class TacGiaController : Controller
    {
        public IActionResult Index()
        {
            TacGiaModel tacGiaModel = new TacGiaModel();
            List<TacGia> tg = tacGiaModel.DocTatCaTacGia();
            
            return View(tg);
        }
       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.id = id;
            TacGiaModel tacGiaModel = new TacGiaModel();
            ViewBag.tacGias = tacGiaModel.DocTacGiaTheoId(id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id,string ten_tac_gia,string ngay_sinh,string gioi_thieu)
        {
            
            TacGiaModel tacGiaModel = new TacGiaModel();
            var i = tacGiaModel.CapNhatTacGiaTheoID(id, ten_tac_gia, ngay_sinh, gioi_thieu);
            if (i == 1) ViewBag.success = "Thành công";
            return Redirect("https://localhost:5001/tacgia/");
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(string ten_tac_gia,string ngay_sinh,string gioi_thieu)
        {
            
            ViewBag.ten_tac_gia = ten_tac_gia;
            ViewBag.ngay_sinh = ngay_sinh;
            ViewBag.gioi_thieu = gioi_thieu;

            TacGiaModel tacGiaModel = new TacGiaModel();
            var result = tacGiaModel.ThemTacGia(ten_tac_gia,ngay_sinh, gioi_thieu);
            if (result == 1) ViewBag.success = "Thành công";
            return View();
        }
    }
}