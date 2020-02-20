using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Empleados
    {
        public int empleado_id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public byte[] imagen_perfil { get; set; }
    }
}
