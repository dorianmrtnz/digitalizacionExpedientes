using mobileDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileBO
{
    internal class M_documentosDigitalizadosEmpleadosBinariosBO
    {

        internal M_documentosDigitalizadosEmpleadosBinariosBO()
        {

        }

        //internal bool roleExistBO(string _roleName)
        //{
        //    bool _resultado = false;
        //    M_documentosDigitalizadosEmpleadosBinariosDAL _c = new M_documentosDigitalizadosEmpleadosBinariosDAL();
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
        //    M_documentosDigitalizadosEmpleadosBinariosDAL _dal = new M_documentosDigitalizadosEmpleadosBinariosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles(""); //obtiene la lista completa de los roles
        //    _dt = _ds.Tables[0];
        //    return _dt;
        //}

        //internal DataTable rolePorUsuario(string _userName)
        //{
        //    DataTable _dt = new DataTable();
        //    M_documentosDigitalizadosEmpleadosBinariosDAL _dal = new M_documentosDigitalizadosEmpleadosBinariosDAL();
        //    DataSet _ds = new DataSet();
        //    _ds = _dal.roles_U(_userName);
        //    _dt = _ds.Tables[0];
        //    return _dt;
        //}

        internal DataTable M_documentosDigitalizadosEmpleadosBinarios_selectBO(ref int _documentoDigitalizadoEmpleadoBinario_id, int _FK_documentosDigitalizadosEmpleadoBinario_id, double _binario, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _fechaModificacion, int _ultimaAct)
        {
            DataTable _dt = new DataTable();
            M_documentosDigitalizadosEmpleadosBinariosDAL _dal = new M_documentosDigitalizadosEmpleadosBinariosDAL();
            DataSet _ds = new DataSet();
            _ds = _dal.M_documentosDigitalizadosEmpleadosBinarios_selectDAL(_documentoDigitalizadoEmpleadoBinario_id, _FK_documentosDigitalizadosEmpleadoBinario_id, _binario, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _fechaModificacion, _ultimaAct);
            _dt = _ds.Tables[0];
            return _dt;
        }

        internal int M_documentosDigitalizadosEmpleadosBinarios_saveBO(List<Entity.M_documentosDigitalizadosEmpleadosBinarios> _Datos)
        {
           
            M_documentosDigitalizadosEmpleadosBinariosDAL _dal = new M_documentosDigitalizadosEmpleadosBinariosDAL();
            int _ds ;
            _ds = _dal.M_documentosDigitalizadosEmpleadosBinarios_saveDAL(_Datos);
     
            return _ds;
        }


    }


}
