// Подключаем пространство имен, где лежат атрибуты
// валидации [Required], [StringLength] и т.д.
using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class Product
    {
        // Конструктор инициализирует строки пустыми значениями
        public Product()
        {
            Name = string.Empty;
            Category = string.Empty;
            Description = string.Empty;
        }
        public int Id { get; set; }

        // Атрибут [Required] означает, что поле обязательно для заполнения.
        // ErrorMessage задает текст ошибки, если поле пустое.
        // [StringLength] ограничивает длину строки. Здесь минимум 3, максимум 100 символов.
        // [Display] задает метку для поля, которая будет использоваться в формах (<label>).

        [Required(ErrorMessage = "Название товара обязательно")]
        [StringLength(100, MinimumLength = 3,
        ErrorMessage = "Название должно быть от 3 до 100 символов")]
        [Display(Name = "Название товара")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Цена обязательна")]
        // [Range] проверяет, что число входит в указанный диапазон (от 0.01 до 10 млн).
        [Range(0.01, 10000000,
        ErrorMessage = "Цена должна быть от 0.01 до 10 000 000")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Категория обязательна")]
        [StringLength(50)] // Максимальная длина 50 символов
        [Display(Name = "Категория")]
        public string Category { get; set; }
        [StringLength(500)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Дата добавления")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "В наличии")]
        public bool InStock { get; set; }
        // Метод для расчета цены со скидкой
        public decimal GetDiscountedPrice(decimal discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Скидка должна быть от 0 до 100");
            return Price * (1 - discountPercent / 100);
        }
    }
}
