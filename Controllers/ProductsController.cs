using Microsoft.AspNetCore.Mvc;
using MvcApp.Repositories;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository repository;

        //Внедрение зависимости через конструктор
        public ProductsController(IProductRepository _repository)
        {
            repository = _repository;
        }

        //GET: /Products
        public IActionResult Index()
        {
            var products = repository.GetAll();
            return View(products);
        }

        // GET: /Products/Details/5
        public IActionResult Details(int id) { 
            var product = repository.GetById(id);
            if (product == null) { 
                return NotFound();
            }
            return View(product);
        }

        // GET: /Products/Create
        // Действие для отображения пустой формы создания товара.
        public IActionResult Create() {
            return View(); // Отдаем пустое представление Create.cshtml,
                           // в которое будет встроена форма
        }

        //POST: /Products/Create
        // Действие для обработки данных, отправленных с формы создания.
        [HttpPost] // Означает, что этот метод обрабатывает только POST-запросы
        [ValidateAntiForgeryToken] // Защита от межсайтовой подделки запроса (CSRF)

        public IActionResult Create(Product product)
        {
            // Проверяем, прошла ли модель валидацию (атрибуты из шага 1).
            if (ModelState.IsValid) {
                product.CreatedDate = DateTime.Now;
                repository.Add(product);
                TempData["SuccessMessage"] = "Товар успешно добавлен!";
                return RedirectToAction(nameof(Index));
                // Перенаправляем пользователя на список товаров (Index)
            }
            return View(product);
        }

        //GET: /Products/Edit/5
        // Отображает форму редактирования, предварительно заполненную данными товара.
        public IActionResult Edit(int id) { 
            var product = repository.GetById(id);
            if (product == null) return NotFound();
            return View(product);  // Передаем товар в представление Edit.cshtml          
        }

        //POST: /Products/Edit
        // Обрабатывает данные, отправленные с формы редактирования.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id) // Проверка, совпадает ли Id в маршруте с Id в модели
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(product);
                    TempData["SuccessMessage"] = "Товар успешно обновлен!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message); // Добавляем ошибку в модель
                }
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        // Отображает страницу с подтверждением удаления.
        public IActionResult Delete(int id)
        {
            var product = repository.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // POST: /Products/Delete/5
        // Окончательное подтверждение удаления.
        [HttpPost, ActionName("Delete")] // ActionName связывает этот метод с формой,
                                         // которая ведет на действие "Delete"
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            TempData["SuccessMessage"] = "Товар удален!";
            return RedirectToAction(nameof(Index));
        }
        // GET: /Products/Category/Электроника
        public IActionResult Category(string category)
        {
            var products = repository.GetByCategory(category);
            ViewBag.Category = category;
            return View(products); // Используем то же представление Index.cshtml для отображения списка
        }
        // GET: /Products/InStock
        public IActionResult InStock()
        {
            var products = repository.GetInStock();
            return View("Index", products); // Явно указываем,
                                            // что нужно использовать представление Index.cshtml
        }
    }
}
