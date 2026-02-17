using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Hello()
        {
            return Content("Привет из DemoController");
        }
        public IActionResult Greeting(string name)
        {
            return Content($"Привет, {name ?? "гость"}!");
        }
        public IActionResult ShowView()
        {
            //Использование ViewBag
            ViewBag.UserName = "WeirdSlugcat";
            ViewBag.RegistrationDate = new DateTime(2026, 2, 05);

            //Использование ViewData
            ViewData["PageTitle"] = "Очень Информативная страница";
            ViewData["VisitCount"] = 25;

            return View();
        }
        public IActionResult UserInfo(string name, int age) {
            ViewBag.name = name ?? "Неизвестный";
            ViewBag.age = age;
            ViewBag.IsAdult = age >= 18;
            ViewData["CurrentYear"] = DateTime.Now.Year;
            ViewData["BirthYear"] = DateTime.Now.Year - age;

            return View();
        }
    }
}
