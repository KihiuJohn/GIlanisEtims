// Decompiled with JetBrains decompiler
// Type: eTims.RequestData
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class RequestData
{
  public string tin { get; set; }

  public string bhfId { get; set; }

  public string trdInvcNo { get; set; }

  public int invcNo { get; set; }

  public string custType { get; set; }

  public string custMblNo { get; set; }

  public string custID { get; set; }

  public string custEmail { get; set; }

  public int orgInvcNo { get; set; }

  public string custTin { get; set; }

  public string custNm { get; set; }

  public string salesTyCd { get; set; }

  public string rcptTyCd { get; set; }

  public string pmtTyCd { get; set; }

  public string salesSttsCd { get; set; }

  public string cfmDt { get; set; }

  public string salesDt { get; set; }

  public string stockRlsDt { get; set; }

  public string cnclReqDt { get; set; }

  public string cnclDt { get; set; }

  public string rfdDt { get; set; }

  public string rfdRsnCd { get; set; }

  public string dbtRsnCd { get; set; }

  public int totItemCnt { get; set; }

  public Decimal taxblAmtA { get; set; }

  public Decimal taxblAmtB { get; set; }

  public Decimal taxblAmtC { get; set; }

  public Decimal taxblAmtD { get; set; }

  public Decimal taxblAmtE { get; set; }

  public Decimal taxRtA { get; set; }

  public Decimal taxRtB { get; set; }

  public Decimal taxRtC { get; set; }

  public Decimal taxRtD { get; set; }

  public Decimal taxRtE { get; set; }

  public Decimal taxAmtA { get; set; }

  public Decimal taxAmtB { get; set; }

  public Decimal taxAmtC { get; set; }

  public Decimal taxAmtD { get; set; }

  public Decimal taxAmtE { get; set; }

  public Decimal cashDcRt { get; set; }

  public Decimal cashDcAmt { get; set; }

  public Decimal totTaxblAmt { get; set; }

  public Decimal totTaxAmt { get; set; }

  public Decimal totAmt { get; set; }

  public string prchrAcptcYn { get; set; }

  public string remark { get; set; }

  public string regrId { get; set; }

  public string regrNm { get; set; }

  public string modrId { get; set; }

  public string modrNm { get; set; }

  public ReceiptEtims receipt { get; set; }

  public List<Item> itemList { get; set; }
}
