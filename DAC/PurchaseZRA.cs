// Decompiled with JetBrains decompiler
// Type: eTims.PurchaseZRA
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.PO;
using System;
using ZRASmartInvoice;

#nullable enable
namespace eTims;

[PXCacheName("PurchaseZRA")]
[Serializable]
public class PurchaseZRA : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Status")]
  [PXStringList(new string[] {"H", "A", "R"}, new string[] {"On Hold", "Approved", "Rejected"})]
  [PXDefault("H")]
  public virtual 
  #nullable disable
  string Status { get; set; }

  [PXDBString(200, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Supplier Pin")]
  public virtual string SpplrTpin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Supplier Branch Id")]
  public virtual string SpplrBhfId { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Supplier Invoice No")]
  public virtual int? SpplrInvcNo { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string RcptTyCd { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Payment Type")]
  public virtual string PmtTyCd { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Confirmed Date")]
  public virtual DateTime? CfmDt { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Sales Date")]
  public virtual DateTime? SalesDt { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Release Date")]
  public virtual DateTime? StockRlsDt { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Count")]
  public virtual int? TotItemCnt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Taxable Amount")]
  public virtual Decimal? TotTaxblAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tax Amount")]
  public virtual Decimal? TotTaxAmt { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Total Amount")]
  public virtual Decimal? TotAmt { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Remark")]
  public virtual string Remark { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "PO Number")]
  [PXSelector(typeof (Search<POOrder.orderNbr>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.vendorID), typeof (POOrder.vendorID_Vendor_acctName)})]
  public virtual string PONumber { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "PO Receipt")]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr>), new Type[] {typeof (PX.Objects.PO.POReceipt.receiptNbr), typeof (PX.Objects.PO.POReceipt.vendorID), typeof (PX.Objects.PO.POReceipt.vendorID_Vendor_acctName)})]
  public virtual string POReceipt { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "PO Bill")]
  [PXSelector(typeof (Search<APInvoice.refNbr>), new Type[] {typeof (APInvoice.refNbr), typeof (APInvoice.vendorID), typeof (APInvoice.vendorID_Vendor_acctName)})]
  public virtual string Pobill { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PaymentMethodKRA.code>), new Type[] {typeof (PaymentMethodKRA.code)}, DescriptionField = typeof (PaymentMethodKRA.codeName))]
  public virtual string PaymentMethod { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Receipt Type")]
  [PXSelector(typeof (Search<PurchaseReceiptType.code>), new Type[] {typeof (PurchaseReceiptType.code)}, DescriptionField = typeof (PurchaseReceiptType.codeName))]
  public virtual string ReceiptType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Purchase Status")]
  [PXSelector(typeof (Search<TransactionProgress.code>), new Type[] {typeof (TransactionProgress.code)}, DescriptionField = typeof (TransactionProgress.codeName))]
  public virtual string TransactionType { get; set; }

  [PXDBString(500, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Remarks")]
  public virtual string Remarks { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor Name")]
  [PXSelector(typeof (Search2<Vendor.bAccountID, LeftJoin<Location, On<Location.bAccountID, Equal<Vendor.bAccountID>>>>), new Type[] {typeof (Vendor.acctCD), typeof (Vendor.acctName), typeof (Location.taxRegistrationID)}, SubstituteKey = typeof (Vendor.acctCD), DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorName { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseZRA.id>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.status>
  {
  }

  public abstract class spplrTpin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.spplrTpin>
  {
  }

  public abstract class spplrBhfId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.spplrBhfId>
  {
  }

  public abstract class spplrInvcNo : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseZRA.spplrInvcNo>
  {
  }

  public abstract class rcptTyCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.rcptTyCd>
  {
  }

  public abstract class pmtTyCd : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.pmtTyCd>
  {
  }

  public abstract class cfmDt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PurchaseZRA.cfmDt>
  {
  }

  public abstract class salesDt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PurchaseZRA.salesDt>
  {
  }

  public abstract class stockRlsDt : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PurchaseZRA.stockRlsDt>
  {
  }

  public abstract class totItemCnt : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseZRA.totItemCnt>
  {
  }

  public abstract class totTaxblAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PurchaseZRA.totTaxblAmt>
  {
  }

  public abstract class totTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PurchaseZRA.totTaxAmt>
  {
  }

  public abstract class totAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PurchaseZRA.totAmt>
  {
  }

  public abstract class remark : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.remark>
  {
  }

  public abstract class pONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.pONumber>
  {
  }

  public abstract class pOReceipt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.pOReceipt>
  {
  }

  public abstract class pobill : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.pobill>
  {
  }

  public abstract class paymentMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.paymentMethod>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.receiptType>
  {
  }

  public abstract class transactionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PurchaseZRA.transactionType>
  {
  }

  public abstract class remarks : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PurchaseZRA.remarks>
  {
  }

  public abstract class vendorName : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PurchaseZRA.vendorName>
  {
  }
}
