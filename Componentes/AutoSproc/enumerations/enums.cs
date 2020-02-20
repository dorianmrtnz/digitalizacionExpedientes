using System;

namespace AutoSproc
{
	/// <summary>
	/// Identifier for local variables declared using Reflection.Emit
	/// </summary>
	public enum Locals{ 
		/// <summary>
		/// local0
		/// </summary>
		local0,

		/// <summary>
		/// local1
		/// </summary>
		local1,

		/// <summary>
		/// local2
		/// </summary>
		local2,


		/// <summary>
		/// local3
		/// </summary>
		local3,

		/// <summary>
		/// local4
		/// </summary>
		local4
	}
	/// <summary>
	/// Used to specify which provider will be used
	/// </summary>
	public enum DBProvider {
		/// <summary>
		/// Defines the database provider to be SQL Server
		/// </summary>
		SQLServer,
		/// <summary>
		/// Defines the database provider to be OleDb
		/// </summary>
		OleDb
	}
}
