using SistematicoCuatro.AppCore.IServices;
using SistematicoCuatro.Domain.Entities;
using SistematicoCuatro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistematicoCuatro.AppCore.Services
{
    public class StudentServices : IStudentServices
    {
        IEstudianteRepository EstudianteRepository;
        public StudentServices(IEstudianteRepository estudianteRepository)
        {
            this.EstudianteRepository = estudianteRepository;
        }
        public double CalculateAverage(Estudiante student)
        {
            return this.EstudianteRepository.CalculateAverage(student);
        }

        public void Create(Estudiante t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("El objeto no puede ser null.");
            }
            this.EstudianteRepository.Create(t);
        }

        public bool Delete(Estudiante t)
        {
            return this.EstudianteRepository.Delete(t);
        }

        public Estudiante FindByCarnet(string carnet)
        {
            return this.EstudianteRepository.FindByCarnet(carnet);
        }

        public Estudiante FindById(int id)
        {
            return this.EstudianteRepository.FindById(id);
        }

        public List<Estudiante> FindByLastName(string lastnames)
        {
            return this.EstudianteRepository.FindByLastName(lastnames);
        }

        public List<Estudiante> FindByName(string names)
        {
            return this.EstudianteRepository.FindByName(names);
        }

        public List<Estudiante> GetAll()
        {
            return this.EstudianteRepository.GetAll();
        }

        public int Update(Estudiante t)
        {
            return this.EstudianteRepository.Update(t);
        }

        public bool verifyCarnet(string carnet)
        {
            return EstudianteRepository.verifyCarnet(carnet);
        }

        public bool verifyEmail(string email)
        {
            return EstudianteRepository.verifyEmail(email);
        }
    }
}
