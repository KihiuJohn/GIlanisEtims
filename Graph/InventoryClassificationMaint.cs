// Decompiled with JetBrains decompiler
// Type: eTims.InventoryClassificationMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class InventoryClassificationMaint : PXGraph<InventoryClassificationMaint>
{
  public PXSave<InventoryClassification> Save;
  public PXCancel<InventoryClassification> Cancel;
  [PXImport(typeof (InventoryClassification))]
  public PXSelect<InventoryClassification> InventoryClassificationView;
  public PXAction<InventoryClassification> RedirectToItemClasses;
  public PXAction<InventoryClassification> viewInventoryItem;
  public PXAction<InventoryClassification> viewItemClass;

  public void Initialize()
  {
    PXTrace.WriteInformation("initialize");
    ((PXAction) this.RedirectToItemClasses).Press();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Populate Inventory Items")]
  protected void redirectToItemClasses()
  {
    if (this.AllItemClassesMapped())
      this.PopulateInventoryItems();
    else if (((PXSelectBase<InventoryClassification>) this.InventoryClassificationView).Ask("Map item classes with eTIMS Classification codes", "Not all item classes have been mapped successfully with an item classification code. Would you like to navigate to the Item Classes screen to configure classification code?", (MessageButtons) 1, (MessageIcon) 2) == (WebDialogResult) 1)
      throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<ItemClassesMaint>(), true, "Map Class Code with Item Classes");
  }

  private bool AllItemClassesMapped()
  {
    bool flag = true;
    foreach (PXResult<ItemClasses> pxResult in PXSelectBase<ItemClasses, PXSelect<ItemClasses>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      if (string.IsNullOrEmpty(((ItemClasses)(pxResult)).ItemClassificationCode))
      {
        flag = false;
        break;
      }
    }
    return flag;
  }

  [PXButton]
  public virtual void ViewInventoryItem()
  {
    if (((PXSelectBase<InventoryClassification>) this.InventoryClassificationView).Current?.InventoryId == null)
      return;
    InventoryItemMaint instance = PXGraph.CreateInstance<InventoryItemMaint>();
    ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) instance).Item).Current = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelectReadonly<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Required<InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<InventoryClassification>) this.InventoryClassificationView).Current.InventoryId
    })));
    if (((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) instance).Item).Current != null)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }

  [PXButton]
  public virtual void ViewItemClass()
  {
    if (((PXSelectBase<InventoryClassification>) this.InventoryClassificationView).Current?.UsrItemClass == null)
      return;
    INItemClassMaint instance = PXGraph.CreateInstance<INItemClassMaint>();
    ((PXSelectBase<INItemClass>) instance.itemclass).Current = ((INItemClass)(PXSelectBase<INItemClass, PXSelectReadonly<INItemClass, Where<INItemClass.itemClassCD, Equal<Required<INItemClass.itemClassCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<InventoryClassification>) this.InventoryClassificationView).Current.UsrItemClass
    })));
    if (((PXSelectBase<INItemClass>) instance.itemclass).Current != null)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }

  private void PopulateInventoryItems()
  {
    Dictionary<int?, string> dictionary = new Dictionary<int?, string>();
    foreach (PXResult<INItemClass> pxResult in PXSelectBase<INItemClass, PXSelect<INItemClass>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      INItemClass inItemClass = ((INItemClass)(pxResult));
      dictionary[inItemClass.ItemClassID] = inItemClass.ItemClassCD;
    }
    foreach (PXResult<InventoryItem> pxResult in PXSelectBase<InventoryItem, PXSelect<InventoryItem>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      InventoryItem inventoryItem = ((InventoryItem)(pxResult));
      InventoryClassification inventoryClassification1 = ((InventoryClassification)(PXSelectBase<InventoryClassification, PXSelect<InventoryClassification, Where<InventoryClassification.inventoryId, Equal<Required<InventoryClassification.inventoryId>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) inventoryItem.InventoryCD
      })));
      if (inventoryClassification1 == null)
      {
        InventoryClassification inventoryClassification2 = new InventoryClassification()
        {
          InventoryId = inventoryItem.InventoryCD.Trim(),
          Description = inventoryItem.Descr.Trim(),
          InvId = inventoryItem.InventoryID
        };
        string str;
        if (inventoryItem.ItemClassID.HasValue && dictionary.TryGetValue(inventoryItem.ItemClassID, out str))
          inventoryClassification2.UsrItemClass = str;
        InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
        inventoryClassification2.ItemClassificationCode = extension?.UsrItemClassificationCode;
        InventoryClassification inventoryClassification3 = GraphHelper.Caches<InventoryClassification>((PXGraph) this).Insert(inventoryClassification2);
        if (inventoryClassification3 != null)
          PXTrace.WriteInformation("Successfully inserted InventoryClassification record: " + inventoryClassification3.InventoryId);
        else
          PXTrace.WriteError("Failed to insert InventoryClassification record for InventoryId: " + inventoryClassification2.InventoryId);
      }
      else
      {
        inventoryClassification1.Description = inventoryItem.Descr?.Trim();
        inventoryClassification1.InvId = inventoryItem.InventoryID;
        InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
        inventoryClassification1.ItemClassificationCode = extension?.UsrItemClassificationCode;
        GraphHelper.Caches<InventoryClassification>((PXGraph) this).Update(inventoryClassification1);
        PXTrace.WriteInformation("Updated InventoryClassification record: " + inventoryClassification1.InventoryId);
        PXTrace.WriteInformation($"Inventory item {inventoryItem.InventoryCD} already exists in InventoryClassification");
      }
    }
    ((PXGraph) this).Actions.PressSave();
  }

  protected void InventoryClassification_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    InventoryClassification row = (InventoryClassification) e.Row;
    if (row == null || row.ItemClassificationCode == null)
      return;
    InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryCD, Equal<Required<InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryId
    })));
    if (inventoryItem != null)
    {
      if (PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem) != null)
      {
        PXDatabase.Update<InventoryItem>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrItemClassificationCode>((object) row.ItemClassificationCode),
          (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
        });
        PXTrace.WriteInformation($"Updated INItemClassExt.UsrItemClassificationCode to: {row.ItemClassificationCode}, MYOBJECT:{inventoryItem.InventoryID}");
      }
      else
        PXTrace.WriteWarning("InventoryItemExtEtims is null.");
    }
    else
      PXTrace.WriteWarning("InventoryItem not found.");
  }

  protected void _(
    Events.FieldUpdated<InventoryClassification, InventoryClassification.itemClassificationCode> e)
  {
    if (!string.IsNullOrEmpty(e.NewValue?.ToString()))
      return;
    PXTrace.WriteInformation("Item classification code is empty.");
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<InventoryClassification, InventoryClassification.itemClassificationCode>>) e).Cache.RaiseExceptionHandling<InventoryClassification.itemClassificationCode>((object) e.Row, e.NewValue, (Exception) new PXSetPropertyException("Item classification code cannot be empty.", (PXErrorLevel) 4));
  }

  protected void _(Events.RowSelected<InventoryClassification> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<InventoryClassification.invId>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<InventoryClassification>>) e).Cache, (object) null, false);
  }
}
