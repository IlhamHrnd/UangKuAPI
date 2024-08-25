
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/25/2024 1:22:34 PM
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
	/// Encapsulates the 'Districts' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Districts))]	
	[XmlType("Districts")]
	public partial class Districts : esDistricts
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Districts();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("DistrictsCollection")]
	public partial class DistrictsCollection : esDistrictsCollection, IEnumerable<Districts>
	{

		
				
	}



	[Serializable]	
	public partial class DistrictsQuery : esDistrictsQuery
	{
		public DistrictsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public DistrictsQuery(string joinAlias, out DistrictsQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "DistrictsQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(DistrictsQuery query)
		{
			return DistrictsQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator DistrictsQuery(string query)
		{
			return (DistrictsQuery)DistrictsQuery.SerializeHelper.FromXml(query, typeof(DistrictsQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esDistricts : esEntity
	{
		public esDistricts()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String districtID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(districtID);
			else
				return LoadByPrimaryKeyStoredProcedure(districtID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String districtID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(districtID);
			else
				return LoadByPrimaryKeyStoredProcedure(districtID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String districtID)
		{
			DistrictsQuery query = new DistrictsQuery("Districts");
			query.Where(query.DisID == districtID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String districtID)
		{
			esParameters parms = new esParameters();
			parms.Add("DisID", districtID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to Districts.DisID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? DisID
		{
			get
			{
				return base.GetSystemInt32(DistrictsMetadata.ColumnNames.DisID);
			}
			
			set
			{
				if(base.SetSystemInt32(DistrictsMetadata.ColumnNames.DisID, value))
				{
					OnPropertyChanged(DistrictsMetadata.PropertyNames.DisID);
				}
			}
		}
		
		/// <summary>
		/// Maps to Districts.DisName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DisName
		{
			get
			{
				return base.GetSystemString(DistrictsMetadata.ColumnNames.DisName);
			}
			
			set
			{
				if(base.SetSystemString(DistrictsMetadata.ColumnNames.DisName, value))
				{
					OnPropertyChanged(DistrictsMetadata.PropertyNames.DisName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Districts.CityID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? CityID
		{
			get
			{
				return base.GetSystemInt32(DistrictsMetadata.ColumnNames.CityID);
			}
			
			set
			{
				if(base.SetSystemInt32(DistrictsMetadata.ColumnNames.CityID, value))
				{
					OnPropertyChanged(DistrictsMetadata.PropertyNames.CityID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return DistrictsMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public DistrictsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DistrictsQuery("Districts");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DistrictsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(DistrictsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((DistrictsQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private DistrictsQuery query;		
	}



	[Serializable]
	abstract public partial class esDistrictsCollection : esEntityCollection<Districts>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DistrictsMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "DistrictsCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public DistrictsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DistrictsQuery("Districts");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DistrictsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DistrictsQuery("Districts");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(DistrictsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((DistrictsQuery)query);
		}

		#endregion
		
		private DistrictsQuery query;
	}



	[Serializable]
	abstract public partial class esDistrictsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DistrictsMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "DisID": return this.DisID;
				case "DisName": return this.DisName;
				case "CityID": return this.CityID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem DisID
		{
			get { return new esQueryItem(this, DistrictsMetadata.ColumnNames.DisID, esSystemType.Int32); }
		} 
		
		public esQueryItem DisName
		{
			get { return new esQueryItem(this, DistrictsMetadata.ColumnNames.DisName, esSystemType.String); }
		} 
		
		public esQueryItem CityID
		{
			get { return new esQueryItem(this, DistrictsMetadata.ColumnNames.CityID, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class DistrictsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DistrictsMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DistrictsMetadata.ColumnNames.DisID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DistrictsMetadata.PropertyNames.DisID;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DistrictsMetadata.ColumnNames.DisName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DistrictsMetadata.PropertyNames.DisName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DistrictsMetadata.ColumnNames.CityID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DistrictsMetadata.PropertyNames.CityID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public DistrictsMetadata Meta()
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
			 public const string DisID = "DisID";
			 public const string DisName = "DisName";
			 public const string CityID = "CityID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DisID = "DisID";
			 public const string DisName = "DisName";
			 public const string CityID = "CityID";
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
			lock (typeof(DistrictsMetadata))
			{
				if(DistrictsMetadata.mapDelegates == null)
				{
					DistrictsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DistrictsMetadata.meta == null)
				{
					DistrictsMetadata.meta = new DistrictsMetadata();
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


				meta.AddTypeMap("DisID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("DisName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("CityID", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "Districts";
				meta.Destination = "Districts";
				
				meta.spInsert = "proc_districtsInsert";				
				meta.spUpdate = "proc_districtsUpdate";		
				meta.spDelete = "proc_districtsDelete";
				meta.spLoadAll = "proc_districtsLoadAll";
				meta.spLoadByPrimaryKey = "proc_districtsLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DistrictsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
