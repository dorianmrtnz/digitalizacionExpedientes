/*
* Copyright Stephen John Rodgers, 2003
* This software is provided as is and has not been fully tested to 
anticipate
* all possible uses. If you elect to include this software as a part of your
* software product then you do so at your own risk - the author accepts no 
responsibility
* for any damages caused from its use
*/


using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;

namespace AutoSproc {

	/// <summary>
	/// Base implementation class for the generated class to derive from
	/// Provides implementation of Transaction / Connection and AutoClose properties
	/// </summary>
	/// 

	[Serializable]
	public class SprocBase : ISprocBase {
		private IDbConnection _connection;
		/// <summary>
		/// Cached parameters
		/// </summary>
		protected Hashtable _htParams = Hashtable.Synchronized (new Hashtable ());
		/// <summary>
		/// Allows one to configure the underlying database connection
		/// </summary>
		public IDbConnection Connection {
			get { return _connection; }
			set { _connection = value; }
		}

		private IDbTransaction _transaction;
		/// <summary>
		/// Allows one to configure the underlying transaction
		/// </summary>
		public IDbTransaction Transaction {
			get { return _transaction; }
			set { _transaction = value; }
		}

		private bool _autoClose = true;
		/// <summary>
		/// Allows one to specify auto closure of the database connection
		/// after method completion
		/// </summary>
		public bool AutoClose {
			get { return _autoClose; }
			set { _autoClose = value; }
		}

	}

	/// <summary>
	/// Stored procedure factory class
	/// </summary>
	/// <remarks>Exposes methods to fabricate the emitted class</remarks>
	public class SprocFactory {
		private static Hashtable _previousImpls = Hashtable.Synchronized (new 
Hashtable ());
		private static ModuleBuilder _modBuilder;
		private static AssemblyBuilder _asmBuilder;
		private static string _fileName;
		private static string _assemblyName = "AutoSprocDynamicAsm";

		/// <summary>
		///
		/// </summary>
		public static string AssemblyName {
			set { _assemblyName = value; }
			get { return _assemblyName; }
		}

		/// <summary>
		/// Internal use for debugging parameters
		/// </summary>
		[System.Diagnostics.Conditional("DEBUG")]
		private static void DebugParams(IDataParameter [] al) {
			System.Diagnostics.Trace.WriteLine ("Parameter Dump");
			foreach (IDataParameter dp in al) {
				IDbDataParameter dpa = (IDbDataParameter) dp;
				string debugInfo = string.Format ("Param Name:{0}, Param Value:{1}, Param Size:{2}", dp.ParameterName, dp.Value, dpa.Size);
				System.Diagnostics.Trace.WriteLine (debugInfo);
			}

		}

		/// <summary>
		///
		/// </summary>
		/// <param name="prms"></param>
		/// <returns></returns>
		public static IDataParameter [] DeepCopyParams (IDataParameter [] prms) {
			IDataParameter [] dupList = (IDataParameter []) prms.Clone ();

			return dupList;
		}



		/// <summary>
		/// Initialises the data connection, opening it if need be
		/// </summary>
		/// <param name="sprocbase"></param>
		/// <param name="autoClose"></param>
		/// <returns></returns>
		private static IDbConnection InitConnection (ISprocBase sprocbase, out bool autoClose) {
			IDbConnection conn = sprocbase.Connection;

			if (conn == null) //they forgot to pass a valid connection through
				throw new ArgumentNullException ("Unknown", "You must set the Connection property prior to making a stored procedure call");

			autoClose = false;

			lock(conn)
			{
				if (conn.State != ConnectionState.Open) 
				{
					conn.Open (); //hehe by hook or by crook ;-)
                    //conn.BeginTransaction();
					autoClose = true;
				}
			}
			return conn;
		}


