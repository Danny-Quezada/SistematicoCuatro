
using SistematicoCuatro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistematicoCuatro.AppCore.IServices
{
    public interface IStudentServices : IServices<Estudiante>
    {
        bool verifyEmail(string email);
        bool verifyCarnet(string carnet);
        List<Estudiante> FindByName(string names);
        List<Estudiante> FindByLastName(string lastnames);
        Estudiante FindByCarnet(string carnet);
        Estudiante FindById(int id);

        double CalculateAverage(Estudiante student);
    }

}
