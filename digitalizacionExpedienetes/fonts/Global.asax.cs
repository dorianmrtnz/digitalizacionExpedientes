using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace recepciondeDocumentos
{
    public class Global : System.Web.HttpApplication
    {
        static Entity.conexionesEntity _conexionesEntity;
        public void LlenaConexion()
        {
            _conexionesEntity = new Entity.conexionesEntity();
            _conexionesEntity.recepcionDocumentos = System.Configuration.ConfigurationManager.ConnectionStrings["recepcionDocumentos"].ToString();
            _conexionesEntity.connectionSIEE = System.Configuration.ConfigurationManager.ConnectionStrings["conexionSIEE"].ToString();
            Application["conexiones"] = _conexionesEntity;
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            LlenaConexion();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}