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
    public interface IM_documentosDigitalizadosEmpleadosDAL : ISprocBase
    {
        DataSet M_documentosDigitalizadosEmpleados_selectDAL( int documentoDigitalizadoEmpleado_id, int FK_documento_id, string nombreArchivo, int FK_empleado_id, int FK_usuario_id_captura, DateTime fechaCaptura, int FK_usuario_id_modifica, DateTime fechaModificacion, int ultimaAct);
        int M_documentosDigitalizadosEmpleados_saveDAL(ref int documentoDigitalizadoEmpleado_id, int FK_documento_id, string nombreArchivo, int FK_empleado_id, int FK_usuario_id);
    }
    public class M_documentosDigitalizadosEmpleadosDAL
    {
        private IM_documentosDigitalizadosEmpleadosDAL proc;
        private SqlConnection conn = null;
        FabricaConexiones fabricaConexiones;
        ~M_documentosDigitalizadosEmpleadosDAL()
        {
            try
            {
                if (conn != null)
                    conn.Dispose();
            }
            catch { }
        }

        public M_documentosDigitalizadosEmpleadosDAL()
        {
            try
            {
                fabricaConexiones = new FabricaConexiones();
                proc = (IM_documentosDigitalizadosEmpleadosDAL)SprocFactory.CreateInstance(typeof(IM_documentosDigitalizadosEmpleadosDAL), DBProvider.SQLServer);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }

        public DataSet M_documentosDigitalizadosEmpleados_selectDAL(int _documentoDigitalizadoEmpleado_id, int _FK_documento_id, string _nombreArchivo, int _FK_empleado_id, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _fechaModificacion, int _ultimaAct)
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                //f
                _dt = proc.M_documentosDigitalizadosEmpleados_selectDAL( _documentoDigitalizadoEmpleado_id, _FK_documento_id, _nombreArchivo, _FK_empleado_id, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _fechaModificacion, _ultimaAct);

            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        public int M_documentosDigitalizadosEmpleados_saveDAL(ref List<Entity.M_documentosDigitalizadosEmpleados> _Datos)
        {
            int _dt = new int();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                int _documentoDigitalizadoEmpleado_id = 0;
                foreach(Entity.M_documentosDigitalizadosEmpleados _d in _Datos)
                {
                    _documentoDigitalizadoEmpleado_id = _d.documentoDigitalizadoEmpleado_id;
                    _dt = proc.M_documentosDigitalizadosEmpleados_saveDAL(ref _documentoDigitalizadoEmpleado_id, _d.FK_documento_id, _d.nombreArchivo, _d.FK_empleado_id, _d.FK_usuario_id_captura);
                    _d.documentoDigitalizadoEmpleado_id = _documentoDigitalizadoEmpleado_id;
                }
                
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