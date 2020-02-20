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
    public interface IM_documentosDigitalizadosEmpleadosBinariosDAL : ISprocBase
    {
        DataSet M_documentosDigitalizadosEmpleadosBinarios_selectDAL(ref int documentoDigitalizadoEmpleadoBinario_id, int FK_documentoDigitalizadoEmpleadoBinario_id, double binario, int FK_usuario_id_captura, DateTime fechaCaptura, int FK_usuario_id_modifica, DateTime fechaModificacion, int ultimaAct);
        int M_documentosDigitalizadosEmpleadosBinarios_saveDAL(int documentoDigitalizadoEmpleadoBinario_id, int FK_documentoDigitalizadoEmpleadoBinario_id, byte[] binario, int FK_usuario_id_captura, DateTime fechaCaptura, int FK_usuario_id_modifica, DateTime fechaModificacion);
    }
    public class M_documentosDigitalizadosEmpleadosBinariosDAL
    {
        private IM_documentosDigitalizadosEmpleadosBinariosDAL proc;
        private SqlConnection conn = null;
        FabricaConexiones fabricaConexiones;
        ~M_documentosDigitalizadosEmpleadosBinariosDAL()
        {
            try
            {
                if (conn != null)
                    conn.Dispose();
            }
            catch { }
        }

        public M_documentosDigitalizadosEmpleadosBinariosDAL()
        {
            try
            {
                fabricaConexiones = new FabricaConexiones();
                proc = (IM_documentosDigitalizadosEmpleadosBinariosDAL)SprocFactory.CreateInstance(typeof(IM_documentosDigitalizadosEmpleadosBinariosDAL), DBProvider.SQLServer);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }

        public DataSet M_documentosDigitalizadosEmpleadosBinarios_selectDAL(int _documentoDigitalizadoEmpleadoBinario_id, int _FK_documentoDigitalizadoEmpleadoBinario_id, double _binario, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _fechaModificacion, int _ultimaAct)
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                
                _dt = proc.M_documentosDigitalizadosEmpleadosBinarios_selectDAL(ref _documentoDigitalizadoEmpleadoBinario_id, _FK_documentoDigitalizadoEmpleadoBinario_id, _binario, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _fechaModificacion, _ultimaAct);

            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        public int M_documentosDigitalizadosEmpleadosBinarios_saveDAL(List<Entity.M_documentosDigitalizadosEmpleadosBinarios> _Datos)
        {
            int _dt = new int();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                int _documentoDigitalizadoEmpleadoBinario_id = 0;
                foreach(Entity.M_documentosDigitalizadosEmpleadosBinarios _d in _Datos)
                {
                    _documentoDigitalizadoEmpleadoBinario_id = _d.documentoDigitalizadoEmpleadoBinario_id;
                    _dt = proc.M_documentosDigitalizadosEmpleadosBinarios_saveDAL(_d.documentoDigitalizadoEmpleadoBinario_id, _d.FK_documentoDigitalizadoEmpleadoBinario_id, _d.binario, _d.FK_usuario_id_captura, _d.fechaCaptura, Convert.ToInt32(_d.FK_usuario_id_modifica), _d.fechaModificacion);
                    _d.documentoDigitalizadoEmpleadoBinario_id = _documentoDigitalizadoEmpleadoBinario_id;
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
