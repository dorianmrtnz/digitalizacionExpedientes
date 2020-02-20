using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//ORIGINAL
namespace recepciondeDocumentos.publico
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void BtnEnviar_Click(object sender, EventArgs e)
        {

            if (txtRFC.Text == "" && txtNombres.Text == "" && txtApellidos.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "MiscriptValidacion", "alert('Ingrese sus datos en cualquiera de los campos');", true);
                txtNombres.Focus();

                GridView1.Visible = false;
                return;
            }
            else
            {
                cargarGrid();
            }
            //GridView1.Columns["empleado_id"].Visible = false;
            //GridView1.Columns[1].Visible = false;
            //GridView1.Columns[5].Visible = false;
            //GridView1.Columns[6].Visible = false;
            //GridView1.Columns[7].Visible = false;
            btn_Buscar.Attributes.Add("onclick", "ocultaracceso()");

            foreach (DataControlField col in GridView1.Columns)
            {
                if (col.HeaderText == "empleado_id")
                {
                    col.Visible = false;
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack && txtRFC.Text == "" && txtApellidos.Text == "" && txtNombres.Text == "")
            {
                GridView1.Visible = false;
            }
            else
            {
                GridView1.Visible = true;
            }
            //cargarGrid();

            if (!IsPostBack)
            {
                ListItem IdentListItem = new ListItem("Identificacion", "1");
                ListItem ComprobanteListItem = new ListItem("Comprobante De Domicilio", "2");
                ListItem CurpListItem = new ListItem("Curp", "3");

                ddlDocumento.Items.Add(IdentListItem);
                ddlDocumento.Items.Add(ComprobanteListItem);
                ddlDocumento.Items.Add(CurpListItem);
            }
        }

        private void cargarGrid()
        {
            string where = "";

            mobileBO.Facade _F;
            _F = new mobileBO.Facade();

            DataSet ds = new DataSet();
            string con = ConfigurationManager.ConnectionStrings["recepcionDocumentos"].ConnectionString;


            if (txtNombres.Text != "" && txtApellidos.Text != "" && txtRFC.Text != "")
            {

                where = " where nombres = '" + txtNombres.Text + "' and apellidos='" + txtApellidos.Text + "' and RFC='" + txtRFC.Text + "'";
            }
            else if (txtNombres.Text != "" && txtApellidos.Text == "" && txtRFC.Text == "")
            {
                where = "where nombres = '" + txtNombres.Text + "'";
            }
            else if (txtApellidos.Text != "" && txtNombres.Text == "" && txtRFC.Text == "")
            {
                where = "where apellidos = '" + txtApellidos.Text + "'";
            }
            else if (txtRFC.Text != "" && txtNombres.Text == "" && txtApellidos.Text == "")
            {
                where = "where RFC = '" + txtRFC.Text + "'";
            }
            else if (txtNombres.Text != "" && txtApellidos.Text != "" && txtRFC.Text == "")
            {
                where = "where nombres = '" + txtNombres.Text + "' and apellidos = '" + txtApellidos.Text + "'";
            }
            else if (txtNombres.Text == "" && txtApellidos.Text != "" && txtRFC.Text != "")
            {
                where = "where apellidos = '" + txtApellidos.Text + "' and RFC = '" + txtRFC.Text + "'";
            }
            else if (txtNombres.Text != "" && txtApellidos.Text == "" && txtRFC.Text != "")
            {
                where = "where nombres = '" + txtNombres.Text + "' and RFC = '" + txtRFC.Text + "'";
            }


            string qry = "select * from Empleados " + where;
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet dt = new DataSet();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataSourceID = null;
            GridView1.DataBind();

            //GridView1.Columns[1].Visible = false;
            //GridView1.Columns[5].Visible = false;
            //GridView1.Columns[6].Visible = false;
            //GridView1.Columns[7].Visible = false;
        }

        //public DataSet ObtenerDatos(string Nombre, string Apellido, string RFC)
        //{
        //    using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["recepcionDocumentos"].ToString()))
        //    {
        //        //var SqlCommand; 
        //        SqlCommand comm = new SqlCommand();
        //        comm.Connection = conexion;
        //        comm.CommandType = CommandType.StoredProcedure;

        //        //cmd.CommandType = CommandType.StoredProcedure;
        //        comm.Parameters.AddWithValue("@Param", Nombre);

        //        SqlDataAdapter da = new SqlDataAdapter(); /* TODO ERROR: Skipped SkippedTokensTrivia *//* TODO ERROR: Skipped SkippedTokensTrivia */

        //        DataSet ds = new DataSet();

        //        da.Fill(ds);

        //        return ds;
        //    };
        //}

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            empleadoInfo.Style.Add("display", "block");
            //GridView1.Style.Add("dislay","none");

            GridView1.Visible = false;
            GridView1.Attributes.Add("onclick", "mostrar_empleadoInfo()");

            lbl_idempleado.Text = GridView1.SelectedDataKey.Values[0].ToString();
            lbl_nombre.Text = GridView1.SelectedDataKey.Values[1].ToString();
            lbl_apellidos.Text = GridView1.SelectedDataKey.Values[2].ToString();
            lbl_CURP.Text = GridView1.SelectedDataKey.Values[3].ToString();
            lbl_RFC.Text = GridView1.SelectedDataKey.Values[4].ToString();
            lbl_Correo.Text = GridView1.SelectedDataKey.Values[5].ToString();
            lbl_telefono.Text = GridView1.SelectedDataKey.Values[6].ToString();
            lbl_domicilio.Text = GridView1.SelectedDataKey.Values[7].ToString();
       
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='silver';this.style.cursor='pointer';");
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);//en una solo línea               
                e.Row.ToolTip = "Haga click para seleccionar la fila";
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            //if (FileUpload1.HasFile)
            //{
            //    string fullPath = Path.Combine(Server.MapPath("~/EmpleadosFotos"), FileUpload1.FileName);
            //    FileUpload1.SaveAs(fullPath);
            //}

            //StarUpLoad();

            GuardarImagenPerfil();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevaConsulta_Click(object sender, EventArgs e)
        {
            GridView1.Visible = false;
            empleadoInfo.Style.Add("display", "none");
        }

        private void StartUpLoad()
        {
            int longitud = 0;
            //int guardar = 0;
            string _extension = System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
            if (FileUpload2.PostedFile != null && FileUpload2.HasFile)
            {
                if (_extension == ".jpg" || _extension == ".png" || _extension == ".gif" || _extension == ".bmp" || _extension == ".jpeg" || _extension == ".pdf")
                {
                    //Entity.M_actaConstitutivaDigitalizacionEntity _E = new Entity.M_actaConstitutivaDigitalizacionEntity();
                    Entity.M_documentosDigitalizadosEmpleados _E = new Entity.M_documentosDigitalizadosEmpleados();
                    _E.documento = new Entity.M_documentosDigitalizadosEmpleadosBinarios();
                    _E.documento.documentoDigitalizadoEmpleadoBinario_id = 0;
                    _E.documento.binario = new byte[FileUpload2.PostedFile.ContentLength];
                    _E.documento.FK_usuario_id_captura = 0;
                    _E.documento.fechaCaptura = DateTime.Now;
                    _E.documento.ultimaAct = 0;
                    longitud = FileUpload2.PostedFile.ContentLength;

                    //_E.documento = new Entity.M_actaConstitutivaDigitalizacionDocumentoEntity();
                    _E.documento.binario = new byte[FileUpload2.PostedFile.ContentLength];
                    //_E.longitud = FileUpload1.PostedFile.ContentLength;
                    FileUpload2.PostedFile.InputStream.Read(_E.documento.binario, 0, longitud);
                    //_E.extensionArchivo = FileUpload1.PostedFile.ContentType;
                    _E.nombreArchivo = FileUpload2.FileName.ToString().ToLower();

                    ////_E.FK_documento_id = int.Parse(this.ddlDocumento.SelectedValue);   // 1 -- MODIFICADO VIKTOR
                    ////_E.FK_empleado_id = int.Parse(hdempleado_id.Value);
                   
                    //_E.Expediente = string.Empty;
                    //_E.Ubicacion = string.Empty;
                    _E.FK_usuario_id_captura = 0;
                    _E.documentoDigitalizadoEmpleado_id = 0;
                    _E.fechaCaptura = DateTime.Now;
                    _E.ultimaAct = 0;
                    //_E.
                    //_E.FK_actaConstitutiva_id = int.Parse(this.hfActaConstitutiva.Value);

                    //try
                    //{
                    //    //guardar = _F.H_documentoEmpleados_Save(ref _E);
                    //    if (guardar > 0)
                    //    {
                    //        ////Mensaje("Archivo Guardado");
                    //        ////mostrarDocumentos(int.Parse(this.hdempleado_id.Value));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    //Mensaje(ex.Message);
                    //}
                }
                else
                {
                    //Mensaje("El archivo seleccionado no es válido.");
                }
            }
        }

        private void GuardarImagenPerfil()
        {
            int longitud = 0;
            //int guardar = 0;
            
            string _extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            if (FileUpload1.PostedFile != null && FileUpload1.HasFile)
            {
                if (_extension == ".jpg" || _extension == ".png" || _extension == ".jpeg" )
                {
                    //Entity.M_actaConstitutivaDigitalizacionEntity _E = new Entity.M_actaConstitutivaDigitalizacionEntity();

                    Entity.Empleados _E = new Entity.Empleados();

                    //_E.documento = new Entity.Empleados();
                    //_E.documento.documentoDigitalizadoEmpleadoBinario_id = 0;
                    //_E.documento.binario = new byte[FileUpload1.PostedFile.ContentLength];
                    //_E.documento.FK_usuario_id_captura = 0;
                    //_E.documento.fechaCaptura = DateTime.Now;
                    //_E.documento.ultimaAct = 0;

                    longitud = FileUpload1.PostedFile.ContentLength;

                    //_E.documento = new Entity.M_actaConstitutivaDigitalizacionDocumentoEntity();

                    _E.imagen_perfil = new byte[FileUpload1.PostedFile.ContentLength];

                    //_E.longitud = FileUpload1.PostedFile.ContentLength;

                    FileUpload1.PostedFile.InputStream.Read(_E.imagen_perfil, 0, longitud);

                    //_E.extensionArchivo = FileUpload1.PostedFile.ContentType;
                    //_E.nombreArchivo = FileUpload1.FileName.ToString().ToLower();
                    ////_E.FK_documento_id = int.Parse(this.ddlDocumento.SelectedValue);   // 1 -- MODIFICADO VIKTOR
                    ////_E.FK_empleado_id = int.Parse(hdempleado_id.Value);
                    //_E.Expediente = string.Empty;
                    //_E.Ubicacion = string.Empty;
                    //_E.FK_usuario_id_captura = 0;
                    //_E.documentoDigitalizadoEmpleado_id = 0;
                    //_E.fechaCaptura = DateTime.Now;
                    //_E.ultimaAct = 0;
                    //_E.
                    //_E.FK_actaConstitutiva_id = int.Parse(this.hfActaConstitutiva.Value);

                    //try
                    //{
                    //    guardar = _F.Empleados_saveDAL(ref _E);
                    //    if (guardar > 0)
                    //    {
                    //        Mensaje("Archivo Guardado");
                    //        mostrarDocumentos(int.Parse(this.hdempleado_id.Value));
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Mensaje(ex.Message);
                    //}
                }
                else
                {
                    //Mensaje("El archivo seleccionado no es válido.");
                }
            }
        }

    }
}
