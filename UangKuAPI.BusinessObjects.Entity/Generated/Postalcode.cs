
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 8/18/2024 10:02:20 AM
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
	/// Encapsulates the 'PostalCode' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Postalcode))]	
	[XmlType("Postalcode")]
	public partial class Postalcode : esPostalcode
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Postalcode();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("PostalcodeCollection")]
	public partial class PostalcodeCollection : esPostalcodeCollection, IEnumerable<Postalcode>
	{

		
				
	}



	[Serializable]	
	public partial class PostalcodeQuery : esPostalcodeQuery
	{
		public PostalcodeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public PostalcodeQuery(string joinAlias, out PostalcodeQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PostalcodeQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(PostalcodeQuery query)
		{
			return PostalcodeQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator PostalcodeQuery(string query)
		{
			return (PostalcodeQuery)PostalcodeQuery.SerializeHelper.FromXml(query, typeof(PostalcodeQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esPostalcode : esEntity
	{
		public esPostalcode()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String postalID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postalID);
			else
				return LoadByPrimaryKeyStoredProcedure(postalID);
		}

		public virtual bool LoadByPrimaryKey(System.String provID, System.String cityID, System.String subdisID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(provID, cityID, subdisID);
			else
				return LoadByPrimaryKeyStoredProcedure(provID, cityID, subdisID);
        }

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String postalID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(postalID);
			else
				return LoadByPrimaryKeyStoredProcedure(postalID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String provID, System.String cityID, System.String subdisID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
                return LoadByPrimaryKeyDynamic(provID, cityID, subdisID);
			else
                return LoadByPrimaryKeyStoredProcedure(provID, cityID, subdisID);
        }

		private bool LoadByPrimaryKeyDynamic(System.String postalID)
		{
			PostalcodeQuery query = new PostalcodeQuery("Postalcode");
			query.Where(query.PostalID == postalID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyDynamic(System.String provID, System.String cityID, System.String subdisID)
		{
			PostalcodeQuery query = new PostalcodeQuery("Postalcode");
			query.Where(query.ProvID == provID && query.CityID == cityID && query.SubdisID == subdisID);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String postalID)
		{
			esParameters parms = new esParameters();
			parms.Add("PostalID", postalID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String provID, System.String cityID, System.String subdisID)
		{
			esParameters parms = new esParameters();
            parms.Add("ProvID", provID);
            parms.Add("CityID", cityID);
            parms.Add("SubdisID", subdisID);
            return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
        }
        #endregion

        #region Properties



        /// <summary>
        /// Maps to PostalCode.PostalID
        /// </summary>
        [DataMember(EmitDefaultValue=false)]
		virtual public System.UInt32? PostalID
		{
			get
			{
				return base.GetSystemUInt32(PostalcodeMetadata.ColumnNames.PostalID);
			}
			
			set
			{
				if(base.SetSystemUInt32(PostalcodeMetadata.ColumnNames.PostalID, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.PostalID);
				}
			}
		}
		
		/// <summary>
		/// Maps to PostalCode.SubdisID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? SubdisID
		{
			get
			{
				return base.GetSystemInt32(PostalcodeMetadata.ColumnNames.SubdisID);
			}
			
			set
			{
				if(base.SetSystemInt32(PostalcodeMetadata.ColumnNames.SubdisID, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.SubdisID);
				}
			}
		}
		
		/// <summary>
		/// Maps to PostalCode.DisID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? DisID
		{
			get
			{
				return base.GetSystemInt32(PostalcodeMetadata.ColumnNames.DisID);
			}
			
			set
			{
				if(base.SetSystemInt32(PostalcodeMetadata.ColumnNames.DisID, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.DisID);
				}
			}
		}
		
		/// <summary>
		/// Maps to PostalCode.CityID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? CityID
		{
			get
			{
				return base.GetSystemInt32(PostalcodeMetadata.ColumnNames.CityID);
			}
			
			set
			{
				if(base.SetSystemInt32(PostalcodeMetadata.ColumnNames.CityID, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.CityID);
				}
			}
		}
		
		/// <summary>
		/// Maps to PostalCode.ProvID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProvID
		{
			get
			{
				return base.GetSystemInt32(PostalcodeMetadata.ColumnNames.ProvID);
			}
			
			set
			{
				if(base.SetSystemInt32(PostalcodeMetadata.ColumnNames.ProvID, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.ProvID);
				}
			}
		}
		
		/// <summary>
		/// Maps to PostalCode.PostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? PostalCode
		{
			get
			{
				return base.GetSystemInt32(PostalcodeMetadata.ColumnNames.PostalCode);
			}
			
			set
			{
				if(base.SetSystemInt32(PostalcodeMetadata.ColumnNames.PostalCode, value))
				{
					OnPropertyChanged(PostalcodeMetadata.PropertyNames.PostalCode);
				}
			}
		}
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return PostalcodeMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public PostalcodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PostalcodeQuery("Postalcode");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PostalcodeQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}

		protected void InitQuery(PostalcodeQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((PostalcodeQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private PostalcodeQuery query;		
	}



	[Serializable]
	abstract public partial class esPostalcodeCollection : esEntityCollection<Postalcode>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PostalcodeMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "PostalcodeCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public PostalcodeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PostalcodeQuery("Postalcode");
                    InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PostalcodeQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PostalcodeQuery("Postalcode");
                this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(PostalcodeQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((PostalcodeQuery)query);
		}

		#endregion
		
		private PostalcodeQuery query;
	}



	[Serializable]
	abstract public partial class esPostalcodeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PostalcodeMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "PostalID": return this.PostalID;
				case "SubdisID": return this.SubdisID;
				case "DisID": return this.DisID;
				case "CityID": return this.CityID;
				case "ProvID": return this.ProvID;
				case "PostalCode": return this.PostalCode;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem PostalID
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.PostalID, esSystemType.UInt32); }
		} 
		
		public esQueryItem SubdisID
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.SubdisID, esSystemType.Int32); }
		} 
		
		public esQueryItem DisID
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.DisID, esSystemType.Int32); }
		} 
		
		public esQueryItem CityID
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.CityID, esSystemType.Int32); }
		} 
		
		public esQueryItem ProvID
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.ProvID, esSystemType.Int32); }
		} 
		
		public esQueryItem PostalCode
		{
			get { return new esQueryItem(this, PostalcodeMetadata.ColumnNames.PostalCode, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class PostalcodeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PostalcodeMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.PostalID, 0, typeof(System.UInt32), esSystemType.UInt32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.PostalID;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.SubdisID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.SubdisID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.DisID, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.DisID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.CityID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.CityID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.ProvID, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.ProvID;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PostalcodeMetadata.ColumnNames.PostalCode, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PostalcodeMetadata.PropertyNames.PostalCode;
			c.NumericPrecision = 11;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public PostalcodeMetadata Meta()
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
			 public const string PostalID = "PostalID";
			 public const string SubdisID = "SubdisID";
			 public const string DisID = "DisID";
			 public const string CityID = "CityID";
			 public const string ProvID = "ProvID";
			 public const string PostalCode = "PostalCode";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string PostalID = "PostalID";
			 public const string SubdisID = "SubdisID";
			 public const string DisID = "DisID";
			 public const string CityID = "CityID";
			 public const string ProvID = "ProvID";
			 public const string PostalCode = "PostalCode";
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
			lock (typeof(PostalcodeMetadata))
			{
				if(PostalcodeMetadata.mapDelegates == null)
				{
					PostalcodeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PostalcodeMetadata.meta == null)
				{
					PostalcodeMetadata.meta = new PostalcodeMetadata();
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


				meta.AddTypeMap("PostalID", new esTypeMap("INT UNSIGNED", "System.UInt32"));
				meta.AddTypeMap("SubdisID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("DisID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("CityID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("ProvID", new esTypeMap("INT", "System.Int32"));
				meta.AddTypeMap("PostalCode", new esTypeMap("INT", "System.Int32"));			
				
				
				
				meta.Source = "PostalCode";
				meta.Destination = "PostalCode";
				
				meta.spInsert = "proc_postalcodeInsert";				
				meta.spUpdate = "proc_postalcodeUpdate";		
				meta.spDelete = "proc_postalcodeDelete";
				meta.spLoadAll = "proc_postalcodeLoadAll";
				meta.spLoadByPrimaryKey = "proc_postalcodeLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PostalcodeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
