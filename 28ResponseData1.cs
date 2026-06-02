// Decompiled with JetBrains decompiler
// Type: eTims.Sale
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class Sale
{
  public string spplrTpin { get; set; }

  public string spplrNm { get; set; }

  public string spplrBhfId { get; set; }

  public int spplrInvcNo { get; set; }

  public string rcptTyCd { get; set; }

  public string pmtTyCd { get; set; }

  public string cfmDt { get; set; }

  public string salesDt { get; set; }

  public string stockRlsDt { get; set; }

  public int totItemCnt { get; set; }

  public Decimal totTaxblAmt { get; set; }

  public Decimal totTaxAmt { get; set; }

  public Decimal totAmt { get; set; }

  public string remark { get; set; }

  public List<SaleItem> itemList { get; set; }
}
