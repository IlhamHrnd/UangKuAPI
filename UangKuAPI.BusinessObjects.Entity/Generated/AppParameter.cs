
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/25/2024 12:40:46 PM
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



namespace UangKuAPI.BusinessObjects.Entity
{
	/// <summary>
	/// Encapsulates the 'AppParameter' table
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
		public virtual bool LoadByPrimaryKey(System.String parameterID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String parameterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(parameterID);
			else
				return LoadByPrimaryKeyStoredProcedure(parameterID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String parameterID)
		{
			AppparameterQuery query = new AppparameterQuery("Appparameter");
			query.Where(query.ParameterID == parameterID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String parameterID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParameterID", parameterID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to AppParameter.ParameterID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ParameterID
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
		/// Maps to AppParameter.ParameterName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ParameterName
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
		/// Maps to AppParameter.ParameterValue
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ParameterValue
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
		/// Maps to AppParameter.SRControl
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SRControl
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
		/// Maps to AppParameter.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
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
		/// Maps to AppParameter.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUserID
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
		/// Maps to AppParameter.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsUsedBySystem
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
				if (this.query == null)
				{
					this.query = new AppparameterQuery("Appparameter");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppparameterQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
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
			this.InitQuery((AppparameterQuery)query);
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
		[BrowsableAttribute(false)]
	#endif
		public AppparameterQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppparameterQuery("Appparameter");
                    InitQuery(this.query);
				}

				return this.query;
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
			if (this.query == null)
			{
				this.query = new AppparameterQuery("Appparameter");
                this.InitQuery(query);
			}
			return this.query;
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
			this.InitQuery((AppparameterQuery)query);
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
				case "ParameterID": return this.ParameterID;
				case "ParameterName": return this.ParameterName;
				case "ParameterValue": return this.ParameterValue;
				case "SRControl": return this.SRControl;
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "LastUpdateByUserID": return this.LastUpdateByUserID;
				case "IsUsedBySystem": return this.IsUsedBySystem;

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

			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.ParameterID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppparameterMetadata.PropertyNames.ParameterID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.ParameterName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppparameterMetadata.PropertyNames.ParameterName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.ParameterValue, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppparameterMetadata.PropertyNames.ParameterValue;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.SRControl, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppparameterMetadata.PropertyNames.SRControl;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppparameterMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = AppparameterMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppparameterMetadata.ColumnNames.IsUsedBySystem, 6, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppparameterMetadata.PropertyNames.IsUsedBySystem;
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
				if(AppparameterMetadata.mapDelegates == null)
				{
					AppparameterMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppparameterMetadata.meta == null)
				{
					AppparameterMetadata.meta = new AppparameterMetadata();
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
