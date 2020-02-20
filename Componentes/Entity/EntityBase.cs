using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;
namespace Entity
{
	/// <summary>
	/// clase base para todas las entidades para manejar los estatus
	/// </summary>	
	[Serializable]
	//[RunInstallerAttribute(true)]
	public abstract class IEntityBase
	{
		#region Atributos
		private int ultimaActualizacion;
        //protected Entity.Estatus estatusEntidad;
		#endregion

		#region Constructor Destructor
		/// <summary>
		/// Constructor
		/// </summary>
        //protected IEntityBase()
        //{
        //    this.estatusEntidad = Entity.Estatus.Agregado;
        //}
		#endregion

		#region Propiedades
		/// <summary>
		/// Obtiene o establece un numero hash determinando la ultima vez que se modifico 
		/// </summary>
        //public int UltimaActualizacion
        //{
        //    get{return this.ultimaActualizacion;}
        //    set{this.ultimaActualizacion=value;}
        //}

		/// <summary>
		/// Obtiene o Establece un Entity.Estatus para un IEntityBase
		/// </summary>
        //public Entity.Estatus EntityStatus
        //{
        //    get{return  this.estatusEntidad;}
        //    set{this.estatusEntidad = value;}
        //}

		#endregion
		
		#region Métodos Públicos
		/// <summary>
		/// Acepta los cambios realizados desde la ultima vez que se llamo AcceptChanges
		/// </summary>
        //public void AcceptChanges()
        //{
        //    this.estatusEntidad  = Entity.Estatus.SinCambios ;			
        //}
		/// <summary>
		/// Determina si la entidad tiene cambios
		/// </summary>
		/// <returns>bool</returns>
        //public bool HasChanges()
        //{
        //    return (this.estatusEntidad  == Entity.Estatus.Agregado || this.estatusEntidad  == Entity.Estatus.Modificado);
        //}

		/// <summary>
		/// Actualiza el estatus de la entidad a modificado
		/// </summary>
        public void Set()
        {
         
        }
		/// <summary>
		/// Serializa la entidad a un string en formato Xml
		/// </summary>
		/// <returns><see cref="string"/></returns>
		public string ToXml()
		{
			System.Text.StringBuilder stringBuilder=null;
			try
			{
				System.Xml.Serialization.XmlSerializer serializador = new System.Xml.Serialization.XmlSerializer(this.GetType(),new XmlRootAttribute(this.GetType().ToString()));
				
				stringBuilder = new System.Text.StringBuilder();
				System.IO.StringWriter stringWriter = new System.IO.StringWriter(stringBuilder);
				serializador.Serialize(stringWriter, this);

				stringBuilder.Replace("utf-16", "utf-8");

			}
			catch(Exception ex)
			{			
				Entity.LogController.Instance().AgregarMensaje(ex);
			}
			return stringBuilder.ToString();					 
		}

		#endregion
		
		#region Manejo de Errores Obsoleto
		private bool hasError;
		private string error;
		/// <summary>
		/// Obtiene o establece un valor que indica si la entidad tiene errores
		/// </summary>
		public bool HasError
		{
			get
			{
				return this.hasError;
			}
			set
			{
				this.hasError=value;
			}
		}
		/// <summary>
		/// Obtiene o establece el mensaje de error si lo hubiera
		/// </summary>
		public string Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error=value;
			}
		}
		#endregion

		/// <summary>
		/// Marca como borrados al detalle del maestro
		/// </summary>
		/// <param name="listaElementos">ICollectionBase del detalle a marcar</param>
		public void BorrarDetalle(ICollectionBase listaElementos)
		{
			ArrayList listaBorrados = new ArrayList(listaElementos);
			foreach (IEntityBase elemento in listaBorrados)
			{
				listaElementos.Remove(elemento);
			}
		}
	}
}
