using System;
using System.Xml;
using System.Web.Configuration;
namespace Entity
{
    /// <summary>
    /// Controla los datos de los parametros del archivo parametros.xml, como la conexion a la base de datos, directorios de DBFs, etc.
    /// </summary>
    [Serializable]
    public class Parametros
    {

        private volatile static Parametros instance = null; //para la utilizacion del patrón Singleton

        /// <summary>
        /// Obtiene o establece un string de conexion para la base de datos para un Entity.Parametros
        /// </summary>
        /// 
        public string connectionRecepcionDocumentos
        {
            get; set;
        }

        public string connectionSIEE
        {
            get; set;
        }

        /// <summary>
        /// Obtiene un objeto Entity.Parametros.
        /// </summary>
        /// <returns>la instancia unica de Entity.Parametros utilizando el patron Singleton de gamma</returns>
        public static Parametros GetInstance()
        {
            if (instance == null)
            {
                lock (typeof(Parametros))
                {
                    if (instance == null)
                    {
                        instance = new Parametros();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// Recarga los valores del archivo parametros.xml sobre los atributos de la clase
        /// </summary>
        public void Recargar()
        {
            
            if (System.Web.HttpContext.Current.Application["conexiones"] != null)
            {
                Entity.conexionesEntity conexion = (Entity.conexionesEntity)System.Web.HttpContext.Current.Application["conexiones"];
                this.connectionRecepcionDocumentos = conexion.recepcionDocumentos;
                this.connectionSIEE = conexion.connectionSIEE;
            }	
        }

        public Parametros()
        {
            Recargar();
        }      
    }
}
