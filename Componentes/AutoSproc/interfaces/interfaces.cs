using System;
using System.Data;

namespace AutoSproc
{
	/// <summary>
	/// Base interface provides connection definition; stored procedure interface must derive from this interface
	/// </summary>
	public interface ISprocBase {
		/// <summary>
		/// Database connection managed by emitted class
		/// </summary>
		IDbConnection Connection { get; set; } 
		/// <summary>
		/// Feature that will automatically close the database connection for you
		/// </summary>
		bool AutoClose { get; set; }
		/// <summary>
		/// Allows one to configure the transaction
		/// </summary>
		IDbTransaction Transaction { get; set; }
	}
}
