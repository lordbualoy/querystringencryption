using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QueryStringEncryption.Helper;
using QueryStringEncryption.Models;
using QueryStringEncryption.ViewModels;

namespace QueryStringEncryption.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(GetViewModel());
        }
        public IActionResult Get(UrlEncodedViewModel request)
        {
            return Json(new { Num = request.Num, Text = request.Text, });
        }
        public IActionResult PostForm(UrlEncodedViewModel request)
        {
            return Json(new { Num = request.Num, Text = request.Text, });
        }
        public IActionResult PostJson([FromBody]JsonViewModel request)
        {
            return Json(new { Num = request.Num.Value, Text = request.Text.Value, });
        }
        public JsonResult ResponseJson()
        {
            return Json(GetViewModel());
        }
        JsonViewModel GetViewModel()
        {
            return new JsonViewModel
            {
                Num = 10,
                Text = "Ten",
            };
        }
    }
}
