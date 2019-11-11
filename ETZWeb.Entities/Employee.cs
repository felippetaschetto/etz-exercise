using System;

namespace ETZWeb.Entities
{
    public class Employee
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public SexEnum Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}
