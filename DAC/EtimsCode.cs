// Decompiled with JetBrains decompiler
// Type: eTims.EtimsCode
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("EtimsCode")]
[Serializable]
public class EtimsCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code", Enabled = false)]
  public virtual 
  #nullable disable
  string TaxCode { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tax Rate", Enabled = false)]
  public virtual Decimal? TaxRate { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Description", Enabled = false)]
  public virtual string CodeDescription { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EtimsCode.id>
  {
  }

  public abstract class taxCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EtimsCode.taxCode>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EtimsCode.taxRate>
  {
  }

  public abstract class codeDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EtimsCode.codeDescription>
  {
  }
}
