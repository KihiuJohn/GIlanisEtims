// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntry_Extension_Etims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.Objects.TX;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderEntry_Extension_Etims : PXGraphExtension<SOOrderEntry>
{
  protected void SOLine_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
  }

  protected void SOLine_InventoryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
    PXCache<SOLine>.GetExtension<SOLineExtEtims>(row);
    _ = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.InventoryID
    })));
    if (((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.BranchID
    }))) == null)
      return;
    Location location = ((Location)(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) current.CustomerID
    })));
    if (location == null || !(location.CTaxZoneID == "ZERORATE"))
      return;
    TaxZone taxZone = ((TaxZone)(PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Required<Location.cTaxZoneID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) location.CTaxZoneID
    })));
    if (taxZone != null)
      row.TaxCategoryID = taxZone.DfltTaxCategoryID;
  }
}
