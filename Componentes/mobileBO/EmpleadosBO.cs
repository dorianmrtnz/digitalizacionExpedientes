using mobileDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileBO
{
    internal class EmpleadosBO
    {
        internal EmpleadosBO()
        {

        }

        internal DataSet EmpleadosSelectBo()
        {
            DataTable _dt = new DataTable();
            EmpleadosDAL _dal = new EmpleadosDAL();
         
            DataSet _ds = _dal.Empleados_selectDAL();
          
            return _ds;
        }

        internal int _EmpleadosSaveBo(List<Entity.Empleados> _Datos)
        {

            EmpleadosDAL _dal = new EmpleadosDAL();
            int _ds;
            _ds = _dal.Empleados_saveDAL(_Datos);

            return _ds;
        }
    }
}
