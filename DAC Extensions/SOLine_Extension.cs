// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public class SOLineExtEtims : PXCacheExtension<
#nullable disable
SOLine>
{
  [PXDBBool]
  [PXUIField(DisplayName = "Item Registered")]
  public virtual bool? UsrIsItemRegistered { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Type")]
  public virtual string UsrTaxType { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Code")]
  public virtual string UsrItemClass { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code")]
  public virtual string UsrItemClassificationCode { get; set; }

  public abstract class usrIsItemRegistered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLineExtEtims.usrIsItemRegistered>
  {
  }

  public abstract class usrTaxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineExtEtims.usrTaxType>
  {
  }

  public abstract class usrItemClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLineExtEtims.usrItemClass>
  {
  }

  public abstract class usrItemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLineExtEtims.usrItemClassificationCode>
  {
  }
}
