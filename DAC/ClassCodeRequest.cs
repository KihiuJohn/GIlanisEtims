// Decompiled with JetBrains decompiler
// Type: eTims.ClassCodeRequests
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ClassCodeRequest")]
[Serializable]
public class ClassCodeRequests : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ZRA Pin")]
  public virtual 
  #nullable disable
  string KraPin { get; set; }

  [PXDBString(10, IsFixed = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch Id")]
  public virtual string BranchId { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Request Date")]
  public virtual DateTime? LastRequestDate { get; set; }

  [PXDBString(70, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string ResultMessage { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Url")]
  public virtual string RequestUrl { get; set; }

  public abstract class kraPin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ClassCodeRequests.kraPin>
  {
  }

  public abstract class branchId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ClassCodeRequests.branchId>
  {
  }

  public abstract class lastRequestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ClassCodeRequests.lastRequestDate>
  {
  }

  public abstract class resultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ClassCodeRequests.resultMessage>
  {
  }

  public abstract class requestUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ClassCodeRequests.requestUrl>
  {
  }
}
