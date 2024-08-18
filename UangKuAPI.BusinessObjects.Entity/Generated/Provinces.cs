
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/18/2024 10:03:59 AM
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
	/// Encapsulates the 'Provinces' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Provinces))]	
	[XmlType("Provinces")]
	public partial class Provinces : esProvinces
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Provinces();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("ProvincesCollection")]
	public partial class ProvincesCollection : esProvincesCollection, IEnumerable<Provinces>
	{

		
				
	}



	[Serializable]	
	public partial class ProvincesQuery : esProvincesQuery
	{
		public ProvincesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public ProvincesQuery(string joinAlias, out ProvincesQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ProvincesQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProvincesQuery query)
		{
			return ProvincesQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProvincesQuery(string query)
		{
			return (ProvincesQuery)ProvincesQuery.SerializeHelper.FromXml(query, typeof(ProvincesQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProvinces : esEntity
	{
		public esProvinces()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String provinsID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(provinsID);
			else
				return LoadByPrimaryKeyStoredProcedure(provinsID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String provinsID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(provinsID);
			else
				return LoadByPrimaryKeyStoredProcedure(provinsID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String provinsID)
		{
			ProvincesQuery query = new ProvincesQuery("Provinces");
			query.Where(query.ProvID == provinsID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String provinsID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProvinsID", provinsID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to Provinces.ProvID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProvID
		{
			get
			{
				return base.GetSystemInt32(ProvincesMetadata.ColumnNames.ProvID);
			}
			
			set
			{
				if(base.SetSystemInt32(ProvincesMetadata.ColumnNames.ProvID, value))
				{
					OnPropertyChanged(ProvincesMetadata.PropertyNames.ProvID);
				}
			}
		}
		
		/// <summary>
		/// Maps to Provinces.ProvName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProvName
		{
			get
			{
				return base.GetSystemString(ProvincesMetadata.ColumnNames.ProvName);
			}
			
			set
			{
				if(base.SetSystemString(ProvincesMetadata.ColumnNames.ProvName, value))
				{
					OnPropertyChanged(ProvincesMetadata.PropertyNames.ProvName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Provinces.LocationID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? LocationID
		{
			get
			{
				return base.GetSystemInt32(ProvincesMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				if(base.SetSystemInt32(ProvincesMetadata.ColumnNames.LocationID, value))
				{
					OnPropertyChanged(ProvincesMetadata.PropertyNames.LocationID);
				}
			}
		}
		
		/// <summary>
		/// Maps to Provinces.Status
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Status
		{
			get
			{
				return base.GetSystemInt32(ProvincesMetadata.ColumnNames.Status);
			}
			
			set
			{
				if(base.SetSystemInt32(ProvincesMetadata.ColumnNames.Status, value))
				{
					OnPropertyChanged(ProvincesMetadata.PropertyNames.Status);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProvincesMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProvincesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProvincesQuery("Provinces");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProvincesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(ProvincesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProvincesQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private ProvincesQuery query;		
	}



	[Serializable]
	abstract public partial class esProvincesCollection : esEntityCollection<Provinces>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProvincesMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProvincesCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProvincesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProvincesQuery("Provinces");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProvincesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProvincesQuery("Provinces");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProvincesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProvincesQuery)query);
		}

		#endregion
		
		private ProvincesQuery query;
	}



	[Serializable]
	abstract public partial class esProvincesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProvincesMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProvID": return this.ProvID;
				case "ProvName": return this.ProvName;
				case "LocationID": return this.LocationID;
				case "Status": return this.Status;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProvID
		{
			get { return new esQueryItem(this, ProvincesMetadata.ColumnNames.ProvID, esSystemType.Int32); }
		} 
		
		public esQueryItem ProvName
		{
			get { return new esQueryItem(this, ProvincesMetadata.ColumnNames.ProvName, esSystemType.String); }
		} 
		
		public esQueryItem LocationID
		{
			get { return new esQueryItem(this, ProvincesMetadata.ColumnNames.LocationID, esSystemType.Int32); }
		} 
		
		public esQueryItem Status
		{
			get { return new esQueryItem(this, ProvincesMetadata.ColumnNames.Status, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class ProvincesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProvincesMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProvincesMetadata.ColumnNames.ProvID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProvincesMetadata.PropertyNames.ProvID;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProvincesMetadata.ColumnNames.ProvName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProvincesMetadata.PropertyNames.ProvName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProvincesMetadata.ColumnNames.LocationID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProvincesMetadata.PropertyNames.LocationID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProvincesMetadata.ColumnNames.Status, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProvincesMetadata.PropertyNames.Status;
			c.NumericPrecision = 11;
			c.HasDefault = true;
			c.Default = @"1";
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProvincesMetadata Meta()
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
			 public const string ProvID = "ProvID";
			 public const string ProvName = "ProvName";
			 public const string LocationID = "LocationID";
			 public const string Status = "Status";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProvID = "ProvID";
			 public const string ProvName = "ProvName";
			 public const string LocationID = "LocationID";
			 public const string Status = "Status";
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
			lock (typeof(ProvincesMetadata))
			{
				if(ProvincesMetadata.mapDelegates == null)
				{
					ProvincesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProvincesMetadata.meta == null)
				{
					ProvincesMetadata.meta = new ProvincesMetadata();
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


				meta.AddTypeMap("ProvID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("ProvName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LocationID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("Status", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "Provinces";
				meta.Destination = "Provinces";
				
				meta.spInsert = "proc_provincesInsert";				
				meta.spUpdate = "proc_provincesUpdate";		
				meta.spDelete = "proc_provincesDelete";
				meta.spLoadAll = "proc_provincesLoadAll";
				meta.spLoadByPrimaryKey = "proc_provincesLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProvincesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
