// Decompiled with JetBrains decompiler
// Type: eTims.ItemsMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class ItemsMaint : PXGraph<ItemsMaint>
{
  public PXCancel<InventoryItem> Cancel;
  public PXSave<InventoryItem> Save;
  public PXFilter<InventoryItem> Filter;
  [PXFilterable(new Type[] {})]
  public PXProcessing<InventoryItem, Where<InventoryItem.itemClassID, IsNotNull, And<Where<InventoryItemExtEtims.usrTaxCodes, IsNull, Or<InventoryItemExtEtims.usrPackagingUnit, IsNull>>>>> Items;
  public PXSelect<PX.Objects.TX.TaxCategory, Where<PX.Objects.TX.TaxCategory.taxCategoryID, Equal<Required<InventoryItem.taxCategoryID>>>> TaxCategory;

  public ItemsMaint()
  {
    ((PXProcessing<InventoryItem>) this.Items).SetProcessCaption("Update");
    ((PXProcessing<InventoryItem>) this.Items).SetProcessAllCaption("Update All");
    ((PXSelectBase) this.Items).AllowSelect = true;
    ((PXProcessingBase<InventoryItem>) this.Items).SetProcessDelegate(new PXProcessingBase<InventoryItem>.ProcessListDelegate(ProcessSelectedItems));
  }

  public static void ProcessSelectedItems(List<InventoryItem> items)
  {
    ItemsMaint instance = PXGraph.CreateInstance<ItemsMaint>();
    GraphHelper.Caches<InventoryItem>((PXGraph) instance);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items[index] != null)
      {
        InventoryItem inventoryItem = items[index];
        string taxCategoryId = inventoryItem.TaxCategoryID;
        string baseUnit = inventoryItem.BaseUnit;
        string inventoryCd = inventoryItem.InventoryCD;
        PXTrace.WriteInformation($"item {inventoryItem.TaxCategoryID}, {inventoryItem.BaseUnit}, {inventoryItem.InventoryCD}, {inventoryItem.StkItem}");
        TaxCdes taxCdes = ((TaxCdes)(PXSelectBase<TaxCdes, PXSelect<TaxCdes, Where<TaxCdes.taxCategories, Equal<Required<InventoryItem.taxCategoryID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) taxCategoryId
        })));
        UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<InventoryItem.baseUnit>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) baseUnit
        })));
        _ = ((InventoryClassification)(PXSelectBase<InventoryClassification, PXSelect<InventoryClassification, Where<InventoryClassification.inventoryId, Equal<Required<InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) inventoryCd
        })));
        PXTrace.WriteInformation("taxCode " + taxCdes.TaxCode);
        if (taxCdes != null)
        {
          if (PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem) != null)
            PXDatabase.Update<InventoryItem>(new PXDataFieldParam[2]
            {
              (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrTaxCodes>((object) taxCdes.TaxCode),
              (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
            });
          else
            PXTrace.WriteInformation("Extension not found for item " + inventoryItem.InventoryCD);
        }
        else
          PXTrace.WriteInformation("Tax category not found for item " + inventoryItem.InventoryCD);
        bool? stkItem = inventoryItem.StkItem;
        bool flag = false;
        if (!(stkItem.GetValueOrDefault() == flag & stkItem.HasValue))
        {
          if (uomMapping != null)
          {
            if (PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem) != null)
            {
              ItemsMaint.DetermineItemType(inventoryItem.ItemType);
              string packagingUnit = uomMapping.PackagingUnit;
              string unitOfQuantity = uomMapping.UnitOfQuantity;
              string countryOfOrigin = inventoryItem.CountryOfOrigin;
              string itemType = ItemsMaint.DetermineItemType(inventoryItem.ItemType);
              string str1 = inventoryItem.InventoryID?.ToString("D7");
              PXTrace.WriteInformation("formattedInventoryID: " + str1);
              string str2 = packagingUnit + unitOfQuantity;
              if (!string.IsNullOrEmpty(itemType) && !string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str1))
              {
                PXTrace.WriteInformation($"Item code: {countryOfOrigin}{itemType}{str2}{str1}");
                PXDatabase.Update<InventoryItem>(new PXDataFieldParam[4]
                {
                  (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrProductType>((object) itemType),
                  (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrPackagingUnit>((object) packagingUnit),
                  (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrUnitOfQuantity>((object) unitOfQuantity),
                  (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
                });
                PXProcessing<InventoryItem>.SetInfo(index, $"Item codes field for InventoryID {inventoryItem.InventoryID}, has been updated successfully");
              }
              else
                PXProcessing<InventoryItem>.SetError(index, $"{$"Cannot update Item Codes field for InventoryID {inventoryItem.InventoryID}: One or more required fields are missing. Please check the missing field,  "}CountryCode: {countryOfOrigin}, ItemType: {itemType}, BaseUnit: {str2}, FormattedInventoryID: {str1}");
            }
          }
          else
            PXProcessing<InventoryItem>.SetError(index, "Please map Unit of measure with packaging unit and unit of quantity");
        }
        else if (uomMapping != null)
        {
          if (PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem) != null)
          {
            string packagingUnit = uomMapping.PackagingUnit;
            string unitOfQuantity = uomMapping.UnitOfQuantity;
            string str3 = "3";
            string str4 = "KE";
            string str5 = inventoryItem.InventoryID?.ToString("D7");
            PXTrace.WriteInformation("formattedInventoryID: " + str5);
            string str6 = packagingUnit + unitOfQuantity;
            if (!string.IsNullOrEmpty(str6) && !string.IsNullOrEmpty(str5))
            {
              PXTrace.WriteInformation($"Item code: {str4}{str3}{str6}{str5}");
              PXDatabase.Update<InventoryItem>(new PXDataFieldParam[4]
              {
                (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrProductType>((object) str3),
                (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrPackagingUnit>((object) packagingUnit),
                (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrUnitOfQuantity>((object) unitOfQuantity),
                (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
              });
              PXProcessing<InventoryItem>.SetInfo(index, $"Item codes field for InventoryID {inventoryItem.InventoryID}, has been updated successfully");
            }
            else
              PXProcessing<InventoryItem>.SetError(index, $"{$"Cannot update Item Codes field for InventoryID {inventoryItem.InventoryID}: One or more required fields are missing. Please check the missing field, "}CountryCode: {str4}, ItemType: {str3}, BaseUnit: {str6}, FormattedInventoryID: {str5}");
          }
        }
        else
          PXProcessing<InventoryItem>.SetError(index, "Please map Unit of measure with packaging unit and unit of quantity");
      }
    }
  }

  private static string DetermineItemType(string itemType)
  {
    switch (itemType)
    {
      case "1":
        return "1";
      case "2":
      case "F":
        return "2";
      case "3":
        return "3";
      default:
        return string.Empty;
    }
  }
}
