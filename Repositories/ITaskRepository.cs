using MvcApp.Models; // Чтобы использовать тип Task
using System.Collections.Generic; // Для использования коллекций, например IEnumerable

namespace MvcApp.Repositories
{
    public interface ITaskRepository
    {
        //Вернуть все задачи
        public IEnumerable<Models.Task> GetAllTasks();
        //Выбрать задачу по id
        public Models.Task? GetById(int id);
        //Добавить задачу
        public void Add(Models.Task task);
        //Обновить существующую задачу
        public void Update(Models.Task task);
        //Удалить задачу
        public void Delete(int id);
        //Выбрать по приоритету
        public IEnumerable<Models.Task> GetByPriority(string priority);
        //Только непросроченные задачи
        public IEnumerable<Models.Task> NotOverDue();
    }
}
