// Decompiled with JetBrains decompiler
// Type: eTims.RegisterItems
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace eTims;

[PXCacheName("RegisterItems")]
[Serializable]
public class RegisterItems : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Warehouse")]
  [PXSelector(typeof (Search<INSite.siteCD>), new Type[] {typeof (INSite.siteCD), typeof (INSite.descr)}, SubstituteKey = typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
  [PXDefault(typeof (Search<INSite.siteCD, Where<INSite.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  public virtual 
  #nullable disable
  string Warehouse { get; set; }

  public abstract class warehouse : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RegisterItems.warehouse>
  {
  }
}
