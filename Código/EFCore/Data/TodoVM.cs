using System.Collections.Generic;
using System.Linq;

namespace EFCore.Data
{
    public class TodoVM
    {
        public List<Tarea> ListaTareas { get; set; } = new();
        public TareaPropiedades Propiedades { get; set; } = new();

        private MiDbContext _context;

        public TodoVM(MiDbContext context)
        {
            _context = context;
            ListaTareas = context.Tareas.ToList();
        }

        public List<Tarea> DameSubtareasDe(Tarea miTarea)
        {
            if (miTarea != null)
            {
                return ListaTareas.Where(tarea => tarea.PadreId == miTarea.Id).ToList();
            }
            return null;
        }

        public List<Tarea> DameListas()
        {            
                return ListaTareas.Where(tarea => tarea.PadreId == -1).ToList();            
        }

        public void CrearLista()
        {
            _context.Tareas.Add(new Tarea());
            _context.SaveChanges();
        }

        public void CrearSubtareaDe(Tarea tareaPadre)
        {
            _context.Tareas.Add(new Tarea() { PadreId = tareaPadre.Id });
            _context.SaveChanges();
        }


    }
}
