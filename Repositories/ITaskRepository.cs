using MvcApp.Models; // Чтобы использовать тип Task
using System.Collections.Generic; // Для использования коллекций, например IEnumerable

namespace MvcApp.Repositories
{
    public interface ITaskRepository
    {
        //Вернуть все задачи
        public IEnumerable<TaskObject> GetAllTasks();
        //Выбрать задачу по id
        public TaskObject? GetById(int id);
        //Добавить задачу
        public void Add(TaskObject task);
        //Обновить существующую задачу
        public void Update(TaskObject task);
        //Удалить задачу
        public void Delete(int id);
        //Выбрать по приоритету
        public IEnumerable<TaskObject> GetByPriority(string priority);
        //Только непросроченные задачи
        public IEnumerable<TaskObject> NotOverDue();
    }
}
