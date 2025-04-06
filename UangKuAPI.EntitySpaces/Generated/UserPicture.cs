
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 10/13/2024 2:32:43 PM
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
	/// Encapsulates the 'userpicture' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Userpicture))]	
	[XmlType("Userpicture")]
	public partial class Userpicture : esUserpicture
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Userpicture();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("UserpictureCollection")]
	public partial class UserpictureCollection : esUserpictureCollection, IEnumerable<Userpicture>
	{

		
				
	}



	[Serializable]	
	public partial class UserpictureQuery : esUserpictureQuery
	{
		public UserpictureQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public UserpictureQuery(string joinAlias, out UserpictureQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UserpictureQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserpictureQuery query)
		{
			return UserpictureQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserpictureQuery(string query)
		{
			return (UserpictureQuery)UserpictureQuery.SerializeHelper.FromXml(query, typeof(UserpictureQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUserpicture : esEntity
	{
		public esUserpicture()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string pictureID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pictureID);
			else
				return LoadByPrimaryKeyStoredProcedure(pictureID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string pictureID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(pictureID);
			else
				return LoadByPrimaryKeyStoredProcedure(pictureID);
		}

		private bool LoadByPrimaryKeyDynamic(string pictureID)
		{
			UserpictureQuery query = new UserpictureQuery("Userpicture");
			query.Where(query.PictureID == pictureID);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string pictureID)
		{
			esParameters parms = new esParameters();
			parms.Add("PictureID", pictureID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to userpicture.PictureID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PictureID
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.PictureID);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.PictureID, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.PictureID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.Picture
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public byte[] Picture
		{
			get
			{
				return base.GetSystemByteArray(UserpictureMetadata.ColumnNames.Picture);
			}
			
			set
			{
				if(base.SetSystemByteArray(UserpictureMetadata.ColumnNames.Picture, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.Picture);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.PictureName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PictureName
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.PictureName);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.PictureName, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.PictureName);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.PictureFormat
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PictureFormat
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.PictureFormat);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.PictureFormat, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.PictureFormat);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PersonID
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.IsDeleted
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsDeleted
		{
			get
			{
				return base.GetSystemInt32(UserpictureMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				if(base.SetSystemInt32(UserpictureMetadata.ColumnNames.IsDeleted, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.IsDeleted);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.CreatedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string CreatedByUserID
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.CreatedByUserID, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.CreatedByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.CreatedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserpictureMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserpictureMetadata.ColumnNames.CreatedDateTime, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.CreatedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserpictureMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserpictureMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userpicture.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UserpictureMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserpictureMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(UserpictureMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserpictureMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserpictureQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserpictureQuery("Userpicture");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserpictureQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(UserpictureQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserpictureQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private UserpictureQuery query;		
	}



	[Serializable]
	abstract public partial class esUserpictureCollection : esEntityCollection<Userpicture>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserpictureMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserpictureCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public UserpictureQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserpictureQuery("Userpicture");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserpictureQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new UserpictureQuery("Userpicture");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(UserpictureQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserpictureQuery)query);
		}

		#endregion
		
		private UserpictureQuery query;
	}



	[Serializable]
	abstract public partial class esUserpictureQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserpictureMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "PictureID": return PictureID;
				case "Picture": return Picture;
				case "PictureName": return PictureName;
				case "PictureFormat": return PictureFormat;
				case "PersonID": return PersonID;
				case "IsDeleted": return IsDeleted;
				case "CreatedByUserID": return CreatedByUserID;
				case "CreatedDateTime": return CreatedDateTime;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem PictureID
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.PictureID, esSystemType.String); }
		} 
		
		public esQueryItem Picture
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.Picture, esSystemType.ByteArray); }
		} 
		
		public esQueryItem PictureName
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.PictureName, esSystemType.String); }
		} 
		
		public esQueryItem PictureFormat
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.PictureFormat, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		public esQueryItem IsDeleted
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.IsDeleted, esSystemType.Int32); }
		} 
		
		public esQueryItem CreatedByUserID
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.CreatedByUserID, esSystemType.String); }
		} 
		
		public esQueryItem CreatedDateTime
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, UserpictureMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class UserpictureMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserpictureMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.PictureID, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PictureID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Picture, 1, typeof(byte[]), esSystemType.ByteArray);
			c.PropertyName = PropertyNames.Picture;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PictureName, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PictureName;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PictureFormat, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PictureFormat;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PersonID, 4, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsDeleted, 5, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsDeleted;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedByUserID, 6, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedDateTime, 7, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.CreatedDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 8, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 9, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserpictureMetadata Meta()
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
			 public const string PictureID = "PictureID";
			 public const string Picture = "Picture";
			 public const string PictureName = "PictureName";
			 public const string PictureFormat = "PictureFormat";
			 public const string PersonID = "PersonID";
			 public const string IsDeleted = "IsDeleted";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PictureID = "PictureID";
			 public const string Picture = "Picture";
			 public const string PictureName = "PictureName";
			 public const string PictureFormat = "PictureFormat";
			 public const string PersonID = "PersonID";
			 public const string IsDeleted = "IsDeleted";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
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
			lock (typeof(UserpictureMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new UserpictureMetadata();
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


				meta.AddTypeMap("PictureID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Picture", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));
				meta.AddTypeMap("PictureName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PictureFormat", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "UserPicture";
				meta.Destination = "UserPicture";
				
				meta.spInsert = "proc_userpictureInsert";				
				meta.spUpdate = "proc_userpictureUpdate";		
				meta.spDelete = "proc_userpictureDelete";
				meta.spLoadAll = "proc_userpictureLoadAll";
				meta.spLoadByPrimaryKey = "proc_userpictureLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserpictureMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
