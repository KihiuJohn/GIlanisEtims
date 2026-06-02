// Decompiled with JetBrains decompiler
// Type: eTims.DeviceToken
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

[PXCacheName("DeviceToken")]
[Serializable]
public class DeviceToken : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Token Branch")]
  [PXSelector(typeof (Search<Branch.branchID>), SubstituteKey = typeof (Branch.branchCD), DescriptionField = typeof (Branch.acctName))]
  [PXForeignReference(typeof (Field<DeviceToken.tokenBranch>.IsRelatedTo<DeviceInitialise.branchID>))]
  public virtual int? TokenBranch { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Access Token")]
  public virtual 
  #nullable disable
  string AccessToken { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceToken.id>
  {
  }

  public abstract class tokenBranch : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DeviceToken.tokenBranch>
  {
  }

  public abstract class accessToken : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DeviceToken.accessToken>
  {
  }
}
