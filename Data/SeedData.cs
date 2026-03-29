using MvcApp.Models;

namespace MvcApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            // Если в базе уже есть данные пропускаем инициализацию
            if (context.Products.Any()) {
                return;
            }
            // Добавляем тестовые товары
            var products = new Product[]
            {
                new Product
                {
                    Name = "Ноутбук ASUS",
                    Price = 75000,
                    Brand = "ASUS",
                    Category = "Электроника",
                    Description = "Игровой ноутбук с RTX 3060",
                    CreatedDate = DateTime.Now.AddDays(-30),
                    InStock = true
                },
                new Product
                {
                    Name = "Смартфон Samsung",
                    Price = 45000,
                    Brand = "Samsung",
                    Category = "Электроника",
                    Description = "Galaxy S23 256GB",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    InStock = true
                },
                new Product
                {
                    Name = "Графический планшет для рисования XPPen Artist 13 (2nd)",
                    Price = 25295,
                    Brand = "XPPen",
                    Category = "Электроника",
                    Description = "XPPen Artist 13 (2nd)",
                    CreatedDate = DateTime.Now.AddDays(-15),
                    InStock = true
                },
                new Product
                {
                    Name = "Книга 'Живые игрушки: котики, енотики, лисички'",
                    Price = 1700,
                    Category = "Книги",
                    Description = "Шьем каркасные игрушки из меха",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    InStock = false
                }
            };

            await context.Products.AddRangeAsync(products);            

            //Добавляем тестовые задачи
            DateTime later = DateTime.Now.AddDays(5);
            DateTime earlier = DateTime.Now.AddDays(-3);

            var tasks = new TaskObject[] { 
                new TaskObject
                {
                    Title = "Разработать архитектуру базы данных",
                    Description = "Создать " +
                    "ER-диаграмму и определить основные сущности для проекта",
                    Status = "В работе",
                    Assignee = "Мурье Никита",
                    Priority = "Высокий",
                    IsCompleted = false,
                    DueDate = later,
                },

                new TaskObject
                {                    
                    Title = "Написать документацию API",
                    Description = "Описать все эндпоинты и форматы запросов/ответов",
                    Status = "К выполнению",
                    Assignee = "Волнистикова Альбина",
                    Priority = "Средний",
                    IsCompleted = false,
                    DueDate = later,
                },

                new TaskObject
                {
                    Title = "Провести код-ревью",
                    Description = "Проверить пул-реквест от команды фронтенда",
                    Status = "Выполнено",
                    Assignee = "Диалмазов Евгений",
                    Priority = "Низкий",
                    IsCompleted = false,
                    DueDate = earlier,
                }
            };

            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}
