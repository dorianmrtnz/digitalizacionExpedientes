using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Serializable]
    
    public class M_documentosDigitalizadosEmpleados
    {
        public M_documentosDigitalizadosEmpleadosBinarios documento;

        public int documentoDigitalizadoEmpleado_id { get; set; }
        
        public int FK_documento_id { get; set; }
        
        public string nombreArchivo { get; set; }
        
        public int FK_empleado_id { get; set; }
        
        public int FK_usuario_id_captura { get; set; }
      
        public DateTime fechaCaptura { get; set; }
        
        public int FK_usuario_id_modifica { get; set; }
        
        public DateTime fechaModificacion { get; set; }
        
        public int ultimaAct { get; set; }

    }
}
