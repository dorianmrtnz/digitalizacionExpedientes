using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace recepciondeDocumentos.publico
{
    public partial class RecepcionDocumentosTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void save()
        {
            mobileBO.Facade _F;

            DateTime thisday = DateTime.Today;

            List<Entity.M_documentosDigitalizadosEmpleados> _lista = new List<Entity.M_documentosDigitalizadosEmpleados>();
            
            Entity.M_documentosDigitalizadosEmpleados _elemento = new Entity.M_documentosDigitalizadosEmpleados()
            {
                documentoDigitalizadoEmpleado_id = 0,
                FK_documento_id = 0,
                nombreArchivo = this.txtNombreArchivo.Text,
                FK_empleado_id = int.Parse(this.txtFKEmpleadoID.Text), 
                FK_usuario_id_captura = int.Parse(this.txtFKUsuarioIdCaptura.Text),
                fechaCaptura = thisday,
                FK_usuario_id_modifica = int.Parse(txtFKUsuarioIDModifica.Text),
                fechaModificacion = thisday,
                ultimaAct = thisday
            };

            _lista.Add(_elemento);
            try
            {
                _F = new mobileBO.Facade();
                _F.M_DocumentosDigitalizadosEmpleados_Save(ref _lista);
            }
            catch (Exception _ex)
            { }

            
        }

        //protected bool Validar()
        //{
        //    bool ret = true;

        //    if (string.IsNullOrEmpty(txtdocumentodigitalizado_id.Text))
        //    {
        //        ret = false;
        //        lbldocdig.Text = "El id es requerido";
        //    }
        //    else
        //    {
        //        lbldocdig.Text = "";
        //    }

        //    if (string.IsNullOrEmpty(txtFKdocumentoID.Text))
        //    {
        //        ret = false;
        //        lblNomarchivo.Text = "El nombre del archivo es requerido";
        //    }
        //    else
        //    {
        //        lblNomarchivo.Text = "";
        //    }

        //    if (string.IsNullOrEmpty(txtNombreArchivo.Text))
        //    {
        //        ret = false;
        //        lblDescripcion.Text = "La descripcion de documento es requerida";
        //    }
        //    else
        //    {
        //        lblDescripcion.Text = "";
        //    }
        //    return ret;
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (Validar())
            //{
            //    save();
            //}

            save();
        }


    }
}