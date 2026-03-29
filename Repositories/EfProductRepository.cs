using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Models;

namespace MvcApp.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        //внедрение контекста через конструктор
        public EfProductRepository(AppDbContext _context) { 
            context = _context;
        }
        public IEnumerable<Product> GetAll() {
            return context.Products.ToList();
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }
        public Product? GetById(int id)
        {
            return context.Products.Find(id);
        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }
        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }
        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }
        public async Task UpdateAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }
        public IEnumerable<Product> GetByCategory(string category)
        {
            return context.Products
            .Where(p => p.Category == category)
            .ToList();
        }
        public IEnumerable<Product> GetInStock()
        {
            return context.Products
            .Where(p => p.InStock)
            .ToList();
        }
    }
}
