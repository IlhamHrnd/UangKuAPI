
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/18/2024 10:02:37 AM
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
	/// Encapsulates the 'Profile' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Profile))]	
	[XmlType("Profile")]
	public partial class Profile : esProfile
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Profile();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("ProfileCollection")]
	public partial class ProfileCollection : esProfileCollection, IEnumerable<Profile>
	{

		
				
	}



	[Serializable]	
	public partial class ProfileQuery : esProfileQuery
	{
		public ProfileQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public ProfileQuery(string joinAlias, out ProfileQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ProfileQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProfileQuery query)
		{
			return ProfileQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProfileQuery(string query)
		{
			return (ProfileQuery)ProfileQuery.SerializeHelper.FromXml(query, typeof(ProfileQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProfile : esEntity
	{
		public esProfile()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String personID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String personID)
		{
			ProfileQuery query = new ProfileQuery("Profile");
			query.Where(query.PersonID == personID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String personID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to Profile.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PersonID
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.FirstName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String FirstName
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.FirstName);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.FirstName, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.FirstName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.MiddleName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String MiddleName
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.MiddleName);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.MiddleName, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.MiddleName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.LastName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastName
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.LastName);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.LastName, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.LastName);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.BirthDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? BirthDate
		{
			get
			{
				return base.GetSystemDateTime(ProfileMetadata.ColumnNames.BirthDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(ProfileMetadata.ColumnNames.BirthDate, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.BirthDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.PlaceOfBirth
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String PlaceOfBirth
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.PlaceOfBirth);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.PlaceOfBirth, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.PlaceOfBirth);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.Photo
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Byte[] Photo
		{
			get
			{
				return base.GetSystemByteArray(ProfileMetadata.ColumnNames.Photo);
			}
			
			set
			{
				if(base.SetSystemByteArray(ProfileMetadata.ColumnNames.Photo, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.Photo);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.Address
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Address
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.Address);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.Address, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.Address);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.Province
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Province
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.Province);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.Province, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.Province);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.City
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String City
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.City);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.City, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.City);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.Subdistrict
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Subdistrict
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.Subdistrict);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.Subdistrict, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.Subdistrict);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.District
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String District
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.District);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.District, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.District);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.PostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? PostalCode
		{
			get
			{
				return base.GetSystemInt32(ProfileMetadata.ColumnNames.PostalCode);
			}
			
			set
			{
				if(base.SetSystemInt32(ProfileMetadata.ColumnNames.PostalCode, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.PostalCode);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ProfileMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(ProfileMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to Profile.LastUpdateByUser
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUser
		{
			get
			{
				return base.GetSystemString(ProfileMetadata.ColumnNames.LastUpdateByUser);
			}
			
			set
			{
				if(base.SetSystemString(ProfileMetadata.ColumnNames.LastUpdateByUser, value))
				{
					OnPropertyChanged(ProfileMetadata.PropertyNames.LastUpdateByUser);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProfileMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProfileQuery("Profile");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProfileQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(ProfileQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProfileQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private ProfileQuery query;		
	}



	[Serializable]
	abstract public partial class esProfileCollection : esEntityCollection<Profile>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProfileMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProfileCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProfileQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProfileQuery("Profile");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProfileQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProfileQuery("Profile");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProfileQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProfileQuery)query);
		}

		#endregion
		
		private ProfileQuery query;
	}



	[Serializable]
	abstract public partial class esProfileQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProfileMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "PersonID": return this.PersonID;
				case "FirstName": return this.FirstName;
				case "MiddleName": return this.MiddleName;
				case "LastName": return this.LastName;
				case "BirthDate": return this.BirthDate;
				case "PlaceOfBirth": return this.PlaceOfBirth;
				case "Photo": return this.Photo;
				case "Address": return this.Address;
				case "Province": return this.Province;
				case "City": return this.City;
				case "Subdistrict": return this.Subdistrict;
				case "District": return this.District;
				case "PostalCode": return this.PostalCode;
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "LastUpdateByUser": return this.LastUpdateByUser;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		public esQueryItem FirstName
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.FirstName, esSystemType.String); }
		} 
		
		public esQueryItem MiddleName
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.MiddleName, esSystemType.String); }
		} 
		
		public esQueryItem LastName
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.LastName, esSystemType.String); }
		} 
		
		public esQueryItem BirthDate
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.BirthDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem PlaceOfBirth
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.PlaceOfBirth, esSystemType.String); }
		} 
		
		public esQueryItem Photo
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.Photo, esSystemType.ByteArray); }
		} 
		
		public esQueryItem Address
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.Address, esSystemType.String); }
		} 
		
		public esQueryItem Province
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.Province, esSystemType.String); }
		} 
		
		public esQueryItem City
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.City, esSystemType.String); }
		} 
		
		public esQueryItem Subdistrict
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.Subdistrict, esSystemType.String); }
		} 
		
		public esQueryItem District
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.District, esSystemType.String); }
		} 
		
		public esQueryItem PostalCode
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.PostalCode, esSystemType.Int32); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUser
		{
			get { return new esQueryItem(this, ProfileMetadata.ColumnNames.LastUpdateByUser, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class ProfileMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProfileMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProfileMetadata.ColumnNames.PersonID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.FirstName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.FirstName;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.MiddleName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.MiddleName;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.LastName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.LastName;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.BirthDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProfileMetadata.PropertyNames.BirthDate;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.PlaceOfBirth, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.PlaceOfBirth;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.Photo, 6, typeof(System.Byte[]), esSystemType.ByteArray);
			c.PropertyName = ProfileMetadata.PropertyNames.Photo;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.Address, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.Address;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.Province, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.Province;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.City, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.City;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.Subdistrict, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.Subdistrict;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.District, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.District;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.PostalCode, 12, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProfileMetadata.PropertyNames.PostalCode;
			c.NumericPrecision = 11;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.LastUpdateDateTime, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProfileMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProfileMetadata.ColumnNames.LastUpdateByUser, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = ProfileMetadata.PropertyNames.LastUpdateByUser;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProfileMetadata Meta()
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
			 public const string PersonID = "PersonID";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string BirthDate = "BirthDate";
			 public const string PlaceOfBirth = "PlaceOfBirth";
			 public const string Photo = "Photo";
			 public const string Address = "Address";
			 public const string Province = "Province";
			 public const string City = "City";
			 public const string Subdistrict = "Subdistrict";
			 public const string District = "District";
			 public const string PostalCode = "PostalCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUser = "LastUpdateByUser";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PersonID = "PersonID";
			 public const string FirstName = "FirstName";
			 public const string MiddleName = "MiddleName";
			 public const string LastName = "LastName";
			 public const string BirthDate = "BirthDate";
			 public const string PlaceOfBirth = "PlaceOfBirth";
			 public const string Photo = "Photo";
			 public const string Address = "Address";
			 public const string Province = "Province";
			 public const string City = "City";
			 public const string Subdistrict = "Subdistrict";
			 public const string District = "District";
			 public const string PostalCode = "PostalCode";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUser = "LastUpdateByUser";
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
			lock (typeof(ProfileMetadata))
			{
				if(ProfileMetadata.mapDelegates == null)
				{
					ProfileMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProfileMetadata.meta == null)
				{
					ProfileMetadata.meta = new ProfileMetadata();
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


				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("FirstName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("MiddleName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("BirthDate", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("PlaceOfBirth", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Photo", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));
				meta.AddTypeMap("Address", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Province", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("City", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Subdistrict", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("District", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PostalCode", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUser", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "Profile";
				meta.Destination = "Profile";
				
				meta.spInsert = "proc_profileInsert";				
				meta.spUpdate = "proc_profileUpdate";		
				meta.spDelete = "proc_profileDelete";
				meta.spLoadAll = "proc_profileLoadAll";
				meta.spLoadByPrimaryKey = "proc_profileLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProfileMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
