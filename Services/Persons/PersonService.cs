using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEtablissement_C_Sharp.Models;
using GestionEtablissement_C_Sharp.Services.Students;
using GestionEtablissement_C_Sharp.Services.Teachers;
using Newtonsoft.Json;

namespace GestionEtablissement_C_Sharp.Services.Persons
{
    internal abstract class PersonService :IPersonService
    {
        public static List<Person> persons = new();
        //public string FileName { get; set; }
        //string fileLocation;
        //public string SubDirectory { get; set; }

        //public PersonService(string fileName, string directory)
        //{
        //    FileName = fileName;
        //    SubDirectory = directory;
        //    fileLocation = Path.Combine(SubDirectory, fileName);
        //    FileStream fStream = null;
        //    //creation of diectory if exist
        //    if (!Directory.Exists(SubDirectory))
        //        Directory.CreateDirectory(SubDirectory);
        //    //create file if not exist
        //    if (!File.Exists(fileLocation))
        //        fStream = File.Create(fileLocation);
        //    fStream?.Close();
        //    Deserialize();
        //}
        //protected PersonService(string fileName)
        //{
        //    FileName = fileName;
        //    fileLocation = fileName;
        //    if (!File.Exists(fileLocation))
        //        File.Create(fileLocation);
        //    Deserialize();
        //}

        //private void Serialize()
        //{
        //    try
        //    {
        //        var jsonData = JsonConvert.SerializeObject(persons);
        //        File.WriteAllText(fileLocation, jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        //private void Deserialize()
        //{
        //    var json = File.ReadAllText(fileLocation);
        //    var persons = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
        //}

        public void Show(Person p)
        {
            if (p is Student)
            {
                Student? student = p as Student;
            }
            else
            {
                Teacher? teacher = p as Teacher;
            }
        }

        public Person Add(Person p)
        {
            persons.Add(p);
            //Serialize();
            return p;
        }

        public bool Remove(Person p)
        {
            try
            {
                persons.Remove(p);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            //Serialize();
        }
        public Person? Update(Person p)
        {
            if (p is Student)
            {
                Student? student = p as Student;
            }
            else
            {
                Teacher? teacher = p as Teacher;
                try
                {
                    Console.WriteLine("salary =0");

                }
                catch (FormatException)
                {
                    Console.WriteLine("Montant invalide. Le salary sera mis à zero.");
                }
            }
            //Serialize();
            return p;
        }

        /// <summary>
        /// Returns the list of Names From Person 
        /// </summary>
        /// <returns></returns>
        public Person? FindByName(string name)
        {
            return persons.Find((p) => p.LastName == name);
        }

        /// <summary>
        /// returns the youngest Person
        /// </summary>
        /// <returns></returns>
        public Person? GetYoung()
        {
            Person? youngestPerson = persons.First();

            if (youngestPerson != null)
            {
                foreach (var p in persons)
                {
                    if (p.Age < youngestPerson.Age)
                    {
                        youngestPerson = p;
                    }
                }
            }

            return youngestPerson;
        }


        
    }
}
