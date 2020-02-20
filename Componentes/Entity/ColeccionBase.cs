using System;
using System.Xml;
using System.Xml.Serialization;

namespace Entity
{
	/// <summary>
	/// clase base de colecciones para todas las colecciones
	/// </summary>
	/// 
	[Serializable]
	public abstract class ICollectionBase : System.Collections.CollectionBase 
	{
		ICollectionBase listaBorrados;

		protected ICollectionBase()
		{
			//listaBorrados = (ICollectionBase)Activator.CreateInstance(this.GetType());
		}

		/// <summary>
		/// Agrega un elemento IEntityBase a la colección
		/// </summary>
		/// <param name="entityBase">cualquier clase heredada del IEntityBase</param>
		/// <returns>int</returns>
		public int Add(IEntityBase entityBase)
		{		  
			return List.Add(entityBase);
		}
        
		/// <summary>
		/// Quita el objeto IEntityBase con el objeto especificado
		/// </summary>
		/// <param name="entityBase">objeto IEntityBase a quitar</param>
		public virtual void Remove(IEntityBase entityBase)
		{
			if (listaBorrados == null)
			{
				listaBorrados = (ICollectionBase)Activator.CreateInstance(this.GetType());
			}
            //entityBase.EntityStatus = Entity.Estatus.Borrado;
			this.listaBorrados.Add(entityBase);
			List.Remove(entityBase);
		}

		/// <summary>
		/// Quita el objeto IEntityBase con el objeto especificado
		/// </summary>
		/// <param name="entityBase">objeto IEntityBase a quitar</param>
		public void RemoveElementOfList(IEntityBase entityBase)
		{
			List.Remove(entityBase);
		}

		/// <summary>
		/// Inserta un objeto IEntityBase en la posicion especificada
		/// </summary>
		/// <param name="index">pocicion de la colección donde se va a insertar el objeto</param>
		/// <param name="entityBase">cualquier clase heredada del IEntityBase</param>
		public void Insert(int index, IEntityBase entityBase)
		{
			List.Insert(index, entityBase);
		}
		
		/// <summary>
		/// Determina si la colección contiene una clase IEntityBase
		/// </summary>
		/// <param name="entityBase">cualquier clase heredada del IEntityBase</param>
		/// <returns>bool</returns>
		public bool Contains(IEntityBase entityBase)
		{
			return List.Contains(entityBase);
		}

		/// <summary>
		/// Obtiene el objeto listaBorrados de la colección
		/// </summary>
		/// <returns>IColectionBase</returns>
		public ICollectionBase GetDeleted()
		{
			if (listaBorrados == null)
			{
				listaBorrados = (ICollectionBase)Activator.CreateInstance(this.GetType());
			}
			return listaBorrados;
		}
		/// <summary>
		/// Confirma todos los cambios realizados en la colección desde la ultima vez que se llamo AcceptChanges
		/// </summary>
		public void AcceptChanges()
		{
            //foreach (Entity.IEntityBase elemento in this.List)
            //{
            //    elemento.AcceptChanges();			
            //}
			if (this.listaBorrados != null)
			{
				this.listaBorrados.Clear();
			}
		}

		/// <summary>
		/// Obtiene un valor que indica si hay elementos eliminados dentro de la colección
		/// </summary>
		/// <returns>Regresa verdadero si existen elementos eliminados o de lo contrario falso</returns>
		public bool HasDeleted()
		{
			return (this.listaBorrados != null && this.listaBorrados.Count > 0);
		}

		/// <summary>
		/// Convierte la clase a un XML
		/// </summary>
		/// <returns>Cadena XML que representa a la clase</returns>
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
                //Entity.LogController.Instance().AgregarMensaje(ex);
			}
			return stringBuilder.ToString();					 
		}
		
		
		#region Manejo de Errores Obsoleto
		private bool hasError=false;
		private string error="";
		/// <summary>
		/// Obtiene o establece un valor booleano que indica si el objeto tiene errores
		/// </summary>
		/// 
		[XmlAttribute(DataType = "bool", AttributeName = "HasError")]
		public bool HasError
		{
			get {return hasError;}
			set {hasError=value;}
		}
		/// <summary>
		/// Obtiene o establece el mensaje de error actual si lo hubiera
		/// </summary>
		/// 
		[XmlAttribute(DataType = "string", AttributeName = "Error")]
		public string Error
		{
			get {return error;}
			set {error=value;}
		}
		#endregion

		/// <summary>
		/// Realiza una copia de la colleccion hacia un arreglo
		/// </summary>
		/// <param name="array">IEntityBase arreglo donde se dejara la informacion</param>
		/// <param name="index">indice a partir de donde se va a copiar</param>
		public void CopyTo(IEntityBase[] array, int index)
		{			
			this.InnerList.CopyTo(array,index);
		}

		/// <summary>
		/// Obtiene el indice del elemento especificado
		/// </summary>
		/// <param name="entidad">entidad a buscar</param>
		/// <returns>int</returns>
		public int IndexOf(IEntityBase entidad)
		{
			return this.InnerList.IndexOf(entidad);
		}
	}
}
