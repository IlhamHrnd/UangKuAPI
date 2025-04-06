
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 10/4/2024 7:50:56 PM
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
	/// Encapsulates the 'user' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(User))]	
	[XmlType("User")]
	public partial class User : esUser
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new User();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("UserCollection")]
	public partial class UserCollection : esUserCollection, IEnumerable<User>
	{

		
				
	}



	[Serializable]	
	public partial class UserQuery : esUserQuery
	{
		public UserQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public UserQuery(string joinAlias, out UserQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UserQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserQuery query)
		{
			return UserQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserQuery(string query)
		{
			return (UserQuery)UserQuery.SerializeHelper.FromXml(query, typeof(UserQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUser : esEntity
	{
		public esUser()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string username)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(username);
			else
				return LoadByPrimaryKeyStoredProcedure(username);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string username)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(username);
			else
				return LoadByPrimaryKeyStoredProcedure(username);
		}

		private bool LoadByPrimaryKeyDynamic(string username)
		{
			UserQuery query = new UserQuery("User");
			query.Where(query.Username == username);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string username)
		{
			esParameters parms = new esParameters();
			parms.Add("Username", username);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion

		#region LoadByPrimaryKey Username And Password
		public virtual bool LoadByPrimaryKey(string username, string password)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(username, password);
			else
				return LoadByPrimaryKeyStoredProcedure(username, password);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string username, string password)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(username, password);
			else
				return LoadByPrimaryKeyStoredProcedure(username, password);
		}
		private bool LoadByPrimaryKeyDynamic(string username, string password)
		{
			UserQuery query = new UserQuery("User");
			query.Where(query.Username == username, query.Password == password);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string username, string password)
		{
			esParameters parms = new esParameters();
			parms.Add("Username", username);
			parms.Add("Password", password);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to user.Username
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Username
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Username);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Username, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Username);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.Password
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Password
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Password);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Password, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Password);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.SRSex
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRSex
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.SRSex);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.SRSex, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.SRSex);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.SRAccess
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRAccess
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.SRAccess);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.SRAccess, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.SRAccess);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.Email
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Email
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.Email);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.Email, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.Email);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.SRStatus
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRStatus
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.SRStatus);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.SRStatus, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.SRStatus);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.ActiveDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? ActiveDate
		{
			get
			{
				return base.GetSystemDateTime(UserMetadata.ColumnNames.ActiveDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserMetadata.ColumnNames.ActiveDate, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.ActiveDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.LastLogin
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastLogin
		{
			get
			{
				return base.GetSystemDateTime(UserMetadata.ColumnNames.LastLogin);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserMetadata.ColumnNames.LastLogin, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.LastLogin);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.LastUpdateByUser
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUser
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.LastUpdateByUser);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.LastUpdateByUser, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.LastUpdateByUser);
				}
			}
		}
		
		/// <summary>
		/// Maps to user.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PersonID
		{
			get
			{
				return base.GetSystemString(UserMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(UserMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(UserMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserQuery("User");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(UserQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private UserQuery query;		
	}



	[Serializable]
	abstract public partial class esUserCollection : esEntityCollection<User>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public UserQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserQuery("User");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new UserQuery("User");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(UserQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserQuery)query);
		}

		#endregion
		
		private UserQuery query;
	}



	[Serializable]
	abstract public partial class esUserQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Username": return Username;
				case "Password": return Password;
				case "SRSex": return SRSex;
				case "SRAccess": return SRAccess;
				case "Email": return Email;
				case "SRStatus": return SRStatus;
				case "ActiveDate": return ActiveDate;
				case "LastLogin": return LastLogin;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUser": return LastUpdateByUser;
				case "PersonID": return PersonID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Username
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Username, esSystemType.String); }
		} 
		
		public esQueryItem Password
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Password, esSystemType.String); }
		} 
		
		public esQueryItem SRSex
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.SRSex, esSystemType.String); }
		} 
		
		public esQueryItem SRAccess
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.SRAccess, esSystemType.String); }
		} 
		
		public esQueryItem Email
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.Email, esSystemType.String); }
		} 
		
		public esQueryItem SRStatus
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.SRStatus, esSystemType.String); }
		} 
		
		public esQueryItem ActiveDate
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.ActiveDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastLogin
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.LastLogin, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUser
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.LastUpdateByUser, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, UserMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class UserMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.Username, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Username;
			c.CharacterMaxLength = 15;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Password, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Password;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRSex, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRSex;
			c.CharacterMaxLength = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRAccess, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRAccess;
			c.CharacterMaxLength = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Email, 4, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Email;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRStatus, 5, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRStatus;
			c.CharacterMaxLength = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ActiveDate, 6, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.ActiveDate;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastLogin, 7, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastLogin;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 8, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUser, 9, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUser;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PersonID, 10, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserMetadata Meta()
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
			 public const string Username = "Username";
			 public const string Password = "Password";
			 public const string SRSex = "SRSex";
			 public const string SRAccess = "SRAccess";
			 public const string Email = "Email";
			 public const string SRStatus = "SRStatus";
			 public const string ActiveDate = "ActiveDate";
			 public const string LastLogin = "LastLogin";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUser = "LastUpdateByUser";
			 public const string PersonID = "PersonID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Username = "Username";
			 public const string Password = "Password";
			 public const string SRSex = "SRSex";
			 public const string SRAccess = "SRAccess";
			 public const string Email = "Email";
			 public const string SRStatus = "SRStatus";
			 public const string ActiveDate = "ActiveDate";
			 public const string LastLogin = "LastLogin";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUser = "LastUpdateByUser";
			 public const string PersonID = "PersonID";
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
			lock (typeof(UserMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new UserMetadata();
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


				meta.AddTypeMap("Username", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Password", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRSex", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRAccess", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Email", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRStatus", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ActiveDate", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastLogin", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUser", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "User";
				meta.Destination = "User";
				
				meta.spInsert = "proc_userInsert";				
				meta.spUpdate = "proc_userUpdate";		
				meta.spDelete = "proc_userDelete";
				meta.spLoadAll = "proc_userLoadAll";
				meta.spLoadByPrimaryKey = "proc_userLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
