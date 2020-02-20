using mobileDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileBO
{
    internal class M_documentosDigitalizadosEmpleadosBO
    {
        internal M_documentosDigitalizadosEmpleadosBO()
        {

        }
        //internal bool roleExistBO(string _roleName)
        //{
        //    bool _resultado = false;
        //    M_documentosDigitalizadosEmpleadosDAL _c = new M_documentosDigitalizadosEmpleadosDAL();
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
        //    M_documentosDigitalizadosEmpleadosDAL _dal = new M_documentosDigitalizadosEmpleadosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles(""); //obtiene la lista completa de los roles
        //    _dt = _ds.Tables[0];
        //    return _dt;
        //}

        //internal DataTable rolePorUsuario(string _userName)
        //{
        //    DataTable _dt = new DataTable();
        //    M_documentosDigitalizadosEmpleadosDAL _dal = new M_documentosDigitalizadosEmpleadosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles_U(_userName);
        //    _dt = _ds.Tables[0];
        //    return _dt;
        //}

        internal DataTable M_documentosDigitalizadosEmpleados_selectBo(ref int _documentoDigitalizadoEmpleado_id, int _FK_documento_id, string _nombreArchivo, int _FK_empleado_id, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _fechaModificacion, int _ultimaAct)
        {
            DataTable _dt = new DataTable();
            M_documentosDigitalizadosEmpleadosDAL _dal = new M_documentosDigitalizadosEmpleadosDAL();
            DataSet _ds = new DataSet();
            _ds = _dal.M_documentosDigitalizadosEmpleados_selectDAL(_documentoDigitalizadoEmpleado_id, _FK_documento_id, _nombreArchivo, _FK_empleado_id, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _fechaModificacion, _ultimaAct);
            _dt = _ds.Tables[0];
            return _dt;
        }

        internal int M_DocumentosDigitalizadosEmpleados_SaveBO(ref List<Entity.M_documentosDigitalizadosEmpleados> _Datos)
        {

            M_documentosDigitalizadosEmpleadosDAL _dal = new M_documentosDigitalizadosEmpleadosDAL();
            int _ds;
            _ds = _dal.M_documentosDigitalizadosEmpleados_saveDAL(ref _Datos);

            return _ds;
        }
    }


}
