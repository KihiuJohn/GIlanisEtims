// Decompiled with JetBrains decompiler
// Type: eTims.PurchasesData
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class PurchasesData
{
  public string tpin { get; set; }

  public string bhfId { get; set; }

  public string cisInvcNo { get; set; }

  public int orgInvcNo { get; set; }

  public string spplrTpin { get; set; }

  public string spplrNm { get; set; }

  public string spplrInvcNo { get; set; }

  public string regTyCd { get; set; }

  public string pchsTyCd { get; set; }

  public string rcptTyCd { get; set; }

  public string pmtTyCd { get; set; }

  public string pchsSttsCd { get; set; }

  public string cfmDt { get; set; }

  public string pchsDt { get; set; }

  public int totItemCnt { get; set; }

  public Decimal totTaxblAmt { get; set; }

  public Decimal totTaxAmt { get; set; }

  public Decimal totAmt { get; set; }

  public string remark { get; set; }

  public string regrId { get; set; }

  public string regrNm { get; set; }

  public string modrId { get; set; }

  public string modrNm { get; set; }

  public List<PurchaseItems> itemList { get; set; }
}
