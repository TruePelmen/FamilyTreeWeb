namespace FamilyTreeWeb.Models
{
    public enum AnniversaryType
    {
        Birth,
        Death
    }

    public class Anniversary
    {
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public AnniversaryType Type { get; set; }

        public string Record 
        {
            get 
            {
                string start = "";
                if (Date.Day == DateTime.Today.Day )
                {
                    start = "Сьогодні";
                }
                else
                {
                    start = $"Через {Date.Day - DateTime.Today.Day} днів";
                }
                
                if (Type == AnniversaryType.Birth)
                {
                    return $"{start} {Person.FullName} виповниться {DateTime.Today.Year - Date.Year} років.";
                }
                else
                {
                    return $" {start} {DateTime.Today.Year - Date.Year} років з дня сметрі {Person.FullName}.";
                }
            }
        }
    }
}
