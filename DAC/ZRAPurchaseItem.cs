// Decompiled with JetBrains decompiler
// Type: eTims.ZRAPurchaseItem
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ZRAPurchaseItem")]
[Serializable]
public class ZRAPurchaseItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField(DisplayName = "Item Seq")]
  public virtual int? ItemSeq { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Invoice Nbr")]
  [PXForeignReference(typeof (Field<ZRAPurchaseItem.spplrInvcNo>.IsRelatedTo<PurchaseZRA.spplrInvcNo>))]
  public virtual int? SpplrInvcNo { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Code")]
  public virtual 
  #nullable disable
  string ItemCd { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class Code")]
  public virtual string ItemClsCd { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Name")]
  public virtual string ItemNm { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Pkg Unit")]
  public virtual string PkgUnitCd { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Pkg")]
  public virtual Decimal? Pkg { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Qty Code")]
  public virtual string QtyUnitCd { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Qty")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Price")]
  public virtual Decimal? Prc { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Supply Amt")]
  public virtual Decimal? SplyAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Discount Rate")]
  public virtual Decimal? Dcrt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Discount Amount")]
  public virtual Decimal? DcAmt { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Vat Code")]
  public virtual string VatCatCd { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Ipl Code")]
  public virtual string IplCatCd { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tl Code")]
  public virtual string TlCatCd { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Excise Tax Code")]
  public virtual string ExciseTxCatCd { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Taxable Amt")]
  public virtual Decimal? VatTaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Excise Taxable Amt")]
  public virtual Decimal? ExciseTaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Ipl Taxable Amt")]
  public virtual Decimal? IplTaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tl Taxable Amt")]
  public virtual Decimal? TlTaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Taxble Amt")]
  public virtual Decimal? TaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Vat Amt")]
  public virtual Decimal? VatAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Ipl Amt")]
  public virtual Decimal? IplAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tl Amt")]
  public virtual Decimal? TlAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Excise Tx Amt")]
  public virtual Decimal? ExciseTxAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tot Amt")]
  public virtual Decimal? TotAmt { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXSelector(typeof (Search<InventoryItem.inventoryID>), new Type[] {typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr)}, SubstituteKey = typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  public abstract class itemSeq : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZRAPurchaseItem.itemSeq>
  {
  }

  public abstract class spplrInvcNo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZRAPurchaseItem.spplrInvcNo>
  {
  }

  public abstract class itemCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.itemCd>
  {
  }

  public abstract class itemClsCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.itemClsCd>
  {
  }

  public abstract class itemNm : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.itemNm>
  {
  }

  public abstract class pkgUnitCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.pkgUnitCd>
  {
  }

  public abstract class pkg : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.pkg>
  {
  }

  public abstract class qtyUnitCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.qtyUnitCd>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.qty>
  {
  }

  public abstract class prc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.prc>
  {
  }

  public abstract class splyAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.splyAmt>
  {
  }

  public abstract class dcrt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.dcrt>
  {
  }

  public abstract class dcAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.dcAmt>
  {
  }

  public abstract class vatCatCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.vatCatCd>
  {
  }

  public abstract class iplCatCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.iplCatCd>
  {
  }

  public abstract class tlCatCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZRAPurchaseItem.tlCatCd>
  {
  }

  public abstract class exciseTxCatCd : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ZRAPurchaseItem.exciseTxCatCd>
  {
  }

  public abstract class vatTaxblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ZRAPurchaseItem.vatTaxblAmt>
  {
  }

  public abstract class exciseTaxblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ZRAPurchaseItem.exciseTaxblAmt>
  {
  }

  public abstract class iplTaxblAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ZRAPurchaseItem.iplTaxblAmt>
  {
  }

  public abstract class tlTaxblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.tlTaxblAmt>
  {
  }

  public abstract class taxblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.taxblAmt>
  {
  }

  public abstract class vatAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.vatAmt>
  {
  }

  public abstract class iplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.iplAmt>
  {
  }

  public abstract class tlAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.tlAmt>
  {
  }

  public abstract class exciseTxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ZRAPurchaseItem.exciseTxAmt>
  {
  }

  public abstract class totAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ZRAPurchaseItem.totAmt>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZRAPurchaseItem.id>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZRAPurchaseItem.inventoryID>
  {
  }
}
