using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    [Serializable]
    [DataContract]
    public class UsuarioEntity
    {
        [DataMember]
        public int usuario_id { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}
