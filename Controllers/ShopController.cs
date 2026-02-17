using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{

    //Атрибутная машрутизация на уровне контроллера
    [Route("store")]
    [Route("shop")]
    public class ShopController : Controller
    {
        //GET: /store
        //GET: /shop

        //Создаем List с разными товарами, так удобнее хранить данные
        //См. класс Item в папке Models

        private List<Item> items = new() {
            new Item(1, "Ноутбук", "ASUS", "ProArt GoPro Edition (PX13; HN7306); " +
                "Copilot+ PC", 4.5f, "https://i.ibb.co/PSSMbw8/laptop.jpg"),

            new Item(2, "Смартфон", "Samsung", "ProArt GoPro Edition (PX13; HN7306); " +
                "Copilot+ PC", 4.2f, "https://i.ibb.co/kg9NnDZF/smartphone.jpg"),

            new Item(3, "Планшет", "Xiaomi", "ProArt GoPro Edition (PX13; HN7306); " +
                "Copilot+ PC", 5.0f, "https://i.ibb.co/LhDL8mV9/tablet.png"),

            new Item(4, "Графический планшет", "Serenelife", "ProArt GoPro Edition (PX13; HN7306); " +
                "Copilot+ PC", 3.9f, "https://i.ibb.co/tT3ZgbVw/graphic-tablet.jpg"),

            new Item(5, "Колонки", "JBL", "ProArt GoPro Edition (PX13; HN7306); " +
                "Copilot+ PC", 4.8f, "https://i.ibb.co/DP3k6gcK/kolonki.jpg"),

        };
       
        public IActionResult Index()
        {
            ViewBag.StoreName = "Магазин";
            ViewData["ProductsCount"] = 15;
            return View();
        }

        // GET: /store/category/
        // GET: /store/category/
        [Route("category/{categoryName}")]
        public IActionResult Category(string categoryName)
        {
            ViewBag.Category = categoryName;
            ViewBag.Products = items;
            return View();
        }

        //Архи нужно!!! Без строчки ниже не находит страницу!
        [Route("product/{id}/details")]
        public IActionResult ProductDetails(int id)
        {
            ViewBag.ProductId = id;
            var item = id <= items.Count ? items[id - 1] : items[0]; //Ищем нужный товар по индексу
            ViewBag.ProductName = $"{item.Name} #{id}";  //Имя товара 
            ViewBag.Rating = item.Rating; //Рейтинг
            ViewBag.Corp = item.Corp; //Производитель
            ViewBag.ItemModel = item.ItemModel; //Модель товара (чтобы с id и MOD не генерировать)
            ViewBag.ImagePath = item.ImagePath; //путь к изображению (необязательно)
            return View();
        }
    }
}
