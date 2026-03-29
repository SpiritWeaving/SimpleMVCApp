using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class TaskObject
    {
        //Конструктор по умолчанию
        public TaskObject()
        {
            Title = string.Empty;
            Description = string.Empty;
            Assignee = string.Empty;
            Priority = "Средний";
            Status = "К выполнению";
            IsCompleted = false;
            CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Количество символов " +
            "должно быть от 3 до 100")]
        [Display(Name = "Название задачи")]
        public string Title { get; set; }

        [StringLength(500, MinimumLength = 0)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        //Низкий, Средний, Высокий, Критичный        
        [Display(Name = "Приоритет")]
        public string Priority { get; set; }

        //Исполнитель
        [Display(Name = "Исполнитель")]
        public string Assignee { get; set; }

        //К выполнению, В работе, Выполнено
        [Required(ErrorMessage = "Статус обязателен!")]
        [Display(Name = "Статус")]
        public string Status {  get; set; }

        //дата (не раньше сегодня)
        [DataType(DataType.Date)]
        [Display(Name = "Дедлайн")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Завершено")]
        public bool IsCompleted { get; set; }

        //Проверка просрочки, бизнес-логика
        public bool IsOverdue()
        {
            DateTime today = DateTime.Now;
            int result = DateTime.Compare(today, DueDate);
            if(result > 0)            
                return true;
            else 
                return false;
        }

        public DateTime CreatedDate { get; set; }
    }
}
