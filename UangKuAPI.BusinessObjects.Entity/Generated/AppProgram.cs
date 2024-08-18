
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/18/2024 9:50:03 AM
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
	/// Encapsulates the 'AppProgram' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(AppProgram))]	
	[XmlType("AppProgram")]
	public partial class AppProgram : esAppprogram
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new AppProgram();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("AppprogramCollection")]
	public partial class AppprogramCollection : esAppprogramCollection, IEnumerable<AppProgram>
	{

		
				
	}



	[Serializable]	
	public partial class AppprogramQuery : esAppprogramQuery
	{
		public AppprogramQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public AppprogramQuery(string joinAlias, out AppprogramQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppprogramQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(AppprogramQuery query)
		{
			return AppprogramQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator AppprogramQuery(string query)
		{
			return (AppprogramQuery)AppprogramQuery.SerializeHelper.FromXml(query, typeof(AppprogramQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esAppprogram : esEntity
	{
		public esAppprogram()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String programID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String programID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(programID);
			else
				return LoadByPrimaryKeyStoredProcedure(programID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String programID)
		{
			AppprogramQuery query = new AppprogramQuery("AppProgram");
			query.Where(query.ProgramID == programID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String programID)
		{
			esParameters parms = new esParameters();
			parms.Add("ParameterID", programID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to AppProgram.ProgramID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProgramID
		{
			get
			{
				return base.GetSystemString(AppprogramMetadata.ColumnNames.ProgramID);
			}
			
			set
			{
				if(base.SetSystemString(AppprogramMetadata.ColumnNames.ProgramID, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.ProgramID);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.ProgramName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProgramName
		{
			get
			{
				return base.GetSystemString(AppprogramMetadata.ColumnNames.ProgramName);
			}
			
			set
			{
				if(base.SetSystemString(AppprogramMetadata.ColumnNames.ProgramName, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.ProgramName);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.Note
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(AppprogramMetadata.ColumnNames.Note);
			}
			
			set
			{
				if(base.SetSystemString(AppprogramMetadata.ColumnNames.Note, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.Note);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgram
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgram
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgram);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgram, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgram);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramAddAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramAddAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramAddAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramAddAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramAddAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramEditAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramEditAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramEditAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramEditAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramEditAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramDeleteAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramDeleteAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramDeleteAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramDeleteAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramDeleteAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramViewAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramViewAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramViewAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramViewAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramViewAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramApprovalAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramApprovalAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramApprovalAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramApprovalAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramApprovalAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramUnApprovalAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramUnApprovalAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramUnApprovalAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramUnApprovalAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramUnApprovalAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramVoidAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramVoidAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramVoidAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramVoidAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramVoidAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramUnVoidAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramUnVoidAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramUnVoidAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramUnVoidAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramUnVoidAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsProgramPrintAble
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsProgramPrintAble
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramPrintAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsProgramPrintAble, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsProgramPrintAble);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsVisible
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsVisible
		{
			get
			{
				return base.GetSystemSByte(AppprogramMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				if(base.SetSystemSByte(AppprogramMetadata.ColumnNames.IsVisible, value))
				{
					OnPropertyChanged(AppprogramMetadata.PropertyNames.IsVisible);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return AppprogramMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public AppprogramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppprogramQuery("AppProgram");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppprogramQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(AppprogramQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((AppprogramQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private AppprogramQuery query;		
	}



	[Serializable]
	abstract public partial class esAppprogramCollection : esEntityCollection<AppProgram>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppprogramMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "AppprogramCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public AppprogramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppprogramQuery("AppProgram");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppprogramQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppprogramQuery("AppProgram");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(AppprogramQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((AppprogramQuery)query);
		}

		#endregion
		
		private AppprogramQuery query;
	}



	[Serializable]
	abstract public partial class esAppprogramQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppprogramMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProgramID": return this.ProgramID;
				case "ProgramName": return this.ProgramName;
				case "Note": return this.Note;
				case "IsProgram": return this.IsProgram;
				case "IsProgramAddAble": return this.IsProgramAddAble;
				case "IsProgramEditAble": return this.IsProgramEditAble;
				case "IsProgramDeleteAble": return this.IsProgramDeleteAble;
				case "IsProgramViewAble": return this.IsProgramViewAble;
				case "IsProgramApprovalAble": return this.IsProgramApprovalAble;
				case "IsProgramUnApprovalAble": return this.IsProgramUnApprovalAble;
				case "IsProgramVoidAble": return this.IsProgramVoidAble;
				case "IsProgramUnVoidAble": return this.IsProgramUnVoidAble;
				case "IsProgramPrintAble": return this.IsProgramPrintAble;
				case "IsVisible": return this.IsVisible;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProgramID
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.ProgramID, esSystemType.String); }
		} 
		
		public esQueryItem ProgramName
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.ProgramName, esSystemType.String); }
		} 
		
		public esQueryItem Note
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.Note, esSystemType.String); }
		} 
		
		public esQueryItem IsProgram
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgram, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramAddAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramAddAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramEditAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramEditAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramDeleteAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramDeleteAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramViewAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramViewAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramApprovalAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramApprovalAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramUnApprovalAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramUnApprovalAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramVoidAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramVoidAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramUnVoidAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramUnVoidAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramPrintAble
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsProgramPrintAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsVisible
		{
			get { return new esQueryItem(this, AppprogramMetadata.ColumnNames.IsVisible, esSystemType.SByte); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class AppprogramMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppprogramMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppprogramMetadata.PropertyNames.ProgramID;
			c.CharacterMaxLength = 30;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.ProgramName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppprogramMetadata.PropertyNames.ProgramName;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.Note, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppprogramMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgram, 3, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgram;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramAddAble, 4, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramAddAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramEditAble, 5, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramEditAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramDeleteAble, 6, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramDeleteAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramViewAble, 7, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramViewAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramApprovalAble, 8, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramApprovalAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramUnApprovalAble, 9, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramUnApprovalAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramVoidAble, 10, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramVoidAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramUnVoidAble, 11, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramUnVoidAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsProgramPrintAble, 12, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsProgramPrintAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppprogramMetadata.ColumnNames.IsVisible, 13, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppprogramMetadata.PropertyNames.IsVisible;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public AppprogramMetadata Meta()
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
			 public const string ProgramID = "ProgramID";
			 public const string ProgramName = "ProgramName";
			 public const string Note = "Note";
			 public const string IsProgram = "IsProgram";
			 public const string IsProgramAddAble = "IsProgramAddAble";
			 public const string IsProgramEditAble = "IsProgramEditAble";
			 public const string IsProgramDeleteAble = "IsProgramDeleteAble";
			 public const string IsProgramViewAble = "IsProgramViewAble";
			 public const string IsProgramApprovalAble = "IsProgramApprovalAble";
			 public const string IsProgramUnApprovalAble = "IsProgramUnApprovalAble";
			 public const string IsProgramVoidAble = "IsProgramVoidAble";
			 public const string IsProgramUnVoidAble = "IsProgramUnVoidAble";
			 public const string IsProgramPrintAble = "IsProgramPrintAble";
			 public const string IsVisible = "IsVisible";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProgramID = "ProgramID";
			 public const string ProgramName = "ProgramName";
			 public const string Note = "Note";
			 public const string IsProgram = "IsProgram";
			 public const string IsProgramAddAble = "IsProgramAddAble";
			 public const string IsProgramEditAble = "IsProgramEditAble";
			 public const string IsProgramDeleteAble = "IsProgramDeleteAble";
			 public const string IsProgramViewAble = "IsProgramViewAble";
			 public const string IsProgramApprovalAble = "IsProgramApprovalAble";
			 public const string IsProgramUnApprovalAble = "IsProgramUnApprovalAble";
			 public const string IsProgramVoidAble = "IsProgramVoidAble";
			 public const string IsProgramUnVoidAble = "IsProgramUnVoidAble";
			 public const string IsProgramPrintAble = "IsProgramPrintAble";
			 public const string IsVisible = "IsVisible";
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
			lock (typeof(AppprogramMetadata))
			{
				if(AppprogramMetadata.mapDelegates == null)
				{
					AppprogramMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppprogramMetadata.meta == null)
				{
					AppprogramMetadata.meta = new AppprogramMetadata();
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


				meta.AddTypeMap("ProgramID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("ProgramName", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsProgram", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramAddAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramEditAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramDeleteAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramViewAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramApprovalAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramUnApprovalAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramVoidAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramUnVoidAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsProgramPrintAble", new esTypeMap("BIT", "System.SByte"));
				meta.AddTypeMap("IsVisible", new esTypeMap("BIT", "System.SByte"));			
				
				
				
				meta.Source = "AppProgram";
				meta.Destination = "AppProgram";
				
				meta.spInsert = "proc_appprogramInsert";				
				meta.spUpdate = "proc_appprogramUpdate";		
				meta.spDelete = "proc_appprogramDelete";
				meta.spLoadAll = "proc_appprogramLoadAll";
				meta.spLoadByPrimaryKey = "proc_appprogramLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppprogramMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
