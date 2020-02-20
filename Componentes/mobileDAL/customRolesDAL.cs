using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AutoSproc;
using System.Data.SqlClient;

namespace mobileDAL
{

        public interface IcustomRolesDAL : ISprocBase
        {
            DataSet rolesSeguridad(string role); 
            DataSet rolesSeguridad_U(string usuario);
        
        }

        public class customRolesDAL
        {
            private IcustomRolesDAL proc;		
		    private SqlConnection conn=null;
            FabricaConexiones fabricaConexiones;
            ~customRolesDAL()
		    {
			    try
			    {
				    if(conn!=null)
					    conn.Dispose();
			    }
			    catch{}
		}

        public customRolesDAL()
        {            
            try
            {
                fabricaConexiones= new FabricaConexiones();
                proc = (IcustomRolesDAL)SprocFactory.CreateInstance(typeof(IcustomRolesDAL), DBProvider.SQLServer);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }

        public DataSet roles(string _roleName)
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                //foreach
                 _dt = proc.rolesSeguridad(_roleName);

            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        public DataSet roles_U(string _usuario)
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();
                _dt = proc.rolesSeguridad_U(_usuario);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        
    }
}
