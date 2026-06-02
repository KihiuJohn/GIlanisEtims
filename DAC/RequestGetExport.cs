// Decompiled with JetBrains decompiler
// Type: eTims.RequestGetExport
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("RequestGetExport")]
[Serializable]
public class RequestGetExport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ZRA pin")]
  public virtual 
  #nullable disable
  string Zrapin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch ID")]
  public virtual string BranchID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Request Date")]
  public virtual DateTime? LastRequestDate { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Declaration Ref Nbr")]
  public virtual string DeclarationRefNbr { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Url")]
  public virtual string RequestUrl { get; set; }

  [PXDBString(250, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string ResultMessage { get; set; }

  public abstract class zrapin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestGetExport.zrapin>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestGetExport.branchID>
  {
  }

  public abstract class lastRequestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RequestGetExport.lastRequestDate>
  {
  }

  public abstract class declarationRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestGetExport.declarationRefNbr>
  {
  }

  public abstract class requestUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RequestGetExport.requestUrl>
  {
  }

  public abstract class resultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RequestGetExport.resultMessage>
  {
  }
}
