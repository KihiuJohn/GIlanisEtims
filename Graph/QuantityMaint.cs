// Decompiled with JetBrains decompiler
// Type: eTims.QuantityMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;

#nullable disable
namespace eTims;

public class QuantityMaint : PXGraph<QuantityMaint>
{
  public PXSave<QuantityUnit> Save;
  public PXCancel<QuantityUnit> Cancel;
  [PXImport(typeof (QuantityUnit))]
  public PXSelect<QuantityUnit> QuantityView;

  public QuantityMaint() => ((PXSelectBase) this.QuantityView).AllowDelete = false;
}
