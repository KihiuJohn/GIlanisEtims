// Decompiled with JetBrains decompiler
// Type: eTims.BranchEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace eTims;

[PXCacheName("BranchEtims")]
[Serializable]
public class BranchEtims : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Registered")]
  public virtual bool? IsRegistered { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Branch ID")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (Select<InventoryItem, Where<InventoryItem.inventoryID, Equal<Current<BranchEtims.inventoryID>>>>))]
  [PXSelector(typeof (Search<InventoryItem.inventoryID>), SubstituteKey = typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Isaleid")]
  public virtual int? Isaleid { get; set; }

  public abstract class id : BqlType<IBqlInt, int>.Field<
  #nullable disable
  BranchEtims.id>
  {
  }

  public abstract class isRegistered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BranchEtims.isRegistered>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchEtims.branchID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchEtims.inventoryID>
  {
  }

  public abstract class isaleid : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchEtims.isaleid>
  {
  }
}
