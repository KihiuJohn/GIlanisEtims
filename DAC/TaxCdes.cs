// Decompiled with JetBrains decompiler
// Type: eTims.TaxCdes
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("TaxCdes")]
[Serializable]
public class TaxCdes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Categories", Enabled = false)]
  public virtual 
  #nullable disable
  string TaxCategories { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code")]
  [PXSelector(typeof (Search4<KRATaxCodes.taxCode, Aggregate<GroupBy<KRATaxCodes.taxCode>>>), new Type[] {typeof (KRATaxCodes.taxCode), typeof (KRATaxCodes.taxRate), typeof (KRATaxCodes.codeDescription)})]
  public virtual string TaxCode { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Tax Rate", Enabled = false)]
  [PXFormula(typeof (Selector<TaxCdes.taxCode, KRATaxCodes.taxRate>))]
  public virtual Decimal? TaxRate { get; set; }

  [PXDBString(200, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Description", Enabled = false)]
  [PXFormula(typeof (Selector<TaxCdes.taxCode, KRATaxCodes.codeDescription>))]
  public virtual string CodeDescription { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxCdes.id>
  {
  }

  public abstract class taxCategories : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCdes.taxCategories>
  {
  }

  public abstract class taxCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCdes.taxCode>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxCdes.taxRate>
  {
  }

  public abstract class codeDescription : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxCdes.codeDescription>
  {
  }
}
