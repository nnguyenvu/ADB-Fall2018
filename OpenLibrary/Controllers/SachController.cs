using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenLibrary.Models;
namespace OpenLibrary.Controllers
{
    public class SachController : Controller
    {
        public IActionResult Index()
        {
            
            SachModel sachModel = new SachModel();
            
            List<Sach> sachs = sachModel.DocTatCaSach(); ;
            return View(sachs);
        }
    }
}