// Decompiled with JetBrains decompiler
// Type: eTims.DeviceInfo
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("DeviceInfo")]
[Serializable]
public class DeviceInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "KRA Pin")]
  public virtual 
  #nullable disable
  string KraPin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Taxpayer Name")]
  public virtual string TaxpayerName { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch Office ID")]
  public virtual string BranchOfficeID { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Device Id")]
  public virtual string DeviceId { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Sales Control Unit ID")]
  public virtual string SalesControlUnitID { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Mrcno")]
  public virtual string Mrcno { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Communication Key")]
  public virtual string CommunicationKey { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Result Message")]
  public virtual string ResultMessage { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Res Branch ID")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  [PXForeignReference(typeof (Field<DeviceInfo.resBranchID>.IsRelatedTo<DeviceInitialise.branchID>))]
  public virtual int? ResBranchID { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceInfo.id>
  {
  }

  public abstract class kraPin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.kraPin>
  {
  }

  public abstract class taxpayerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.taxpayerName>
  {
  }

  public abstract class branchOfficeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.branchOfficeID>
  {
  }

  public abstract class deviceId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.deviceId>
  {
  }

  public abstract class salesControlUnitID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceInfo.salesControlUnitID>
  {
  }

  public abstract class mrcno : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.mrcno>
  {
  }

  public abstract class communicationKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DeviceInfo.communicationKey>
  {
  }

  public abstract class resultMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceInfo.resultMessage>
  {
  }

  public abstract class resBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceInfo.resBranchID>
  {
  }
}
