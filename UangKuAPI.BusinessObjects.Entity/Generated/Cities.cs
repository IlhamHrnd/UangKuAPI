
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/18/2024 10:01:53 AM
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
	/// Encapsulates the 'Cities' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Cities))]	
	[XmlType("Cities")]
	public partial class Cities : esCities
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Cities();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
		
		override protected string GetConnectionName()
		{
			return "UangKuAPI.BusinessObjects.Entity";
		}			
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("CitiesCollection")]
	public partial class CitiesCollection : esCitiesCollection, IEnumerable<Cities>
	{

		
		
		override protected string GetConnectionName()
		{
			return "UangKuAPI.BusinessObjects.Entity";
		}		
	}



	[Serializable]	
	public partial class CitiesQuery : esCitiesQuery
	{
		public CitiesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public CitiesQuery(string joinAlias, out CitiesQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "CitiesQuery";
		}
		
		
		override protected string GetConnectionName()
		{
			return "UangKuAPI.BusinessObjects.Entity";
		}			
	
		#region Explicit Casts
		
		public static explicit operator string(CitiesQuery query)
		{
			return CitiesQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CitiesQuery(string query)
		{
			return (CitiesQuery)CitiesQuery.SerializeHelper.FromXml(query, typeof(CitiesQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCities : esEntity
	{
		public esCities()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String cityID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(cityID);
			else
				return LoadByPrimaryKeyStoredProcedure(cityID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String cityID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(cityID);
			else
				return LoadByPrimaryKeyStoredProcedure(cityID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String cityID)
		{
			CitiesQuery query = new CitiesQuery("Cities");
			query.Where(query.CityID == cityID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String cityID)
		{
			esParameters parms = new esParameters();
			parms.Add("CityID", cityID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to Cities.CityID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? CityID
		{
			get
			{
				return base.GetSystemInt32(CitiesMetadata.ColumnNames.CityID);
			}
			
			set
			{
				if(base.SetSystemInt32(CitiesMetadata.ColumnNames.CityID, value))
				{
					OnPropertyChanged(CitiesMetadata.PropertyNames.CityID);
				}
			}
		}
		
		/// <summary>
		/// Maps to Cities.CityName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CityName
		{
			get
			{
				return base.GetSystemString(CitiesMetadata.ColumnNames.CityName);
			}
			
			set
			{
				if(base.SetSystemString(CitiesMetadata.ColumnNames.CityName, value))
				{
					OnPropertyChanged(CitiesMetadata.PropertyNames.CityName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Cities.ProvID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProvID
		{
			get
			{
				return base.GetSystemInt32(CitiesMetadata.ColumnNames.ProvID);
			}
			
			set
			{
				if(base.SetSystemInt32(CitiesMetadata.ColumnNames.ProvID, value))
				{
					OnPropertyChanged(CitiesMetadata.PropertyNames.ProvID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CitiesMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CitiesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CitiesQuery("Cities");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CitiesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(CitiesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CitiesQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private CitiesQuery query;		
	}



	[Serializable]
	abstract public partial class esCitiesCollection : esEntityCollection<Cities>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CitiesMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CitiesCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CitiesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CitiesQuery("Cities");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CitiesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CitiesQuery("Cities");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CitiesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CitiesQuery)query);
		}

		#endregion
		
		private CitiesQuery query;
	}



	[Serializable]
	abstract public partial class esCitiesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CitiesMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "CityID": return this.CityID;
				case "CityName": return this.CityName;
				case "ProvID": return this.ProvID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem CityID
		{
			get { return new esQueryItem(this, CitiesMetadata.ColumnNames.CityID, esSystemType.Int32); }
		} 
		
		public esQueryItem CityName
		{
			get { return new esQueryItem(this, CitiesMetadata.ColumnNames.CityName, esSystemType.String); }
		} 
		
		public esQueryItem ProvID
		{
			get { return new esQueryItem(this, CitiesMetadata.ColumnNames.ProvID, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class CitiesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CitiesMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CitiesMetadata.ColumnNames.CityID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CitiesMetadata.PropertyNames.CityID;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CitiesMetadata.ColumnNames.CityName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CitiesMetadata.PropertyNames.CityName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CitiesMetadata.ColumnNames.ProvID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CitiesMetadata.PropertyNames.ProvID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CitiesMetadata Meta()
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
			 public const string CityID = "CityID";
			 public const string CityName = "CityName";
			 public const string ProvID = "ProvID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string CityID = "CityID";
			 public const string CityName = "CityName";
			 public const string ProvID = "ProvID";
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
			lock (typeof(CitiesMetadata))
			{
				if(CitiesMetadata.mapDelegates == null)
				{
					CitiesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CitiesMetadata.meta == null)
				{
					CitiesMetadata.meta = new CitiesMetadata();
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


				meta.AddTypeMap("CityID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("CityName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ProvID", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "Cities";
				meta.Destination = "Cities";
				
				meta.spInsert = "proc_citiesInsert";				
				meta.spUpdate = "proc_citiesUpdate";		
				meta.spDelete = "proc_citiesDelete";
				meta.spLoadAll = "proc_citiesLoadAll";
				meta.spLoadByPrimaryKey = "proc_citiesLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CitiesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
