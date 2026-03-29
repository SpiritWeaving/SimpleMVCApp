using System;
using System.Collections.Generic;
using System.Linq; //// Для LINQ-методов: FirstOrDefault, Where, etc.
using MvcApp.Models;

//Это конкретный класс, который выполняет контракт IProductRepository,
//но хранит данные не в базе данных, а в оперативной памяти (List<Product>).
//Это идеально для лабораторных и прототипов.

namespace MvcApp.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        //Наследуемся от интерфейса. Если не реализовать какой-то 
        //метод из интерфейса, компилятор выдаст ошибку.

        private readonly List<Product> products;

        // Поле для генерации нового Id. Каждый раз,
        // когда добавляется товар, _nextId увеличивается.
        private int nextId = 1;

        public InMemoryProductRepository()
        {
            products = new List<Product>();
            SeedData(); // Вызываем метод для заполнения тестовыми данными.
        }

        private void SeedData()
        {
            // Добавляем тестовые данные
            Add(new Product
            {
                Id = nextId,
                Name = "Ноутбук ASUS",
                Brand = "ASUS",
                Price = 75000,
                Category = "Электроника",
                Description = "Игровой ноутбук",
                CreatedDate = DateTime.Now,
                InStock = true
            });
            Add(new Product
            {
                Id = nextId,
                Name = "Смартфон Samsung",
                Brand = "Samsung",
                Price = 45000,
                Category = "Электроника",
                Description = "Galaxy S23",
                CreatedDate = DateTime.Now,
                InStock = true
            });
        }

        public void Add(Product product) { 
            nextId++;
            products.Add(product);
        }

        // Ищет первый элемент (FirstOrDefault), у которого Id совпадает с запрошенным.
        // Если не находит, вернет null.
        public Product? GetById(int id) => 
products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetAll() => products;

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Brand = product.Brand;
                existing.Price = product.Price;
                existing.Category = product.Category;
                existing.Description = product.Description;
                existing.InStock = product.InStock;
                // Не обновляем CreatedDate
            }
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
                products.Remove(product);
        }

        // LINQ-метод Where фильтрует список.
        // StringComparison.OrdinalIgnoreCase - игнорируем регистр букв.
        public IEnumerable<Product> GetByCategory(string category) =>
        products.Where(p => p.Category.Equals(category,
        StringComparison.OrdinalIgnoreCase));
        public IEnumerable<Product> GetInStock() =>
        products.Where(p => p.InStock);
    }
}