        private static IDbConnection InitConnection2(ISprocBase sprocbase, out bool autoClose)
        {
            IDbConnection conn = sprocbase.Connection;

            if (conn == null) //they forgot to pass a valid connection through
                throw new ArgumentNullException("Unknown", "You must set the Connection property prior to making a stored procedure call");

            autoClose = false;

            lock (conn)
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open(); //hehe by hook or by crook ;-)
                    //conn.BeginTransaction();
                    autoClose = true;
                }
            }
            return conn;
        }

		/// <summary>
		/// Constructs the command object and populates it with the parameters
		/// </summary>
		/// <param name="conn">The database connection</param>
		/// <param name="spParams">The parameters to initialise the command object with</param>
		/// <param name="paramCount">The number of parameters</param>
		/// <param name="spName">The name of the stored procedure to invoke</param>
		/// <returns>The fabricated command object</returns>
		private static IDbCommand BuildCommandWithParams (IDbConnection conn, IDataParameter [] spParams,
															int paramCount, string spName) {
			
				IDbCommand cmd = conn.CreateCommand ();
			try
			{				
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = spName;

				for (int paramIndex = 0; paramIndex < paramCount; paramIndex++) 
				{
					IDataParameter pdcurrent = spParams [paramIndex];
					//handle NULLs
					if (pdcurrent.Value == null) 
					{
						pdcurrent.Value = DBNull.Value;
					}
					if(pdcurrent.DbType==DbType.DateTime && DateTime.Parse(pdcurrent.Value.ToString()) ==DateTime.MinValue)
					{
						pdcurrent.Value=DBNull.Value;
					}
					cmd.Parameters.Add (pdcurrent);
				}
			}
			catch(Exception ex)
			{
				try
				{
					cmd.Parameters.Clear();
				}
				catch{}				
				throw ex;

			}
			return cmd;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="spParams"></param>
		/// <param name="spName"></param>
		/// <param name="sprocbase"></param>
		/// <returns></returns>
		public static object ExecuteStoredProcGenerateCommand (IDataParameter [] spParams, String spName,ISprocBase sprocbase) {
			IDbCommand cmd =null;
			try
			{
				System.Diagnostics.Trace.WriteLine ("ExecuteStoredProcGenerateCommand");
				System.Diagnostics.Trace.Write (String.Format ("Stored Procedure Name: {0} ", spName));
				DebugParams (spParams);

				bool autoClose;
				IDbConnection conn = InitConnection (sprocbase, out autoClose);

				int paramCount = spParams.Length;
				cmd = BuildCommandWithParams (conn, spParams, paramCount, spName);

				//set up the transaction (if there is one that is)
				cmd.Transaction = sprocbase.Transaction;

				if (autoClose == true)
					conn.Close ();

			}
			catch(Exception ex)
			{
				
				try
				{
					cmd.Parameters.Clear();
				}
				catch (Exception x)
				{ 
					throw x;
				}
				
				throw ex;
			}
			return cmd;
		}


		private static IDataParameter [] HarvestOutputParams (IDbCommand cmd, int outParamCount) {
			IDataParameter [] outparamList = new IDataParameter [outParamCount];
			try
			{
				int paramCount = cmd.Parameters.Count;
				

				int outParamPos = 0;

				for (int paramIndex = 0; paramIndex < paramCount; paramIndex++) 
				{
					IDataParameter param = (IDataParameter) cmd.Parameters [paramIndex];
					if (param.Direction == ParameterDirection.InputOutput
						| param.Direction == ParameterDirection.Output) 
					{
						outparamList [outParamPos++] = param;
					}
				}
			}
			catch(Exception ex)
			{
			
				try
				{
					cmd.Parameters.Clear();
				}
				catch (Exception x)
				{ 
					throw x;
				}
				throw ex;
			}
			return outparamList;
		}


		/// <summary>
		///
		/// </summary>
		/// <param name="spParams"></param>
		/// <param name="spName"></param>
		/// <param name="sprocbase"></param>
		/// <param name="outputParams"></param>
		/// <param name="outputParamCount"></param>
		/// <returns></returns>
		public static IDataReader ExecuteStoredProcIDataReader (IDataParameter [] 
spParams,
																String spName, ISprocBase sprocbase,
																out IDataParameter [] outputParams,
																int outputParamCount) {
			IDataReader rdr=null;
			outputParams=null;
			IDbCommand cmd=null;
			try
			{
				System.Diagnostics.Trace.WriteLine ("ExecuteStoredProc");
				System.Diagnostics.Trace.Write (String.Format ("Stored Procedure Name: {0} ", spName));
				DebugParams (spParams);

				//they should not have AutoClose turned on when using IDataReader
				if (true == sprocbase.AutoClose)
					throw new IllegalAutoCloseSettingException (String.Format ("AutoClose setting is set to true for stored procedure: {0}, this must be turned off for this method prior to making the call", spName));

				bool autoClose;
				IDbConnection conn = InitConnection (sprocbase, out autoClose);

				int paramCount = spParams.Length;
				cmd = BuildCommandWithParams (conn, spParams, paramCount, spName);

				//set up the transaction (if there is one that is)
				cmd.Transaction = sprocbase.Transaction;

				rdr = cmd.ExecuteReader ();

				if (outputParamCount > 0)
					outputParams = HarvestOutputParams (cmd, outputParamCount);
				else
					outputParams = new IDataParameter [] {};

				cmd.Parameters.Clear ();
			}
			catch(Exception ex)
			{
				
				try
				{
					cmd.Parameters.Clear();
				}
				catch (Exception x)
				{ 
					throw x;
				}
				
				throw ex;
			}
			return rdr;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="spParams"></param>
		/// <param name="spName"></param>
		/// <param name="sprocbase"></param>
		/// <param name="outputParams"></param>
		/// <param name="outputParamCount"></param>
		/// <returns></returns>

		public static int ExecuteStoredProcNonQuery (IDataParameter [] spParams, String spName, ISprocBase sprocbase,out IDataParameter [] outputParams, int outputParamCount) {
			int rowsAffected = 0;
			outputParams=null;
			IDbCommand cmd=null;
			try
			{
				System.Diagnostics.Trace.WriteLine ("ExecuteStoredProc");
				System.Diagnostics.Trace.Write (String.Format ("Stored Procedure Name: {0} ", spName));
				DebugParams (spParams);

				bool autoClose;
                IDbConnection conn = InitConnection2(sprocbase, out autoClose);
                //conn.BeginTransaction();
				int paramCount = spParams.Length;
				cmd = BuildCommandWithParams (conn, spParams, paramCount, spName);
				cmd.CommandTimeout=500; 
				//set up the transaction (if there is one that is)
                //cmd.Transaction = sprocbase.Transaction;	

                
                // Start a local transaction.
                //sprocbase.Transaction = conn.BeginTransaction();
             
                //cmd.Transaction = sprocbase.Transaction;	
				try 
				{
					rowsAffected = cmd.ExecuteNonQuery();
				}
                catch(Exception ex){
                    //sprocbase.Transaction = conn.b();
                     cmd.Transaction = sprocbase.Transaction;
                     throw;
                }
				finally 
				{
					//if we opened the connection then we close it!
					if (autoClose == true || sprocbase.AutoClose == true)
						conn.Close ();
				}

				if (outputParamCount > 0)
					outputParams = HarvestOutputParams (cmd, outputParamCount);
				else
					outputParams = new IDataParameter [] {};

				cmd.Parameters.Clear ();
			}
			catch(Exception ex)
			{
				try
				{
					cmd.Parameters.Clear();
				}
				catch (Exception x)
				{ 
                    throw x;
				}
				
				throw ex;
			}
			return rowsAffected;
		}


		/// <summary>
		///
		/// </summary>
		/// <param name="spParams"></param>
		/// <param name="spName"></param>
		/// <param name="sprocbase"></param>
		/// <param name="outputParams"></param>
		/// <param name="containingAssembly"></param>
		/// <param name="dsType"></param>
		/// <param name="outputParamCount"></param>
		/// <returns></returns>
		public static object ExecuteStoredProcDataAdapter (IDataParameter [] spParams, String spName, ISprocBase sprocbase, out IDataParameter [] outputParams, /*string containingAssembly, string dsType, */Type dsType, DataSet theds, int outputParamCount) {
			outputParams=null;
			IDbCommand cmd=null;
			try
			{
				System.Diagnostics.Trace.WriteLine ("ExecuteStoredProcDataAdapter");
				System.Diagnostics.Debug.Assert (theds != null);
				System.Diagnostics.Trace.Write (String.Format ("Stored Procedure Name: {0} ", spName));
				DebugParams (spParams);

				bool autoClose;
				IDbConnection conn = InitConnection (sprocbase, out autoClose);

                //conn.BeginTransaction();

               
				int paramCount = spParams.Length;
				cmd = BuildCommandWithParams (conn, spParams, paramCount, spName);
				IDbDataAdapter da = null;

				if (conn is SqlConnection)
					da = new SqlDataAdapter ();
				else if (conn is OleDbConnection)
					da = new OleDbDataAdapter ();
				else
					throw new DatabaseNotSupportedException ("Only SQL Server and OleDb are valid database types");

				lock(cmd)
				{
					da.SelectCommand = cmd;
					cmd.CommandTimeout=500;  

					DbDataAdapter actualAdapter = (DbDataAdapter) da;

					//set up the transaction (if there is one that is)
					cmd.Transaction = sprocbase.Transaction;

				
					try 
					{
						if (dsType == null)
							actualAdapter.Fill (theds);
						else 
						{
							//strongly typed dataset, need to figure out the table name to fill
							//from the class name
							actualAdapter.Fill (theds, dsType.Name);

						}
					}
					finally 
					{
						//if we opened the connection then we close it!
						//or they configured the sproc for autoclose
						if (autoClose == true || sprocbase.AutoClose == true)
							conn.Close ();
					}
				}
				if (outputParamCount > 0)
					outputParams = HarvestOutputParams (cmd, outputParamCount);
				else
					outputParams = new IDataParameter [] {};

				cmd.Parameters.Clear ();
			}
			catch(Exception ex)
			{
				try
				{
					cmd.Parameters.Clear();
				}
				catch {}
				throw ex;
			}
			return theds;
		}


		/// <summary>
		/// Figures out the DbType based on the presence of the DbDataTypeAttribute custom attribute
		/// </summary>
		/// <param name="pi">Parameter type descriptor</param>
		/// <param name="dbAttrSet">Ouput parameter indicating whether the attribute has been set to a sensible value</param>
		/// <param name="actualDbType">The value set</param>
		private static void GetDbType (ParameterInfo pi, out bool dbAttrSet, out 
DbType actualDbType) {

			if (pi.IsDefined (typeof (DbDataTypeAttribute), false)) {
				DbDataTypeAttribute [] dbTypeAttrs = (DbDataTypeAttribute []) 
pi.GetCustomAttributes (typeof(DbDataTypeAttribute), false);

				actualDbType = dbTypeAttrs [0].ActualDbType;
				dbAttrSet = true;
			}
			else {
				actualDbType = DbType.String; //must set it to something!
				dbAttrSet = false;
			}
		}


		/// <summary>
		/// Persists the assembly (only call this method for debug purposes)
		/// </summary>
		public static void PersistAssembly () {
			//check that we have an assembly to persist
			if (null != _asmBuilder)
				_asmBuilder.Save(_fileName);
		}


		private static void ConstructAssemblyModuleHelper () {
			String moduleName = "DynamicModule";

			AssemblyName asmName = new AssemblyName();

			asmName.Name = SprocFactory.AssemblyName;

			SprocFactory._fileName = asmName.Name + ".dll";

			SprocFactory._asmBuilder = 
AppDomain.CurrentDomain.DefineDynamicAssembly(asmName,
				AssemblyBuilderAccess.RunAndSave);

			SprocFactory._modBuilder = 
SprocFactory._asmBuilder.DefineDynamicModule(moduleName, 
SprocFactory._fileName);
		}


		/// <summary>
		/// Internal method for detecting use of SprocNameAttribute
		/// If SprocNameAttribute not used then the sproc name is the same as the method name
		/// </summary>
		/// <param name="method">Method descriptor</param>
		/// <returns>Stored procedure name</returns>
		private static string GetSprocName (MethodInfo method) {
			string sprocName;

			if (method.IsDefined (typeof (SprocNameAttribute), false)) {
				//this means they have changed the method name with an attribute
				SprocNameAttribute [] aspna = (SprocNameAttribute []) 
method.GetCustomAttributes (typeof (SprocNameAttribute), false);

				sprocName = aspna [0].SprocName;
			}
			else
				sprocName = method.Name;

			return sprocName;
		}


		/// <summary>
		/// Retrieves the DbTypeAttribute if specified and calls
		/// IDbParameter.set_DbType
		/// local1 contains the SqlParameter
		/// </summary>
		/// <param name="IDbParameterType"></param>
		/// <param name="ilGen"></param>
		/// <param name="pi"></param>
		private static void EmitDbTypeHelper (Type IDbParameterType, ILGenerator 
ilGen, ParameterInfo pi) {

			
			try
			{
			DbType actualDbType;
			bool attrSet;
			GetDbType (pi, out attrSet, out actualDbType);

			if (false == attrSet) {
				return;
			}

			MethodInfo mi = IDbParameterType.GetMethod ("set_DbType");

			//push objref on stack
			ilGen.Emit (OpCodes.Ldloc_1);
			ilGen.Emit (OpCodes.Ldc_I4, (int) actualDbType);
			//make the call to set_DbType
			ilGen.Emit (OpCodes.Call, mi);
			}
			catch(Exception ex)
			{			
				throw ex;
			}
		}

		private static void EmitDirectionHelper (Type IDbParameterType, 
			ILGenerator ilGen,
			ParameterInfo pi, ref int outputParamCount) 
		{

			try
			{
				//MethodInfo mi = IDbParameterType.GetMethod ("set_Direction");
				MethodInfo mi = typeof(IDataParameter).GetMethod ("set_Direction");

				//push objref on the stack
				ilGen.Emit (OpCodes.Ldloc_1);
				ilGen.Emit (OpCodes.Castclass, typeof (IDataParameter));

				ParameterDirection pd;

				if (pi.ParameterType.IsByRef) 
				{
					outputParamCount++;
					pd = ParameterDirection.InputOutput;
				}
				else
					pd = ParameterDirection.Input;

				ilGen.Emit (OpCodes.Ldc_I4, (int) pd);
				ilGen.Emit (OpCodes.Callvirt, mi);
			}
			catch(Exception ex)
			{			
				throw ex;
			}
		}



		private static void EmitLoadIndirectHelper (ILGenerator ilGen, 
ParameterInfo pi, int paramIndex) {
			try
			{
				//TODO throw an exception for ref type unrecognised
				switch (pi.ParameterType.FullName) 
				{
					case "System.Byte&":
						ilGen.Emit (OpCodes.Ldind_I1);
						ilGen.Emit (OpCodes.Box, typeof (System.Byte));
						break;

					case "System.Int16&":
						ilGen.Emit (OpCodes.Ldind_I2);
						ilGen.Emit (OpCodes.Box, typeof (System.Int16));
						break;

					case "System.Int32&":
						ilGen.Emit (OpCodes.Ldind_I4);
						ilGen.Emit (OpCodes.Box, typeof (System.Int32));
						break;

					case "System.Int64&":
						ilGen.Emit (OpCodes.Ldind_I8);
						ilGen.Emit (OpCodes.Box, typeof (System.Int64));
						break;

					case "System.Single&":
						ilGen.Emit (OpCodes.Ldind_R4);
						ilGen.Emit (OpCodes.Box, typeof (System.Single));
						break;

					case "System.Double&":
						ilGen.Emit (OpCodes.Ldind_R8);
						ilGen.Emit (OpCodes.Box, typeof (System.Double));
						break;

					case "System.DateTime&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.DateTime));
						ilGen.Emit (OpCodes.Box, typeof (System.DateTime));
						break;

					case "System.Boolean&":
						ilGen.Emit (OpCodes.Ldind_I1);
						ilGen.Emit (OpCodes.Box, typeof (System.Boolean));
						break;

					case "System.Decimal&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Decimal));
						ilGen.Emit (OpCodes.Box, typeof (System.Decimal));
						break;

					case "System.String&":
						ilGen.Emit (OpCodes.Ldind_Ref);
						break;

					case "System.Guid&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Guid));
						ilGen.Emit (OpCodes.Box, typeof (System.Guid));
						break;


					case "System.Data.SqlTypes.SqlBinary&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlBinary));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlBinary));
						break;

					case "System.Data.SqlTypes.SqlBoolean&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlBoolean));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlBoolean));
						break;

					case "System.Data.SqlTypes.SqlByte&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlByte));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlByte));
						break;

					case "System.Data.SqlTypes.SqlDateTime&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlDateTime));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlDateTime));
						break;

					case "System.Data.SqlTypes.SqlDecimal&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlDecimal));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlDecimal));
						break;

					case "System.Data.SqlTypes.SqlDouble&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlDouble));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlDouble));
						break;

					case "System.Data.SqlTypes.SqlGuid&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlGuid));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlGuid));
						break;

					case "System.Data.SqlTypes.SqlInt16&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlInt16));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlInt16));
						break;


					case "System.Data.SqlTypes.SqlInt32&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlInt32));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlInt32));
						break;

					case "System.Data.SqlTypes.SqlInt64&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlInt64));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlInt64));
						break;

					case "System.Data.SqlTypes.SqlMoney&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlMoney));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlMoney));
						break;

					case "System.Data.SqlTypes.SqlSingle&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlSingle));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlSingle));
						break;

					case "System.Data.SqlTypes.SqlString&":
						ilGen.Emit (OpCodes.Ldobj, typeof (System.Data.SqlTypes.SqlString));
						ilGen.Emit (OpCodes.Box, typeof (System.Data.SqlTypes.SqlString));
						break;

					default:
						throw new UnsupportedReferenceParameterType (String.Format ("Reference parameters of type: {0}, are not supported", pi.ParameterType));
				}
			}
			catch(Exception ex)
			{				
				throw ex;
			}
		}


		private static void HarvestParameter (ILGenerator ilGen, Type localType, 
int nParamCount,
												int [] paramPos, MethodInfo get_Value,
												OpCode [] opSequence) {
			try
			{
				//push argument on the stack..it's +1 because the "this" pointer is on the stack
				ilGen.Emit (OpCodes.Ldarg_S, paramPos [nParamCount]+1);

				//push the objref for the output param IDataParameter [] on the stack
				ilGen.Emit (OpCodes.Ldloc_S, (int) Locals.local2);

				//index into the IDataParameter []
				ilGen.Emit (OpCodes.Ldc_I4, nParamCount);

				ilGen.Emit (OpCodes.Ldelem_Ref);

				//call get_Value on IDataParameter
				ilGen.Emit (OpCodes.Callvirt, get_Value);

				if (localType.IsValueType)
					//emit unbox for value type
					ilGen.Emit (OpCodes.Unbox, localType);

				foreach (OpCode code in opSequence) 
				{
					switch (code.Name) 
					{
						case "stobj": 
							ilGen.Emit (code, localType);
							break;
						case "ldobj":
							ilGen.Emit (code, localType);
							break;
						case "castclass":
							ilGen.Emit (code, localType);
							break;
						default:
							ilGen.Emit (code);
							break;
					}
				}
			}
			catch(Exception ex)
			{				
				throw ex;
			}
		}


		private static void EmitHarvestOutputParamsHelper (ILGenerator ilGen,
															ParameterInfo [] prms,
															int outputParamCount) {
			try
			{
				int paramPosCount = 0;

				int [] paramPos = new int [outputParamCount];

				//store the array position
				for (int nParamCount = 0; nParamCount < prms.Length; nParamCount++) 
				{
					if (prms [nParamCount].ParameterType.IsByRef) 
					{
						paramPos [paramPosCount] = nParamCount;
						paramPosCount++;
					}
				}


				MethodInfo get_Value = typeof (IDataParameter).GetMethod ("get_Value");

				//walk the IDataParameter [] extracting each output parameter, unboxing as necessary
				for (int nParamCount = 0; nParamCount < paramPos.Length; nParamCount++) 
				{
					ParameterInfo pi = prms [ paramPos [nParamCount] ];

					switch (pi.ParameterType.FullName) 
					{
						case "System.Byte&":
							HarvestParameter  (ilGen, typeof (System.Byte),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_I1, OpCodes.Stind_I1});
							break;


						case "System.Int16&":
							HarvestParameter  (ilGen, typeof (System.Int16),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_I2, OpCodes.Stind_I2});
							break;

						case "System.Int32&":
							HarvestParameter  (ilGen, typeof (System.Int32),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_I4, OpCodes.Stind_I4});
							break;

						case "System.Int64&":
							HarvestParameter  (ilGen, typeof (System.Int64),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_I8, OpCodes.Stind_I8});
							break;

						case "System.Single&":
							HarvestParameter  (ilGen, typeof (System.Single),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_R4, OpCodes.Stind_R4});
							break;

						case "System.Double&":
							HarvestParameter  (ilGen, typeof (System.Double),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_R8, OpCodes.Stind_R8});
							break;

						case "System.DateTime&":
							HarvestParameter  (ilGen, typeof (System.DateTime),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Boolean&":
							HarvestParameter  (ilGen, typeof (System.Boolean),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldind_I1, OpCodes.Stind_I1});
							break;

						case "System.Decimal&":
							HarvestParameter  (ilGen, typeof (System.Decimal),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.String&":
							HarvestParameter  (ilGen, typeof (System.String),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Castclass, OpCodes.Stind_Ref});
							break;

						case "System.Guid&":
							HarvestParameter  (ilGen, typeof (System.Guid),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlBinary&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlBinary),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlBoolean&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlBoolean),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlByte&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlByte),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlDateTime&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlDateTime),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlDecimal&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlDecimal),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlDouble&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlDouble),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlGuid&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlGuid),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlInt16&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlInt16),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;


						case "System.Data.SqlTypes.SqlInt32&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlInt32),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlInt64&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlInt64),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlMoney&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlMoney),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlSingle&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlSingle),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;

						case "System.Data.SqlTypes.SqlString&":
							HarvestParameter  (ilGen, typeof (System.Data.SqlTypes.SqlString),
								nParamCount,
								paramPos,
								get_Value,
								new OpCode [] {OpCodes.Ldobj, OpCodes.Stobj});
							break;
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Builds an array of Types from a ParameterInfo arary
		/// </summary>
		/// <param name="prms"></param>
		/// <returns></returns>
		private static Type [] GetParameterTypesFromParameterInfo (ParameterInfo 
[] prms) {
			Type [] typeArr = new Type [prms.Length];
			try
			{				
				int paramPos = 0;

				foreach (ParameterInfo pi in prms) 
				{
					typeArr [paramPos] = pi.ParameterType;
					paramPos++;
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return typeArr;
		}


		private static void CreateParametersHelper (ILGenerator ilGen, DBProvider 
prov,
												int paramCount, ParameterInfo [] prms,
												Type [] paramTypes, MethodInfo method,
												ref int outputParamCount,
												string paramPrefixChar) {
			try
			{
			//Create the IDataParameter []
			Type [] constructorArgs = {}; //no arg constructor

			//outputParamCount = 0;

			//how big is the array..
			ilGen.Emit (OpCodes.Ldc_I4, paramCount);

			ilGen.Emit (OpCodes.Newarr, typeof (IDataParameter));
			ilGen.Emit(OpCodes.Stloc_0);

			Type [] constructorArgsTypes = new Type [] {typeof (string), typeof 
(object)}; //two arg .ctor

			Type IDbParameterType = null;
			//configure param type
			switch (prov)
			{
				case DBProvider.SQLServer:
					IDbParameterType = typeof (SqlParameter);
					break;

				case DBProvider.OleDb:
					IDbParameterType = typeof (OleDbParameter);
					break;

				default:
					throw new DatabaseNotSupportedException ("Only SQL Server and OleDb are valid database types");
			}

			ConstructorInfo paramConstructor = IDbParameterType.GetConstructor 
(constructorArgsTypes);

			for (int paramIndex = 0; paramIndex < paramCount; paramIndex++)
			{
				//we're going to create an instance of the param type now (SqlParameter/OleDbParameter)
				//1) push param name on stack
				//2) push reference / valuetype parameter on stack ..NB the "this" ptr is at paramindex==0!
				//3) box if necessary
				//4) Fire param .ctor
				//5) Store objref of above in local1
				//6) call set_DbType
				//7) call set_Direction


				ilGen.Emit (OpCodes.Ldstr, paramPrefixChar + prms 
[paramIndex].Name);			//1)


				System.Diagnostics.Debug.WriteLine (String.Format ("Stored Proc Parameters are being prefixed with the leading character:{0}", (prov == 
DBProvider.SQLServer || prov == DBProvider.OleDb) ? "@" : ":"));
				ilGen.Emit (OpCodes.Ldarg, paramIndex + 1);					//2)

				if (paramTypes [paramIndex].IsByRef)
					EmitLoadIndirectHelper (ilGen, prms [paramIndex], paramIndex);

				//this bit only fires if valuetype and input parameter
				if (paramTypes [paramIndex].IsValueType)					//3)
					ilGen.Emit (OpCodes.Box, paramTypes [paramIndex]);

				//fire the 2 arg .ctor
				ilGen.Emit (OpCodes.Newobj, paramConstructor);				//4)
				ilGen.Emit (OpCodes.Stloc_1);								//5)

				EmitDbTypeHelper (IDbParameterType, ilGen, prms [paramIndex]);
				EmitDirectionHelper (IDbParameterType, ilGen, prms [paramIndex], ref 
outputParamCount);

				////////////////////////////////////////////////////////////////

				//get the objref into the stack
				ilGen.Emit(OpCodes.Ldloc_0);
				ilGen.Emit(OpCodes.Ldc_I4_S, paramIndex);
				//then get the param on the stack
				ilGen.Emit(OpCodes.Ldloc_1);								//7)
				ilGen.Emit (OpCodes.Stelem_Ref);
			}

			//store paramaters in the cache

			//get type info for field containing hashtable
			FieldInfo fiht = typeof (SprocBase).GetField ("_htParams", 
BindingFlags.Instance|
				BindingFlags.NonPublic);
			ilGen.Emit (OpCodes.Ldarg_0); //put the this on the stack!
			ilGen.Emit (OpCodes.Ldfld, fiht);
			ilGen.Emit (OpCodes.Ldstr, method.GetHashCode ().ToString ()); //wahoo!
			//push the IDataParameter [] on the stack
			ilGen.Emit (OpCodes.Ldloc_0);
			//method info for Hashtable.set_Item method
			MethodInfo miHashtablesetItem = typeof (Hashtable).GetMethod 
("set_Item");
			ilGen.Emit (OpCodes.Callvirt, miHashtablesetItem);
			}
			catch(Exception ex)
			{				
				throw ex;
			}
		}

		/// <summary>
		/// Does all the grunt work of actually emitting the command and parameters
		/// The emitted code initially checks the cache to see if the parameters
		/// for this method have been created before and if so, takes a copy of them
		/// to save the creation time overhead
		/// </summary>
		/// <param name="method"></param>
		/// <param name="tb"></param>
		/// <param name="prov"></param>
		/// <param name="paramPrefixChar"></param>
		private static void ImplementMethodHelper (MethodInfo method, TypeBuilder 
tb,
													DBProvider prov,
													string paramPrefixChar) {
			try
			{
				MethodBuilder methodBody = null;
				int paramCount = 0;
				int paramIndex = 0;
				int outputParamCount = 0;



				ParameterInfo [] prms = method.GetParameters();
				paramCount = prms.Length;
				Type [] paramTypes = new Type [paramCount];

				for (paramIndex = 0; paramIndex < paramCount; paramIndex++) 
				{
					paramTypes [paramIndex] = prms [paramIndex].ParameterType;
				}

				methodBody = tb.DefineMethod(method.Name, MethodAttributes.Public
					| MethodAttributes.Virtual,
					method.ReturnType, paramTypes);

				ILGenerator ilGen = methodBody.GetILGenerator();

				ilGen.DeclareLocal(typeof (IDataParameter [])); //local0 input params
				//IDataParameter pd;
				ilGen.DeclareLocal (typeof (IDataParameter)); //local1
				ilGen.DeclareLocal (typeof (IDataParameter [])); //local2 output params

				//perform cache lookup for parameters

				//get type info for field containing hashtable
				FieldInfo fiht = typeof (SprocBase).GetField ("_htParams", 
					BindingFlags.Instance|
					BindingFlags.NonPublic);
				ilGen.Emit (OpCodes.Ldarg_0); //put the this on the stack!
				ilGen.Emit (OpCodes.Ldfld, fiht);
				ilGen.Emit (OpCodes.Ldstr, method.GetHashCode ().ToString ()); //wahoo!

				//method info for Hashtable.get_Item method
				MethodInfo miHashtablegetItem = typeof (Hashtable).GetMethod 
					("get_Item");
				ilGen.Emit (OpCodes.Callvirt, miHashtablegetItem);
				ilGen.Emit (OpCodes.Castclass, typeof (IDataParameter []));
				ilGen.Emit (OpCodes.Stloc_0); //store objref of IDataParameter [] into local0

				//if (array == null) then goto label
				ilGen.Emit (OpCodes.Ldloc_0);

				Label createParamsLabel = ilGen.DefineLabel ();

				ilGen.Emit (OpCodes.Brfalse, createParamsLabel);


				System.Diagnostics.Debug.WriteLine ("Parameter cache hit!");
				MethodInfo miSprocFactory_DeepCopyParams = typeof 
					(SprocFactory).GetMethod ("DeepCopyParams",
					BindingFlags.Public|
					BindingFlags.Static);
				ilGen.Emit (OpCodes.Ldloc_0);
				ilGen.Emit (OpCodes.Call, miSprocFactory_DeepCopyParams);
				ilGen.Emit (OpCodes.Stloc_0);


				for (int pc = 0; pc < paramCount; pc++) 
				{
					//Get the IDataParameter[] on the stack
					ilGen.Emit (OpCodes.Ldloc_0);
					//Get the index on the stack of the loop item
					ilGen.Emit (OpCodes.Ldc_I4, pc);
					ilGen.Emit (OpCodes.Ldelem_Ref);

					ilGen.Emit (OpCodes.Stloc_1);
					ilGen.Emit (OpCodes.Ldloc_1);

					////////////HACK CUT N PASTE!!
					ilGen.Emit (OpCodes.Ldarg, pc + 1);

					if (paramTypes [pc].IsByRef)
						EmitLoadIndirectHelper (ilGen, prms [pc], pc);

					//this bit only fires if valuetype and input parameter
					if (paramTypes [pc].IsValueType)
						ilGen.Emit (OpCodes.Box, paramTypes [pc]);
					//////////

					//now call IDataParameter.set_Value...
					MethodInfo miIDataParameterset_Value = typeof (IDataParameter).GetMethod 
						("set_Value");
					ilGen.Emit (OpCodes.Callvirt, miIDataParameterset_Value);
				}
				Label work = ilGen.DefineLabel ();
				ilGen.Emit (OpCodes.Br, work);

				ilGen.MarkLabel (createParamsLabel);

				CreateParametersHelper (ilGen, prov, paramCount, prms, paramTypes, 
					method, ref outputParamCount, paramPrefixChar);
				ilGen.MarkLabel (work);

				/*now execute the function that will take the IDataParameter [] and 
	execute the SP*/


				//push IDataParameter [] objref on the stack
				ilGen.Emit(OpCodes.Ldloc_0);

				//push the method name on the stack
				ilGen.Emit (OpCodes.Ldstr, SprocFactory.GetSprocName (method));

				//put this reference on the stack
				ilGen.Emit (OpCodes.Ldarg_0);
				//it takes 3 params: the IDataParameter[], the name of the sproc to execute + this reference
				Type [] spParamsInTypes = GetParameterTypesFromParameterInfo (typeof 
					(SprocFactory).GetMethod ("ExecuteStoredProcGenerateCommand").GetParameters 
					());
				//TODO fix up these variable names
				Type [] spParamsInOutTypes = GetParameterTypesFromParameterInfo (typeof 
					(SprocFactory).GetMethod ("ExecuteStoredProcNonQuery").GetParameters ());
				Type [] spParamTypesExecuteStoredProcDataAdapter = 
					GetParameterTypesFromParameterInfo (typeof (SprocFactory).GetMethod 
					("ExecuteStoredProcDataAdapter").GetParameters ());
				MethodInfo mi_getTypeFromHandle = typeof (System.Type).GetMethod 
					("GetTypeFromHandle",
					BindingFlags.Static|
					BindingFlags.Public);

				switch (method.ReturnType.FullName) 
				{
					case "System.Int32":
						//push the address of local2 on to the stack - the IDataParameter [] output param
						ilGen.Emit (OpCodes.Ldloca_S, (byte) Locals.local2);
						ilGen.Emit (OpCodes.Ldc_I4, outputParamCount);
						MethodInfo executeStoredProcNonQuery = typeof 
							(SprocFactory).GetMethod("ExecuteStoredProcNonQuery",
							BindingFlags.Public |
							BindingFlags.Static,
							null,
							spParamsInOutTypes,
							null);


						ilGen.Emit(OpCodes.Call, executeStoredProcNonQuery);
						//fixup any output params in the IDataParameter []
						EmitHarvestOutputParamsHelper (ilGen, prms, outputParamCount);
						break;

					case "System.Data.DataSet":
						//push the address of local2 on to the stack - the IDataParameter[] output param
						ilGen.Emit (OpCodes.Ldloca_S, (byte) Locals.local2);

						//finally push null ref on the stack
						ilGen.Emit (OpCodes.Ldnull);
						ilGen.Emit (OpCodes.Newobj, method.ReturnType.GetConstructor (new Type 
							[] {} ));

						//call System.Type.GetTypeFromHandle
						ilGen.Emit (OpCodes.Ldc_I4, outputParamCount);

						MethodInfo executeStoredProcDataAdapter = typeof 
							(SprocFactory).GetMethod("ExecuteStoredProcDataAdapter",
							BindingFlags.Public |
							BindingFlags.Static,
							null,
							spParamTypesExecuteStoredProcDataAdapter,
							null);
						//NB - we don't do a pop in this case, thus leaving the retval DataSet on the stack!
						ilGen.Emit (OpCodes.Call, executeStoredProcDataAdapter);

						EmitHarvestOutputParamsHelper (ilGen, prms, outputParamCount);
						break;

					case "System.Data.IDbCommand":
						//no output param processing necessary as we are not
						//executing the target sproc
						MethodInfo executeStoredProcGenerateCommand = typeof 
							(SprocFactory).GetMethod("ExecuteStoredProcGenerateCommand",
							BindingFlags.Public |
							BindingFlags.Static,
							null,
							spParamsInTypes,
							null);
						//NB - we don't do a pop in this case, thus leaving the retval Command object on the stack!
						ilGen.Emit (OpCodes.Call, executeStoredProcGenerateCommand);

						break;

					case "System.Data.IDataReader":
						//push the address of local2 on to the stack - the IDataParameter [] output param
						ilGen.Emit (OpCodes.Ldloca_S, (byte) Locals.local2);
						ilGen.Emit (OpCodes.Ldc_I4, outputParamCount);
						MethodInfo executeStoredProcIDataReader = typeof 
							(SprocFactory).GetMethod("ExecuteStoredProcIDataReader",
							BindingFlags.Public |
							BindingFlags.Static,
							null,
							spParamsInOutTypes,
							null);
						//NB - we don't do a pop in this case, thus leaving the retval Command object on the stack!
						ilGen.Emit (OpCodes.Call, executeStoredProcIDataReader);

						//fixup any output params in the IDataParameter []
						EmitHarvestOutputParamsHelper (ilGen, prms, outputParamCount);
						break;

					default:
						//test for strongly typed dataset, if it aint one of those then bail
						if (method.ReturnType.BaseType == typeof (System.Data.DataSet)) 
						{
							ilGen.Emit (OpCodes.Ldloca_S, (byte) Locals.local2);

							//ilGen.Emit (OpCodes.Ldstr, method.ReturnType.Assembly.FullName);
							//finally push class of DataSet on stack

							//ilGen.Emit (OpCodes.Ldstr, method.ReturnType.FullName);


							//call System.Type.GetTypeFromHandle
							//						MethodInfo mi_getTypeFromHandle = typeof (System.Type).GetMethod ("GetTypeFromHandle",
							//																					BindingFlags.Static|
							//																					BindingFlags.Public);


							ilGen.Emit (OpCodes.Ldtoken, method.ReturnType);
							ilGen.Emit (OpCodes.Call, mi_getTypeFromHandle);

							ilGen.Emit (OpCodes.Newobj, method.ReturnType.GetConstructor (new Type 
								[] {} ));

							ilGen.Emit (OpCodes.Ldc_I4, outputParamCount);

							MethodInfo executeStoredProcDataAdapterStronglyTyped = typeof 
								(SprocFactory).GetMethod("ExecuteStoredProcDataAdapter",
								BindingFlags.Public |
								BindingFlags.Static,
								null,
								spParamTypesExecuteStoredProcDataAdapter,
								null);
							//NB - we don't do a pop in this case, thus leaving the retval DataSet on the stack!
							ilGen.Emit (OpCodes.Call, executeStoredProcDataAdapterStronglyTyped);

							EmitHarvestOutputParamsHelper (ilGen, prms, outputParamCount);
							break; //we don't want to throw the exception below!
						}

						throw new UnsupportedStoredProcedureReturnTypeException (String.Format ("Stored Procedure method: {0} has an illegal return type of {1}; only Int32, DataSet and IDbCommand are supported", method.Name, method.ReturnType));

				}


				if (!method.ReturnType.IsValueType)
					ilGen.Emit (OpCodes.Castclass, method.ReturnType);

				ilGen.Emit(OpCodes.Ret);

				tb.DefineMethodOverride(methodBody, method);
			}
			catch(Exception ex)
			{				
				throw ex;
			}
		}


		/// <summary>
		/// Helper class; fleshes out the emitted class
		/// </summary>
		/// <param name="tb">The newly created type</param>
		/// <param name="itf">The interface to implement</param>
		/// <param name="prov"></param>
		private static void ImplementDerivedInterfaceHelper (TypeBuilder tb, Type 
itf, DBProvider prov) {
			try
			{

			//the interface this type will implement
			tb.AddInterfaceImplementation(itf);

			string paramPrefixChar = null;

			if (DBProvider.SQLServer == prov)
				paramPrefixChar = "@";
			else {
				if (!itf.IsDefined (typeof (ParamPrefixCharAttribute), false))
					throw new ParamPrefixCharException ("Parameter prefix attribute not specified. You must specify the ParamPrefixChar custom attribute on the interface");

				else {
					ParamPrefixCharAttribute [] prefixChar = (ParamPrefixCharAttribute []) 
itf.GetCustomAttributes (typeof (ParamPrefixCharAttribute), false);
					paramPrefixChar = prefixChar [0].ParamPrefixChar;
				}
			}


			//walk the parent and produce an overriden version of the method
			MethodInfo [] parentMethodInfo = itf.GetMethods(BindingFlags.Public |
				BindingFlags.Instance |
				BindingFlags.DeclaredOnly);

			//now implement each of the interface methods on the generated class
			foreach (MethodInfo method in parentMethodInfo)
				SprocFactory.ImplementMethodHelper (method, tb, prov, paramPrefixChar);
			}
			catch(Exception ex)
			{				
				throw ex;
			}
		}

		/// <summary>
		/// Overloaded method for seeding Connection and AutoClose properties
		/// </summary>
		/// <param name="itf"></param>
		/// <param name="conn"></param>
		/// <param name="autoClose"></param>
		/// <returns></returns>
		public static object CreateInstance (Type itf, IDbConnection conn, bool 
autoClose) {
			ISprocBase baseItf=null;
			try
			{
				//figure out if it's a SqlConnection or OleDbConnection
				DBProvider prov;

				if (conn is SqlConnection)
					prov = DBProvider.SQLServer;
				else if (conn is OleDbConnection)
					prov = DBProvider.OleDb;
				else
					throw new DatabaseNotSupportedException ("Only SQL Server and OleDb are valid database types");


				baseItf = (ISprocBase) SprocFactory.CreateInstance (itf, prov, 
					autoClose);
				baseItf.Connection = conn;
			}
			catch(Exception ex)
			{				
				throw ex;
			}
			return baseItf;
		}


		/// <summary>
		/// Overloaded method for seeding Connection property
		/// Automatically sets AutoClose to true
		/// </summary>
		/// <param name="itf"></param>
		/// <param name="conn"></param>
		/// <returns></returns>
		public static object CreateInstance (Type itf, IDbConnection conn) {
			//figure out if it's a SqlConnection or OleDbConnection
			
			DBProvider prov;
			ISprocBase baseItf=null;
			try
			{
				if (conn is SqlConnection)
					prov = DBProvider.SQLServer;
				else if (conn is OleDbConnection)
					prov = DBProvider.OleDb;
				else
					throw new DatabaseNotSupportedException ("Only SQL Server and OleDb are valid database types");

				baseItf = (ISprocBase) SprocFactory.CreateInstance (itf, 
					prov);
				baseItf.Connection = conn;
			}
			catch(Exception ex)
			{				
				throw ex;
			}
			return baseItf;
		}


		private static void ConfigureAutoClose (ISprocBase sproc, bool autoClose) 
{
			sproc.AutoClose = autoClose;
		}

		/// <summary>
		/// Ensures the interface type derives from ISprocBase
		/// </summary>
		/// <param name="itf"></param>
		private static void VerifyBaseInterface (Type itf) {
			Type baseInterfaceType = typeof (ISprocBase);

			//make sure they remembered to derive they're interface from the base interface type
			Type baseitf = itf.GetInterface (baseInterfaceType.FullName);

			if (baseitf != baseInterfaceType) {
				string msg = string.Format ("interface {0} must inherit from base interface {1}", itf.FullName, baseInterfaceType.FullName);
				throw new IllegalBaseInterfaceException (msg);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="itf"></param>
		/// <param name="prov"></param>
		/// <param name="autoClose"></param>
		/// <returns></returns>
		public static object CreateInstance (Type itf, DBProvider prov, bool 
autoClose) {
			object target = null;

			try
			{
				//check the cache to see if we have built a class of this type before
				//if so, simply create a new instance of it and return it
				Type typeToCreate = (Type) SprocFactory._previousImpls 
					[itf.FullName+prov.ToString()];
				if (typeToCreate != null) 
				{
					target = Activator.CreateInstance (typeToCreate);

					//honour autoClose setting on new instance
					ConfigureAutoClose ((ISprocBase) target, autoClose);
					return target;
				}

				//now make sure they remembered to derive from ISprocBase
				//will throw an exception if they forgot
				VerifyBaseInterface (itf);

				//could be multiple threads surfing in at this point
				//need to protect the statics
				lock (typeof (SprocFactory)) 
				{
					//see if the assembly+module has been created and if not then create it
					if (null == SprocFactory._asmBuilder)
						SprocFactory.ConstructAssemblyModuleHelper ();

					//check the cache again to see if someone just added this one...
					typeToCreate = (Type) SprocFactory._previousImpls 
						[itf.FullName+prov.ToString()];

					if (null == typeToCreate) 
					{
						//go ahead and build the type
						TypeBuilder tb = SprocFactory._modBuilder.DefineType (itf.FullName + 
							prov + "Impl",
							TypeAttributes.Class |
							TypeAttributes.Public,
							typeof (SprocBase));

						//flesh out the method impls
						SprocFactory.ImplementDerivedInterfaceHelper (tb, itf, prov);

						//cook the type
						typeToCreate = tb.CreateType();

						//cache the implementation away for the future
						SprocFactory._previousImpls.Add (itf.FullName+prov.ToString (), 
							typeToCreate);
						System.Diagnostics.Debug.WriteLine (string.Format ("Emitted class: {0}", itf.FullName + prov + "Impl"));
					}
				}

				//create an instance of this type
				target = Activator.CreateInstance(typeToCreate);

				//set AutoClose bit
				ConfigureAutoClose ((ISprocBase) target, autoClose);
			}
			catch(Exception ex)
			{			
				throw ex;
			}
			return target;
		}

		/// <summary>
		/// Convenient overload,automatically sets the AutoClose feature on
		/// </summary>
		/// <param name="itf">The interface the emitted class will implement</param>
		/// <param name="prov">The underlying resource manager to use: SQL Server and OleDb are the only ones supported</param>
		/// <returns>Newly created (emitted) object</returns>
		public static object CreateInstance (Type itf, DBProvider prov) {
			ISprocBase baseItf = (ISprocBase) SprocFactory.CreateInstance (itf, prov, true);
			return baseItf;
			
		}		
	}

	public class Utilities
	{
		private static Utilities instance;

		public static Utilities GetInstance()
		{
			if(instance==null)
			{
				lock(typeof(Utilities))
				{
					if(instance==null)
					{
						instance=new Utilities();
					}
				}
			}
			return instance;
		}

		public System.Data.DataSet ExecuteSearch(IDbConnection connection, string spName,System.Collections.Hashtable parameterInfo)
		{		
			DataSet dsResult=new DataSet();					
			SqlCommand command=new SqlCommand();
			command.CommandType=CommandType.StoredProcedure ;
			command.CommandText=spName;
			command.Connection=(SqlConnection)connection;

			if (parameterInfo != null)
			{
				IEnumerator  enumerator=parameterInfo.Keys.GetEnumerator() ;
				string key;
				while (enumerator.MoveNext())
				{				
					key = (string) enumerator.Current;
					SqlParameter parameter=new SqlParameter("@"+key,SqlDbType.VarChar );
					parameter.Value=parameterInfo[key].ToString();
					command.Parameters.Add(parameter);
				}
			}

			SqlDataAdapter adapter=new SqlDataAdapter(command);
			adapter.Fill(dsResult);

			return dsResult;
		}

		public System.Data.DataSet ExecuteSearch(IDbConnection connection, string spName,System.Collections.Hashtable parameterInfo,DataSet dsResult)
		{		
			SqlCommand command=new SqlCommand();
			command.CommandType=CommandType.StoredProcedure ;
			command.CommandText=spName;
			command.Connection=(SqlConnection)connection;
			if (parameterInfo != null)
			{
				IEnumerator  enumerator=parameterInfo.Keys.GetEnumerator() ;
				string key;
				while (enumerator.MoveNext())
				{				
					key = (string) enumerator.Current;
					SqlParameter parameter=new SqlParameter("@"+key,SqlDbType.VarChar );
					parameter.Value=parameterInfo[key].ToString();
					command.Parameters.Add(parameter);
				}
			}
			SqlDataAdapter adapter=new SqlDataAdapter(command);
			adapter.Fill(dsResult);

			return dsResult;
		}
	}

}