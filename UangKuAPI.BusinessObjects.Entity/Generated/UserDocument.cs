
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 12/15/2024 2:12:26 PM
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
	/// Encapsulates the 'UserDocument' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(UserDocument))]	
	[XmlType("UserDocument")]
	public partial class UserDocument : esUserDocument
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new UserDocument();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.String documentID, System.String personID)
		{
			var obj = new UserDocument();
			obj.DocumentID = documentID;
			obj.PersonID = personID;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.String documentID, System.String personID, esSqlAccessType sqlAccessType)
		{
			var obj = new UserDocument();
			obj.DocumentID = documentID;
			obj.PersonID = personID;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("UserDocumentCollection")]
	public partial class UserDocumentCollection : esUserDocumentCollection, IEnumerable<UserDocument>
	{
		public UserDocument FindByPrimaryKey(System.String documentID, System.String personID)
		{
			return this.SingleOrDefault(e => e.DocumentID == documentID && e.PersonID == personID);
		}

		
				
	}



	[Serializable]	
	public partial class UserDocumentQuery : esUserDocumentQuery
	{
		public UserDocumentQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public UserDocumentQuery(string joinAlias, out UserDocumentQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UserDocumentQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserDocumentQuery query)
		{
			return UserDocumentQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserDocumentQuery(string query)
		{
			return (UserDocumentQuery)UserDocumentQuery.SerializeHelper.FromXml(query, typeof(UserDocumentQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUserDocument : esEntity
	{
		public esUserDocument()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String documentID, System.String personID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentID, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentID, personID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String documentID, System.String personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(documentID, personID);
			else
				return LoadByPrimaryKeyStoredProcedure(documentID, personID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String documentID, System.String personID)
		{
			UserDocumentQuery query = new UserDocumentQuery("UserDocument");
			query.Where(query.DocumentID == documentID, query.PersonID == personID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String documentID, System.String personID)
		{
			esParameters parms = new esParameters();
			parms.Add("DocumentID", documentID);			parms.Add("PersonID", personID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to UserDocument.DocumentID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DocumentID
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.DocumentID);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.DocumentID, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.DocumentID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PersonID
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.FileName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String FileName
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.FileName);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.FileName, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.FileName);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.FileExtention
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String FileExtention
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.FileExtention);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.FileExtention, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.FileExtention);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.Note
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.Note);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.Note, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.Note);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.DocumentDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? DocumentDate
		{
			get
			{
				return base.GetSystemDateTime(UserDocumentMetadata.ColumnNames.DocumentDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserDocumentMetadata.ColumnNames.DocumentDate, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.DocumentDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.FilePath
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String FilePath
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.FilePath);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.FilePath, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.FilePath);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.IsDeleted
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsDeleted
		{
			get
			{
				return base.GetSystemSByte(UserDocumentMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				if(base.SetSystemSByte(UserDocumentMetadata.ColumnNames.IsDeleted, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.IsDeleted);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserDocumentMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserDocumentMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.CreatedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserDocumentMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserDocumentMetadata.ColumnNames.CreatedDateTime, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.CreatedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to UserDocument.CreatedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(UserDocumentMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserDocumentMetadata.ColumnNames.CreatedByUserID, value))
				{
					OnPropertyChanged(UserDocumentMetadata.PropertyNames.CreatedByUserID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserDocumentMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserDocumentQuery("UserDocument");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserDocumentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(UserDocumentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((UserDocumentQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private UserDocumentQuery query;		
	}



	[Serializable]
	abstract public partial class esUserDocumentCollection : esEntityCollection<UserDocument>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserDocumentMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserDocumentCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public UserDocumentQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new UserDocumentQuery("UserDocument");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(UserDocumentQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new UserDocumentQuery("UserDocument");
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(UserDocumentQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((UserDocumentQuery)query);
		}

		#endregion
		
		private UserDocumentQuery query;
	}



	[Serializable]
	abstract public partial class esUserDocumentQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserDocumentMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "DocumentID": return this.DocumentID;
				case "PersonID": return this.PersonID;
				case "FileName": return this.FileName;
				case "FileExtention": return this.FileExtention;
				case "Note": return this.Note;
				case "DocumentDate": return this.DocumentDate;
				case "FilePath": return this.FilePath;
				case "IsDeleted": return this.IsDeleted;
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "LastUpdateByUserID": return this.LastUpdateByUserID;
				case "CreatedDateTime": return this.CreatedDateTime;
				case "CreatedByUserID": return this.CreatedByUserID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem DocumentID
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.DocumentID, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		public esQueryItem FileName
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.FileName, esSystemType.String); }
		} 
		
		public esQueryItem FileExtention
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.FileExtention, esSystemType.String); }
		} 
		
		public esQueryItem Note
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.Note, esSystemType.String); }
		} 
		
		public esQueryItem DocumentDate
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.DocumentDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem FilePath
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.FilePath, esSystemType.String); }
		} 
		
		public esQueryItem IsDeleted
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.IsDeleted, esSystemType.SByte); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem CreatedDateTime
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByUserID
		{
			get { return new esQueryItem(this, UserDocumentMetadata.ColumnNames.CreatedByUserID, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class UserDocumentMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserDocumentMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.DocumentID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.DocumentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.PersonID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.FileName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.FileName;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.FileExtention, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.FileExtention;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.Note, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.DocumentDate, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserDocumentMetadata.PropertyNames.DocumentDate;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.FilePath, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.FilePath;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.IsDeleted, 7, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = UserDocumentMetadata.PropertyNames.IsDeleted;
			c.NumericPrecision = 1;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserDocumentMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.CreatedDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = UserDocumentMetadata.PropertyNames.CreatedDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(UserDocumentMetadata.ColumnNames.CreatedByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = UserDocumentMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserDocumentMetadata Meta()
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
			 public const string DocumentID = "DocumentID";
			 public const string PersonID = "PersonID";
			 public const string FileName = "FileName";
			 public const string FileExtention = "FileExtention";
			 public const string Note = "Note";
			 public const string DocumentDate = "DocumentDate";
			 public const string FilePath = "FilePath";
			 public const string IsDeleted = "IsDeleted";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DocumentID = "DocumentID";
			 public const string PersonID = "PersonID";
			 public const string FileName = "FileName";
			 public const string FileExtention = "FileExtention";
			 public const string Note = "Note";
			 public const string DocumentDate = "DocumentDate";
			 public const string FilePath = "FilePath";
			 public const string IsDeleted = "IsDeleted";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(UserDocumentMetadata))
			{
				if(UserDocumentMetadata.mapDelegates == null)
				{
					UserDocumentMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (UserDocumentMetadata.meta == null)
				{
					UserDocumentMetadata.meta = new UserDocumentMetadata();
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


				meta.AddTypeMap("DocumentID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("FileName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("FileExtention", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("DocumentDate", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("FilePath", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsDeleted", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "UserDocument";
				meta.Destination = "UserDocument";
				
				meta.spInsert = "proc_UserDocumentInsert";				
				meta.spUpdate = "proc_UserDocumentUpdate";		
				meta.spDelete = "proc_UserDocumentDelete";
				meta.spLoadAll = "proc_UserDocumentLoadAll";
				meta.spLoadByPrimaryKey = "proc_UserDocumentLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserDocumentMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
