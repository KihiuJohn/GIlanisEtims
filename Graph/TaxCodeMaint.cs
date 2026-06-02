using PX.Data;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class TaxCodeMaint : PXGraph<TaxCodeMaint>
{
  public PXSave<TaxCdes> Save;
  public PXCancel<TaxCdes> Cancel;
  public PXSelect<TaxCdes> TaxView;
  public PXAction<TaxCdes> PopulateMissing;

  public TaxCodeMaint()
  {
    if (((PXSelectBase<TaxCdes>)this.TaxView).Select(Array.Empty<object>()).Count != 0)
      return;
    this.PopulateTaxCategories();
  }

  private void PopulateTaxCategories()
  {
    foreach (PXResult<TaxCategory> pxResult in PXSelectBase<TaxCategory, PXSelect<TaxCategory>.Config>.Select((PXGraph)this, Array.Empty<object>()))
    {
      TaxCategory taxCategory = ((TaxCategory)(pxResult));
      ((PXSelectBase<TaxCdes>)this.TaxView).Insert(new TaxCdes
      {
        TaxCategories = taxCategory.TaxCategoryID
      });
    }
    ((PXGraph)this).Actions.PressSave();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Populate Tax Categories", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable populateMissing(PXAdapter adapter)
  {
    HashSet<string> existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    foreach (PXResult<TaxCdes> r in ((PXSelectBase<TaxCdes>)this.TaxView).Select(Array.Empty<object>()))
    {
      TaxCdes row = (TaxCdes)r;
      if (!string.IsNullOrEmpty(row.TaxCategories))
        existing.Add(row.TaxCategories);
    }

    int inserted = 0;
    foreach (PXResult<TaxCategory> pxResult in PXSelectBase<TaxCategory, PXSelect<TaxCategory>.Config>.Select((PXGraph)this, Array.Empty<object>()))
    {
      TaxCategory taxCategory = (TaxCategory)pxResult;
      if (taxCategory == null || string.IsNullOrEmpty(taxCategory.TaxCategoryID))
        continue;
      if (existing.Contains(taxCategory.TaxCategoryID))
        continue;
      ((PXSelectBase<TaxCdes>)this.TaxView).Insert(new TaxCdes
      {
        TaxCategories = taxCategory.TaxCategoryID
      });
      inserted++;
    }

    if (inserted > 0)
    {
      ((PXGraph)this).Actions.PressSave();
      ((PXSelectBase<TaxCdes>)this.TaxView).Cache.Clear();
      ((PXSelectBase<TaxCdes>)this.TaxView).View.Clear();
      PXTrace.WriteInformation($"TaxCodeMaint.PopulateMissing: inserted {inserted} new tax categories.");
    }
    else
    {
      ((PXSelectBase<TaxCdes>)this.TaxView).Cache.RaiseExceptionHandling<TaxCdes.taxCategories>(null, null, new PXSetPropertyException("No new tax categories to add.", PXErrorLevel.RowInfo));
    }
    return adapter.Get();
  }

  protected virtual void _(Events.FieldVerifying<TaxCdes.taxCode> e)
  {
    if (!(((Events.FieldVerifyingBase<Events.FieldVerifying<TaxCdes.taxCode>, object, object>)e).NewValue is string newValue))
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<TaxCdes.taxCode>, object, object>)e).NewValue = (object)newValue.ToUpper();
  }
}
