
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/16/2024 2:07:52 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;



namespace UangKuAPI.EntitySpaces.Generated
{
	/// <summary>
	/// Encapsulates the 'appparameter' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Appparameter))]	
	[XmlType("Appparameter")]
	public partial class Appparameter : esAppparameter
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Appparameter();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("AppparameterCollection")]
	public partial class AppparameterCollection : esAppparameterCollection, IEnumerable<Appparameter>
	{

		
				
	}



	[Serializable]	
	public partial class AppparameterQuery : esAppparameterQuery
	{
		public AppparameterQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public AppparameterQuery(string joinAlias, out AppparameterQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppparameterQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(AppparameterQuery query)
		{
			return AppparameterQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator AppparameterQuery(string query)
		{
			return (AppparameterQuery)AppparameterQuery.SerializeHelper.FromXml(query, typeof(AppparameterQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esAppparameter : esEntity
	{
		public esAppparameter()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string parameterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string parameterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
		}

		private bool LoadByPrimaryKeyDynamic(string parameterID)
		{
			AppparameterQuery query = new AppparameterQuery("Appparameter");
			query.Where(query.ParameterID == parameterID);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string parameterID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParameterID", parameterID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to appparameter.ParameterID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ParameterID
		{
			get
			{
				return base.GetSystemString(AppparameterMetadata.ColumnNames.ParameterID);
			}
			
			set
			{
				if(base.SetSystemString(AppparameterMetadata.ColumnNames.ParameterID, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.ParameterID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.ParameterName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ParameterName
		{
			get
			{
				return base.GetSystemString(AppparameterMetadata.ColumnNames.ParameterName);
			}
			
			set
			{
				if(base.SetSystemString(AppparameterMetadata.ColumnNames.ParameterName, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.ParameterName);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.ParameterValue
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ParameterValue
		{
			get
			{
				return base.GetSystemString(AppparameterMetadata.ColumnNames.ParameterValue);
			}
			
			set
			{
				if(base.SetSystemString(AppparameterMetadata.ColumnNames.ParameterValue, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.ParameterValue);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.SRControl
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRControl
		{
			get
			{
				return base.GetSystemString(AppparameterMetadata.ColumnNames.SRControl);
			}
			
			set
			{
				if(base.SetSystemString(AppparameterMetadata.ColumnNames.SRControl, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.SRControl);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppparameterMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(AppparameterMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppparameterMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(AppparameterMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appparameter.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public sbyte? IsUsedBySystem
		{
			get
			{
				return base.GetSystemSByte(AppparameterMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				if(base.SetSystemSByte(AppparameterMetadata.ColumnNames.IsUsedBySystem, value))
				{
					OnPropertyChanged(AppparameterMetadata.PropertyNames.IsUsedBySystem);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return AppparameterMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public AppparameterQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppparameterQuery("Appparameter");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppparameterQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(AppparameterQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppparameterQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private AppparameterQuery query;		
	}



	[Serializable]
	abstract public partial class esAppparameterCollection : esEntityCollection<Appparameter>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppparameterMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "AppparameterCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public AppparameterQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppparameterQuery("Appparameter");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppparameterQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new AppparameterQuery("Appparameter");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(AppparameterQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppparameterQuery)query);
		}

		#endregion
		
		private AppparameterQuery query;
	}



	[Serializable]
	abstract public partial class esAppparameterQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppparameterMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ParameterID": return ParameterID;
				case "ParameterName": return ParameterName;
				case "ParameterValue": return ParameterValue;
				case "SRControl": return SRControl;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;
				case "IsUsedBySystem": return IsUsedBySystem;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ParameterID
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.ParameterID, esSystemType.String); }
		} 
		
		public esQueryItem ParameterName
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.ParameterName, esSystemType.String); }
		} 
		
		public esQueryItem ParameterValue
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.ParameterValue, esSystemType.String); }
		} 
		
		public esQueryItem SRControl
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.SRControl, esSystemType.String); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem IsUsedBySystem
		{
			get { return new esQueryItem(this, AppparameterMetadata.ColumnNames.IsUsedBySystem, esSystemType.SByte); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class AppparameterMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppparameterMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.ParameterID, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ParameterID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ParameterName, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ParameterName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ParameterValue, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ParameterValue;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRControl, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRControl;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 4, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 5, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsUsedBySystem, 6, typeof(sbyte), esSystemType.SByte);
			c.PropertyName = PropertyNames.IsUsedBySystem;
			c.NumericPrecision = 1;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public AppparameterMetadata Meta()
		{
			return meta;
		}	
		
		public Guid DataID
		{
			get { return base.m_dataID; }
		}	
		
		public bool MultiProviderMode
		{
			get { return false; }
		}		

		public esColumnMetadataCollection Columns
		{
			get	{ return base.m_columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string ParameterID = "ParameterID";
			 public const string ParameterName = "ParameterName";
			 public const string ParameterValue = "ParameterValue";
			 public const string SRControl = "SRControl";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsUsedBySystem = "IsUsedBySystem";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ParameterID = "ParameterID";
			 public const string ParameterName = "ParameterName";
			 public const string ParameterValue = "ParameterValue";
			 public const string SRControl = "SRControl";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsUsedBySystem = "IsUsedBySystem";
		}
		#endregion	

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}
		
		#region MAP esDefault
		
		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(AppparameterMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new AppparameterMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!m_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();			


				meta.AddTypeMap("ParameterID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ParameterName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ParameterValue", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRControl", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("BIT", "System.SByte"));			
				
				
				
				meta.Source = "AppParameter";
				meta.Destination = "AppParameter";
				
				meta.spInsert = "proc_appparameterInsert";				
				meta.spUpdate = "proc_appparameterUpdate";		
				meta.spDelete = "proc_appparameterDelete";
				meta.spLoadAll = "proc_appparameterLoadAll";
				meta.spLoadByPrimaryKey = "proc_appparameterLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppparameterMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
