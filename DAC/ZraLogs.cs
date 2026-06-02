// Decompiled with JetBrains decompiler
// Type: eTims.ZraLogs
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ZraLogs")]
[Serializable]
public class ZraLogs : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch Id")]
  public virtual 
  #nullable disable
  string BranchId { get; set; }

  [PXDBString(4000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Log")]
  public virtual string RequestLog { get; set; }

  [PXDBString(2000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Response Log")]
  public virtual string ResponseLog { get; set; }

  [PXDBString(4000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Log1")]
  public virtual string RequestLog1 { get; set; }

  [PXDBString(2000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Response Log1")]
  public virtual string ResponseLog1 { get; set; }

  [PXDBString(4000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Log2")]
  public virtual string RequestLog2 { get; set; }

  [PXDBString(2000, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Response Log2")]
  public virtual string ResponseLog2 { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Request Log One")]
  public virtual string RequestLogOne { get; set; }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Request Log Two")]
  public virtual string RequestLogTwo { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Document Type")]
  public virtual string DocumentType { get; set; }

  [PXDBString(100, InputMask = "")]
  [PXUIField(DisplayName = "Document Nbr")]
  public virtual string DocumentNbr { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ZraLogs.id>
  {
  }

  public abstract class branchId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.branchId>
  {
  }

  public abstract class requestLog : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.requestLog>
  {
  }

  public abstract class responseLog : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.responseLog>
  {
  }

  public abstract class requestLog1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.requestLog1>
  {
  }

  public abstract class responseLog1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.responseLog1>
  {
  }

  public abstract class requestLog2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.requestLog2>
  {
  }

  public abstract class responseLog2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.responseLog2>
  {
  }

  public abstract class requestLogOne : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.requestLogOne>
  {
  }

  public abstract class requestLogTwo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.requestLogTwo>
  {
  }

  public abstract class documentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.documentType>
  {
  }

  public abstract class documentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ZraLogs.documentNbr>
  {
  }
}
