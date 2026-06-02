// Decompiled with JetBrains decompiler
// Type: eTims.ResponseData
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

#nullable disable
namespace eTims;

public class ResponseData
{
  public bool status { get; set; }

  public string message { get; set; }

  public KraResult kraResult { get; set; }

  public SaleRes sale { get; set; }

  public ExistSaleRes existingSale { get; set; }

  public ExistCreditRes creditNote { get; set; }

  public ExistCreditResponse credit { get; set; }
}
