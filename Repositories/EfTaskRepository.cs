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
    }
}
