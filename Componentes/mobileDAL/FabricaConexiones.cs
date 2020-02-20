using AutoSproc;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Xml;

namespace mobileDAL
{
    public interface iFabricaConexiones : ISprocBase
        {              
        }
        public class FabricaConexiones
        {
            private ISprocBase proc;		
		    private SqlConnection conn=null;
            public System.Data.IDbConnection GeneraConexionRecepcionDocumentos()
            {   
                try
                {
                    SqlConnection conn = new SqlConnection(Entity.Parametros.GetInstance().connectionRecepcionDocumentos);
                    object imp = null;
                    imp = SprocFactory.CreateInstance(typeof(iFabricaConexiones), DBProvider.SQLServer);
                    proc = (ISprocBase)imp;
                    proc.Connection = conn;
                    return proc.Connection;
                }
                catch (Exception ex)
                {
                    Entity.LogController.Instance().AgregarMensaje(ex);
                    throw;
                }
            }   

        public System.Data.IDbConnection GeneraConexionSIEE()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Entity.Parametros.GetInstance().connectionSIEE);
                object imp = null;
                imp = SprocFactory.CreateInstance(typeof(iFabricaConexiones), DBProvider.SQLServer);
                proc = (ISprocBase)imp;
                proc.Connection = conn;
                return proc.Connection;
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }

    }  
}
