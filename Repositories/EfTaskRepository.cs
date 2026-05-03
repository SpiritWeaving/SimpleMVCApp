using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Models;
using System.Threading.Tasks;
namespace MvcApp.Repositories
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;
        public EfTaskRepository(AppDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<TaskObject> GetAllTasks()
        {
            return context.Tasks.ToList();
        }

        public async Task<IEnumerable<TaskObject>> GetAllTasksAsync()
        {
            return await context.Tasks.ToListAsync();
        }

        public TaskObject? GetById(int taskId) { 
            return context.Tasks.Find(taskId);
        }

        public async Task<TaskObject?> GetByIdAsync(int taskId) {
            return await context.Tasks.FindAsync(taskId);
        }

        public void Add(TaskObject task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
        }
        public async Task AddAsync(TaskObject task)
        {
            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();
        }

        public void Update(TaskObject task)
        {
            context.Tasks.Update(task);
            context.SaveChanges();
        }
        public async Task UpdateAsync(TaskObject task)
        {
            context.Tasks.Update(task);
            await context.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                context.SaveChanges();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<TaskObject> GetByPriority(string priority)
        {
            return context.Tasks.Where(task => task.Priority == priority);
        }

        public IEnumerable<TaskObject> NotOverDue()
        {
            return context.Tasks.Where(p => p.DueDate >= DateTime.Now).ToList();
        }

        //Новые LINQ методы 
        public IEnumerable<TaskObject> OverDue() => context.Tasks.Where(p => p.IsOverdue()).ToList();

        /// <summary>
        /// Поиск задач по названию, описанию и исполнителю
        /// </summary>
        public IEnumerable<TaskObject> SearchTasks(string searchTerm) =>
        context.Tasks.Where(p => p.Title.Contains(searchTerm) ||
        p.Description.Contains(searchTerm) || p.Assignee.Contains(searchTerm)).OrderBy(p => p.Title).ToList();

        /// <summary>
        /// Пагинация: получение товаров для указанной страницы
        /// </summary>
        public IEnumerable<TaskObject> GetProductsWithPagination(int page, int pageSize) =>
        context.Tasks.OrderBy(p => p.Id)
        .Skip((page - 1) * pageSize) // Пропустить n элементов (сколько страниц "пролистали")
        .Take(pageSize) // Взять k элементов (сколько элементов на странице)
        .ToList();

        public int GetTotalPages(int pageSize)
        {
            var totalCount = GetTotalCount();
            return (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public int GetTotalCount() => context.Tasks.Count();

        // Группировка
        public IEnumerable<IGrouping<string, TaskObject>> GetTasksGroupedByStatus() =>
        context.Tasks.GroupBy(p => p.Status)
        .OrderBy(g => g.Key)
        .ToList();


        //Асинхронные версии
        public async Task<int> GetTotalCountAsync() => await context.Tasks.CountAsync();
        public async Task<IEnumerable<IGrouping<string, TaskObject>>> GetTasksGroupedByStatusAsync() =>
        await context.Tasks.GroupBy(p => p.Status)
        .OrderBy(g => g.Key)
        .ToListAsync();
    }
}
