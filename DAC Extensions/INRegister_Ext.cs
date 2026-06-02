// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRegisterExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INRegisterExtEtims : PXCacheExtension<
#nullable disable
INRegister>
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Stock In/Out Type")]
  [PXSelector(typeof (Search<StockInOut.code>), new Type[] {typeof (StockInOut.code)}, DescriptionField = typeof (StockInOut.codeDescription))]
  public virtual string UsrStockInOut { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Registration Type")]
  [PXSelector(typeof (Search<RegistrationType.code>), new Type[] {typeof (RegistrationType.code)}, DescriptionField = typeof (RegistrationType.codeName))]
  public virtual string UsrRegType { get; set; }

  [PXDBString(400, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string UsrResultMessage { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Saved")]
  public virtual bool? UsrIsSaved { get; set; }

  public abstract class usrStockInOut : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterExtEtims.usrStockInOut>
  {
  }

  public abstract class usrRegType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INRegisterExtEtims.usrRegType>
  {
  }

  public abstract class usrResultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INRegisterExtEtims.usrResultMessage>
  {
  }

  public abstract class usrIsSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INRegisterExtEtims.usrIsSaved>
  {
  }
}
