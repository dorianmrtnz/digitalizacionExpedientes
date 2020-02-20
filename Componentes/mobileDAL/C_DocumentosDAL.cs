using AutoSproc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mobileDAL
{
    public interface IC_DocumentosDAL : ISprocBase  
    {
        DataSet C_Documentos_select();
        int C_Documentos_save(int documento_id, int clave, string descripcionDocumento, int FK_usuario_id_captura, DateTime fecha_Captura, int FK_usuario_id_modifica, DateTime fechaModificacion);
        
    }
    public class C_DocumentosDAL
    {
        private IC_DocumentosDAL proc;
        private SqlConnection conn = null;
        FabricaConexiones fabricaConexiones;
        ~C_DocumentosDAL()
        {
            try
            {
                if (conn != null)
                    conn.Dispose();
            }
            catch { }
        }

        public C_DocumentosDAL()
        {
            try
            {
                fabricaConexiones = new FabricaConexiones();
                proc = (IC_DocumentosDAL)SprocFactory.CreateInstance(typeof(IC_DocumentosDAL), DBProvider.SQLServer);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }

        //public DataSet C_Documentos_selectDAL(int _documento_id, int _clave, string _descripcionDocumento, int _FK_usuario_id_captura, string _fecha_Captura, int _FK_usuario_id_modifica, string _fechaModificacion, int _ultimaAct)
        //{
        public DataSet C_Documentos_selectDAL()
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                
                _dt = proc.C_Documentos_select();

            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        public int C_Documentos_saveDAL(List<Entity.C_documentos> _Datos)
        {
            int _dt = new int();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                int _documento_id = 0;
                foreach (Entity.C_documentos _d in _Datos)
                {
                    _documento_id = _d.documento_id;
                    _dt = proc.C_Documentos_save(_d.documento_id, _d.clave, _d.descripcionDocumento, _d.FK_usuario_id_captura, _d.fecha_Captura, _d.FK_usuario_id_modifica, _d.fechaModificacion);
                    _d.documento_id = _documento_id;
                }
                //_dt = proc.C_Documentos_save(_documento_id, _clave, _descripcionDocumento, _FK_usuario_id_captura, _fecha_Captura, _FK_usuario_id_modifica, _fechaModificacion);
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