using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionEtablissement_C_Sharp.Models
{
    public class Teacher : Person
    {
        /// <summary>
        /// properties involving only teachers
        /// </summary>
        public DateTime? HireDate { get; set; }
        public double Salary { get; set; }

        private string subject;

        //public string subject;
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public Teacher(string lastName, string? firstName, DateTime? dateOfBirth, DateTime? hireDate,
            double salary, string subject = "None")
            : base(lastName, firstName, dateOfBirth)
        {
            HireDate = hireDate;
            Salary = salary;
            Subject = subject;
        }

        public Teacher() : base("", "", null)
        {
            HireDate = null;
            subject = "None";
        }

        //Méthodes

        public void Explain()
        {
            Console.WriteLine("Explanation begins.");
        }

    }
}
