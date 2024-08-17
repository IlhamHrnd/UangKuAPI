
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/17/2024 2:40:41 PM
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
	/// Encapsulates the 'AppStandardReferenceItem' table
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
		public virtual bool LoadByPrimaryKey(System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID)
		{
			AppstandardreferenceitemQuery query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
			query.Where(query.ItemID == itemID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID", itemID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to AppStandardReferenceItem.StandardReferenceID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String StandardReferenceID
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
		/// Maps to AppStandardReferenceItem.ItemID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ItemID
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
		/// Maps to AppStandardReferenceItem.ItemName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ItemName
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
		/// Maps to AppStandardReferenceItem.Note
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Note
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
		/// Maps to AppStandardReferenceItem.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? IsUsedBySystem
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
		/// Maps to AppStandardReferenceItem.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? IsActive
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
		/// Maps to AppStandardReferenceItem.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
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
		/// Maps to AppStandardReferenceItem.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUserID
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
		/// Maps to AppStandardReferenceItem.ItemIcon
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] ItemIcon
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
				if (this.query == null)
				{
					this.query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppstandardreferenceitemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
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
			this.InitQuery((AppstandardreferenceitemQuery)query);
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
		[BrowsableAttribute(false)]
	#endif
		public AppstandardreferenceitemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
                    InitQuery(this.query);
				}

				return this.query;
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
			if (this.query == null)
			{
				this.query = new AppstandardreferenceitemQuery("Appstandardreferenceitem");
                this.InitQuery(query);
			}
			return this.query;
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
			this.InitQuery((AppstandardreferenceitemQuery)query);
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
				case "StandardReferenceID": return this.StandardReferenceID;
				case "ItemID": return this.ItemID;
				case "ItemName": return this.ItemName;
				case "Note": return this.Note;
				case "IsUsedBySystem": return this.IsUsedBySystem;
				case "IsActive": return this.IsActive;
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "LastUpdateByUserID": return this.LastUpdateByUserID;
				case "ItemIcon": return this.ItemIcon;

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

			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.StandardReferenceID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.StandardReferenceID;
			c.CharacterMaxLength = 30;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.ItemName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.ItemName;
			c.CharacterMaxLength = 200;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.Note, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.IsUsedBySystem, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.IsUsedBySystem;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.IsActive, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.IsActive;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppstandardreferenceitemMetadata.ColumnNames.ItemIcon, 8, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = AppstandardreferenceitemMetadata.PropertyNames.ItemIcon;
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
				if(AppstandardreferenceitemMetadata.mapDelegates == null)
				{
					AppstandardreferenceitemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppstandardreferenceitemMetadata.meta == null)
				{
					AppstandardreferenceitemMetadata.meta = new AppstandardreferenceitemMetadata();
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
