using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DownInsImg.Models;
using DownInsImg.Process;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DownInsImg.Controllers
{
    public class HomeController : Controller
    {
        GetLink getLink = new GetLink();
        public IActionResult Index()
        {
            List<Select> Selects = new List<Select>()
            {
                new Select() {Id = 1, Selection = "ImageURL"},
                new Select() {Id = 2, Selection = "UserURL"}
            };
            ViewData["selectId"] = new SelectList(Selects,"Id","Selection");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetLink([FromForm]InsInfo insInfo)
        {
            List<string> listLink = new List<string>();
            insInfo.Url = Request.Form["Url"];
            insInfo.SelectId = int.Parse(Request.Form["SelectId"]);
            if (ModelState.IsValid&&insInfo.Url!="") 
            {
                if (insInfo.SelectId == 1)
                {
                    string link = getLink.GetLinkFromImgUrl(insInfo.Url);
                    listLink.Add(link);
                }
                else
                {
                    listLink = getLink.GetLinkFromUserUrl(insInfo.Url);
                }
                return View("Result", listLink);
            }
            List<Select> Selects = new List<Select>()
            {
                new Select() {Id = 1, Selection = "ImageURL"},
                new Select() {Id = 2, Selection = "UserURL"}
            };
            ViewData["selectId"] = new SelectList(Selects, "Id", "Selection");
            return View("Index");
        }
    }
}
