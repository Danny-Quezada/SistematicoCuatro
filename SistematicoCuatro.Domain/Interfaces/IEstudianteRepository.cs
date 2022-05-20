using SistematicoCuatro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistematicoCuatro.Domain.Interfaces
{

    public interface IEstudianteRepository: IRepository<Estudiante>
    {
        bool verifyCarnet(string carnet);
        bool verifyEmail(string email);
        List<Estudiante> FindByName(string names);
        List<Estudiante> FindByLastName(string lastnames);
        Estudiante FindByCarnet(string carnet);
        Estudiante FindById(int id);

        double CalculateAverage(Estudiante student);
    }
}
