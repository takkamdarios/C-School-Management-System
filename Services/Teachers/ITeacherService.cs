using GestionEtablissement_C_Sharp.Models;
using GestionEtablissement_C_Sharp.Services.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEtablissement_C_Sharp.Services.Teachers
{
    internal interface ITeacherService :IPersonService
    {
        List<Person>? GetTeachers();

        Teacher? GetBestTeacher();
    }
}
