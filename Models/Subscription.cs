namespace MvcApp.Models
{
    //Это класс с данными для одного абонемента
    public class Subscription
    {
        //Наименование
        public string name { get; set; }

        //Описание
        public string description { get; set; }

        //Путь к изображению
        public string imagePath { get; set; }

        //Цена
        public double price { get; set; }

        //Скидка
        public int discount { get; set; }

        public Subscription()
        {
            name = "Абонемент (?)";
            description = "Описание (?)";
            imagePath = "";
            price = 0;
            discount = 0;
        }

        public Subscription(string _name, string _description, 
            string _imagePath= "https://i.ibb.co/fYzs5N34/empty.jpg", double _price=10000,
            int _discount = 0)
        {
            this.name = _name;
            this.description = _description;
            this.imagePath = _imagePath;
            this.price = _price;
            this.discount = _discount;
        }

        //Подсчет скидки
        public double Discount()
        {
            double result = price * (100 - discount) / 100;
            return Math.Round(result, 2);
        }
    }
}
