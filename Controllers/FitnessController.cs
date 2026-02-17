using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class FitnessController : Controller
    {
        //Создаем List коллекции для хранения объектов с данными
        public List<Subscription> memberShip = new()
        {
            new Subscription("Безлимит 12 месяцев", "Круглосуточный или полный доступ " +
                "ко всем зонам (тренажерный зал, бассейн, " +
                "групповые занятия), обычно включает 30-90 дней «заморозки».",
                "https://i.ibb.co/fYzs5N34/empty.jpg", 13400, 20),
            new Subscription("Дневной абонемент", "Доступ в клуб в будни с открытия до 16:00–17:00. " +
                "Идеально для студентов и фрилансеров, цена ниже на 30-50%.",
                "https://i.ibb.co/fYzs5N34/empty.jpg", 8380, 10),
            new Subscription("Выходной день", "Посещение только в субботу и воскресенье.",
                "https://i.ibb.co/fYzs5N34/empty.jpg", 5500, 5),
            new Subscription("Карта на 10/20 занятий", "Абонемент с фиксированным количеством " +
                "посещений, действует, например, 3 месяца. " +
                "Подходит для нерегулярных тренировок.",
                "https://i.ibb.co/fYzs5N34/empty.jpg", 7000, 0),
            new Subscription("Семейный", "Скидки при покупке двух и более карт для членов семьи.",
                "https://i.ibb.co/fYzs5N34/empty.jpg", 4000, 23)          
        };

        public List<TrainerInformation> trainers = new()
        {
            new TrainerInformation("Дмитрий Соколов", "Силовой тренинг, подготовка к соревнованиям " +
                "(пауэрлифтинг), коррекция фигуры, работа с новичками.", "Моя задача — не просто дать упражнения, " +
                "а научить тебя чувствовать свое тело и кайфовать от процесса»", "РГУФКСМиТ (Российский университет спорта)",
                "https://i.ibb.co/fYzs5N34/empty.jpg", "«Тяжело только первую тренировку. " +
                "Дальше — интересно»"),

            new TrainerInformation("Анна Петрова", "Йога, пилатес, растяжка",
            "Помогу вам обрести гибкость и душевное равновесие",
            "Международная академия йоги, сертификат инструктора по хатха-йоге",
            "https://i.ibb.co/fYzs5N34/empty.jpg", "Йога — это путь к себе"),

            new TrainerInformation("Михаил Иванов", "Кроссфит, функциональный тренинг",
            "Превращу ваше тело в машину",
            "CrossFit Level 1 Trainer, семинары по спортивной медицине",
            "https://i.ibb.co/fYzs5N34/empty.jpg", "Нет предела совершенству")
        };

        //Обязательно по требованиям через ViewBag передаем время в системе
        //Лучше передавать List через ViewBag, с ViewData[''] выдает ошибку 
        public IActionResult Index()
        {
            ViewBag.Title = "Главная";
            ViewBag.trainers = trainers;
            ViewBag.membership = memberShip;
            ViewBag.Time = DateTime.Now.ToString("dd.MM.yyyy");
            return View();
        }
        public IActionResult Schedule() {
            ViewBag.Title = "Расписание групповых занятий (неделя)";            
            return View(); 
        }

        [Route("Fitness/Trainer/{trainerId}")]
        public IActionResult Trainer(int trainerId) {            
            ViewBag.trainerId = trainerId;
            ViewBag.trainers = trainers;            
            return View(); 
        }
        public IActionResult Membership() {
            ViewData["Title"] = "Основные виды абонементов";
            ViewBag.MemberShips = memberShip;
            return View(); 
        }       
    }
}
