using Microsoft.AspNetCore.Mvc;
using MvcApp.Repositories;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class TasksController : Controller
    {
        private ITaskRepository repository;
        public TasksController(ITaskRepository _repository)
        {
            repository = _repository;
        }
        //GET: /Tasks
        public IActionResult Index()
        {
            var tasks = repository.GetAllTasks();
            return View(tasks);
        }

        // GET: /Tasks/Details/5
        public IActionResult Details(int id)
        {
            var task = repository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: /Tasks/Create
        // Действие для отображения пустой формы создания задачи.
        public IActionResult Create()
        {
            return View();
            // Отдаем пустое представление Create.cshtml,
            // в которое будет встроена форма
        }

        //POST: /Tasks/Create
        // Действие для обработки данных, отправленных с формы создания.
        [HttpPost] // Означает, что этот метод обрабатывает только POST-запросы
        [ValidateAntiForgeryToken] // Защита от межсайтовой подделки запроса (CSRF)
        public IActionResult Create(Models.Task task)
        {
            // Проверяем, прошла ли модель валидацию (атрибуты из шага 1).
            if (ModelState.IsValid)
            {
                // ДОБАВЛЯЕМ ОТЛАДОЧНУЮ ИНФОРМАЦИЮ
                Console.WriteLine($"=== ПОПЫТКА ДОБАВЛЕНИЯ ЗАДАЧИ ===");
                Console.WriteLine($"Title: {task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Priority: {task.Priority}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"DueDate: {task.DueDate}");
                Console.WriteLine($"Assignee: {task.Assignee}");
                Console.WriteLine($"IsCompleted: {task.IsCompleted}");

                repository.Add(task);
                TempData["SuccessMessage"] = "Задача успешно добавлена!";
                return RedirectToAction(nameof(Index));
                // Перенаправляем пользователя на список задач (Index)
            }
            return View(task);
        }

        //GET: /Tasks/Edit/5
        // Отображает форму редактирования, предварительно заполненную данными задачи.
        public IActionResult Edit(int id)
        {
            var product = repository.GetById(id);
            if (product == null) return NotFound();
            return View(product);  // Передаем товар в представление Edit.cshtml          
        }

        //POST: /Tasks/Edit
        // Обрабатывает данные, отправленные с формы редактирования.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Models.Task task)
        {
            if (id != task.Id) // Проверка, совпадает ли Id в маршруте с Id в модели
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(task);
                    TempData["SuccessMessage"] = "Задача успешно обновлена!";
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message); // Добавляем ошибку в модель
                }
            }
            return View(task);
        }

        // GET: /Tasks/Delete/5
        // Отображает страницу с подтверждением удаления.
        public IActionResult Delete(int id)
        {
            var task = repository.GetById(id);
            if (task == null)
                return NotFound();
            return View(task);
        }

        // POST: /Tasks/Delete/5
        // Окончательное подтверждение удаления.
        [HttpPost, ActionName("Delete")] 
        // ActionName связывает этот метод с формой,                                         
        // которая ведет на действие "Delete"
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            TempData["SuccessMessage"] = "Задача удалена!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Priotity/Высокий
        public IActionResult Priority(string priority)
        {
            var tasks = repository.GetByPriority(priority);
            ViewBag.Priority = priority;
            return View(tasks); // Используем то же представление Index.cshtml для отображения списка
        }

        // GET: /Tasks/NotOverDue
        public IActionResult NotOverDue()
        {
            var tasks = repository.NotOverDue();
            return View("Index", tasks); // Явно указываем,
                                            // что нужно использовать представление Index.cshtml
        }
    }
}
