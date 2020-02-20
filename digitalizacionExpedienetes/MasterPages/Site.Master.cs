using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BECAS
{
    public partial class SiteMaster : MasterPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack && Session["alumno"] == null)
            //    FormsAuthentication.SignOut();
        }

        protected void validacionesMenu()
        {
            //List<object> L = new List<object>();
            ////Agregue la siguiente Línea para añadir un link al menú dinamico según su validació
            ////L.Add(new {ruta = "ruta a la que te llevara liga", text="Texto que dirá en el menú"});
            //try
            //{
            //    #region Modificacion de Solicitud e Impresion Solicitud
            //    if (new mobileBO.Facade().BECAS_C_fechaSolicitud_Select(((Entity.CE_C_ciclosEscolaresEntity)Session["cicloEscolar"]).cicloEscolar_id).Count > 0)
            //    {
            //        L.Add(new { ruta = ResolveUrl("~/Consulta/ReimpresionSolicitud/"), texto = "IMPRIMIR SOLICITUD" });
            //        L.Add(new { ruta = ResolveUrl("~/Solicitud/modificacionSolicitud/"), texto = "MODIFICAR SOLICITUD" });
            //    }
            //    #endregion

            //    #region Resultados Solicitud
            //    if(new mobileBO.Facade().C_CalendarioPagos_Select(DateTime.Now).Count > 0)
            //    {
            //        L.Add(new { ruta = ResolveUrl("~/Solicitud/consultaResultados/"), texto = "CONSULTA RESULTADOS" });
            //    }
            //    #endregion



            //    //Se hace el Bind para el Repeater que carga los Links.
            //    if (L.Count > 0)
            //    {
            //        this.rpt1.DataSource = L;
            //        this.DataBind();
            //    }
            //}
            //catch
            //{
            //    throw;
            //}
        }

    }
}