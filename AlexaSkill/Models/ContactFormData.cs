using System;

namespace AlexaSkill.Models
{
    public class ContactFormData
    {
        public ContactFormData(string firstName, int age, DateTime date)
        {
            FirstName = firstName;
            Age = age;
            Date = date;
        }

        public string FirstName { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
    }
}