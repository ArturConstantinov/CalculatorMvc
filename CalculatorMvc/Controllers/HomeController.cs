using CalculatorMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CalculatorMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string Result = "0")
        {
            if(!ModelState.IsValid)
            {
                //ModelState.AddModelError("", "Input is null");
                //return View("Error");
                return Json(new { result = "Error" });
            }
            Calculator calc = new(Result);
            var result = calc.Calculate();

            //ViewBag.ResultValue = result;

            //return View(result);

            if (double.IsInfinity(result))
            {
                return Json(new { result = "Division by 0" });
            }

            return Json(new { result = result });

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}