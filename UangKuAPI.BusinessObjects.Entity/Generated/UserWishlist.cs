
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/17/2024 9:50:25 PM
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
	/// Encapsulates the 'UserWishlist' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Userwishlist))]	
	[XmlType("Userwishlist")]
	public partial class Userwishlist : esUserwishlist
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Userwishlist();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("UserwishlistCollection")]
	public partial class UserwishlistCollection : esUserwishlistCollection, IEnumerable<Userwishlist>
	{

		
				
	}



	[Serializable]	
	public partial class UserwishlistQuery : esUserwishlistQuery
	{
		public UserwishlistQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public UserwishlistQuery(string joinAlias, out UserwishlistQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UserwishlistQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserwishlistQuery query)
		{
			return UserwishlistQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserwishlistQuery(string query)
		{
			return (UserwishlistQuery)UserwishlistQuery.SerializeHelper.FromXml(query, typeof(UserwishlistQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUserwishlist : esEntity
	{
		public esUserwishlist()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String wishlistID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wishlistID);
			else
				return LoadByPrimaryKeyStoredProcedure(wishlistID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String wishlistID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(wishlistID);
			else
				return LoadByPrimaryKeyStoredProcedure(wishlistID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String wishlistID)
		{
			UserwishlistQuery query = new UserwishlistQuery("Userwishlist");
			query.Where(query.WishlistID == wishlistID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String wishlistID)
		{
			esParameters parms = new esParameters();
			parms.Add("WishlistID", wishlistID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to UserWishlist.WishlistID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String WishlistID
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.WishlistID);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.WishlistID, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.WishlistID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PersonID
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.SRProductCategory
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SRProductCategory
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.SRProductCategory);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.SRProductCategory, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.SRProductCategory);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.ProductName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductName
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.ProductName);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.ProductName, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.ProductName);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.ProductQuantity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductQuantity
		{
			get
			{
				return base.GetSystemInt32(UserwishlistMetadata.ColumnNames.ProductQuantity);
			}
			
			set
			{
				if(base.SetSystemInt32(UserwishlistMetadata.ColumnNames.ProductQuantity, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.ProductQuantity);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.ProductPrice
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ProductPrice
		{
			get
			{
				return base.GetSystemDecimal(UserwishlistMetadata.ColumnNames.ProductPrice);
			}
			
			set
			{
				if(base.SetSystemDecimal(UserwishlistMetadata.ColumnNames.ProductPrice, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.ProductPrice);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.ProductLink
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductLink
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.ProductLink);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.ProductLink, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.ProductLink);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.CreatedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.CreatedByUserID, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.CreatedByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.CreatedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserwishlistMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserwishlistMetadata.ColumnNames.CreatedDateTime, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.CreatedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UserwishlistMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserwishlistMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserwishlistMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserwishlistMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.WishlistDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? WishlistDate
		{
			get
			{
				return base.GetSystemDateTime(UserwishlistMetadata.ColumnNames.WishlistDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserwishlistMetadata.ColumnNames.WishlistDate, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.WishlistDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.ProductPicture
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] ProductPicture
		{
			get
			{
				return base.GetSystemByteArray(UserwishlistMetadata.ColumnNames.ProductPicture);
			}
			
			set
			{
				if(base.SetSystemByteArray(UserwishlistMetadata.ColumnNames.ProductPicture, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.ProductPicture);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserWishlist.IsComplete
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? IsComplete
		{
			get
			{
				return base.GetSystemInt32(UserwishlistMetadata.ColumnNames.IsComplete);
			}
			
			set
			{
				if(base.SetSystemInt32(UserwishlistMetadata.ColumnNames.IsComplete, value))
				{
					OnPropertyChanged(UserwishlistMetadata.PropertyNames.IsComplete);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserwishlistMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserwishlistQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserwishlistQuery("Userwishlist");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserwishlistQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(UserwishlistQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((UserwishlistQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private UserwishlistQuery query;		
	}



	[Serializable]
	abstract public partial class esUserwishlistCollection : esEntityCollection<Userwishlist>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserwishlistMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserwishlistCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public UserwishlistQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserwishlistQuery("Userwishlist");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserwishlistQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserwishlistQuery("Userwishlist");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(UserwishlistQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((UserwishlistQuery)query);
		}

		#endregion
		
		private UserwishlistQuery query;
	}



	[Serializable]
	abstract public partial class esUserwishlistQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserwishlistMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "WishlistID": return this.WishlistID;
				case "PersonID": return this.PersonID;
				case "SRProductCategory": return this.SRProductCategory;
				case "ProductName": return this.ProductName;
				case "ProductQuantity": return this.ProductQuantity;
				case "ProductPrice": return this.ProductPrice;
				case "ProductLink": return this.ProductLink;
				case "CreatedByUserID": return this.CreatedByUserID;
				case "CreatedDateTime": return this.CreatedDateTime;
				case "LastUpdateByUserID": return this.LastUpdateByUserID;
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "WishlistDate": return this.WishlistDate;
				case "ProductPicture": return this.ProductPicture;
				case "IsComplete": return this.IsComplete;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem WishlistID
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.WishlistID, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		public esQueryItem SRProductCategory
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.SRProductCategory, esSystemType.String); }
		} 
		
		public esQueryItem ProductName
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.ProductName, esSystemType.String); }
		} 
		
		public esQueryItem ProductQuantity
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.ProductQuantity, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductPrice
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.ProductPrice, esSystemType.Decimal); }
		} 
		
		public esQueryItem ProductLink
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.ProductLink, esSystemType.String); }
		} 
		
		public esQueryItem CreatedByUserID
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.CreatedByUserID, esSystemType.String); }
		} 
		
		public esQueryItem CreatedDateTime
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem WishlistDate
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.WishlistDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem ProductPicture
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.ProductPicture, esSystemType.ByteArray); }
		} 
		
		public esQueryItem IsComplete
		{
			get { return new esQueryItem(this, UserwishlistMetadata.ColumnNames.IsComplete, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class UserwishlistMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserwishlistMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.WishlistID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.WishlistID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.PersonID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.SRProductCategory, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.SRProductCategory;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.ProductName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.ProductName;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.ProductQuantity, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = UserwishlistMetadata.PropertyNames.ProductQuantity;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.ProductPrice, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = UserwishlistMetadata.PropertyNames.ProductPrice;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.ProductLink, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.ProductLink;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.CreatedByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.CreatedDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserwishlistMetadata.PropertyNames.CreatedDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = UserwishlistMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserwishlistMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.WishlistDate, 11, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserwishlistMetadata.PropertyNames.WishlistDate;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.ProductPicture, 12, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = UserwishlistMetadata.PropertyNames.ProductPicture;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserwishlistMetadata.ColumnNames.IsComplete, 13, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = UserwishlistMetadata.PropertyNames.IsComplete;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserwishlistMetadata Meta()
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
			 public const string WishlistID = "WishlistID";
			 public const string PersonID = "PersonID";
			 public const string SRProductCategory = "SRProductCategory";
			 public const string ProductName = "ProductName";
			 public const string ProductQuantity = "ProductQuantity";
			 public const string ProductPrice = "ProductPrice";
			 public const string ProductLink = "ProductLink";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string WishlistDate = "WishlistDate";
			 public const string ProductPicture = "ProductPicture";
			 public const string IsComplete = "IsComplete";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string WishlistID = "WishlistID";
			 public const string PersonID = "PersonID";
			 public const string SRProductCategory = "SRProductCategory";
			 public const string ProductName = "ProductName";
			 public const string ProductQuantity = "ProductQuantity";
			 public const string ProductPrice = "ProductPrice";
			 public const string ProductLink = "ProductLink";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string WishlistDate = "WishlistDate";
			 public const string ProductPicture = "ProductPicture";
			 public const string IsComplete = "IsComplete";
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
			lock (typeof(UserwishlistMetadata))
			{
				if(UserwishlistMetadata.mapDelegates == null)
				{
					UserwishlistMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (UserwishlistMetadata.meta == null)
				{
					UserwishlistMetadata.meta = new UserwishlistMetadata();
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


				meta.AddTypeMap("WishlistID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRProductCategory", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ProductName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ProductQuantity", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("ProductPrice", new esTypeMap("DECIMAL", "System.Decimal"));
				meta.AddTypeMap("ProductLink", new esTypeMap("LONGTEXT", "System.String"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("WishlistDate", new esTypeMap("DATE", "System.DateTime"));
				meta.AddTypeMap("ProductPicture", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));
				meta.AddTypeMap("IsComplete", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "UserWishlist";
				meta.Destination = "UserWishlist";
				
				meta.spInsert = "proc_userwishlistInsert";				
				meta.spUpdate = "proc_userwishlistUpdate";		
				meta.spDelete = "proc_userwishlistDelete";
				meta.spLoadAll = "proc_userwishlistLoadAll";
				meta.spLoadByPrimaryKey = "proc_userwishlistLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserwishlistMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
