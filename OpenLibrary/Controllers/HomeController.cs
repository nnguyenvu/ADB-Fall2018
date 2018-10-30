using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenLibrary.Models;

namespace OpenLibrary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //LoaiSachModel loaisachModel = new LoaiSachModel();

            //ViewBag.loaisachs = loaisachModel.DocTatCaLoaiSach();
            //ViewBag.loaisachtheoidchs = loaisachModel.DocLoaiSachTheoIdCha(2);
            //ViewBag.ThemResult = loaisachModel.ThemLoaiSach("LẬP TRÌNH IT", 1, 1);
            //ViewBag.ResultXoa = loaisachModel.XoaLoaiSachTheoID(97);
            //ViewBag.ResultCapNhat = loaisachModel.CapNhatLoaiSachTheoID(100,"Mầm non sách 12",12,0);
            return View();
        }

        
        public IActionResult Dashboard()
        {
            return View("test");
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
