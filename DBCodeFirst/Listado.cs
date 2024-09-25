using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCodeFirst
{
    public class Listado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public bool estado { get; set; }
    }
}

/*
Id(auto incremental) de tipo entero
 Título de tipo String
 Descripción de tipo String
 Fecha de creación de tipo DateTime
 Fecha de finalización de tipo DateTime
 Estado de tipo bool  aca es 1 y 0 validar eso al hacer cambio de estado
*/