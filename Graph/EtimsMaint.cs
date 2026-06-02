// Decompiled with JetBrains decompiler
// Type: eTims.EtimsMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using System;

#nullable disable
namespace eTims;

public class EtimsMaint : PXGraph<EtimsMaint>
{
  public PXSave<EtimsCode> Save;
  public PXCancel<EtimsCode> Cancel;
  public PXSelect<EtimsCode> EtimsView;

  public void Initialize()
  {
    this.InsertStaticRows();
    ((PXSelectBase) this.EtimsView).AllowDelete = false;
    ((PXSelectBase) this.EtimsView).AllowInsert = false;
  }

  private void InsertStaticRows()
  {
    if (((PXSelectBase<EtimsCode>) this.EtimsView).Select(Array.Empty<object>()).Count != 0)
      return;
    ((PXSelectBase<EtimsCode>) this.EtimsView).Insert(new EtimsCode()
    {
      TaxCode = "A",
      TaxRate = new Decimal?(0.000000M),
      CodeDescription = "A- Exempt"
    });
    ((PXSelectBase<EtimsCode>) this.EtimsView).Insert(new EtimsCode()
    {
      TaxCode = "B",
      TaxRate = new Decimal?(16.000000M),
      CodeDescription = "B- 16%"
    });
    ((PXSelectBase<EtimsCode>) this.EtimsView).Insert(new EtimsCode()
    {
      TaxCode = "C",
      TaxRate = new Decimal?(0.000000M),
      CodeDescription = "C-Zero Rated"
    });
    ((PXSelectBase<EtimsCode>) this.EtimsView).Insert(new EtimsCode()
    {
      TaxCode = "D",
      TaxRate = new Decimal?(0.000000M),
      CodeDescription = "D- Non-VAT"
    });
    ((PXSelectBase<EtimsCode>) this.EtimsView).Insert(new EtimsCode()
    {
      TaxCode = "E",
      TaxRate = new Decimal?(8.000000M),
      CodeDescription = "E- 8%"
    });
    ((PXGraph) this).Actions.PressSave();
  }
}
