using Newtonsoft.Json;
using SQLite;

namespace XamTestAppDataLibrary.Models
{
    public class Contact
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public float Height { get; set; }
        public string Biography { get; set; }
        public Temperament Temperament { get; set; }

        [Ignore]
        public EducationPeriod EducationPeriod { get; set; }


        // json-сериализованное поле EducationPeriod для хранения в БД. В данном случае такая реализация показалась мне проще чем one-to-one и 2ая таблица
        public string EducationPeriodSerialized
        {
            get => JsonConvert.SerializeObject(EducationPeriod);
            set => EducationPeriod = JsonConvert.DeserializeObject<EducationPeriod>(value);
        }

    }
}
