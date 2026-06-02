// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodExttEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

public class PaymentMethodExttEtims : PXCacheExtension<
#nullable disable
PaymentMethod>
{
  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ZRA Payment Code")]
  [PXSelector(typeof (Search<PaymentMethodKRA.code>), new Type[] {typeof (PaymentMethodKRA.code)}, DescriptionField = typeof (PaymentMethodKRA.codeName))]
  public virtual string UsrKraPaymentCode { get; set; }

  public abstract class usrKraPaymentCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodExttEtims.usrKraPaymentCode>
  {
  }
}
