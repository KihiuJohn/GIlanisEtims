// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSiteExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public class INItemSiteExtEtims : PXCacheExtension<
#nullable disable
INItemSite>
{
  [PXDBBool]
  [PXUIField(DisplayName = "Is Registered With KRA")]
  public virtual bool? UsrIsRegistered { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "ISale ID")]
  public virtual int? UsrIsaleID { get; set; }

  public abstract class usrIsRegistered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSiteExtEtims.usrIsRegistered>
  {
  }

  public abstract class usrIsaleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSiteExtEtims.usrIsaleID>
  {
  }
}
