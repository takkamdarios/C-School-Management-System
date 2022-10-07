using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEtablissement_C_Sharp.Services.Persons;

namespace GestionEtablissement_C_Sharp.Models
{
    public abstract class Person
    {
        public static List<Person> persons = new();

        public string LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        protected int age;

        /// <summary>
        /// Clculating the person's age 
        /// based on the current year and year of burth
        /// </summary>
        public int Age
        {
            get
            {
                try
                {
                    if (age == 0)
                    {
                        DateTime birthday = Convert.ToDateTime(DateOfBirth);
                        age = DateTime.Now.Year - birthday.Year;
                        if (DateTime.Now.DayOfYear < birthday.DayOfYear)
                            age--;
                    }
                }
                catch
                {
                    age = 0;
                    Console.WriteLine("Le format de date est incorrecte.");
                    Console.WriteLine("\n essayer le jour/mois/annee.");
                }

                return age;
            }
        }
        public Person(string lastName, string? prenom, DateTime? dateOfBirth)
        {
            LastName = lastName;
            FirstName = prenom;
            DateOfBirth = dateOfBirth;

        }

        
    }
}
