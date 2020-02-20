using System;
using System.Data;

namespace AutoSproc
{
	/// <summary>
	/// Used to specify stored proc param prefix character
	/// </summary>
	[AttributeUsage (AttributeTargets.Interface, AllowMultiple=false)]
	public class ParamPrefixCharAttribute : Attribute {
		private string _paramPrefixChar;

		/// <summary>
		/// 
		/// </summary>
		public string ParamPrefixChar {
			get { return _paramPrefixChar; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="paramPrefixChar"></param>
		public ParamPrefixCharAttribute (string paramPrefixChar) {
			_paramPrefixChar = paramPrefixChar;
		}
	}


	/// <summary>
	/// Attribute to control name of sproc (default uses the method name)
	/// </summary>
	[AttributeUsage (AttributeTargets.Method, AllowMultiple=false)]
	public class SprocNameAttribute : Attribute {
		private string _sprocName;

		/// <summary>
		/// Name of stored procedure to use
		/// </summary>
		public string SprocName {
			get { return _sprocName; }
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="sprocName"></param>
		public SprocNameAttribute (string sprocName) {
			_sprocName = sprocName;
		}
	}

	/// <summary>
	/// Attribute for annotating parameters with DbType
	/// </summary>
	[AttributeUsageAttribute (AttributeTargets.Parameter)] 
	public class DbDataTypeAttribute : Attribute {
		private DbType _DbType;

		/// <summary>
		/// Constructor for attribute 
		/// </summary>
		/// <param name="dbtype"></param>
		public DbDataTypeAttribute (DbType dbtype) {
			this._DbType = dbtype;
		}

		/// <summary>
		/// Read only accessor to annotated value
		/// </summary>
		public DbType ActualDbType  {
			get { return this._DbType; }
		}
	}

	
}
