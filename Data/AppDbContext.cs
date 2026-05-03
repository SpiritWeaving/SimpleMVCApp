using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
namespace MvcApp.Data
{
    public class AppDbContext : DbContext
    {
        // Конструктор, принимающий параметры подключения
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) {
        }

        //DbSet представляет таблицу Products в БД
        public DbSet<Product> Products { get; set; }
        public DbSet<TaskObject> Tasks { get; set; }

        //Дополнительная настройка модели (опционально)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Настройка таблиц Products и Tasks
            modelBuilder.Entity<Product>(entity =>
            {
                // PK
                entity.HasKey(p => p.Id);

                //Name
                entity.Property(p => p.Name)
                .IsRequired() //NOT NULL
                .HasMaxLength(100); //VARCHAR(100)

                //Price (точность для денежных сумм)
                entity.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

                //Индекс для быстрого поиска по категории
                entity.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");
            });
            modelBuilder.Entity<TaskObject>(entity => {
                // PK
                entity.HasKey(p => p.Id);

                //Title
                entity.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

                //Description
                entity.Property(p => p.Description)
                .HasMaxLength(500);

                //Priority
                entity.Property(p => p.Priority)
                .HasDefaultValue("Средний");

                //Status
                entity.Property(p => p.Status)
                .HasDefaultValue("К выполнению");

                //Status
                entity.Property(p => p.ImagePath).IsRequired(false)
                .HasDefaultValue("empty.png");
            });
        }
    }
}
