using Microsoft.EntityFrameworkCore;
using SistematicoCuatro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SistematicoCuatro.Domain.Interfaces
{
    public interface IStudentDBContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
