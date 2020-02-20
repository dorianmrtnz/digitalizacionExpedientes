using System;
using System.IO;
using System.Web.Mail;
using System.Collections;

 
namespace Entity
{
	/// <summary>
	/// clase que se va a utilizar para la
	/// generacion de archivo log de todos los mensajes de excepciones
	/// del servidor de componentes
	/// </summary>
    [Serializable]
	public class LogController 
	{
		private static LogController instance;  // para utilizar el patron singleton		
		private System.Diagnostics.EventLog log;
		private StreamWriter streamLog;
		private String logName;
		private String path=@"c:\logs";

        /// <summary>
		/// Constructor crea el archivo que almacenara el log de los mensajes
		/// </summary>
		public LogController()
		{
			log = new System.Diagnostics.EventLog("Application");
			log.Source="Componentes";			
			try 
			{
				string fecha ;
				if (!Directory.Exists(path)) 
				{
					Directory.CreateDirectory(path);
				}
				fecha = System.DateTime.Now.Day.ToString()+"_"+System.DateTime.Now.Month.ToString()+"_"+System.DateTime.Now.Year.ToString()+"___"+System.DateTime.Now.Hour.ToString()+"_"+System.DateTime.Now.Minute.ToString()+"_"+System.DateTime.Now.Second.ToString();
				logName =path+"\\COMLOG_"+fecha+".txt";				
				streamLog= new StreamWriter(logName,false);
			}
			catch (Exception ex)
			{
				Entity.LogController.Instance().AgregarMensaje(ex.ToString() ,true);
			}
		}
		/// <summary>
		/// Desctructor que manda un email con el archivo del los logs
		/// </summary>
		~LogController()
		{
			
			streamLog.Close();
			SendMail("finalizo en servidor de componentes","",true);
		}

		/// <summary>
		/// Obtiene una instancia unica de Entity.LogController utilizando el patron Singleton
		/// </summary>
		/// <returns>una instancia unica de LogController</returns>
		public static LogController Instance()
		{
			if (instance==null)
			{
				instance=new LogController();
			}
			return instance;
		}

		/// <summary>
		/// Agrega un mensaje al archivo de logs
		/// </summary>
		/// <param name="mensaje">mensaje a agregar el el archivo log</param>
		/// <param name="enviarMail">valor que indica si se va a enviar por email el mensaje, utilizado para los mensajes criticos</param>
		public void AgregarMensaje(string mensaje, bool enviarMail)
		{
			string fechaHora=DateTime.Now.ToString(); 
			
			try
			{
				log.WriteEntry(fechaHora +":  "+mensaje,System.Diagnostics.EventLogEntryType.Warning);
				LogController.instance.streamLog.WriteLine(fechaHora +":  "+mensaje);
				
                //if (enviarMail)
                //{
                SendMail(fechaHora + ":  " + mensaje, "luis_rodriguez_1986@hotmail.com", false);
                //}
			}
			catch (System.Web.HttpException ex)
			{
				Console.WriteLine(ex.Message + " --- No se pudo mandar el Email");
			}
			finally
			{
				    LogController.instance.streamLog.Flush();
			}
		}		
		
		/// <summary>
		/// Agrega un mensaje al archivo de logs
		/// </summary>
		/// <param name="exc">Exception exc</param>
		public void AgregarMensaje(Exception exc)
		{
			string strackTrace="", exceptionTipo="";
			int found, foundTipo;

			try
			{
				foundTipo=exc.ToString().IndexOf(":",1);
				if (exc.StackTrace != null && exc.StackTrace.Length>0)
				{
					found = exc.StackTrace.IndexOf(":line", 1);
					exceptionTipo=exc.ToString().Substring(0,++foundTipo);
					strackTrace=exc.StackTrace.Substring(++found);
				}				
				this.AgregarMensaje(exceptionTipo+"  "+ exc.TargetSite +" - "+strackTrace,true); 
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				LogController.instance.streamLog.Flush();
			}
		}		

		
		/// <summary>
		/// Envia un mensaje por email
		/// </summary>
		/// <param name="mensaje">mensaje a enviar</param>
		/// <param name="attach">indica si se va a mandar el archivo de logs adjunto al email</param>
		public void SendMail(string mensaje,string para,bool attach)
		{
            MailMessage newMessage = new MailMessage();
            newMessage.From = "luis_rodriguez_1986@hotmail.com";
            newMessage.To = para;
            //newMessage.Cc="jelizondo@homex.com.mx";
            newMessage.Subject = "Archivo LOG del Servidor de Componentes " + DateTime.Now.ToString();

            if (attach)
            {

                MailAttachment myAttach =
                    new MailAttachment(logName, (MailEncoding)System.Net.Mime.TransferEncoding.Base64);
                newMessage.Attachments.Add(myAttach);
            }

            newMessage.Body = mensaje;
            newMessage.BodyFormat = MailFormat.Text;

            try
            {
                SmtpMail.Send(newMessage);
            }
            catch (System.Web.HttpException ex)
            {
                Console.WriteLine(ex.Message + " --- Application can't send your EMail - See The Help Content");
            }
		}
		
		/// <summary>
		/// Obtiene un bool en el que true significa que se encontraron la facade y el usuario en el archivo xml
		/// </summary>
		/// <param name="facade">string referente a la facada a buscar</param>
		/// <param name="usuario">string referente a el usuario a buscar</param>
		/// <returns>bool</returns>			
		public bool ChecarFacadeUsuario(string facadeXml, string usuarioXml) //System.Data.DataSet dsp)
		{
			bool encontrado = false;
			System.Data.DataSet ds = new System.Data.DataSet();
			ds.ReadXml(@"c:\ds.xml");
			
			System.Data.DataRow[] renglon = ds.Tables[0].Select("facade='"+facadeXml+"' and usuario='"+usuarioXml+"'");
			if(renglon.Length > 0)
			{
				encontrado = true;
			}
			return encontrado;
		}

	}
}
