using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Security;

namespace SIEE
{
    //BLABLA x22
    public class Global : System.Web.HttpApplication
    {

        static Entity.conexionesEntity _conexionesEntity;
        void Application_Start(object sender, EventArgs e)
        {

            LlenaConexion();
        }

        public void LlenaConexion()
        {
            _conexionesEntity = new Entity.conexionesEntity();
           


            _conexionesEntity.recepcionDocumentos = System.Configuration.ConfigurationManager.ConnectionStrings["recepcionDocumentos"].ToString();

            Application["conexiones"] = _conexionesEntity;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
       
            //if ((int)Application["sesionesAbiertas"])
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
      
            //Application["sesionesAbiertas"] = (int)Application["sesionesAbiertas"] - 1;
        }

    }
}
