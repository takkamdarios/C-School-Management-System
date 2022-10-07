using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEtablissement_C_Sharp.Services.Students;

namespace GestionEtablissement_C_Sharp.Models
{
    public class Student : Person
    {


        /// <summary>
        /// Matricule (Id) property
        /// properties involving only Students
        /// </summary>
        public double? Id { get; set; }

        public Student(string lastName, string? firstName, DateTime? dateOfBirth, double? id) :
            base(lastName, firstName, dateOfBirth)
        {
            Id = id;

        }

        public Student() : base("", "", null)
        {
            Id = 0;
            //this.Matricule = "";
        }

    }
}
