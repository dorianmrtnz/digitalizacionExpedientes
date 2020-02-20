using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web.UI;
using System.IO;


namespace mobileBO
{
    /// <summary>
    /// Clase Fachada para manejar todos los metodos expuestos para el sistema 
    /// </summary>
    public class Facade 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public int M_DocumentosDigitalizadosEmpleados_Save(ref List<Entity.M_documentosDigitalizadosEmpleados> _Datos)
        {
            return new M_documentosDigitalizadosEmpleadosBO().M_DocumentosDigitalizadosEmpleados_SaveBO(ref _Datos);    
        }

        //public DataSet EmpleadosSelectBo()
        //{
        //    throw new NotImplementedException();
        //}

        public DataTable M_documentosDigitalizadosEmpleados_selectBo(ref int _documentoDigitalizadoEmpleado_id, int _FK_documento_id, string _nombreArchivo, int _FK_empleado_id, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _fechaModificacion, int _ultimaAct)
        {
            return new M_documentosDigitalizadosEmpleadosBO().M_documentosDigitalizadosEmpleados_selectBo(ref _documentoDigitalizadoEmpleado_id, _FK_documento_id, _nombreArchivo, _FK_empleado_id, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _fechaModificacion, _ultimaAct);
        }


        //maybe
        public int DocumentosSave(ref List<Entity.C_documentos> _Datos)
        {
            return new C_DocumentosBO()._DocumentosSaveBo(_Datos);
        }

        public DataSet DocumentosSelect()
        {
            return new mobileBO.C_DocumentosBO().DocumentosSelectBo();
        }


        //maybe
        public int M_DocumentosDigitalizadosBinarios_Save(List<Entity.M_documentosDigitalizadosEmpleadosBinarios> _Datos)
        {
            return new M_documentosDigitalizadosEmpleadosBinariosBO().M_documentosDigitalizadosEmpleadosBinarios_saveBO(_Datos);
        }

        public DataTable M_DocumentosDigitalizadosBinarios_SelectBO(ref int _documentoDigitalizadoEmpleadoBinario_id, int _FK_documentoDigitalizadoEmpleadoBinario_id, int _binario, int _FK_usuario_id_captura, DateTime _fechaCaptura, int _FK_usuario_id_modifica, DateTime _FechaModificacion, int _ultimaAct)
        {
            return new mobileBO.M_documentosDigitalizadosEmpleadosBinariosBO().M_documentosDigitalizadosEmpleadosBinarios_selectBO(ref _documentoDigitalizadoEmpleadoBinario_id, _FK_documentoDigitalizadoEmpleadoBinario_id, _binario, _FK_usuario_id_captura, _fechaCaptura, _FK_usuario_id_modifica, _FechaModificacion, _ultimaAct);
        }

        //
        public int EmpleadosSave(ref List<Entity.Empleados> _Datos)
        {
            return new EmpleadosBO()._EmpleadosSaveBo(_Datos);
        }

        public DataSet EmpleadosSelect()
        {
            return new mobileBO.EmpleadosBO().EmpleadosSelectBo();
        }


        #region utilerias
        public byte[] utileriasResizeImage(Stream streamImagen)
        {
            return new mobileBO.Utilerias().utileriasResizeImage(streamImagen);
        }

        public bool Utilerias_DataSetValido(DataSet DS)
        {
            return new mobileBO.Utilerias().Utilerias_DataSetValido(DS);
        }
        public decimal Utilerias_Truncate(decimal numero, int digitos)
        {
            return new mobileBO.Utilerias().Utilerias_Truncate(numero, digitos);
        }
        public string Utilerias_GenerarCadenaSeparadaPorPipes(DataSet DS, string Campo, int Tabla)
        {
            return new mobileBO.Utilerias().GenerarCadenaSeparadaPorPipes(DS, Campo, Tabla);
        }

        public bool Utilerias_EmailValido(string strIn)
        {
            return new mobileBO.Utilerias().Utilerias_EmailValido(strIn);
        }

        public string Utilerias_generarStringXml(DataSet xml)
        {
            return new mobileBO.Utilerias().Utilerias_generarStringXml(xml);
        }

        public string Utileria_convierteFechaQuincena(DateTime _fecha)
        {
            return new Utilerias().Utileria_convierteFechaQuincenaBO(_fecha);
        }

        public void Utilerias_ExportExcel(Page pPagina, DataSet dataSet)
        {
            new mobileBO.Utilerias().Utilerias_ExportExcel(pPagina, dataSet);
        }
        public void Utilerias_ExportExcel_2(Page pPagina, DataSet dataSet, string nombreArchivo)
        {
            new mobileBO.Utilerias().Utilerias_ExportExcel_2(pPagina, dataSet, nombreArchivo);
        }
        public void Utilerias_Mensaje(Page pPagina, string sAviso)
        {
            new mobileBO.Utilerias().Utilerias_Mensaje(pPagina, sAviso);
        }
        #endregion
    }
}