
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/28/2024 4:20:46 PM
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



namespace UangKuAPI.BusinessObjects.Entity.Generated
{
	/// <summary>
	/// Encapsulates the 'subdistricts' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Subdistricts))]	
	[XmlType("Subdistricts")]
	public partial class Subdistricts : esSubdistricts
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Subdistricts();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("SubdistrictsCollection")]
	public partial class SubdistrictsCollection : esSubdistrictsCollection, IEnumerable<Subdistricts>
	{

		
				
	}



	[Serializable]	
	public partial class SubdistrictsQuery : esSubdistrictsQuery
	{
		public SubdistrictsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public SubdistrictsQuery(string joinAlias, out SubdistrictsQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SubdistrictsQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(SubdistrictsQuery query)
		{
			return SubdistrictsQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator SubdistrictsQuery(string query)
		{
			return (SubdistrictsQuery)SubdistrictsQuery.SerializeHelper.FromXml(query, typeof(SubdistrictsQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esSubdistricts : esEntity
	{
		public esSubdistricts()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String subdisID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subdisID);
			else
				return LoadByPrimaryKeyStoredProcedure(subdisID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String subdisID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(subdisID);
			else
				return LoadByPrimaryKeyStoredProcedure(subdisID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String subdisID)
		{
			SubdistrictsQuery query = new SubdistrictsQuery("Subdistricts");
			query.Where(query.SubdisID == subdisID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String subdisID)
		{
			esParameters parms = new esParameters();
			parms.Add("SubdisID", subdisID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to subdistricts.SubdisID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? SubdisID
		{
			get
			{
				return base.GetSystemInt32(SubdistrictsMetadata.ColumnNames.SubdisID);
			}
			
			set
			{
				if(base.SetSystemInt32(SubdistrictsMetadata.ColumnNames.SubdisID, value))
				{
					OnPropertyChanged(SubdistrictsMetadata.PropertyNames.SubdisID);
				}
			}
		}
		
		/// <summary>
		/// Maps to subdistricts.SubdisName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SubdisName
		{
			get
			{
				return base.GetSystemString(SubdistrictsMetadata.ColumnNames.SubdisName);
			}
			
			set
			{
				if(base.SetSystemString(SubdistrictsMetadata.ColumnNames.SubdisName, value))
				{
					OnPropertyChanged(SubdistrictsMetadata.PropertyNames.SubdisName);
				}
			}
		}
		
		/// <summary>
		/// Maps to subdistricts.DisID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? DisID
		{
			get
			{
				return base.GetSystemInt32(SubdistrictsMetadata.ColumnNames.DisID);
			}
			
			set
			{
				if(base.SetSystemInt32(SubdistrictsMetadata.ColumnNames.DisID, value))
				{
					OnPropertyChanged(SubdistrictsMetadata.PropertyNames.DisID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return SubdistrictsMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public SubdistrictsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubdistrictsQuery("Subdistricts");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(SubdistrictsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(SubdistrictsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((SubdistrictsQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private SubdistrictsQuery query;		
	}



	[Serializable]
	abstract public partial class esSubdistrictsCollection : esEntityCollection<Subdistricts>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SubdistrictsMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "SubdistrictsCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public SubdistrictsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SubdistrictsQuery("Subdistricts");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(SubdistrictsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SubdistrictsQuery("Subdistricts");
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(SubdistrictsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((SubdistrictsQuery)query);
		}

		#endregion
		
		private SubdistrictsQuery query;
	}



	[Serializable]
	abstract public partial class esSubdistrictsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return SubdistrictsMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "SubdisID": return this.SubdisID;
				case "SubdisName": return this.SubdisName;
				case "DisID": return this.DisID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem SubdisID
		{
			get { return new esQueryItem(this, SubdistrictsMetadata.ColumnNames.SubdisID, esSystemType.Int32); }
		} 
		
		public esQueryItem SubdisName
		{
			get { return new esQueryItem(this, SubdistrictsMetadata.ColumnNames.SubdisName, esSystemType.String); }
		} 
		
		public esQueryItem DisID
		{
			get { return new esQueryItem(this, SubdistrictsMetadata.ColumnNames.DisID, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class SubdistrictsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SubdistrictsMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SubdistrictsMetadata.ColumnNames.SubdisID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubdistrictsMetadata.PropertyNames.SubdisID;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(SubdistrictsMetadata.ColumnNames.SubdisName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SubdistrictsMetadata.PropertyNames.SubdisName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(SubdistrictsMetadata.ColumnNames.DisID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SubdistrictsMetadata.PropertyNames.DisID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public SubdistrictsMetadata Meta()
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
			 public const string SubdisID = "SubdisID";
			 public const string SubdisName = "SubdisName";
			 public const string DisID = "DisID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string SubdisID = "SubdisID";
			 public const string SubdisName = "SubdisName";
			 public const string DisID = "DisID";
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
			lock (typeof(SubdistrictsMetadata))
			{
				if(SubdistrictsMetadata.mapDelegates == null)
				{
					SubdistrictsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (SubdistrictsMetadata.meta == null)
				{
					SubdistrictsMetadata.meta = new SubdistrictsMetadata();
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


				meta.AddTypeMap("SubdisID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("SubdisName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("DisID", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "Subdistricts";
				meta.Destination = "Subdistricts";
				
				meta.spInsert = "proc_subdistrictsInsert";				
				meta.spUpdate = "proc_subdistrictsUpdate";		
				meta.spDelete = "proc_subdistrictsDelete";
				meta.spLoadAll = "proc_subdistrictsLoadAll";
				meta.spLoadByPrimaryKey = "proc_subdistrictsLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SubdistrictsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
