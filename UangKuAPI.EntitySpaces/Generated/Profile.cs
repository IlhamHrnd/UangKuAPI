
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/29/2024 12:48:14 PM
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
	/// Encapsulates the 'profile' table
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
		public virtual bool LoadByPrimaryKey(string personID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string personID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID);
			else
				return LoadByPrimaryKeyStoredProcedure(personID);
		}

		private bool LoadByPrimaryKeyDynamic(string personID)
		{
			ProfileQuery query = new ProfileQuery("Profile");
			query.Where(query.PersonID == personID);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string personID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to profile.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PersonID
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
		/// Maps to profile.FirstName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string FirstName
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
		/// Maps to profile.MiddleName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string MiddleName
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
		/// Maps to profile.LastName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastName
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
		/// Maps to profile.BirthDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? BirthDate
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
		/// Maps to profile.PlaceOfBirth
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PlaceOfBirth
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
		/// Maps to profile.Photo
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public byte[] Photo
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
		/// Maps to profile.Address
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Address
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
		/// Maps to profile.Province
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Province
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
		/// Maps to profile.City
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string City
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
		/// Maps to profile.Subdistrict
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Subdistrict
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
		/// Maps to profile.District
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string District
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
		/// Maps to profile.PostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? PostalCode
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
		/// Maps to profile.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
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
		/// Maps to profile.LastUpdateByUser
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUser
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
				if (query == null)
				{
                    query = new ProfileQuery("Profile");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(ProfileQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
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
            InitQuery((ProfileQuery)query);
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
		[Browsable(false)]
	#endif
		public ProfileQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new ProfileQuery("Profile");
					InitQuery(query);
				}

				return query;
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
			if (query == null)
			{
                query = new ProfileQuery("Profile");
                InitQuery(query);
			}
			return query;
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
            InitQuery((ProfileQuery)query);
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
				case "PersonID": return PersonID;
				case "FirstName": return FirstName;
				case "MiddleName": return MiddleName;
				case "LastName": return LastName;
				case "BirthDate": return BirthDate;
				case "PlaceOfBirth": return PlaceOfBirth;
				case "Photo": return Photo;
				case "Address": return Address;
				case "Province": return Province;
				case "City": return City;
				case "Subdistrict": return Subdistrict;
				case "District": return District;
				case "PostalCode": return PostalCode;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUser": return LastUpdateByUser;

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

			c = new esColumnMetadata(ColumnNames.PersonID, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.FirstName, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.FirstName;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.MiddleName, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.MiddleName;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastName, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastName;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.BirthDate, 4, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.BirthDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PlaceOfBirth, 5, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PlaceOfBirth;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Photo, 6, typeof(byte[]), esSystemType.ByteArray);
			c.PropertyName = PropertyNames.Photo;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Address, 7, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Address;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Province, 8, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Province;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.City, 9, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.City;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Subdistrict, 10, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Subdistrict;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.District, 11, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.District;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PostalCode, 12, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.PostalCode;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 13, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUser, 14, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUser;
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
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new ProfileMetadata();
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
