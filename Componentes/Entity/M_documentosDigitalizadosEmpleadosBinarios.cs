using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Serializable]
    
    public class M_documentosDigitalizadosEmpleadosBinarios
    { 
        public int documentoDigitalizadoEmpleadoBinario_id { get; set; }
        
        public int FK_documentoDigitalizadoEmpleadoBinario_id { get; set; }
        
        public byte[] binario { get; set; }
        
        public int FK_usuario_id_captura { get; set; }
        
        public DateTime fechaCaptura { get; set; }
        
        public DateTime FK_usuario_id_modifica { get; set; }
        
        public DateTime fechaModificacion { get; set; }

        public int ultimaAct { get; set; }

    }
}
