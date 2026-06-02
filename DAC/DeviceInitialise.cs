// Decompiled with JetBrains decompiler
// Type: eTims.DeviceInitialise
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("DeviceInitialise")]
[Serializable]
public class DeviceInitialise : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "KRA Pin")]
  public virtual 
  #nullable disable
  string KraPin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "KRA Branch ID")]
  public virtual string KraBranchID { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Device Serial No")]
  public virtual string DeviceSerialNo { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Branch ID")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Password")]
  public virtual string Password { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Platform")]
  public virtual string Platform { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceInitialise.id>
  {
  }

  public abstract class kraPin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInitialise.kraPin>
  {
  }

  public abstract class kraBranchID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInitialise.kraBranchID>
  {
  }

  public abstract class deviceSerialNo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceInitialise.deviceSerialNo>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceInitialise.branchID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DeviceInitialise.active>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInitialise.password>
  {
  }

  public abstract class platform : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInitialise.platform>
  {
  }
}
