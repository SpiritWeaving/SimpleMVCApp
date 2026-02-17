namespace MvcApp.Models
{
    //Это класс с данными для товара (модификация!)
    public class Item
    {
        //id
        public int Id { get; set; }
        //наименование
        public string Name { get; set; }   
        //производитель
        public string Corp { get; set; }  
        //модель устройства
        public string ItemModel { get; set; }
        //рейтинг (нужно по заданию)
        public float Rating { get; set; }

        //путь к изображению (необязательно, можно убрать из кода если лень искать фотохостинг)
        public string ImagePath { get; set; }

        public Item(int _id, string name, string corp, string model, float rating, string imagepath)
        {
            Id = _id;
            Name = name;
            Corp = corp;
            ItemModel = model;
            Rating = rating;
            ImagePath = imagepath;
        }
    }
}
