// Decompiled with JetBrains decompiler
// Type: eTims.SaleItem
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;

#nullable disable
namespace eTims;

public class SaleItem
{
  public int itemSeq { get; set; }

  public string itemCd { get; set; }

  public string itemClsCd { get; set; }

  public string itemNm { get; set; }

  public string bcd { get; set; }

  public string pkgUnitCd { get; set; }

  public Decimal pkg { get; set; }

  public string qtyUnitCd { get; set; }

  public Decimal qty { get; set; }

  public Decimal prc { get; set; }

  public Decimal splyAmt { get; set; }

  public Decimal dcRt { get; set; }

  public Decimal dcAmt { get; set; }

  public string vatCatCd { get; set; }

  public string iplCatCd { get; set; }

  public string tlCatCd { get; set; }

  public string exciseTxCatCd { get; set; }

  public Decimal vatTaxblAmt { get; set; }

  public Decimal exciseTaxblAmt { get; set; }

  public Decimal iplTaxblAmt { get; set; }

  public Decimal tlTaxblAmt { get; set; }

  public Decimal taxblAmt { get; set; }

  public Decimal vatAmt { get; set; }

  public Decimal iplAmt { get; set; }

  public Decimal tlAmt { get; set; }

  public Decimal exciseTxAmt { get; set; }

  public Decimal totAmt { get; set; }
}
