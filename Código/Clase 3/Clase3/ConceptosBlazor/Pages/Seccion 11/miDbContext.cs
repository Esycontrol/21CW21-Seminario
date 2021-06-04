using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptosBlazor.Pages.Seccion_11
{
    public class miDbContext:DbContext
    {
        public miDbContext(DbContextOptions<miDbContext> options):base(options)
        {

        }
        public DbSet<Alumno> Alumnos { get; set; }


    }
}
