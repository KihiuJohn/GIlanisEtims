// Decompiled with JetBrains decompiler
// Type: eTims.RequestPurchases
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("RequestPurchases")]
[Serializable]
public class RequestPurchases : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ZRA Pin")]
  public virtual 
  #nullable disable
  string Tpin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch Id")]
  public virtual string BranchId { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Latest Request Date")]
  public virtual DateTime? LatestResquest { get; set; }

  [PXDBString(250, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string ResultMessage { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Url")]
  public virtual string RequestUrl { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Purchases Fetched")]
  public virtual int? PurchasesFetched { get; set; }

  public abstract class tpin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestPurchases.tpin>
  {
  }

  public abstract class branchId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestPurchases.branchId>
  {
  }

  public abstract class latestResquest : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RequestPurchases.latestResquest>
  {
  }

  public abstract class resultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestPurchases.resultMessage>
  {
  }

  public abstract class requestUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestPurchases.requestUrl>
  {
  }

  public abstract class purchasesFetched : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RequestPurchases.purchasesFetched>
  {
  }
}
