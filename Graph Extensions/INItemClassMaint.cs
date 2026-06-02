// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClassMaint_Extension_Etims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using eTims;
using PX.Data;
using PX.Objects.IN;
using System.Collections;

#nullable disable
namespace PX.Objects.IN;

public class INItemClassMaint_Extension_Etims : PXGraphExtension<INItemClassMaint>
{
  public PXAction<INItemClass> UpdateStockItems;

  [PXProcessButton]
  [PXUIField]
  protected IEnumerable updateStockItems(PXAdapter adapter)
  {
    INItemClass itemClass = ((PXSelectBase<INItemClass>)this.Base.itemclass).Current;
    if (itemClass != null && ((PXSelectBase<INItemClass>)this.Base.itemclass).Ask("Confirmation", "Are you sure you want to update item classification codes for all items in this class?", (MessageButtons)1) == (WebDialogResult)1)
    {
      ((PXGraph)this.Base).Actions.PressSave();
      PXLongOperation.StartOperation((PXGraph)this.Base, delegate
      {
        INItemClassExtEtims classExt = PXCache<INItemClass>.GetExtension<INItemClassExtEtims>(itemClass);
        if (classExt == null) return;
        InventoryItemMaint itemMaint = PXGraph.CreateInstance<InventoryItemMaint>();
        foreach (InventoryItem item in PXSelect<InventoryItem, Where<InventoryItem.itemClassID, Equal<Required<InventoryItem.itemClassID>>>>.Select((PXGraph)itemMaint, new object[] { itemClass.ItemClassID }))
        {
          ((PXSelectBase<InventoryItem>)itemMaint.Item).Current = item;
          InventoryItemExtEtims itemExt = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(item);
          if (itemExt != null)
          {
            itemExt.UsrItemClassificationCode = classExt.UsrItemClassificationCode;
            ((PXSelectBase)itemMaint.Item).Cache.Update((object)item);
            ((PXGraph)itemMaint).Actions.PressSave();
          }
        }
      });
    }
    return adapter.Get();
  }

  protected void INItemClass_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    INItemClass row = (INItemClass)e.Row;
    if (row == null)
      return;
    bool flag = Utility.IsActive();
    if (PXCache<INItemClass>.GetExtension<INItemClassExtEtims>(row) == null)
      return;
    ((PXAction)this.UpdateStockItems).SetVisible(flag);
    PXUIFieldAttribute.SetVisible<INItemClassExtEtims.usrItemClassificationCode>(cache, (object)row, flag);
  }
}
