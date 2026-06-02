// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INTranExtEtims : PXCacheExtension<
#nullable disable
INTran>
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Packaging Unit")]
  public virtual string UsrPackagingUnit { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Unit Of Qty")]
  public virtual string UsrUnitOfQty { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Classification Codes")]
  public virtual string UsrClassificationCodes { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code")]
  public virtual string UsrTaxCode { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Country Code")]
  [PXSelector(typeof (Search<Country.countryID>), new Type[] {typeof (Country.countryID)}, DescriptionField = typeof (Country.description))]
  public virtual string UsrCountryCode { get; set; }

  public abstract class usrPackagingUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranExtEtims.usrPackagingUnit>
  {
  }

  public abstract class usrUnitOfQty : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranExtEtims.usrUnitOfQty>
  {
  }

  public abstract class usrClassificationCodes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranExtEtims.usrClassificationCodes>
  {
  }

  public abstract class usrTaxCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranExtEtims.usrTaxCode>
  {
  }

  public abstract class usrCountryCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranExtEtims.usrCountryCode>
  {
  }
}
