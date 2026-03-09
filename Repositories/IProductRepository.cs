using MvcApp.Models; // Чтобы использовать тип Product
using System.Collections.Generic; // Для использования коллекций, например IEnumerable

namespace MvcApp.Repositories
{
    // Интерфейс называется IProductRepository (I - стандартное обозначение интерфейса)
    public interface IProductRepository
    {
        // Получить все товары. IEnumerable позволяет проходиться по списку (например, в цикле foreach)
        IEnumerable<Product> GetAll();
        // Получить один товар по его Id. Может вернуть null, если товар не найден.
        Product? GetById(int id);
        // Добавить новый товар. Обычно в реализациях здесь ему присваивается Id.
        void Add(Product product);
        // Обновить существующий товар.
        void Update(Product product);
        // Удалить товар по Id.
        void Delete(int id);
        // Получить товары определенной категории.
        IEnumerable<Product> GetByCategory(string category);
        // Получить только те товары, что есть в наличии (InStock == true).
        IEnumerable<Product> GetInStock();
    }
}