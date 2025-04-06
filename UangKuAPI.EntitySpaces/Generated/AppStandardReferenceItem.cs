
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/27/2024 11:26:17 PM
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
	/// Encapsulates the 'appstandardreferenceitem' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Appstandardreferenceitem))]	
	[XmlType("Appstandardreferenceitem")]
	public partial class Appstandardreferenceitem : esAppstandardreferenceitem
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Appstandardreferenceitem();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("AppstandardreferenceitemCollection")]
	public partial class AppstandardreferenceitemCollection : esAppstandardreferenceitemCollection, IEnumerable<Appstandardreferenceitem>
	{

		
				
	}



	[Serializable]	
	public partial class AppstandardreferenceitemQuery : esAppstandardreferenceitemQuery
	{
		public AppstandardreferenceitemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public AppstandardreferenceitemQuery(string joinAlias, out AppstandardreferenceitemQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppstandardreferenceitemQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(AppstandardreferenceitemQuery query)
		{
			return AppstandardreferenceitemQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator AppstandardreferenceitemQuery(string query)
		{
			return (AppstandardreferenceitemQuery)AppstandardreferenceitemQuery.SerializeHelper.FromXml(query, typeof(AppstandardreferenceitemQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esAppstandardreferenceitem : esEntity
	{
		public esAppstandardreferenceitem()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string referenceID, string itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string referenceID, string itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(referenceID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(referenceID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(string referenceID, string itemID)
		{
			AppstandardreferenceitemQuery query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
			query.Where(query.StandardReferenceID == referenceID, query.ItemID == itemID);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string referenceID, string itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("StandardReferenceID", referenceID);
			parms.Add("ItemID", itemID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to appstandardreferenceitem.StandardReferenceID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string StandardReferenceID
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceitemMetadata.ColumnNames.StandardReferenceID);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceitemMetadata.ColumnNames.StandardReferenceID, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.StandardReferenceID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.ItemID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ItemID
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceitemMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceitemMetadata.ColumnNames.ItemID, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.ItemID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.ItemName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ItemName
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceitemMetadata.ColumnNames.ItemName);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceitemMetadata.ColumnNames.ItemName, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.ItemName);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.Note
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Note
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceitemMetadata.ColumnNames.Note);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceitemMetadata.ColumnNames.Note, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.Note);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsUsedBySystem
		{
			get
			{
				return base.GetSystemInt32(AppstandardreferenceitemMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				if(base.SetSystemInt32(AppstandardreferenceitemMetadata.ColumnNames.IsUsedBySystem, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.IsUsedBySystem);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsActive
		{
			get
			{
				return base.GetSystemInt32(AppstandardreferenceitemMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				if(base.SetSystemInt32(AppstandardreferenceitemMetadata.ColumnNames.IsActive, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.IsActive);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to appstandardreferenceitem.ItemIcon
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public byte[] ItemIcon
		{
			get
			{
				return base.GetSystemByteArray(AppstandardreferenceitemMetadata.ColumnNames.ItemIcon);
			}
			
			set
			{
				if(base.SetSystemByteArray(AppstandardreferenceitemMetadata.ColumnNames.ItemIcon, value))
				{
					OnPropertyChanged(AppstandardreferenceitemMetadata.PropertyNames.ItemIcon);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceitemMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public AppstandardreferenceitemQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppstandardreferenceitemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(AppstandardreferenceitemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppstandardreferenceitemQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private AppstandardreferenceitemQuery query;		
	}



	[Serializable]
	abstract public partial class esAppstandardreferenceitemCollection : esEntityCollection<Appstandardreferenceitem>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceitemMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "AppstandardreferenceitemCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public AppstandardreferenceitemQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(AppstandardreferenceitemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(AppstandardreferenceitemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((AppstandardreferenceitemQuery)query);
		}

		#endregion
		
		private AppstandardreferenceitemQuery query;
	}



	[Serializable]
	abstract public partial class esAppstandardreferenceitemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppstandardreferenceitemMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StandardReferenceID": return StandardReferenceID;
				case "ItemID": return ItemID;
				case "ItemName": return ItemName;
				case "Note": return Note;
				case "IsUsedBySystem": return IsUsedBySystem;
				case "IsActive": return IsActive;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;
				case "ItemIcon": return ItemIcon;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StandardReferenceID
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.StandardReferenceID, esSystemType.String); }
		} 
		
		public esQueryItem ItemID
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.ItemID, esSystemType.String); }
		} 
		
		public esQueryItem ItemName
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.ItemName, esSystemType.String); }
		} 
		
		public esQueryItem Note
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.Note, esSystemType.String); }
		} 
		
		public esQueryItem IsUsedBySystem
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.IsUsedBySystem, esSystemType.Int32); }
		} 
		
		public esQueryItem IsActive
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.IsActive, esSystemType.Int32); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem ItemIcon
		{
			get { return new esQueryItem(this, AppstandardreferenceitemMetadata.ColumnNames.ItemIcon, esSystemType.ByteArray); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class AppstandardreferenceitemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppstandardreferenceitemMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.StandardReferenceID, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.StandardReferenceID;
			c.CharacterMaxLength = 30;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ItemID, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ItemID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ItemName, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Note, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsUsedBySystem, 4, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsUsedBySystem;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsActive, 5, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsActive;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 6, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 7, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ItemIcon, 8, typeof(byte[]), esSystemType.ByteArray);
			c.PropertyName = PropertyNames.ItemIcon;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public AppstandardreferenceitemMetadata Meta()
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
			 public const string ItemID = "ItemID";
			 public const string ItemName = "ItemName";
			 public const string Note = "Note";
			 public const string IsUsedBySystem = "IsUsedBySystem";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ItemIcon = "ItemIcon";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StandardReferenceID = "StandardReferenceID";
			 public const string ItemID = "ItemID";
			 public const string ItemName = "ItemName";
			 public const string Note = "Note";
			 public const string IsUsedBySystem = "IsUsedBySystem";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string ItemIcon = "ItemIcon";
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
			lock (typeof(AppstandardreferenceitemMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new AppstandardreferenceitemMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ItemName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("IsActive", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ItemIcon", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));			
				
				
				
				meta.Source = "AppStandardReferenceItem";
				meta.Destination = "AppStandardReferenceItem";
				
				meta.spInsert = "proc_appstandardreferenceitemInsert";				
				meta.spUpdate = "proc_appstandardreferenceitemUpdate";		
				meta.spDelete = "proc_appstandardreferenceitemDelete";
				meta.spLoadAll = "proc_appstandardreferenceitemLoadAll";
				meta.spLoadByPrimaryKey = "proc_appstandardreferenceitemLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppstandardreferenceitemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
