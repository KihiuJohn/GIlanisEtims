// Decompiled with JetBrains decompiler
// Type: eTims.DeviceUrls
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("DeviceUrls")]
[Serializable]
public class DeviceUrls : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField(DisplayName = "Server Urls")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  [PXForeignReference(typeof (Field<DeviceUrls.servBranch>.IsRelatedTo<DeviceInitialise.branchID>))]
  public virtual int? ServBranch { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Register Items Url")]
  public virtual 
  #nullable disable
  string RegisterUrl { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Save Stock Item Url")]
  public virtual string SaveStockItem { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Save Stock Master Url")]
  public virtual string SaveStockMaster { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Save Stock Composition")]
  public virtual string SaveStockComposition { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Update Items Url")]
  public virtual string UpdateItemUrl { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Save Purchase Url")]
  public virtual string PurchaseUrl { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Credit Url")]
  public virtual string UpdateImportUrl { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Sales Url")]
  public virtual string SalesUrl { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  public abstract class servBranch : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceUrls.servBranch>
  {
  }

  public abstract class registerUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceUrls.registerUrl>
  {
  }

  public abstract class saveStockItem : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceUrls.saveStockItem>
  {
  }

  public abstract class saveStockMaster : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceUrls.saveStockMaster>
  {
  }

  public abstract class saveStockComposition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceUrls.saveStockComposition>
  {
  }

  public abstract class updateItemUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceUrls.updateItemUrl>
  {
  }

  public abstract class purchaseUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceUrls.purchaseUrl>
  {
  }

  public abstract class updateImportUrl : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceUrls.updateImportUrl>
  {
  }

  public abstract class salesUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceUrls.salesUrl>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceUrls.id>
  {
  }
}
