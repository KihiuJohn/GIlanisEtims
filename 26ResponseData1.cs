// Decompiled with JetBrains decompiler
// Type: eTims.SaleResponse
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

#nullable disable
namespace eTims;

public class SaleResponse
{
  public string resultCd { get; set; }

  public string resultMsg { get; set; }

  public string resultDt { get; set; }

  public SaleData data { get; set; }
}
