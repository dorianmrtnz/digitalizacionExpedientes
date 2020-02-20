using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mobileDAL;
using System.Data;

namespace mobileBO
{
    internal class customRoleBO
    {
        internal customRoleBO()
        {
        }

        internal bool roleExistBO(string _roleName)
        {
            bool _resultado=false;
            customRolesDAL _c = new customRolesDAL();
            DataSet _ds = new DataSet();
            DataTable _dt = new DataTable();

            _ds = _c.roles(_roleName);

            _dt = _ds.Tables[0];
            if (_dt.Rows.Count > 0)
                _resultado = true;
            return _resultado;

        }

        internal DataTable allRolesBO()
        {
            DataTable _dt = new DataTable();
            customRolesDAL _dal =new customRolesDAL();
            DataSet _ds = new DataSet();
            _ds = _dal.roles(""); //obtiene la lista completa de los roles
            _dt = _ds.Tables[0];
            return _dt;
        }

        internal DataTable rolePorUsuario(string _userName)
        {
            DataTable _dt = new DataTable();
            customRolesDAL _dal = new customRolesDAL();
            DataSet _ds = new DataSet();
            _ds = _dal.roles_U(_userName);
            _dt = _ds.Tables[0];
            return _dt;
        }

    }
}
