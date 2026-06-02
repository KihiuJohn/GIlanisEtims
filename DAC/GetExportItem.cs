// Decompiled with JetBrains decompiler
// Type: eTims.GetExportItem
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.IN;
using System;

#nullable enable
namespace eTims;

[PXCacheName("GetExportItem")]
[Serializable]
public class GetExportItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Task Code")]
  public virtual 
  #nullable disable
  string TaskCode { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Declaration Date")]
  public virtual string DeclarationDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Sequence")]
  public virtual int? ItemSequence { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Declaration Number")]
  public virtual string DeclarationNumber { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Hscode")]
  public virtual string Hscode { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Name")]
  public virtual string ItemName { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Country Of Origin")]
  public virtual string CountryOfOrigin { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Export Country Code")]
  public virtual string ExportCountryCode { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Package")]
  public virtual Decimal? Package { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Packaging Code")]
  public virtual string PackagingCode { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Quantity { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Unit Of Quantity")]
  public virtual string UnitOfQuantity { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Gross Weight")]
  public virtual Decimal? GrossWeight { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Net Weight")]
  public virtual Decimal? NetWeight { get; set; }

  [PXDBString(250, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Supplier Name")]
  public virtual string SupplierName { get; set; }

  [PXDBString(250, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Agent Name")]
  public virtual string AgentName { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Foreign Currency Amount")]
  public virtual Decimal? ForeignCurrencyAmount { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Foreign Currency")]
  public virtual string ForeignCurrency { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Exchange Rate")]
  public virtual Decimal? ExchangeRate { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Dec Ref Nbr")]
  public virtual string DecRefNbr { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Import Item Status")]
  [PXSelector(typeof (Search<ImportItem.code>), new Type[] {typeof (ImportItem.code)}, DescriptionField = typeof (ImportItem.codeName))]
  public virtual string ImportItemStatus { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class code")]
  [PXSelector(typeof (Search<ItemClassificationCodees.itemClassificationCode>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string ClassificationCode { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXSelector(typeof (Search<InventoryItem.inventoryID>), new Type[] {typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr)}, SubstituteKey = typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor ID")]
  [PXSelector(typeof (Search<Vendor.bAccountID>), new Type[] {typeof (Vendor.acctCD), typeof (Vendor.acctName)}, SubstituteKey = typeof (Vendor.acctCD), DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorID { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Remarks")]
  public virtual string Remarks { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GetExportItem.id>
  {
  }

  public abstract class taskCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.taskCode>
  {
  }

  public abstract class declarationDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.declarationDate>
  {
  }

  public abstract class itemSequence : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GetExportItem.itemSequence>
  {
  }

  public abstract class declarationNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.declarationNumber>
  {
  }

  public abstract class hscode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.hscode>
  {
  }

  public abstract class itemName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.itemName>
  {
  }

  public abstract class countryOfOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.countryOfOrigin>
  {
  }

  public abstract class exportCountryCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.exportCountryCode>
  {
  }

  public abstract class package : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GetExportItem.package>
  {
  }

  public abstract class packagingCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.packagingCode>
  {
  }

  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GetExportItem.quantity>
  {
  }

  public abstract class unitOfQuantity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.unitOfQuantity>
  {
  }

  public abstract class grossWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GetExportItem.grossWeight>
  {
  }

  public abstract class netWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GetExportItem.netWeight>
  {
  }

  public abstract class supplierName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.supplierName>
  {
  }

  public abstract class agentName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.agentName>
  {
  }

  public abstract class foreignCurrencyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GetExportItem.foreignCurrencyAmount>
  {
  }

  public abstract class foreignCurrency : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.foreignCurrency>
  {
  }

  public abstract class exchangeRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GetExportItem.exchangeRate>
  {
  }

  public abstract class decRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.decRefNbr>
  {
  }

  public abstract class importItemStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.importItemStatus>
  {
  }

  public abstract class classificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GetExportItem.classificationCode>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GetExportItem.inventoryID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GetExportItem.vendorID>
  {
  }

  public abstract class remarks : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GetExportItem.remarks>
  {
  }
}
