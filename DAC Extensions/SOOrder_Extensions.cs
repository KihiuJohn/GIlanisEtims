// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public class SOOrderExtEtims : PXCacheExtension<
#nullable disable
SOOrder>
{
  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Customer Pin")]
  public virtual string UsrCustomersTin { get; set; }

  public abstract class usrCustomersTin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderExtEtims.usrCustomersTin>
  {
  }
}
