using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionEtablissement_C_Sharp.Services.Persons;
using GestionEtablissement_C_Sharp.Models;
using System.Text.Json;
using Newtonsoft.Json;
using GestionEtablissement_C_Sharp.Services.Students;
using GestionEtablissement_C_Sharp.Services.Teachers;


namespace GestionEtablissement_C_Sharp
{
    class Test
    {
        static IStudentService studentService = new StudentService();
        static ITeacherService teacherService = new TeacherService();
        
        public static void Main()
        {

            string filepath = "database.txt";
            //PersonService database = new StudentService(filepath, "data");

            string type = ""; //student ou teacher
            int choixOperation = 0;
            
        #region (loop that brings up the menu and methods)
            //Boucle principale du programme
            do
            {
                //On affiche le menu
                Console.WriteLine();
                ShowMenu(out int choixPrimaire);
                Console.Clear();


                if (choixPrimaire != 3)
                    //On affiche le menu de choix de l'opération à effectuer
                    ShowCRUDMenu(out choixOperation);

                switch (choixPrimaire)
                {
                    case 1:
                        //Etudiant
                        type = "student";            
                        break;
                    case 2:
                        //Enseignant
                        type = "teacher";
                        break;
                    case 3:
                        //Afficher tout le monde
                        ShowAllPersons();
                        break;
                }

                Person p = null;
                switch (choixOperation)
                {
                    case 1:
                        //Ajouter
                        AddPerson(type);
                        
                        break;
                    case 2:
                        //Supprimer
                        DeletePerson(type , p);
                        
                        break;
                    case 3:
                        //Modifier
                        UpdatePerson(type ,p);
                        
                        break;
                    case 4:
                        //Afficher la liste
                        ShowPersons(type);
                        
                        break;
                }
                Serialize();
                Deserialize();

                //On réinitialise les choix
                choixOperation = 0;

                //On répète le programme jusqu'à fermeture de l'application
            } while (true);

        }

        #endregion


        #region ( CRUD Methods)


        private static void AddPerson(string type)
        {
            
            var p = new StudentService();
            var pStudent = new Student(); 
            var m = new TeacherService();
            var mTeacher = new Teacher();

            string? lastName, firstName;
            DateTime? dateOfBirth;

            Console.Write("Veuillez entrer son lastName : ");
            lastName = Console.ReadLine();

            Console.Write("Veuillez entrer son firstName : ");
            firstName = Console.ReadLine();

            Console.Write("Veuillez entrer sa date de naissance par exemple(30/02/2000) : ");
            dateOfBirth = Convert.ToDateTime(Console.ReadLine());

            switch (type)
            {
                case "student":
                    Console.Write("Veuillez entrer le id de l'etudiant : ");
                    double?  id = Convert.ToDouble(Console.ReadLine());
                    
                    if (lastName != null && id != null)
                    {
                        pStudent = new Student(lastName, firstName, dateOfBirth, id);
                        try
                        {
                            p.Add(pStudent);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Success ");
                        }
                    }
                    
                    break;

                case "teacher":
                    Console.Write("Veuillez entrer la date de prise de fonction de l'enseignant par exemple(30/02/2000): ");
                    DateTime? hireDate = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Veuillez entrer le salary de l'enseignant : ");
                    int salary;
                    try
                    {
                         salary = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Montant invalide. Le salary sera mis à zero.");
                        salary = 0;

                    }

                    if (lastName != null)
                    {
                        mTeacher = new Teacher(lastName, firstName, dateOfBirth, hireDate, salary);
                        m.Add(mTeacher);
                        
                    }
                    
                    break;

            }
            
            if (p != null)
            {
                Console.WriteLine("L'ajoutte c'est bien effectué!!!!\n");
            }

            
        }

        
        
