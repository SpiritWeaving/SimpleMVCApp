using MvcApp.Models; // Чтобы использовать тип Task
using System.Collections.Generic;
using System.Reflection; // Для использования коллекций, например IEnumerable
namespace MvcApp.Repositories
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private List<TaskObject> repository;
        private int nextId = 1;

        //Конструктор по умолчанию
        public InMemoryTaskRepository()
        {
            repository = new List<TaskObject>();
            SeedData();
        }

        //Заполнить репозиторий тестовыми данными
        void SeedData()
        {
            DateTime later = DateTime.Now.AddDays(5);
            DateTime earlier = DateTime.Now.AddDays(-3);
            Add(new TaskObject
            {                
                Title = "Разработать архитектуру базы данных",
                Description = "Создать " +
                "ER-диаграмму и определить основные сущности для проекта",
                Status = "В работе",
                Assignee = "Мурье Никита",
                Priority = "Высокий",
                IsCompleted = false,
                DueDate = later,
            });            

            Add(new TaskObject
            {                
                Title = "Провести код-ревью",
                Description = "Проверить пул-реквест от команды фронтенда",
                Status = "Выполнено",
                Assignee = "Диалмазов Евгений",
                Priority = "Низкий",
                IsCompleted = false,
                DueDate = earlier,
            });

            Add(new TaskObject
            {
                Title = "Написать документацию API",
                Description = "Описать все эндпоинты и форматы запросов/ответов",
                Status = "К выполнению",
                Assignee = "Волнистикова Альбина",
                Priority = "Средний",
                IsCompleted = false,
                DueDate = later,
            });

            Add(new TaskObject
            {
                Title = "Подготовить отчет для заказчика",
                Description = "",
                Status = "Выполнено",
                Assignee = "Рябкова Инга",
                Priority = "Низкий",
                IsCompleted = true,
                DueDate = DateTime.Now,
            });
        }
        //Выбрать все задания
        public IEnumerable<TaskObject> GetAllTasks() => repository;

        //Выбрать задание по id
        public TaskObject? GetById(int id) =>
        repository.FirstOrDefault(p => p.Id == id);

        //Добавить новое задание
        public void Add(TaskObject task)
        {
            task.Id = nextId++;
            repository.Add(task);
        }

        //Обновить существующее задание
        public void Update(TaskObject task)
        {
            var existing = GetById(task.Id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.Priority = task.Priority;
                existing.Status = task.Status;
                existing.Assignee = task.Assignee;
                existing.DueDate = task.DueDate;
                existing.IsCompleted = task.IsCompleted;
            }
        }

        //Удалить задачу по id
        public void Delete(int id)
        {
            var task = GetById(id);
            if (task != null) repository.Remove(task);
        }

        //Выбрать по приоритету
        public IEnumerable<TaskObject> GetByPriority(string priority) => 
            repository.Where(task => task.Priority.Equals(priority,
                StringComparison.OrdinalIgnoreCase));

        //Только просроченные задачи
        public IEnumerable<TaskObject> NotOverDue() =>
        repository.Where(task => !task.IsOverdue());
    }
}
