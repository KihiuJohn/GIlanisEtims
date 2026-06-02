// Decompiled with JetBrains decompiler
// Type: eTims.UnitMappingMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace eTims;

public class UnitMappingMaint : PXGraph<UnitMappingMaint>
{
  public PXSave<UomMapping> Save;
  public PXCancel<UomMapping> Cancel;
  public PXSelect<UomMapping> UomMappingView;

  public UnitMappingMaint() => this.PopulateUOM();

  private void PopulateUOM()
  {
    foreach (PXResult<INUnit> pxResult in PXSelectBase<INUnit, PXSelect<INUnit>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      INUnit inUnit = ((INUnit)(pxResult));
      if (((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) inUnit.FromUnit
      }))) == null)
        ((PXSelectBase<UomMapping>) this.UomMappingView).Insert(new UomMapping()
        {
          UnitOfMeasure = inUnit.FromUnit
        });
    }
    ((PXGraph) this).Actions.PressSave();
  }
}
