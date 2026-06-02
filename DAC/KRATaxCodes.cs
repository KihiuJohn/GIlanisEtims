// Decompiled with JetBrains decompiler
// Type: eTims.KRATaxCodes
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("KRATaxCodes")]
[Serializable]
public class KRATaxCodes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code")]
  public virtual 
  #nullable disable
  string TaxCode { get; set; }

  [PXDBDecimal]
  [PXUIField(DisplayName = "Tax Rate")]
  public virtual Decimal? TaxRate { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Description")]
  public virtual string CodeDescription { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch ID")]
  public virtual string ZRABranch { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KRATaxCodes.id>
  {
  }

  public abstract class taxCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KRATaxCodes.taxCode>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  KRATaxCodes.taxRate>
  {
  }

  public abstract class codeDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KRATaxCodes.codeDescription>
  {
  }

  public abstract class zRABranch : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KRATaxCodes.zRABranch>
  {
  }
}
