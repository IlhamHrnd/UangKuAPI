
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 11/9/2024 7:43:03 PM
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
	/// Encapsulates the 'userreport' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Userreport))]	
	[XmlType("Userreport")]
	public partial class Userreport : esUserreport
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Userreport();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("UserreportCollection")]
	public partial class UserreportCollection : esUserreportCollection, IEnumerable<Userreport>
	{

		
				
	}



	[Serializable]	
	public partial class UserreportQuery : esUserreportQuery
	{
		public UserreportQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public UserreportQuery(string joinAlias, out UserreportQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "UserreportQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(UserreportQuery query)
		{
			return UserreportQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator UserreportQuery(string query)
		{
			return (UserreportQuery)UserreportQuery.SerializeHelper.FromXml(query, typeof(UserreportQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esUserreport : esEntity
	{
		public esUserreport()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string reportNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reportNo);
			else
				return LoadByPrimaryKeyStoredProcedure(reportNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string reportNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(reportNo);
			else
				return LoadByPrimaryKeyStoredProcedure(reportNo);
		}

		private bool LoadByPrimaryKeyDynamic(string reportNo)
		{
			UserreportQuery query = new UserreportQuery("Userreport");
			query.Where(query.ReportNo == reportNo);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string reportNo)
		{
			esParameters parms = new esParameters();
			parms.Add("ReportNo", reportNo);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to userreport.ReportNo
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ReportNo
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.ReportNo);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.ReportNo, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.ReportNo);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.DateErrorOccured
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? DateErrorOccured
		{
			get
			{
				return base.GetSystemDateTime(UserreportMetadata.ColumnNames.DateErrorOccured);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserreportMetadata.ColumnNames.DateErrorOccured, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.DateErrorOccured);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.SRErrorLocation
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRErrorLocation
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.SRErrorLocation);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.SRErrorLocation, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.SRErrorLocation);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.SRErrorPossibility
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRErrorPossibility
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.SRErrorPossibility);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.SRErrorPossibility, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.SRErrorPossibility);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.ErrorCronologic
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ErrorCronologic
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.ErrorCronologic);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.ErrorCronologic, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.ErrorCronologic);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.Picture
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public byte[] Picture
		{
			get
			{
				return base.GetSystemByteArray(UserreportMetadata.ColumnNames.Picture);
			}
			
			set
			{
				if(base.SetSystemByteArray(UserreportMetadata.ColumnNames.Picture, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.Picture);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.IsApprove
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public int? IsApprove
		{
			get
			{
				return base.GetSystemInt32(UserreportMetadata.ColumnNames.IsApprove);
			}
			
			set
			{
				if(base.SetSystemInt32(UserreportMetadata.ColumnNames.IsApprove, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.IsApprove);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.SRReportStatus
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRReportStatus
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.SRReportStatus);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.SRReportStatus, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.SRReportStatus);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.ApprovedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? ApprovedDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserreportMetadata.ColumnNames.ApprovedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserreportMetadata.ColumnNames.ApprovedDateTime, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.ApprovedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.ApprovedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string ApprovedByUserID
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.ApprovedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.ApprovedByUserID, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.ApprovedByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.VoidDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserreportMetadata.ColumnNames.VoidDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserreportMetadata.ColumnNames.VoidDateTime, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.VoidDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.VoidByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string VoidByUserID
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.VoidByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.VoidByUserID, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.VoidByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.CreatedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserreportMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserreportMetadata.ColumnNames.CreatedDateTime, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.CreatedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.CreatedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string CreatedByUserID
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.CreatedByUserID, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.CreatedByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(UserreportMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(UserreportMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to userreport.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PersonID
		{
			get
			{
				return base.GetSystemString(UserreportMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(UserreportMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(UserreportMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return UserreportMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public UserreportQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserreportQuery("Userreport");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserreportQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(UserreportQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserreportQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private UserreportQuery query;		
	}



	[Serializable]
	abstract public partial class esUserreportCollection : esEntityCollection<Userreport>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return UserreportMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "UserreportCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public UserreportQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new UserreportQuery("Userreport");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(UserreportQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new UserreportQuery("Userreport");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(UserreportQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((UserreportQuery)query);
		}

		#endregion
		
		private UserreportQuery query;
	}



	[Serializable]
	abstract public partial class esUserreportQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return UserreportMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ReportNo": return ReportNo;
				case "DateErrorOccured": return DateErrorOccured;
				case "SRErrorLocation": return SRErrorLocation;
				case "SRErrorPossibility": return SRErrorPossibility;
				case "ErrorCronologic": return ErrorCronologic;
				case "Picture": return Picture;
				case "IsApprove": return IsApprove;
				case "SRReportStatus": return SRReportStatus;
				case "ApprovedDateTime": return ApprovedDateTime;
				case "ApprovedByUserID": return ApprovedByUserID;
				case "VoidDateTime": return VoidDateTime;
				case "VoidByUserID": return VoidByUserID;
				case "CreatedDateTime": return CreatedDateTime;
				case "CreatedByUserID": return CreatedByUserID;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;
				case "PersonID": return PersonID;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ReportNo
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.ReportNo, esSystemType.String); }
		} 
		
		public esQueryItem DateErrorOccured
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.DateErrorOccured, esSystemType.DateTime); }
		} 
		
		public esQueryItem SRErrorLocation
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.SRErrorLocation, esSystemType.String); }
		} 
		
		public esQueryItem SRErrorPossibility
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.SRErrorPossibility, esSystemType.String); }
		} 
		
		public esQueryItem ErrorCronologic
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.ErrorCronologic, esSystemType.String); }
		} 
		
		public esQueryItem Picture
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.Picture, esSystemType.ByteArray); }
		} 
		
		public esQueryItem IsApprove
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.IsApprove, esSystemType.Int32); }
		} 
		
		public esQueryItem SRReportStatus
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.SRReportStatus, esSystemType.String); }
		} 
		
		public esQueryItem ApprovedDateTime
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.ApprovedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem ApprovedByUserID
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.ApprovedByUserID, esSystemType.String); }
		} 
		
		public esQueryItem VoidDateTime
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem VoidByUserID
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.VoidByUserID, esSystemType.String); }
		} 
		
		public esQueryItem CreatedDateTime
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByUserID
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.CreatedByUserID, esSystemType.String); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, UserreportMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class UserreportMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected UserreportMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.ReportNo, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ReportNo;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.DateErrorOccured, 1, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.DateErrorOccured;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRErrorLocation, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRErrorLocation;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRErrorPossibility, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRErrorPossibility;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ErrorCronologic, 4, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ErrorCronologic;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Picture, 5, typeof(byte[]), esSystemType.ByteArray);
			c.PropertyName = PropertyNames.Picture;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.IsApprove, 6, typeof(int), esSystemType.Int32);
			c.PropertyName = PropertyNames.IsApprove;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRReportStatus, 7, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRReportStatus;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ApprovedDateTime, 8, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.ApprovedDateTime;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.ApprovedByUserID, 9, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.ApprovedByUserID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.VoidDateTime, 10, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.VoidDateTime;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.VoidByUserID, 11, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedDateTime, 12, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.CreatedDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedByUserID, 13, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 14, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 15, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PersonID, 16, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public UserreportMetadata Meta()
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
			 public const string ReportNo = "ReportNo";
			 public const string DateErrorOccured = "DateErrorOccured";
			 public const string SRErrorLocation = "SRErrorLocation";
			 public const string SRErrorPossibility = "SRErrorPossibility";
			 public const string ErrorCronologic = "ErrorCronologic";
			 public const string Picture = "Picture";
			 public const string IsApprove = "IsApprove";
			 public const string SRReportStatus = "SRReportStatus";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string VoidDateTime = "VoidDateTime";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PersonID = "PersonID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ReportNo = "ReportNo";
			 public const string DateErrorOccured = "DateErrorOccured";
			 public const string SRErrorLocation = "SRErrorLocation";
			 public const string SRErrorPossibility = "SRErrorPossibility";
			 public const string ErrorCronologic = "ErrorCronologic";
			 public const string Picture = "Picture";
			 public const string IsApprove = "IsApprove";
			 public const string SRReportStatus = "SRReportStatus";
			 public const string ApprovedDateTime = "ApprovedDateTime";
			 public const string ApprovedByUserID = "ApprovedByUserID";
			 public const string VoidDateTime = "VoidDateTime";
			 public const string VoidByUserID = "VoidByUserID";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(UserreportMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new UserreportMetadata();
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


				meta.AddTypeMap("ReportNo", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("DateErrorOccured", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("SRErrorLocation", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRErrorPossibility", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ErrorCronologic", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Picture", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));
				meta.AddTypeMap("IsApprove", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("SRReportStatus", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ApprovedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("ApprovedByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));			
				
				
				
				meta.Source = "UserReport";
				meta.Destination = "UserReport";
				
				meta.spInsert = "proc_userreportInsert";				
				meta.spUpdate = "proc_userreportUpdate";		
				meta.spDelete = "proc_userreportDelete";
				meta.spLoadAll = "proc_userreportLoadAll";
				meta.spLoadByPrimaryKey = "proc_userreportLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private UserreportMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
