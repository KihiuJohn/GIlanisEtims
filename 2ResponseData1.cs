// Decompiled with JetBrains decompiler
// Type: eTims.Item
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;

#nullable disable
namespace eTims;

public class Item
{
  public int itemSeq { get; set; }

  public int itemId { get; set; }

  public string itemCd { get; set; }

  public string itemClsCd { get; set; }

  public string itemNm { get; set; }

  public string itemNmDef { get; set; }

  public string bcd { get; set; }

  public string pkgUnitCd { get; set; }

  public Decimal pkg { get; set; }

  public string qtyUnitCd { get; set; }

  public Decimal qty { get; set; }

  public Decimal prc { get; set; }

  public Decimal splyAmt { get; set; }

  public Decimal dcRt { get; set; }

  public Decimal dcAmt { get; set; }

  public string taxTyCd { get; set; }

  public Decimal taxblAmt { get; set; }

  public Decimal taxAmt { get; set; }

  public Decimal totAmt { get; set; }
}
