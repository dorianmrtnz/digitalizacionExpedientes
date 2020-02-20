using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Serializable]
    
    public class C_documentos
    {
        public int documento_id { get; set; }

        public int clave { get; set; }
        
        public string descripcionDocumento { get; set; }
        
        public int FK_usuario_id_captura { get; set; }
        
        public DateTime fecha_Captura { get; set; }
        
        public int FK_usuario_id_modifica { get; set; }
        
        public DateTime fechaModificacion { get; set; }
        
        public int ultimaAct { get; set; }

    }
}

