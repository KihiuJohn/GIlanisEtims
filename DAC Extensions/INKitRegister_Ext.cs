// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitRegisterExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INKitRegisterExtEtims : PXCacheExtension<
#nullable disable
INKitRegister>
{
  [PXDBString(400, IsUnicode = true, InputMask = "", BqlField = typeof (INRegisterExtEtims.usrResultMessage))]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string UsrResultMessage { get; set; }

  [PXDBBool(BqlField = typeof (INRegisterExtEtims.usrIsSaved))]
  [PXUIField(DisplayName = "Is Saved")]
  public virtual bool? UsrIsSaved { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "", BqlField = typeof (INRegisterExtEtims.usrStockInOut))]
  [PXUIField(DisplayName = "Stock In/Out Type")]
  [PXSelector(typeof (Search<StockInOut.code>), new Type[] {typeof (StockInOut.code)}, DescriptionField = typeof (StockInOut.codeDescription))]
  public virtual string UsrBranchLogs { get; set; }

  public abstract class usrResultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegisterExtEtims.usrResultMessage>
  {
  }

  public abstract class usrIsSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegisterExtEtims.usrIsSaved>
  {
  }

  public abstract class usrBranchLogs : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegisterExtEtims.usrBranchLogs>
  {
  }
}
