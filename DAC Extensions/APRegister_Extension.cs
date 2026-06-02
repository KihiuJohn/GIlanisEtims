// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegisterExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;
using ZRASmartInvoice;

#nullable enable
namespace PX.Objects.AP;

public class APRegisterExtEtims : PXCacheExtension<
#nullable disable
APRegister>
{
  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string UsrResultMessage { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Purchase Saved")]
  public virtual bool? UsrIsPurchaseSaved { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Payment Type")]
  [PXSelector(typeof (Search<PaymentMethodKRA.code>), new Type[] {typeof (PaymentMethodKRA.code)}, DescriptionField = typeof (PaymentMethodKRA.codeName))]
  public virtual string UsrPaymentMethodZRA { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Receipt Type")]
  [PXSelector(typeof (Search<PurchaseReceiptType.code>), new Type[] {typeof (PurchaseReceiptType.code)}, DescriptionField = typeof (PurchaseReceiptType.codeName))]
  public virtual string UsrReceiptType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Purchase Type")]
  [PXSelector(typeof (Search<TransactionTypeZRA.code>), new Type[] {typeof (TransactionTypeZRA.code)}, DescriptionField = typeof (TransactionTypeZRA.codeName))]
  public virtual string UsrTransactionType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Registration Type")]
  [PXSelector(typeof (Search<RegistrationType.code>), new Type[] {typeof (RegistrationType.code)}, DescriptionField = typeof (RegistrationType.codeName))]
  public virtual string UsrRegistrationType { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Stock In/Out Type")]
  [PXSelector(typeof (Search<StockInOut.code>), new Type[] {typeof (StockInOut.code)}, DescriptionField = typeof (StockInOut.codeDescription))]
  public virtual string UsrStockInOutType { get; set; }

  public abstract class usrResultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrResultMessage>
  {
  }

  public abstract class usrIsPurchaseSaved : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegisterExtEtims.usrIsPurchaseSaved>
  {
  }

  public abstract class usrPaymentMethodZRA : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrPaymentMethodZRA>
  {
  }

  public abstract class usrReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrReceiptType>
  {
  }

  public abstract class usrTransactionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrTransactionType>
  {
  }

  public abstract class usrRegistrationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrRegistrationType>
  {
  }

  public abstract class usrStockInOutType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegisterExtEtims.usrStockInOutType>
  {
  }
}
