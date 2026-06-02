// Decompiled with JetBrains decompiler
// Type: eTims.StandardCodes
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("StandardCodes")]
[Serializable]
public class StandardCodes : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Zra Pin")]
  public virtual 
  #nullable disable
  string ZraPin { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch Id")]
  public virtual string BranchId { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Request Date")]
  public virtual DateTime? LastRequestDate { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string ResultMessage { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Url")]
  public virtual string RequestUrl { get; set; }

  public abstract class zraPin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StandardCodes.zraPin>
  {
  }

  public abstract class branchId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StandardCodes.branchId>
  {
  }

  public abstract class lastRequestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    StandardCodes.lastRequestDate>
  {
  }

  public abstract class resultMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StandardCodes.resultMessage>
  {
  }

  public abstract class requestUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  StandardCodes.requestUrl>
  {
  }
}
