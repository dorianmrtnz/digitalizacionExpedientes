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
    public interface IEmpleadosDAL : ISprocBase
    {
        DataSet empleados_select();
        int Empleados_save(int empleado_id, string nombres, string apellidos, string CURP, string RFC, string correo, string telefono, byte[] imagen_perfil);

    }

    public class EmpleadosDAL
    {
        private IEmpleadosDAL proc;
        private SqlConnection conn = null;
        FabricaConexiones fabricaConexiones;
        ~EmpleadosDAL()
        {
            try
            {
                if (conn != null)
                    conn.Dispose();
            }
            catch { }
        }


        public EmpleadosDAL()
        {
            try
            {
                fabricaConexiones = new FabricaConexiones();
                proc = (IEmpleadosDAL)SprocFactory.CreateInstance(typeof(IEmpleadosDAL), DBProvider.SQLServer);
            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
        }


        public DataSet Empleados_selectDAL()
        {
            DataSet _dt = new DataSet();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();


                _dt = proc.empleados_select();

            }
            catch (Exception ex)
            {
                Entity.LogController.Instance().AgregarMensaje(ex);
                throw;
            }
            return _dt;
        }

        //public DataSet Empleados_selectDAL(int _empleado_id, string _nombres, string _apellidos, string _CURP, string _RFC, string _correo, string _telefono, int _FK_documento_id)
        //{
        //    DataSet _dt = new DataSet();
        //    try
        //    {
        //        proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

        //        //foreach
        //        _dt = proc.Empleados_selectDAL(_empleado_id, _nombres, _apellidos, _CURP, _RFC, _correo, _telefono, _FK_documento_id);

        //    }
        //    catch (Exception ex)
        //    {
        //        Entity.LogController.Instance().AgregarMensaje(ex);
        //        throw;
        //    }
        //    return _dt;
        //}

        public int Empleados_saveDAL(List<Entity.Empleados> _Datos)
        {
            int _dt = new int();
            try
            {
                proc.Connection = (SqlConnection)fabricaConexiones.GeneraConexionRecepcionDocumentos();

                int _empleado_id = 0;
                foreach (Entity.Empleados _d in _Datos)
                {
                    _empleado_id = _d.empleado_id;
                    _dt = proc.Empleados_save(_d.empleado_id, _d.nombres, _d.apellidos, _d.CURP, _d.RFC, _d.correo, _d.telefono, _d.imagen_perfil);
                    _d.empleado_id = _empleado_id;
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
