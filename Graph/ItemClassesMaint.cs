// Decompiled with JetBrains decompiler
// Type: eTims.ItemClassesMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace eTims;

public class ItemClassesMaint : PXGraph<ItemClassesMaint>
{
  public PXSave<ItemClasses> Save;
  public PXCancel<ItemClasses> Cancel;
  [PXImport(typeof (ItemClasses))]
  public PXSelect<ItemClasses> ItemClassesView;
  public PXAction<ItemClasses> viewItemClasses;

  public ItemClassesMaint()
  {
    PXTrace.WriteInformation("populate item classes on initialization");
    this.PopulateItemClasses();
  }

  [PXButton]
  public virtual void ViewItemClasses()
  {
    if (((PXSelectBase<ItemClasses>) this.ItemClassesView).Current?.ItemClass == null)
      return;
    INItemClassMaint instance = PXGraph.CreateInstance<INItemClassMaint>();
    ((PXSelectBase<INItemClass>) instance.itemclass).Current = ((INItemClass)(PXSelectBase<INItemClass, PXSelectReadonly<INItemClass, Where<INItemClass.itemClassCD, Equal<Required<INItemClass.itemClassCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<ItemClasses>) this.ItemClassesView).Current.ItemClass
    })));
    if (((PXSelectBase<INItemClass>) instance.itemclass).Current != null)
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
  }

  private void PopulateItemClasses()
  {
    foreach (PXResult<INItemClass> pxResult in PXSelectBase<INItemClass, PXSelect<INItemClass>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      INItemClass inItemClass = ((INItemClass)(pxResult));
      ItemClasses itemClasses = ((ItemClasses)(PXSelectBase<ItemClasses, PXSelect<ItemClasses, Where<ItemClasses.itemClass, Equal<Required<ItemClasses.itemClass>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) inItemClass.ItemClassCD
      })));
      if (itemClasses == null)
      {
        ((PXSelectBase<ItemClasses>) this.ItemClassesView).Insert(new ItemClasses()
        {
          ItemClassID = inItemClass.ItemClassID,
          ItemClass = inItemClass.ItemClassCD?.Trim(),
          ItemClassDescr = inItemClass.Descr?.Trim()
        });
      }
      else
      {
        itemClasses.ItemClassID = inItemClass.ItemClassID;
        itemClasses.ItemClassDescr = inItemClass.Descr?.Trim();
        ((PXSelectBase<ItemClasses>) this.ItemClassesView).Update(itemClasses);
      }
    }
    ((PXGraph) this).Actions.PressSave();
  }

  protected void ItemClasses_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ItemClasses row = (ItemClasses) e.Row;
    if (row == null || row.ItemClassificationCode == null)
      return;
    INItemClass inItemClass = ((INItemClass)(PXSelectBase<INItemClass, PXSelect<INItemClass, Where<INItemClass.itemClassID, Equal<Required<INItemClass.itemClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ItemClassID
    })));
    if (inItemClass != null)
    {
      INItemClassExtEtims extension = PXCache<INItemClass>.GetExtension<INItemClassExtEtims>(inItemClass);
      if (extension != null)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXDatabase.Update<INItemClass>(new PXDataFieldParam[2]
          {
            (PXDataFieldParam) new PXDataFieldAssign<INItemClassExtEtims.usrItemClassificationCode>((object) row.ItemClassificationCode),
            (PXDataFieldParam) new PXDataFieldRestrict<INItemClass.itemClassID>((object) row.ItemClassID)
          });
          transactionScope.Complete();
        }
        PXTrace.WriteInformation($"Updated INItemClassExt.UsrItemClassificationCode to: {extension.UsrItemClassificationCode}, MYOBJECT:{inItemClass}");
      }
      else
        PXTrace.WriteWarning("INItemClassExt is null.");
    }
    else
      PXTrace.WriteWarning("INItemClass not found.");
    foreach (PXResult<InventoryItem> pxResult in ((PXSelectBase<InventoryItem>) new PXSelect<InventoryItem, Where<InventoryItem.itemClassID, Equal<Required<InventoryItem.itemClassID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) row.ItemClassID
    }))
    {
      InventoryItem inventoryItem = ((InventoryItem)(pxResult));
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
        PXTrace.WriteWarning("InventoryItemExt is null.");
    }
  }

  protected void _(
    Events.FieldVerifying<ItemClasses.itemClassificationCode> e)
  {
    if (!string.IsNullOrEmpty(((Events.FieldVerifyingBase<Events.FieldVerifying<ItemClasses.itemClassificationCode>, object, object>) e).NewValue?.ToString()))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("Item classification code cannot be empty.", (PXErrorLevel) 4);
    ((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<ItemClasses.itemClassificationCode>>) e).Cache.RaiseExceptionHandling<ItemClasses.itemClassificationCode>(e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<ItemClasses.itemClassificationCode>, object, object>) e).NewValue, (Exception) propertyException);
  }

  protected void _(Events.RowSelected<ItemClasses> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<ItemClasses.itemClassID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ItemClasses>>) e).Cache, (object) null, false);
  }
}
