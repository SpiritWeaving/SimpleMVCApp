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

        //Новые LINQ методы 
        public IEnumerable<TaskObject> OverDue();

        // Поиск по тексту
        public IEnumerable<TaskObject> SearchTasks(string searchTerm);
        // Пагинация
        public IEnumerable<TaskObject> GetProductsWithPagination(int page, int pageSize);
        public int GetTotalPages(int pageSize);
        public int GetTotalCount();        
        
        // Группировка
        public IEnumerable<IGrouping<string, TaskObject>> GetTasksGroupedByStatus();


        //Асинхронные версии
        Task<IEnumerable<TaskObject>> GetAllTasksAsync();
        Task<TaskObject?> GetByIdAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<IGrouping<string, TaskObject>>> GetTasksGroupedByStatusAsync();
    }
}
