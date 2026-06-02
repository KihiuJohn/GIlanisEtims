// Decompiled with JetBrains decompiler
// Type: eTims.PackageMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;

#nullable disable
namespace eTims;

public class PackageMaint : PXGraph<PackageMaint>
{
  public PXSave<Packaging> Save;
  public PXCancel<Packaging> Cancel;
  [PXImport(typeof (Packaging))]
  public PXSelect<Packaging> PackageView;

  public PackageMaint() => ((PXSelectBase) this.PackageView).AllowDelete = false;
}
