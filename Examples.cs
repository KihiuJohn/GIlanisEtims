// Decompiled with JetBrains decompiler
// Type: MyProject.InventoryItemMaintExtension
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.IN;

#nullable disable
namespace MyProject;

public class InventoryItemMaintExtension : PXGraphExtension<InventoryItemMaint>
{
  protected void InventoryItem_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e,
    PXRowSelected del)
  {
    del?.Invoke(sender, e);
  }
}
