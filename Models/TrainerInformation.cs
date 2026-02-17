namespace MvcApp.Models
{
    public class TrainerInformation
    {
        public string FIO{ get; set; }
        public string avatarPath { get; set; }
        public string specialization { get; set; }
        public string aboutMe { get; set; }
        public string education { get; set; }
        public string phrase { get; set; }

        public TrainerInformation()
        {
            FIO = "ФИО (?)";
            avatarPath = "https://i.ibb.co/fYzs5N34/empty.jpg";
            specialization = "Специализация (?)";
            aboutMe = "Обо мне (?)";
            education = "Образование (?)";
            phrase = "Цитата отсутствует";
        }

        public TrainerInformation(string _FIO, string _specialization, 
            string _aboutMe, string _education, string _avatarPath= "https://i.ibb.co/fYzs5N34/empty.jpg",
            string _phrase = "Цитата отсутствует")
        {
            FIO = _FIO;
            avatarPath = _avatarPath;
            specialization = _specialization;
            aboutMe = _aboutMe;
            education = _education;
            phrase = _phrase;
        }

    }
}
