using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace recepciondeDocumentos.publico
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet _ds;
                mobileBO.Facade _f = new mobileBO.Facade();
                _ds = _f.DocumentosSelect();

                this.ddlDocumeto.DataSource = _ds.Tables[0];
                this.ddlDocumeto.DataTextField = "descripcionDocumento";
                this.ddlDocumeto.DataValueField = "documento_id";
                this.ddlDocumeto.DataBind();
            }
        }

        private void Save()
        {
            mobileBO.Facade _F = new mobileBO.Facade();

            //DateTime thisday = DateTime.Today;

            List<Entity.M_documentosDigitalizadosEmpleados> _lista = new List<Entity.M_documentosDigitalizadosEmpleados>();

            Entity.M_documentosDigitalizadosEmpleados _elemento = new Entity.M_documentosDigitalizadosEmpleados()
            {
                documentoDigitalizadoEmpleado_id = int.Parse(this.txtDocDigEmpleadoID.Text),
                FK_documento_id = 4,
                nombreArchivo = this.txtNombreArchivo.Text,
                FK_empleado_id = int.Parse(this.txtFKEmpleadoID.Text),
                FK_usuario_id_captura = int.Parse(this.txtFKUsuarioIDCaptura.Text),
                fechaCaptura = Convert.ToDateTime("2019-07-18 00:00:00.000"),
                FK_usuario_id_modifica = int.Parse(txtFKUsuarioIDModifica.Text),
                fechaModificacion = Convert.ToDateTime("2019-07-18 00:00:00.000"),
                ultimaAct = 1   
            };
            
            


            _lista.Add(_elemento);
            //_F = new mobileBO.Facade();
            _F.M_DocumentosDigitalizadosEmpleados_Save(ref _lista);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void ddlDocumeto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}