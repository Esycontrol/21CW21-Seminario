using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<TareaPropiedades> TareasPropiedades { get; set; }
    }
}
