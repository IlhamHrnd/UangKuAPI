
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2019.1.1218.0
EntitySpaces Driver  : MySql
Date Generated       : 9/29/2024 2:53:11 PM
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
	/// Encapsulates the 'transaction' table
	/// </summary>

	[Serializable]
	[DataContract]
	[KnownType(typeof(Transaction))]	
	[XmlType("Transaction")]
	public partial class Transaction : esTransaction
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Transaction();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



	[Serializable]
	[CollectionDataContract]
	[XmlType("TransactionCollection")]
	public partial class TransactionCollection : esTransactionCollection, IEnumerable<Transaction>
	{

		
				
	}



	[Serializable]	
	public partial class TransactionQuery : esTransactionQuery
	{
		public TransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		public TransactionQuery(string joinAlias, out TransactionQuery query)
		{
			query = this;
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "TransactionQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(TransactionQuery query)
		{
			return TransactionQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator TransactionQuery(string query)
		{
			return (TransactionQuery)TransactionQuery.SerializeHelper.FromXml(query, typeof(TransactionQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esTransaction : esEntity
	{
		public esTransaction()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(string transNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, string transNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transNo);
		}

		private bool LoadByPrimaryKeyDynamic(string transNo)
		{
			TransactionQuery query = new TransactionQuery("Transaction");
			query.Where(query.TransNo == transNo);
			return Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(string transNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transNo);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to transaction.TransNo
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string TransNo
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.TransNo);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.TransNo, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.TransNo);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.PersonID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string PersonID
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.PersonID);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.PersonID, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.PersonID);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.SRTransaction
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRTransaction
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.SRTransaction);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.SRTransaction, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.SRTransaction);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.SRTransItem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string SRTransItem
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.SRTransItem);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.SRTransItem, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.SRTransItem);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.Amount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(TransactionMetadata.ColumnNames.Amount);
			}
			
			set
			{
				if(base.SetSystemDecimal(TransactionMetadata.ColumnNames.Amount, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.Amount);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.Description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string Description
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.Description);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.Photo
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public byte[] Photo
		{
			get
			{
				return base.GetSystemByteArray(TransactionMetadata.ColumnNames.Photo);
			}
			
			set
			{
				if(base.SetSystemByteArray(TransactionMetadata.ColumnNames.Photo, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.Photo);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.TransType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string TransType
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.TransType);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.TransType, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.TransType);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.TransDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? TransDate
		{
			get
			{
				return base.GetSystemDateTime(TransactionMetadata.ColumnNames.TransDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(TransactionMetadata.ColumnNames.TransDate, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.TransDate);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.CreatedDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransactionMetadata.ColumnNames.CreatedDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(TransactionMetadata.ColumnNames.CreatedDateTime, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.CreatedDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.CreatedByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string CreatedByUserID
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.CreatedByUserID);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.CreatedByUserID, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.CreatedByUserID);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.LastUpdateDateTime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(TransactionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				if(base.SetSystemDateTime(TransactionMetadata.ColumnNames.LastUpdateDateTime, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.LastUpdateDateTime);
				}
			}
		}
		
		/// <summary>
		/// Maps to transaction.LastUpdateByUserID
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public string LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(TransactionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				if(base.SetSystemString(TransactionMetadata.ColumnNames.LastUpdateByUserID, value))
				{
					OnPropertyChanged(TransactionMetadata.PropertyNames.LastUpdateByUserID);
				}
			}
		}

        /// <summary>
        /// Maps to UserDocument.FileExtention
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        virtual public string PhotoExtention
        {
            get
            {
                return base.GetSystemString(TransactionMetadata.ColumnNames.PhotoExtention);
            }

            set
            {
                if (base.SetSystemString(TransactionMetadata.ColumnNames.PhotoExtention, value))
                {
                    OnPropertyChanged(TransactionMetadata.PropertyNames.PhotoExtention);
                }
            }
        }

        #endregion

        #region Housekeeping methods

        override protected IMetadata Meta
		{
			get
			{
				return TransactionMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public TransactionQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new TransactionQuery("Transaction");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(TransactionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		protected void InitQuery(TransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((TransactionQuery)query);
		}

		#endregion
		
        [IgnoreDataMember]
		private TransactionQuery query;		
	}



	[Serializable]
	abstract public partial class esTransactionCollection : esEntityCollection<Transaction>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TransactionMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "TransactionCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[Browsable(false)]
	#endif
		public TransactionQuery Query
		{
			get
			{
				if (query == null)
				{
                    query = new TransactionQuery("Transaction");
					InitQuery(query);
				}

				return query;
			}
		}

		public bool Load(TransactionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (query == null)
			{
                query = new TransactionQuery("Transaction");
                InitQuery(query);
			}
			return query;
		}

		protected void InitQuery(TransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
            InitQuery((TransactionQuery)query);
		}

		#endregion
		
		private TransactionQuery query;
	}



	[Serializable]
	abstract public partial class esTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TransactionMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "TransNo": return TransNo;
				case "PersonID": return PersonID;
				case "SRTransaction": return SRTransaction;
				case "SRTransItem": return SRTransItem;
				case "Amount": return Amount;
				case "Description": return Description;
				case "Photo": return Photo;
				case "TransType": return TransType;
				case "TransDate": return TransDate;
				case "CreatedDateTime": return CreatedDateTime;
				case "CreatedByUserID": return CreatedByUserID;
				case "LastUpdateDateTime": return LastUpdateDateTime;
				case "LastUpdateByUserID": return LastUpdateByUserID;
                case "PhotoExtention": return PhotoExtention;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem TransNo
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.TransNo, esSystemType.String); }
		} 
		
		public esQueryItem PersonID
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.PersonID, esSystemType.String); }
		} 
		
		public esQueryItem SRTransaction
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.SRTransaction, esSystemType.String); }
		} 
		
		public esQueryItem SRTransItem
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.SRTransItem, esSystemType.String); }
		} 
		
		public esQueryItem Amount
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.Amount, esSystemType.Decimal); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Photo
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.Photo, esSystemType.ByteArray); }
		} 
		
		public esQueryItem TransType
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.TransType, esSystemType.String); }
		} 
		
		public esQueryItem TransDate
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.TransDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedDateTime
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByUserID
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.CreatedByUserID, esSystemType.String); }
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime); }
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get { return new esQueryItem(this, TransactionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String); }
		}

        public esQueryItem PhotoExtention
        {
            get { return new esQueryItem(this, TransactionMetadata.ColumnNames.PhotoExtention, esSystemType.String); }
        }

        #endregion

    }



	[Serializable]
	public partial class TransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TransactionMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ColumnNames.TransNo, 0, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.TransNo;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.PersonID, 1, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.PersonID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRTransaction, 2, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRTransaction;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.SRTransItem, 3, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.SRTransItem;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Amount, 4, typeof(decimal), esSystemType.Decimal);
			c.PropertyName = PropertyNames.Amount;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Description, 5, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.Description;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.Photo, 6, typeof(byte[]), esSystemType.ByteArray);
			c.PropertyName = PropertyNames.Photo;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.TransType, 7, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.TransType;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.TransDate, 8, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.TransDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedDateTime, 9, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.CreatedDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.CreatedByUserID, 10, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateDateTime, 11, typeof(DateTime), esSystemType.DateTime);
			c.PropertyName = PropertyNames.LastUpdateDateTime;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ColumnNames.LastUpdateByUserID, 12, typeof(string), esSystemType.String);
			c.PropertyName = PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);

            c = new esColumnMetadata(ColumnNames.PhotoExtention, 13, typeof(string), esSystemType.String);
            c.PropertyName = PropertyNames.PhotoExtention;
            c.CharacterMaxLength = 4;
            m_columns.Add(c);
        }
		#endregion	
	
		static public TransactionMetadata Meta()
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
			 public const string TransNo = "TransNo";
			 public const string PersonID = "PersonID";
			 public const string SRTransaction = "SRTransaction";
			 public const string SRTransItem = "SRTransItem";
			 public const string Amount = "Amount";
			 public const string Description = "Description";
			 public const string Photo = "Photo";
			 public const string TransType = "TransType";
			 public const string TransDate = "TransDate";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
             public const string PhotoExtention = "PhotoExtention";
        }
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransNo = "TransNo";
			 public const string PersonID = "PersonID";
			 public const string SRTransaction = "SRTransaction";
			 public const string SRTransItem = "SRTransItem";
			 public const string Amount = "Amount";
			 public const string Description = "Description";
			 public const string Photo = "Photo";
			 public const string TransType = "TransType";
			 public const string TransDate = "TransDate";
			 public const string CreatedDateTime = "CreatedDateTime";
			 public const string CreatedByUserID = "CreatedByUserID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
             public const string PhotoExtention = "PhotoExtention";
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
			lock (typeof(TransactionMetadata))
			{
				if(mapDelegates == null)
				{
                    mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (meta == null)
				{
                    meta = new TransactionMetadata();
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


				meta.AddTypeMap("TransNo", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("PersonID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRTransaction", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("SRTransItem", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("DECIMAL", "System.Decimal"));
				meta.AddTypeMap("Description", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("Photo", new esTypeMap("MEDIUMBLOB", "System.Byte[]"));
				meta.AddTypeMap("TransType", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("TransDate", new esTypeMap("DATE", "System.DateTime"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("VARCHAR", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("DATETIME", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("VARCHAR", "System.String"));
                meta.AddTypeMap("PhotoExtention", new esTypeMap("VARCHAR", "System.String"));



                meta.Source = "Transaction";
				meta.Destination = "Transaction";
				
				meta.spInsert = "proc_transactionInsert";				
				meta.spUpdate = "proc_transactionUpdate";		
				meta.spDelete = "proc_transactionDelete";
				meta.spLoadAll = "proc_transactionLoadAll";
				meta.spLoadByPrimaryKey = "proc_transactionLoadByPrimaryKey";
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