        public static void Serialize()
        {
            string path = "database.txt";
            FileStream fstream;
            if (File.Exists(path))
            {
                try
                {
                    var jsonData = JsonConvert.SerializeObject(StudentService.persons);
                    File.WriteAllText(path, jsonData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                fstream = File.Create(path);
                try
                {
                    var jsonData = JsonConvert.SerializeObject(StudentService.persons);
                    File.WriteAllText(path, jsonData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        private static void Deserialize()
        {
            string path = "database.txt";
            FileStream fstream;
            var json = File.ReadAllText(path);
            var persons = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
        }

        private static void DeletePerson(string type , Person p)
        {
            
            var pp = new StudentService();
            var pStudent = new Student(); 
            var m = new TeacherService();
            var mTeacher = new Teacher();
            Console.Write("Veuillez entrer le lastName de la personne à supprime : ");
            string? lastName = Console.ReadLine();

            #region (commet)
            //bool? ok = StudentService.persons.Find((studentService) =>
            //{

            //    if (studentService is Student && type == "student")
            //    {
            //        //Student student = (Student)person;
            //        pp.Remove(p);
            //        return studentService.LastName == lastName;

            //    }
            //    else if (studentService is Teacher && type == "teacher")
            //    {
            //        teacherService.Remove(p);
            //        return studentService.LastName == lastName;
            //    }

            //    return false;

            //})?.Remove(p);

            #endregion
            StudentService result = new StudentService();
            Person person = new Student();
            person = result.FindByName(lastName);
            PersonService.persons.Remove(person);   

        }


        private static void UpdatePerson(string type , Person p)
        {
            
            Console.Write("Veuillez entrer le lastName de la personne à modifier : ");
            string? lastName = Console.ReadLine();

            foreach(var person in PersonService.persons)
            {
                if (type == "student" && person is Student)
                {
                    Student? student = person as Student;
                    Console.Write("Veuillez entrer son nouveau lastName : ");
                    student.LastName = Convert.ToString(Console.ReadLine());

                    Console.Write("Veuillez entrer son nouveau firstName : ");
                    student.FirstName = Console.ReadLine();

                    Console.Write("Veuillez entrer sa nouvelle date de naissance par exemple(30/02/2000): ");

                    student.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Veuillez entrer le nouveau id de l'etudiant : ");
                    student.Id = Convert.ToDouble(Console.ReadLine());

                }
                else if (type == "teacher" && person is Teacher)
                {
                    Teacher? teacher = person as Teacher;
                    Console.Write("Veuillez entrer la nouvelle date de prise de fonction de l'enseignantt par exemple(30/02/2000): ");
                    teacher.HireDate = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Veuillez entrer le nouveau salary de l'enseignant : ");

                    try
                    {
                        teacher.Salary = Convert.ToDouble(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Montant invalide. Le salary sera mis à zero.");
                        teacher.Salary = 0;
                    }
                }
            }

        }


        private static void ShowAllPersons()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (var person in PersonService.persons)
            {
                //studentService.Show();
                if (person is Student)
                {
                    Student? student = person as Student;
                    Console.WriteLine("\n-----------------------------------------------------------------");
                    Console.WriteLine($"Student : \n  LastName : {student.LastName}\n  FirstName : {student.FirstName}\n  " +
                        $"Date de naissance : {student.DateOfBirth}\n " +
                        $" Matricule : {student.Id}");
                    Console.WriteLine("\n-----------------------------------------------------------------");
                }
                else
                {
                    Teacher? teacher = person as Teacher;
                    Console.WriteLine("\n*************************************************************************");
                    Console.WriteLine($"Teacher : \n  LastName : {teacher.LastName}\n" +
                        $"  FirstName : {teacher.FirstName}\n  Date de naissance : {teacher.DateOfBirth}\n" +
                        $"  Date de prise de fonction : {teacher.HireDate}\n  Salary : {teacher.Salary}");
                    Console.WriteLine("\n*************************************************************************");
                }
            }
            if (PersonService.persons.Count == 0)
                Console.WriteLine("Aucune personnes enregistrees pour le moment!");
        }

        private static void ShowPersons(string type)
        {

            bool showing = false;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            

            List<Person>? persons = null;
            foreach (var person in PersonService.persons)
            {
                if (person is Student && type == "student")
                {
                    //On récupère tous les étudiants
                    persons = ((StudentService)studentService).GetStudents(); 
                    showing = true;
                    Student? student = person as Student;
                    Console.WriteLine("\n-----------------------------------------------------------------");
                    Console.WriteLine($"Student : \n  LastName : {student.LastName}\n  FirstName : {student.FirstName}\n  " +
                        $"Date de naissance : {student.DateOfBirth}\n " +
                        $" Matricule : {student.Id}");
                    Console.WriteLine("\n-----------------------------------------------------------------");
                    break;
                    

                }
                else if(person is Teacher && type == "teacher")
                {
                    //On récupère tous les enseignants
                    persons = ((TeacherService)teacherService).GetTeachers(); 
                    showing = true;
                    Teacher? teacher = person as Teacher;
                    Console.WriteLine("\n*************************************************************************");
                    Console.WriteLine($"Teacher : \n  LastName : {teacher.LastName}\n" +
                        $"  FirstName : {teacher.FirstName}\n  Date de naissance : {teacher.DateOfBirth}\n" +
                        $"  Date de prise de fonction : {teacher.HireDate}\n  Salary : {teacher.Salary}");
                    Console.WriteLine("\n*************************************************************************");

                    break;
                }
            }

            //Si l'on a pas affiché de résultat
            if (!showing)
            {
                Console.WriteLine("Aucun enregistrement pour le moment!");
            }
            //Sinon on affiche la liste trouvée
            else
            {
                
                //persons?.ForEach(p => p.Show());
                
            }
        }


        #endregion



        #region (Menu and Sub Menu)
        public static void ShowCRUDMenu(out int choix)
        {

            Console.WriteLine("\t#------------------------------------------------------#");
            Console.WriteLine("\t#-------------------- Sous Menu --------------------------------#");
            Console.WriteLine("\t#------------------------------------------------------#\n");

            Console.WriteLine("  1- Ajouter");
            Console.WriteLine("  ----------------------------------------------------");
            Console.WriteLine("  2- Supprimer");
            Console.WriteLine("  ------------------------------------------------------");
            Console.WriteLine("  3- Modifier");
            Console.WriteLine("  -----------------------------------------------------");
            Console.WriteLine("  4- Afficher la liste");
            Console.WriteLine("  --------------------------------------------------------------\n");

            Console.Write("Votre choix:  ");
            
            try
            {
                //On récupère le choix de l'utilisateur
                choix = Convert.ToInt32(Console.ReadLine());

                //On vérifie que le choix est valide
                if (choix < 1 || choix > 4)
                    throw new Exception("Votre choix est invalide. Veuillez entrer une valeur valide.\n");

            }
            catch (FormatException e)
            {
                Console.WriteLine("  Veuillez entrer un nombre!");
                ShowCRUDMenu(out choix);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ShowCRUDMenu(out choix);
                return;
            }
        }

        //MY MENU
        public static void ShowMenu(out int choix)
        {
            Console.WriteLine("\t#--------------------------------------------------------------------#");
            Console.WriteLine("\t#-------------------------------- Menu ------------------------------#");
            Console.WriteLine("\t#--------------------------------------------------------------------#\n");

            Console.WriteLine("  1- Etudiant");
            Console.WriteLine("  --------------------------------------------------------------------");
            Console.WriteLine("  2- Enseignant");
            Console.WriteLine("  ---------------------------------------------------------------------");
            Console.WriteLine("  3- Afficher toutes les personnes");
            Console.WriteLine("  -------------------------------------------------------------------\n");

            Console.Write("Votre choix:  ");
            try
            {
                //On récupère le choix de l'utilisateur
                choix = Convert.ToInt32(Console.ReadLine());

                //On vérifie que le choix est valide
                if (choix < 1 || choix > 3)
                    throw new Exception("Votre choix est invalide. Veuillez entrer une valeur  valide.\n");

            }
            catch(FormatException e)
            {
                Console.WriteLine("  Veuillez entrer un nombre!");
                ShowMenu(out choix);
                return;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                ShowMenu(out choix);
                return;
            }

        }
        #endregion
        //string path = "database.txt";
        //FileStream fstream;

        //if (File.Exists(path))
        //{
        //    string data = JsonConvert.SerializeObject(p);
        //    File.WriteAllText(path, data);

        //}
        //else
        //{
        //    fstream = File.Create(path);
        //    string data = JsonConvert.SerializeObject(p);
        //    File.WriteAllText(path, data);
        //}

            
        }
    
}
