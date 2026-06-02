// Decompiled with JetBrains decompiler
// Type: eTims.ItemPayload
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;

#nullable disable
namespace eTims;

public class ItemPayload
{
  public string tpin { get; set; }

  public string bhfId { get; set; }

  public string itemCd { get; set; }

  public string itemCdDf { get; set; }

  public string itemClsCd { get; set; }

  public string itemTyCd { get; set; }

  public string itemNm { get; set; }

  public string itemStdNm { get; set; }

  public string addInfo { get; set; }

  public string bcd { get; set; }

  public string btchNo { get; set; }

  public string orgnNatCd { get; set; }

  public string pkgUnitCd { get; set; }

  public string qtyUnitCd { get; set; }

  public string taxTyCd { get; set; }

  public Decimal dftPrc { get; set; }

  public Decimal grpPrcL1 { get; set; }

  public Decimal grpPrcL2 { get; set; }

  public Decimal grpPrcL3 { get; set; }

  public Decimal grpPrcL4 { get; set; }

  public Decimal currentStock { get; set; }

  public string isrcAplcbYn { get; set; }

  public string useYn { get; set; }

  public string regrId { get; set; }

  public string regrNm { get; set; }

  public string modrId { get; set; }

  public string modrNm { get; set; }

  public string exciseTxCatCd { get; set; }

  public string vatCatCd { get; set; }

  public string iplCatCd { get; set; }

  public string tlCatCd { get; set; }
}
