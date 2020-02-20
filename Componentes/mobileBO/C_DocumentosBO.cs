using mobileDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileBO
{
    internal class C_DocumentosBO
    {
        internal C_DocumentosBO()
        {

        }

        //internal bool roleExistBO(string _roleName)
        //{
        //    bool _resultado = false;
        //    C_DocumentosDAL _c = new C_DocumentosDAL();
        //    DataSet _ds = new DataSet();
        //    DataTable _dt = new DataTable();

        //    _ds = _c.roles(_roleName);

        //    _dt = _ds.Tables[0];
        //    if (_dt.Rows.Count > 0)
        //        _resultado = true;
        //    return _resultado;
        //}

        //internal DataTable allRolesBO()
        //{
        //    DataTable _dt = new DataTable();
        //    C_DocumentosDAL _dal = new C_DocumentosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles(""); //obtiene la lista completa de los roles
        //    _dt = _ds.Tables[0];
        //    return _dt;
        //}

        //internal DataTable rolePorUsuario(string _userName)
        //{
        //    DataTable _dt = new DataTable();
        //    C_DocumentosDAL _dal = new C_DocumentosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles_U(_userName);
        //    _dt = _ds.Tables[0];
        //    return _dt;

        //}

        internal DataSet DocumentosSelectBo()
        {
            DataTable _dt = new DataTable();
            C_DocumentosDAL _dal = new C_DocumentosDAL();
            DataSet _ds = _dal.C_Documentos_selectDAL();
            return _ds;
        }

        internal int _DocumentosSaveBo(List<Entity.C_documentos> _Datos)
        {
            C_DocumentosDAL _dal = new C_DocumentosDAL();
            int _ds ;
            _ds = _dal.C_Documentos_saveDAL(_Datos);
            return _ds;
        }
    }
}
