// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTranExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AP;

public class APTranExtEtims : PXCacheExtension<
#nullable disable
APTran>
{
  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Packaging Unit")]
  public virtual string UsrPackagingUnit { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Quantity Unit")]
  public virtual string UsrQuantityUnit { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code")]
  public virtual string UsrTaxCode { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Classification Code")]
  public virtual string UsrClassificationCode { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Country Code")]
  [PXSelector(typeof (Search<Country.countryID>), new Type[] {typeof (Country.countryID)}, DescriptionField = typeof (Country.description))]
  public virtual string UsrCountryCode { get; set; }

  public abstract class usrPackagingUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranExtEtims.usrPackagingUnit>
  {
  }

  public abstract class usrQuantityUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranExtEtims.usrQuantityUnit>
  {
  }

  public abstract class usrTaxCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranExtEtims.usrTaxCode>
  {
  }

  public abstract class usrClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranExtEtims.usrClassificationCode>
  {
  }

  public abstract class usrCountryCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranExtEtims.usrCountryCode>
  {
  }
}
