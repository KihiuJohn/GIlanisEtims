// Decompiled with JetBrains decompiler
// Type: eTims.PaymentMethodKRA
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("PaymentMethodKRA")]
[Serializable]
public class PaymentMethodKRA : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code")]
  public virtual 
  #nullable disable
  string Code { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Name")]
  public virtual string CodeName { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentMethodKRA.id>
  {
  }

  public abstract class code : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodKRA.code>
  {
  }

  public abstract class codeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentMethodKRA.codeName>
  {
  }
}
