
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 12/14/2024 8:43:24 AM
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
	/// Encapsulates the 'AppProgram' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(AppProgram))]	
	[XmlType("AppProgram")]
	public partial class AppProgram : esAppProgram
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
		static public void Delete(System.String programID)
		{
			var obj = new AppProgram();
			obj.ProgramID = programID;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.String programID, esSqlAccessType sqlAccessType)
		{
			var obj = new AppProgram();
			obj.ProgramID = programID;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("AppProgramCollection")]
	public partial class AppProgramCollection : esAppProgramCollection, IEnumerable<AppProgram>
	{
		public AppProgram FindByPrimaryKey(System.String programID)
		{
			return this.SingleOrDefault(e => e.ProgramID == programID);
		}

		
				
	}



	[Serializable]	
	public partial class AppProgramQuery : esAppProgramQuery
	{
		public AppProgramQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public AppProgramQuery(string joinAlias, out AppProgramQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppProgramQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(AppProgramQuery query)
		{
			return AppProgramQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator AppProgramQuery(string query)
		{
			return (AppProgramQuery)AppProgramQuery.SerializeHelper.FromXml(query, typeof(AppProgramQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esAppProgram : esEntity
	{
		public esAppProgram()
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
			AppProgramQuery query = new AppProgramQuery("AppProgram");
			query.Where(query.ProgramID == programID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String programID)
		{
			esParameters parms = new esParameters();
			parms.Add("ProgramID", programID);
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
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ProgramID);
			}
			
			set
			{
				if(base.SetSystemString(AppProgramMetadata.ColumnNames.ProgramID, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.ProgramID);
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
				return base.GetSystemString(AppProgramMetadata.ColumnNames.ProgramName);
			}
			
			set
			{
				if(base.SetSystemString(AppProgramMetadata.ColumnNames.ProgramName, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.ProgramName);
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
				return base.GetSystemString(AppProgramMetadata.ColumnNames.Note);
			}
			
			set
			{
				if(base.SetSystemString(AppProgramMetadata.ColumnNames.Note, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.Note);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgram);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgram, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgram);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramAddAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramAddAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramAddAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramEditAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramEditAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramEditAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramDeleteAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramDeleteAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramDeleteAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramViewAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramViewAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramViewAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramApprovalAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramApprovalAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramApprovalAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramUnApprovalAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramVoidAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramVoidAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramVoidAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramUnVoidAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramPrintAble);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsProgramPrintAble, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsProgramPrintAble);
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
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsVisible);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsVisible, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsVisible);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppProgramMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(AppProgramMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppProgramMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(AppProgramMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to AppProgram.IsUsedBySystem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.SByte? IsUsedBySystem
		{
			get
			{
				return base.GetSystemSByte(AppProgramMetadata.ColumnNames.IsUsedBySystem);
			}
			
			set
			{
				if(base.SetSystemSByte(AppProgramMetadata.ColumnNames.IsUsedBySystem, value))
				{
					OnPropertyChanged(AppProgramMetadata.PropertyNames.IsUsedBySystem);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return AppProgramMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public AppProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramQuery("AppProgram");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppProgramQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(AppProgramQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((AppProgramQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private AppProgramQuery query;		
	}



	[Serializable]
	abstract public partial class esAppProgramCollection : esEntityCollection<AppProgram>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "AppProgramCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public AppProgramQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppProgramQuery("AppProgram");
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(AppProgramQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppProgramQuery("AppProgram");
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(AppProgramQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((AppProgramQuery)query);
		}

		#endregion
		
		private AppProgramQuery query;
	}



	[Serializable]
	abstract public partial class esAppProgramQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppProgramMetadata.Meta();
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
				case "LastUpdateDateTime": return this.LastUpdateDateTime;
				case "LastUpdateByUserID": return this.LastUpdateByUserID;
				case "IsUsedBySystem": return this.IsUsedBySystem;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProgramID
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.ProgramID, esSystemType.String); }
		} 
		
		public esQueryItem ProgramName
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.ProgramName, esSystemType.String); }
		} 
		
		public esQueryItem Note
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.Note, esSystemType.String); }
		} 
		
		public esQueryItem IsProgram
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgram, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramAddAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramAddAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramEditAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramEditAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramDeleteAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramDeleteAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramViewAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramViewAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramApprovalAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramApprovalAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramUnApprovalAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramVoidAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramVoidAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramUnVoidAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsProgramPrintAble
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsProgramPrintAble, esSystemType.SByte); }
		} 
		
		public esQueryItem IsVisible
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsVisible, esSystemType.SByte); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		} 
		
		public esQueryItem IsUsedBySystem
		{
			get { return new esQueryItem(this, AppProgramMetadata.ColumnNames.IsUsedBySystem, esSystemType.SByte); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class AppProgramMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppProgramMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ProgramID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ProgramID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.ProgramName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.ProgramName;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.Note, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 1000;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgram, 3, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgram;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramAddAble, 4, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramAddAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramEditAble, 5, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramEditAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramDeleteAble, 6, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramDeleteAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramViewAble, 7, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramViewAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramApprovalAble, 8, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramApprovalAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramUnApprovalAble, 9, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramUnApprovalAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramVoidAble, 10, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramVoidAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramUnVoidAble, 11, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramUnVoidAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsProgramPrintAble, 12, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsProgramPrintAble;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsVisible, 13, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsVisible;
			c.NumericPrecision = 1;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppProgramMetadata.PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = AppProgramMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(AppProgramMetadata.ColumnNames.IsUsedBySystem, 16, typeof(System.SByte), esSystemType.SByte);
			c.PropertyName = AppProgramMetadata.PropertyNames.IsUsedBySystem;
			c.NumericPrecision = 1;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public AppProgramMetadata Meta()
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
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsUsedBySystem = "IsUsedBySystem";
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
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string IsUsedBySystem = "IsUsedBySystem";
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
			lock (typeof(AppProgramMetadata))
			{
				if(AppProgramMetadata.mapDelegates == null)
				{
					AppProgramMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppProgramMetadata.meta == null)
				{
					AppProgramMetadata.meta = new AppProgramMetadata();
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
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("IsUsedBySystem", new esTypeMap("BIT", "System.SByte"));			
				
				
				
				meta.Source = "AppProgram";
				meta.Destination = "AppProgram";
				
				meta.spInsert = "proc_AppProgramInsert";				
				meta.spUpdate = "proc_AppProgramUpdate";		
				meta.spDelete = "proc_AppProgramDelete";
				meta.spLoadAll = "proc_AppProgramLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppProgramLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppProgramMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
