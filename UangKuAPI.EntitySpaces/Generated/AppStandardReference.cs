
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/16/2024 7:21:26 PM
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
	/// Encapsulates the 'appstandardreference' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Appstandardreference))]	
	[XmlType("Appstandardreference")]
	public partial class Appstandardreference : esAppstandardreference
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Appstandardreference();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("AppstandardreferenceCollection")]
	public partial class AppstandardreferenceCollection : esAppstandardreferenceCollection, IEnumerable<Appstandardreference>
	{

		
				
	}



	[Serializable]	
	public partial class AppstandardreferenceQuery : esAppstandardreferenceQuery
	{
		public AppstandardreferenceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public AppstandardreferenceQuery(string joinAlias, out AppstandardreferenceQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppstandardreferenceQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(AppstandardreferenceQuery query)
		{
			return AppstandardreferenceQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator AppstandardreferenceQuery(string query)
		{
			return (AppstandardreferenceQuery)AppstandardreferenceQuery.SerializeHelper.FromXml(query, typeof(AppstandardreferenceQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esAppstandardreference : esEntity
	{
		public esAppstandardreference()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string referenceID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string referenceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID);
		}

		private bool LoadByPrimaryKeyDynamic(string referenceID)
		{
			AppstandardreferenceQuery query = new AppstandardreferenceQuery("Appstandardreference");
			query.Where(query.StandardReferenceID == referenceID);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string referenceID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardReferenceID", referenceID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to appstandardreference.StandardReferenceID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string StandardReferenceID
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceMetadata.ColumnNames.StandardReferenceID, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.StandardReferenceID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.StandardReferenceName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string StandardReferenceName
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceMetadata.ColumnNames.StandardReferenceName);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceMetadata.ColumnNames.StandardReferenceName, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.StandardReferenceName);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.ItemLength
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? ItemLength
		{
			get
			{
				return base.GetSystemInt32(AppstandardreferenceMetadata.ColumnNames.ItemLength);
			}
			
			set
			{
				if(base.SetSystemInt32(AppstandardreferenceMetadata.ColumnNames.ItemLength, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.ItemLength);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsUsedBySystem
		{
			get
			{
				return base.GetSystemInt32(AppstandardreferenceMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				if(base.SetSystemInt32(AppstandardreferenceMetadata.ColumnNames.IsUsedBySystem, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.IsUsedBySystem);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsActive
		{
			get
			{
				return base.GetSystemInt32(AppstandardreferenceMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				if(base.SetSystemInt32(AppstandardreferenceMetadata.ColumnNames.IsActive, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.IsActive);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.Note
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Note
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceMetadata.ColumnNames.Note);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceMetadata.ColumnNames.Note, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.Note);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppstandardreferenceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(AppstandardreferenceMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreference.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(AppstandardreferenceMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public AppstandardreferenceQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppstandardreferenceQuery("Appstandardreference");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppstandardreferenceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(AppstandardreferenceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppstandardreferenceQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private AppstandardreferenceQuery query;		
	}



	[Serializable]
	abstract public partial class esAppstandardreferenceCollection : esEntityCollection<Appstandardreference>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "AppstandardreferenceCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public AppstandardreferenceQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppstandardreferenceQuery("Appstandardreference");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppstandardreferenceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new AppstandardreferenceQuery("Appstandardreference");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(AppstandardreferenceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppstandardreferenceQuery)query);
		}

		#endregion
		
		private AppstandardreferenceQuery query;
	}



	[Serializable]
	abstract public partial class esAppstandardreferenceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StandardReferenceID": return StandardReferenceID;
				case "StandardReferenceName": return StandardReferenceName;
				case "ItemLength": return ItemLength;
				case "IsUsedBySystem": return IsUsedBySystem;
				case "IsActive": return IsActive;
				case "Note": return Note;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StandardReferenceID
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.StandardReferenceID, esSystemType.String); }
		} 
		
		public esQueryItem StandardReferenceName
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.StandardReferenceName, esSystemType.String); }
		} 
		
		public esQueryItem ItemLength
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.ItemLength, esSystemType.Int32); }
		} 
		
		public esQueryItem IsUsedBySystem
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.IsUsedBySystem, esSystemType.Int32); }
		} 
		
		public esQueryItem IsActive
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.IsActive, esSystemType.Int32); }
		} 
		
		public esQueryItem Note
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.Note, esSystemType.String); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, AppstandardreferenceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class AppstandardreferenceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppstandardreferenceMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.StandardReferenceID, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.StandardReferenceID;
			c.CharacterMaxLength = 30;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.StandardReferenceName, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.StandardReferenceName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ItemLength, 2, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.ItemLength;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsUsedBySystem, 3, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsUsedBySystem;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsActive, 4, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsActive;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Note, 5, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 6, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 7, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public AppstandardreferenceMetadata Meta()
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
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string StandardReferenceName = "StandardReferenceName";
			 public const string ItemLength = "ItemLength";
			 public const string IsUsedBySystem = "IsUsedBySystem";
			 public const string IsActive = "IsActive";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string StandardReferenceName = "StandardReferenceName";
			 public const string ItemLength = "ItemLength";
			 public const string IsUsedBySystem = "IsUsedBySystem";
			 public const string IsActive = "IsActive";
			 public const string Note = "Note";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(AppstandardreferenceMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new AppstandardreferenceMetadata();
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


				meta.AddTypeMap("StandardReferenceID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("StandardReferenceName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ItemLength", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("Note", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "AppStandardReference";
				meta.Destination = "AppStandardReference";
				
				meta.spInsert = "proc_appstandardreferenceInsert";				
				meta.spUpdate = "proc_appstandardreferenceUpdate";		
				meta.spDelete = "proc_appstandardreferenceDelete";
				meta.spLoadAll = "proc_appstandardreferenceLoadAll";
				meta.spLoadByPrimaryKey = "proc_appstandardreferenceLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppstandardreferenceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
