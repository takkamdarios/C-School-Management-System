using GestionEtablissement_C_Sharp.Models;
using GestionEtablissement_C_Sharp.Services.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEtablissement_C_Sharp.Services.Teachers
{
    internal class TeacherService :PersonService , ITeacherService
    {
        /// <summary>
        /// retourne la liste des enseignants parmis les personnes 
        /// </summary>
        /// <returns></returns>
        public List<Person>? GetTeachers()
        {
            return persons.FindAll((p) => p is Teacher);
            //foreach (var p in Person.persons)
            //{
            //    if (p is Teacher)
            //    {
            //        persons.Add(p);
            //    }
            //}
            //return persons;
        }

        /// <summary>
        /// retourne l'enseignant le mieux payé
        /// </summary>
        /// <returns></returns>
        public Teacher? GetBestTeacher()
        {
            Teacher? bestTeacher = persons.Find(p => p is Teacher) as Teacher;

            if (bestTeacher != null)
            {
                foreach (var p in persons)
                {
                    if (p is Teacher)
                    {
                        if (((Teacher)p).Salary > bestTeacher.Salary)
                        {
                            bestTeacher = (Teacher)p;
                        }
                    }
                }
            }

            return bestTeacher;

        }

    }
}
