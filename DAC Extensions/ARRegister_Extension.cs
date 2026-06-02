// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

public class ARRegisterExtEtims : PXCacheExtension<
#nullable disable
ARRegister>
{
  [PXDBBool]
  [PXUIField(DisplayName = "Invoice Validated With KRA")]
  public virtual bool? UsrIsEtimsValidated { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Etims Invoice Nbr")]
  [PXSelector(typeof (Search<ARRegisterExtEtims.usrEtimsInvoiceNbr>))]
  public virtual string UsrEtimsInvoiceNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Invoice Nbr")]
  [PXSelector(typeof (Search<ARRegisterExtEtims.usrOriginal>), new Type[] {typeof (ARRegisterExtEtims.usrOriginal), typeof (ARRegister.refNbr), typeof (ARRegister.customerID), typeof (ARRegister.customerID_Customer_acctName)}, ValidateValue = false)]
  public virtual int? UsrOriginal { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Original Invoice Nbr")]
  [PXSelector(typeof (Search<ARRegisterExtEtims.usrOriginal, Where<ARRegister.customerID, Equal<Current<ARRegister.customerID>>, And<ARRegister.docType, Equal<ARDocType.invoice>>>>), new Type[] {typeof (ARRegisterExtEtims.usrOriginal), typeof (ARRegister.refNbr), typeof (ARRegister.branchID), typeof (ARRegister.customerID), typeof (ARRegister.customerID_Customer_acctName)}, ValidateValue = false)]
  public virtual int? UsrOriginalReceiptNbr { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Original Invoice Nbr")]
  public virtual string UsrOriginalCRNInvoiceNo { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ZRA Invoice Nbr")]
  public virtual string UsrZRAReceiptNbr { get; set; }

  [PXDBString(29, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Date")]
  public virtual string UsrDate { get; set; }

  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Date")]
  public virtual string UsrTvDate { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "SCU ID")]
  public virtual string UsrCUNumber { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "SCU Invoice Nbr")]
  public virtual string UsrInvoiceNumber { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Internal Data")]
  public virtual string UsrInternalData { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Receipt Signature")]
  public virtual string UsrReceiptSignature { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string UsrResultMessage { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string UsrRsltMessage { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "QRCode")]
  public virtual string UsrQRCode { get; set; }

  [PXDBWeblink]
  [PXUIField(DisplayName = "QRCode")]
  public virtual string UsrQRCodee { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Customer TPin")]
  public virtual string UsrCustomrTin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "LPO Number")]
  public virtual string UsrLpo { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Credit Reason")]
  [PXSelector(typeof (Search<CreditReasonCode.code>), new Type[] {typeof (CreditReasonCode.code)}, DescriptionField = typeof (CreditReasonCode.codeName))]
  public virtual string UsrZRACreditReason { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Debit Reason")]
  [PXSelector(typeof (Search<DebitReasonCode.code>), new Type[] {typeof (DebitReasonCode.code)}, DescriptionField = typeof (DebitReasonCode.codeName))]
  public virtual string UsrZRADebitReason { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Payment Type")]
  [PXSelector(typeof (Search<PaymentMethodKRA.code>), new Type[] {typeof (PaymentMethodKRA.code)}, DescriptionField = typeof (PaymentMethodKRA.codeName))]
  public virtual string UsrZRAPaymentType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Sales Type")]
  [PXSelector(typeof (Search<SalesReceiptType.code>), new Type[] {typeof (SalesReceiptType.code)}, DescriptionField = typeof (SalesReceiptType.codeName))]
  public virtual string UsrZRASalesType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Destination Country")]
  [PXSelector(typeof (Search<Country.countryID>), new Type[] {typeof (Country.countryID)}, DescriptionField = typeof (Country.description))]
  public virtual string UsrZRAReceiptCode { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Sales Category")]
  [PXSelector(typeof (Search<SalesCategory.code>), new Type[] {typeof (SalesCategory.code)}, DescriptionField = typeof (SalesCategory.codeName))]
  public virtual string UsrSalesCategory { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Stock In/Out Type")]
  [PXSelector(typeof (Search<StockInOut.code>), new Type[] {typeof (StockInOut.code)}, DescriptionField = typeof (StockInOut.codeDescription))]
  public virtual string UsrStockInOutType { get; set; }

  public abstract class usrIsEtimsValidated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegisterExtEtims.usrIsEtimsValidated>
  {
  }

  public abstract class usrEtimsInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrEtimsInvoiceNbr>
  {
  }

  public abstract class usrOriginal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegisterExtEtims.usrOriginal>
  {
  }

  public abstract class usrOriginalReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegisterExtEtims.usrOriginalReceiptNbr>
  {
  }

  public abstract class usrOriginalCRNInvoiceNo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrOriginalCRNInvoiceNo>
  {
  }

  public abstract class usrZRAReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRAReceiptNbr>
  {
  }

  public abstract class usrDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterExtEtims.usrDate>
  {
  }

  public abstract class usrTvDate : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterExtEtims.usrTvDate>
  {
  }

  public abstract class usrCUNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrCUNumber>
  {
  }

  public abstract class usrInvoiceNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrInvoiceNumber>
  {
  }

  public abstract class usrInternalData : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrInternalData>
  {
  }

  public abstract class usrReceiptSignature : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrReceiptSignature>
  {
  }

  public abstract class usrResultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrResultMessage>
  {
  }

  public abstract class usrRsltMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrResultMessage>
  {
  }

  public abstract class usrQRCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterExtEtims.usrQRCode>
  {
  }

  public abstract class usrQRCodee : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterExtEtims.usrQRCodee>
  {
  }

  public abstract class usrCustomrTin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrCustomrTin>
  {
  }

  public abstract class usrLpo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegisterExtEtims.usrLpo>
  {
  }

  public abstract class usrZRACreditReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRACreditReason>
  {
  }

  public abstract class usrZRADebitReason : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRADebitReason>
  {
  }

  public abstract class usrZRAPaymentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRAPaymentType>
  {
  }

  public abstract class usrZRASalesType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRASalesType>
  {
  }

  public abstract class usrZRAReceiptCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrZRAReceiptCode>
  {
  }

  public abstract class usrSalesCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrSalesCategory>
  {
  }

  public abstract class usrStockInOutType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegisterExtEtims.usrStockInOutType>
  {
  }
}
