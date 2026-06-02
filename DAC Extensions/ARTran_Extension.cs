// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTranExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

public class ARTranExtEtims : PXCacheExtension<
#nullable disable
ARTran>
{
  [PXDBBool]
  [PXUIField(DisplayName = "Item Registered")]
  public virtual bool? UsrIsItemRegistered { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Item Code")]
  public virtual string UsrItemCode { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Item Classification Code")]
  public virtual string UsrItemClassificationCode { get; set; }

  [PXDBString(5)]
  [PXUIField(DisplayName = "Tax Type")]
  public virtual string UsrTaxType { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Packaging Unit")]
  public virtual string UsrPackagingUnit { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Unit Of Qty")]
  public virtual string UsrUnitOfQty { get; set; }

  [PXDBString(20)]
  [PXUIField(DisplayName = "Country Code")]
  [PXSelector(typeof (Search<Country.countryID>), new Type[] {typeof (Country.countryID)}, DescriptionField = typeof (Country.description))]
  public virtual string UsrCountryCode { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "ISale ID")]
  public virtual int? UsrIsaleID { get; set; }

  public abstract class usrIsItemRegistered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTranExtEtims.usrIsItemRegistered>
  {
  }

  public abstract class usrItemCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranExtEtims.usrItemCode>
  {
  }

  public abstract class usrItemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranExtEtims.usrItemClassificationCode>
  {
  }

  public abstract class usrTaxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranExtEtims.usrTaxType>
  {
  }

  public abstract class usrPackagingUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranExtEtims.usrPackagingUnit>
  {
  }

  public abstract class usrUnitOfQty : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranExtEtims.usrUnitOfQty>
  {
  }

  public abstract class usrCountryCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranExtEtims.usrCountryCode>
  {
  }

  public abstract class usrIsaleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranExtEtims.usrIsaleID>
  {
  }
}
