// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClassExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INItemClassExtEtims : PXCacheExtension<
#nullable disable
INItemClass>
{
  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Codes")]
  [PXSelector(typeof (Search<ItemClassificationCodees.itemClassificationCode>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string UsrItemClassificationCode { get; set; }

  public abstract class usrItemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassExtEtims.usrItemClassificationCode>
  {
  }
}
