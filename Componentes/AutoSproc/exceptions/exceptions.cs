using System;

namespace AutoSproc
{
	/// <summary>
	/// Thrown when the parameter prefix character is not specified on the
	/// interface
	/// </summary>
	public class ParamPrefixCharException : ApplicationException { 
		/// <summary>
		/// Default constructor
		/// </summary>
		public ParamPrefixCharException () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public ParamPrefixCharException (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner exception</param>
		public ParamPrefixCharException (string msg, Exception inner) : base (msg, inner) { }
	
	}


	/// <summary>
	/// Thrown when unsupported database type is detected
	/// </summary>
	public class DatabaseNotSupportedException : ApplicationException {
		/// <summary>
		/// Default constructor
		/// </summary>
		public DatabaseNotSupportedException () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public DatabaseNotSupportedException (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner exception</param>
		public DatabaseNotSupportedException (string msg, Exception inner) : base (msg, inner) { }
	
	}

	/// <summary>
	/// Thrown when AutoClose set to true and IDataReader return type
	/// detected
	/// </summary>
	public class IllegalAutoCloseSettingException : ApplicationException {
		/// <summary>
		/// Default constructor
		/// </summary>
		public IllegalAutoCloseSettingException () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public IllegalAutoCloseSettingException (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner exception</param>
		public IllegalAutoCloseSettingException (string msg, Exception inner) : base (msg, inner) { }
	
	}
	/// <summary>
	/// Exception class thrown when an unsupported reference type
	/// parameter is detected
	/// </summary>
	public class UnsupportedReferenceParameterType : ApplicationException {
		/// <summary>
		/// Default constructor
		/// </summary>
		public UnsupportedReferenceParameterType () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public UnsupportedReferenceParameterType (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner exception</param>
		public UnsupportedReferenceParameterType (string msg, Exception inner) : base (msg, inner) { }
	
	}

	/// <summary>
	/// Exception class generated when stored procedure method signature type is illegal
	/// </summary>
	public class UnsupportedStoredProcedureReturnTypeException : ApplicationException {

		/// <summary>
		/// Default constructor
		/// </summary>
		public UnsupportedStoredProcedureReturnTypeException () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public UnsupportedStoredProcedureReturnTypeException (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner exception</param>
		public UnsupportedStoredProcedureReturnTypeException (string msg, Exception inner) : base (msg, inner) { }
	}

	/// <summary>
	/// Generated when stored procedure interface (as defined by the class library consumer) fails to inherit from ISprocBase interface
	/// </summary>
	public class IllegalBaseInterfaceException : ApplicationException {
		/// <summary>
		/// Default constructor
		/// </summary>
		public IllegalBaseInterfaceException () {}

		/// <summary>
		/// Constructor with message
		/// </summary>
		/// <param name="msg">Message to display</param>
		public IllegalBaseInterfaceException (string msg) : base (msg) {}

		/// <summary>
		/// Constructor with message and inner exception
		/// </summary>
		/// <param name="msg">Message to display</param>
		/// <param name="inner">Inner Exception</param>
		public IllegalBaseInterfaceException (string msg, Exception inner) : base (msg, inner) { }
	}

}
