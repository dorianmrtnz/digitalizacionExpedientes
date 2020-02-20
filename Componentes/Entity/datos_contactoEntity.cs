using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class Listadatos_contacto : Entity.ICollectionBase
    {
        public datos_contactoEntity this[int index]
        {
            get { return ((datos_contactoEntity)(List[index])); }
            set { List[index] = value; }
        }
        public datos_contactoEntity FindByPrimaryKey(int id_dato)
        {
            System.Collections.IEnumerator elementos = List.GetEnumerator();
            datos_contactoEntity elemento = null;

            while (elementos.MoveNext())
            {
                elemento = (datos_contactoEntity)elementos.Current;
                if (elemento.id_dato == id_dato)
                {
                    return elemento;
                }
            }
            return null;
        }
    }
    [Serializable]

    public class datos_contactoEntity : IEntityBase
    {
        #region variables

          private int _id_dato;
          private string _paterno;
          private string _materno;
          private string _nombre;
          private string _fax;
          private string _direccion;
          private string _FK_idColonia;
          private string _cp;
          private int? _FK_idLocalidad;
          private int? _FK_idMunicipio;
          private int? _FK_idEstado;
          private string _observaciones;
          private DateTime? _fecha;
          private int? _FK_idTitulo;
          private int? _FK_idOrganizacion;
          private int? _FK_idCargo;
          private string _email;
          private int? _FK_idGrupo;
          private int? _FK_idPuesto;
          private string _area;
          private int _UltimaAct;

        #endregion

        #region constructor

        public datos_contactoEntity()
        {
           
            _paterno = null;
            _materno = null;
            _nombre = null;
            _fax= null;
            _direccion = null;
            _FK_idColonia = null;
            _cp = null;
            _FK_idLocalidad = null;
            _FK_idMunicipio = null;
            _FK_idEstado= null;
            _observaciones = null;
            _fecha = null;
            _FK_idTitulo = null;
            _FK_idOrganizacion = null;
            _FK_idCargo = null;
            _email = null;
            _FK_idGrupo = null;
            _FK_idPuesto = null;
            _area = null;
        }
        #endregion
        #region propiedades
        public int id_dato
        {
            get { return _id_dato; }
            set { _id_dato = value; }
        }
        public string paterno
        {
            get { return _paterno; }
            set {  _paterno = value; }
        }
        public string materno
        {
            get { return _materno; }
            set {  _materno = value; }
        }
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public string FK_idColonia
        {
            get { return _FK_idColonia; }
            set {  _FK_idColonia = value; }
        }
        public string cp
        {
            get { return _cp; }
            set { _cp = value; }
        }
        public int? FK_idLocalidad
        {
            get { return _FK_idLocalidad; }
            set { _FK_idLocalidad = value; }
        }
        public int? FK_idMunicipio
        {
            get { return _FK_idMunicipio; }
            set { _FK_idMunicipio = value; }
        }
        public int? FK_idEstado
        {
            get { return _FK_idEstado; }
            set { _FK_idEstado = value; }
        }
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
        public DateTime? fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public int? FK_idTituloo
        {
            get { return _FK_idTitulo; }
            set { _FK_idTitulo = value; }
        }
        public int? FK_idOrganizacion
        {
            get { return _FK_idOrganizacion; }
            set { _FK_idOrganizacion = value; }
        }
        public int? FK_idCargo
        {
            get { return _FK_idCargo; }
            set { _FK_idCargo = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int?  FK_idGrupo
        {
            get { return _FK_idGrupo; }
            set { _FK_idGrupo = value; }
        }

        public int? FK_idPuesto
        {
            get { return _FK_idPuesto; }
            set { _FK_idPuesto = value; }
        }

        public string area
        {
            get { return _area; }
            set { _area = value; }
        }

        public int ultimaact
        {
            get { return _UltimaAct; }
            set { _UltimaAct = value; }
        }
        #endregion
    }
}
