using SistematicoCuatro.Domain.Entities;
using SistematicoCuatro.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistematicoCuatro.Infraestructure.Repository
{
    public class EFEstudianteRepository : IEstudianteRepository
    {
        public IStudentDBContext studentDB;
        public EFEstudianteRepository(IStudentDBContext studentDBContext)
        {
            this.studentDB = studentDBContext;
        }
        public double CalculateAverage(Estudiante student)
        {
            double average = student.Estadistica + student.Matematica + student.Programacion + student.Contabilidad;
            average = average / 4;
            return average;
        }

        public void Create(Estudiante t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentException("El objeto employee no puede ser nulo");
                }
                studentDB.Estudiantes.Add(t);
                studentDB.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Estudiante t)
        {
            try
            {

                if (t == null)
                {
                    throw new ArgumentException("Object null");
                }
                Estudiante estudiante = FindById(t.Id);
                if (estudiante == null)
                {
                    throw new Exception($"Object with Id {t.Id} not exist");
                }
                studentDB.Estudiantes.Remove(t);
                int result = studentDB.SaveChanges();
                return result > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public Estudiante FindByCarnet(string carnet)
        {
            try
            {
                if (carnet == null)
                {

                    throw new Exception($"El carnet {carnet} no puede ser menor o igual a cero.");

                }
                return studentDB.Estudiantes.FirstOrDefault(x => x.Carnet.Equals(carnet, StringComparison.CurrentCultureIgnoreCase));
            }
            catch
            {
                throw;
            }
        }

        public Estudiante FindById(int id)
        {
            try
            {

                if (id <= 0)
                {
                    throw new Exception($"El id {id} no puede ser menor o igual a cero.");
                }
                return studentDB.Estudiantes.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public List<Estudiante> FindByLastName(string lastnames)
        {
            try
            {

                if (lastnames==null)
                {
                    throw new Exception($"El apellido {lastnames} no puede ser menor o igual a cero.");
                }
                return studentDB.Estudiantes.Where(x => x.Apellidos == lastnames).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<Estudiante> FindByName(string names)
        {
            try
            {

                if (names ==null)
                {
                    throw new Exception($"El nombre {names} no puede ser menor o igual a cero.");
                }
                return studentDB.Estudiantes.Where(x => x.Nombres.Contains(names)).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<Estudiante> GetAll()
        {
            try
            {
                return studentDB.Estudiantes.ToList();
            }
            catch
            {
                throw;
            }
        }

        public int Update(Estudiante t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto asset no puede ser null.");
                }

                Estudiante estudiante = FindById(t.Id);
                if (estudiante == null)
                {
                    throw new Exception($"El objeto asset con id {t.Id} no existe.");
                }
                // personal
                estudiante.Id = t.Id;
                estudiante.Nombres = t.Nombres;
                estudiante.Phone = t.Phone;
                estudiante.Apellidos = t.Apellidos;
                estudiante.Carnet = t.Carnet;
                estudiante.Correo = t.Correo;
                estudiante.Direccion = t.Direccion;
                // Clases
                estudiante.Contabilidad = t.Contabilidad;
                estudiante.Estadistica = t.Estadistica;
                estudiante.Matematica = t.Matematica;
                estudiante.Programacion = t.Programacion;
                
                
                studentDB.Estudiantes.Update(estudiante);
                return studentDB.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool verifyCarnet(string carnet)
        {
            return studentDB.Estudiantes.Where(x => x.Carnet == carnet).ToList().Count > 0 ? true : false;
        }

        public bool verifyEmail(string email)
        {
           return studentDB.Estudiantes.Where(x=>x.Correo.ToUpper()==email.ToUpper()).ToList().Count>0 ? true: false;
        }
    }
}
