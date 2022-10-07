using GestionEtablissement_C_Sharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEtablissement_C_Sharp.Services.Persons
{
    internal interface IPersonService
    {
        public static IList<Person>? persons;

        //IList<Person>? GetPerson();
        /// <summary>
        /// Adding a Person
        /// </summary>
        /// <returns></returns>
        Person Add(Person p);

        /// <summary>
        /// Updating a Person that already exists
        /// </summary>
        /// <returns></returns>

        Person? Update(Person p);

        /// <summary>
        /// Remove a Person that exists
        /// </summary>
        /// <returns></returns>
        bool Remove(Person p);

        /// <summary>
        /// Find if a person exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Person? FindByName(string name);

        /// <summary>
        /// Determining the youngest Person 
        /// </summary>
        /// <returns></returns>

        Person? GetYoung();

    }
}
