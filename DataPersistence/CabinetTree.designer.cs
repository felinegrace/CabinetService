﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataPersistence
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PsDbA201302")]
	public partial class CabinetTreeDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCabTree_Region(CabTree_Region instance);
    partial void UpdateCabTree_Region(CabTree_Region instance);
    partial void DeleteCabTree_Region(CabTree_Region instance);
    partial void InsertCabTree_VolClass(CabTree_VolClass instance);
    partial void UpdateCabTree_VolClass(CabTree_VolClass instance);
    partial void DeleteCabTree_VolClass(CabTree_VolClass instance);
    partial void InsertCabTree_Eqptroom(CabTree_Eqptroom instance);
    partial void UpdateCabTree_Eqptroom(CabTree_Eqptroom instance);
    partial void DeleteCabTree_Eqptroom(CabTree_Eqptroom instance);
    #endregion
		
		public CabinetTreeDataContext() : 
				base(global::DataPersistence.Properties.Settings.Default.PsDbA201302ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CabinetTreeDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CabinetTreeDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CabinetTreeDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CabinetTreeDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CabTree_Region> CabTree_Regions
		{
			get
			{
				return this.GetTable<CabTree_Region>();
			}
		}
		
		public System.Data.Linq.Table<CabTree_VolClass> CabTree_VolClasses
		{
			get
			{
				return this.GetTable<CabTree_VolClass>();
			}
		}
		
		public System.Data.Linq.Table<CabTree_Eqptroom> CabTree_Eqptrooms
		{
			get
			{
				return this.GetTable<CabTree_Eqptroom>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CabTree_Region")]
	public partial class CabTree_Region : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private string _shortName;
		
		private EntitySet<CabTree_VolClass> _CabTree_VolClasses;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnshortNameChanging(string value);
    partial void OnshortNameChanged();
    #endregion
		
		public CabTree_Region()
		{
			this._CabTree_VolClasses = new EntitySet<CabTree_VolClass>(new Action<CabTree_VolClass>(this.attach_CabTree_VolClasses), new Action<CabTree_VolClass>(this.detach_CabTree_VolClasses));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shortName", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string shortName
		{
			get
			{
				return this._shortName;
			}
			set
			{
				if ((this._shortName != value))
				{
					this.OnshortNameChanging(value);
					this.SendPropertyChanging();
					this._shortName = value;
					this.SendPropertyChanged("shortName");
					this.OnshortNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CabTree_Region_CabTree_VolClass", Storage="_CabTree_VolClasses", ThisKey="id", OtherKey="parentRegionId")]
		public EntitySet<CabTree_VolClass> CabTree_VolClasses
		{
			get
			{
				return this._CabTree_VolClasses;
			}
			set
			{
				this._CabTree_VolClasses.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_CabTree_VolClasses(CabTree_VolClass entity)
		{
			this.SendPropertyChanging();
			entity.CabTree_Region = this;
		}
		
		private void detach_CabTree_VolClasses(CabTree_VolClass entity)
		{
			this.SendPropertyChanging();
			entity.CabTree_Region = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CabTree_VolClass")]
	public partial class CabTree_VolClass : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private int _parentRegionId;
		
		private EntitySet<CabTree_Eqptroom> _CabTree_Eqptrooms;
		
		private EntityRef<CabTree_Region> _CabTree_Region;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnparentRegionIdChanging(int value);
    partial void OnparentRegionIdChanged();
    #endregion
		
		public CabTree_VolClass()
		{
			this._CabTree_Eqptrooms = new EntitySet<CabTree_Eqptroom>(new Action<CabTree_Eqptroom>(this.attach_CabTree_Eqptrooms), new Action<CabTree_Eqptroom>(this.detach_CabTree_Eqptrooms));
			this._CabTree_Region = default(EntityRef<CabTree_Region>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_parentRegionId", DbType="Int NOT NULL")]
		public int parentRegionId
		{
			get
			{
				return this._parentRegionId;
			}
			set
			{
				if ((this._parentRegionId != value))
				{
					if (this._CabTree_Region.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnparentRegionIdChanging(value);
					this.SendPropertyChanging();
					this._parentRegionId = value;
					this.SendPropertyChanged("parentRegionId");
					this.OnparentRegionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CabTree_VolClass_CabTree_Eqptroom", Storage="_CabTree_Eqptrooms", ThisKey="id", OtherKey="parentVolClassId")]
		public EntitySet<CabTree_Eqptroom> CabTree_Eqptrooms
		{
			get
			{
				return this._CabTree_Eqptrooms;
			}
			set
			{
				this._CabTree_Eqptrooms.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CabTree_Region_CabTree_VolClass", Storage="_CabTree_Region", ThisKey="parentRegionId", OtherKey="id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public CabTree_Region CabTree_Region
		{
			get
			{
				return this._CabTree_Region.Entity;
			}
			set
			{
				CabTree_Region previousValue = this._CabTree_Region.Entity;
				if (((previousValue != value) 
							|| (this._CabTree_Region.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CabTree_Region.Entity = null;
						previousValue.CabTree_VolClasses.Remove(this);
					}
					this._CabTree_Region.Entity = value;
					if ((value != null))
					{
						value.CabTree_VolClasses.Add(this);
						this._parentRegionId = value.id;
					}
					else
					{
						this._parentRegionId = default(int);
					}
					this.SendPropertyChanged("CabTree_Region");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_CabTree_Eqptrooms(CabTree_Eqptroom entity)
		{
			this.SendPropertyChanging();
			entity.CabTree_VolClass = this;
		}
		
		private void detach_CabTree_Eqptrooms(CabTree_Eqptroom entity)
		{
			this.SendPropertyChanging();
			entity.CabTree_VolClass = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CabTree_Eqptroom")]
	public partial class CabTree_Eqptroom : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _name;
		
		private int _parentVolClassId;
		
		private EntityRef<CabTree_VolClass> _CabTree_VolClass;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OnparentVolClassIdChanging(int value);
    partial void OnparentVolClassIdChanged();
    #endregion
		
		public CabTree_Eqptroom()
		{
			this._CabTree_VolClass = default(EntityRef<CabTree_VolClass>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(64) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_parentVolClassId", DbType="Int NOT NULL")]
		public int parentVolClassId
		{
			get
			{
				return this._parentVolClassId;
			}
			set
			{
				if ((this._parentVolClassId != value))
				{
					if (this._CabTree_VolClass.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnparentVolClassIdChanging(value);
					this.SendPropertyChanging();
					this._parentVolClassId = value;
					this.SendPropertyChanged("parentVolClassId");
					this.OnparentVolClassIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CabTree_VolClass_CabTree_Eqptroom", Storage="_CabTree_VolClass", ThisKey="parentVolClassId", OtherKey="id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public CabTree_VolClass CabTree_VolClass
		{
			get
			{
				return this._CabTree_VolClass.Entity;
			}
			set
			{
				CabTree_VolClass previousValue = this._CabTree_VolClass.Entity;
				if (((previousValue != value) 
							|| (this._CabTree_VolClass.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CabTree_VolClass.Entity = null;
						previousValue.CabTree_Eqptrooms.Remove(this);
					}
					this._CabTree_VolClass.Entity = value;
					if ((value != null))
					{
						value.CabTree_Eqptrooms.Add(this);
						this._parentVolClassId = value.id;
					}
					else
					{
						this._parentVolClassId = default(int);
					}
					this.SendPropertyChanged("CabTree_VolClass");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
