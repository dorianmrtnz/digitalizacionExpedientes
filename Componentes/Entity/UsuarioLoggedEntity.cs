using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class UsuarioLoggedEntity
    {
        private int _usuario_id;
        private string _usuario;
        private int _area_id;
        private string _menu;
        private sistemaDatos[] _sistemas;
        private int _sistema_id_seleccionado;
        private string _area;
        private int _FK_empleado_id_usuario;
        private string _color;
        private string _estilosAdicionales;
        private int _FK_centroTrabajo_id;
        private bool _restingido;
        private bool _cambioContraseña;

        public UsuarioLoggedEntity()
        {
            _color = "";
            _restingido = true;

        }

        public int usuario_id
        {
            get { return _usuario_id; }
            set { _usuario_id = value; }
        }

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        public sistemaDatos[] sistemas
        {
            get { return _sistemas; }
            set { _sistemas = value; }
        }

        public int sistemaSeleccionado
        {
            get { return _sistema_id_seleccionado; }
            set { _sistema_id_seleccionado = value; }
        }

        public int area_id
        {
            get { return _area_id; }
            set { _area_id = value; }
        }
        public string area
        {
            get { return _area; }
            set { _area = value; }
        }
        public int FK_empleado_id_usuario
        {
            get { return _FK_empleado_id_usuario; }
            set { _FK_empleado_id_usuario = value; }
        }
        public int FK_centroTrabajo_id
        {
            get { return _FK_centroTrabajo_id; }
            set { _FK_centroTrabajo_id = value; }
        }

        public string color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string estilosAdicionales
        {
            get { return _estilosAdicionales; }
            set { _estilosAdicionales = value; }
        }
        public bool restingido
        {
            get { return _restingido; }
            set { _restingido = value; }
        }
        public bool cambioContraseña
        {
            get { return _cambioContraseña; }
            set { _cambioContraseña = value; }
        }


    }
    [Serializable]
    public struct sistemaDatos
    {
        public int sistema_id;
        public string sistema;
        public string descripcion;
        public string url;
    }

}
