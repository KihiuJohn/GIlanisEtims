// Decompiled with JetBrains decompiler
// Type: eTims.RegisterItemsMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

#nullable disable
namespace eTims;

public class RegisterItemsMaint : PXGraph<RegisterItemsMaint>
{
  public PXCancel<InventoryItem> Cancel;
  public PXFilter<RegisterItems> FilterView;
  public PXProcessingJoin<InventoryItem, InnerJoin<INItemSite, On<INItemSite.inventoryID, Equal<InventoryItem.inventoryID>>, InnerJoin<INSite, On<INItemSite.siteID, Equal<INSite.siteID>>, InnerJoin<INSiteStatus, On<INSiteStatus.inventoryID, Equal<InventoryItem.inventoryID>, And<INSiteStatus.siteID, Equal<INSite.siteID>>>>>>, Where<INSite.siteCD, Equal<Current<RegisterItems.warehouse>>, And<InventoryItemExtEtims.usrIsaleId, IsNull>>> InvItems;

  public RegisterItemsMaint()
  {
    ((PXProcessing<InventoryItem>) this.InvItems).SetProcessCaption("Register");
    ((PXProcessing<InventoryItem>) this.InvItems).SetProcessAllCaption("Register All");
    // ISSUE: method pointer
    ((PXProcessingBase<InventoryItem>) this.InvItems).SetProcessDelegate(new PXProcessingBase<InventoryItem>.ProcessListDelegate(ProcessSelectedInvItems));
  }

  public static void ProcessSelectedInvItems(List<InventoryItem> items)
  {
    ItemsMaint instance = PXGraph.CreateInstance<ItemsMaint>();
    GraphHelper.Caches<InventoryItem>((PXGraph) instance);
    for (int index = 0; index < items.Count; ++index)
    {
      if (items[index] != null)
      {
        InventoryItem inventoryItem = items[index];
        if (inventoryItem != null)
        {
          try
          {
            int? branchId = PXAccess.GetBranchID();
            if (!branchId.HasValue)
              PXProcessing<InventoryItem>.SetError(index, "Current branch ID is not available.");
            PXTrace.WriteInformation($"current branch: {branchId}");
            Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            })));
            if (branch == null)
              PXProcessing<InventoryItem>.SetError(index, "Branch not found.");
            PXTrace.WriteInformation("Branch: " + branch.BranchCD);
            Decimal d = 0M;
            bool flag = false;
            foreach (PXResult<INSite> pxResult1 in PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            }))
            {
              INSite inSite = ((INSite)(pxResult1));
              foreach (PXResult<INSiteStatus> pxResult2 in PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) instance, new object[2]
              {
                (object) inventoryItem.InventoryID,
                (object) inSite.SiteID
              }))
              {
                INSiteStatus inSiteStatus = ((INSiteStatus)(pxResult2));
                d += inSiteStatus.QtyOnHand.GetValueOrDefault();
              }
              PXTrace.WriteInformation($"Total quantity on hand: {d}");
              if (((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) instance, new object[2]
              {
                (object) inventoryItem.InventoryID,
                (object) inSite.SiteID
              }))) != null)
              {
                flag = true;
                PXTrace.WriteInformation($"Item {inventoryItem.InventoryCD} exists in warehouse {inSite.SiteCD}.");
                break;
              }
            }
            if (!flag)
              PXProcessing<InventoryItem>.SetError(index, $"The item '{inventoryItem.InventoryCD.Trim()}' is not available in any warehouse under the selected branch.");
            INSite inSite1 = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>, And<INSite.siteCD, NotEqual<Required<INSite.siteCD>>>>>.Config>.Select((PXGraph) instance, new object[2]
            {
              (object) branchId,
              (object) "INTR"
            })));
            if (inSite1 == null)
              PXProcessing<InventoryItem>.SetError(index, "No warehouse found for the current branch.");
            PXTrace.WriteInformation($"Warehouse for branch: {inSite1.SiteCD}, {inSite1.BranchID}");
            DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            })));
            if (deviceInitialise == null)
              PXProcessing<InventoryItem>.SetError(index, "Device information not found for the current branch.");
            PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
            DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            })));
            if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
              PXProcessing<InventoryItem>.SetError(index, "Device information does not exist since Device is not initialized.");
            _ = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            })));
            DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) branchId
            })));
            if (deviceToken == null)
              PXProcessing<InventoryItem>.SetError(index, "Please Refresh Access Token to be able to continue.");
            PXCache cach = ((PXGraph) instance).Caches[typeof (INItemSite)];
            ((PXGraph) instance).Caches[typeof (InventoryItem)].GetExtension<InventoryItemExtEtims>((object) inventoryItem);
            InventoryItemExtEtims extension = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) inventoryItem);
            if (extension != null && extension.UsrIsaleId.HasValue)
              PXProcessing<InventoryItem>.SetError(index, $"Item '{inventoryItem.Descr}' has already been registered with eTIMS for branch.");
            UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) inventoryItem.BaseUnit
            })));
            if (uomMapping == null)
              PXProcessing<InventoryItem>.SetError(index, "UOM Mapping not found.");
            string packagingUnit = uomMapping.PackagingUnit;
            string unitOfQuantity = uomMapping.UnitOfQuantity;
            string content = JsonConvert.SerializeObject((object) new ItemPayload()
            {
              tpin = deviceInitialise.KraPin,
              bhfId = deviceInitialise.KraBranchID,
              itemCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) inventoryItem)?.UsrItemCodes?.Trim(),
              itemCdDf = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) inventoryItem)?.UsrItemCodes?.Trim(),
              itemClsCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) inventoryItem)?.UsrItemClassificationCode?.Trim(),
              itemTyCd = RegisterItemsMaint.DetermineItemType(inventoryItem.ItemType),
              itemNm = inventoryItem.Descr?.Trim(),
              itemStdNm = inventoryItem.Descr?.Trim(),
              orgnNatCd = (inventoryItem.CountryOfOrigin ?? "KE"),
              pkgUnitCd = packagingUnit,
              qtyUnitCd = unitOfQuantity,
              taxTyCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) inventoryItem)?.UsrTaxCodes?.Trim(),
              dftPrc = 0M,
              isrcAplcbYn = "N",
              useYn = "Y",
              regrId = "ADMIN",
              regrNm = "ADMIN",
              modrId = "ADMIN",
              modrNm = "ADMIN",
              currentStock = Math.Round(d, 2),
              addInfo = "",
              bcd = "",
              btchNo = "",
              grpPrcL1 = 0M,
              grpPrcL2 = 0M,
              grpPrcL3 = 0M,
              grpPrcL4 = 0M
            });
            PXTrace.WriteInformation("Item payload: " + content);
            using (HttpClient httpClient = new HttpClient())
            {
              try
              {
                string requestUri = "https://vibraniumapi.stanbestgroup.com/items";
                string parameter = deviceToken.AccessToken.Trim();
                PXTrace.WriteInformation("Access Token: " + parameter);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
                HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
                result.EnsureSuccessStatusCode();
                EtimsItemsRes etimsItemsRes = JsonConvert.DeserializeObject<EtimsItemsRes>(result.Content.ReadAsStringAsync().Result);
                PXTrace.WriteInformation($"responseData {etimsItemsRes}, {etimsItemsRes.status}, {etimsItemsRes.item}, {etimsItemsRes.item.id}");
                if (etimsItemsRes.status)
                {
                  PXDatabase.Update<InventoryItem>(new PXDataFieldParam[2]
                  {
                    (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrIsaleId>((object) etimsItemsRes.item.id),
                    (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
                  });
                  PXTrace.WriteInformation("item successfully registered with eTIMS");
                  PXProcessing<InventoryItem>.SetInfo(index, "item registered with eTIMS");
                }
                else
                  PXProcessing<InventoryItem>.SetError(index, "The item was not registered on VSDC, " + etimsItemsRes.resultMsg);
              }
              catch (Exception ex)
              {
                PXTrace.WriteError("Error sending data to eTIMS: " + ex.Message);
              }
            }
          }
          catch (Exception ex)
          {
            PXTrace.WriteError("Error processing item: " + ex.Message);
          }
        }
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
        return "2";
      case "F":
        return "2";
      case "3":
        return "3";
      default:
        return string.Empty;
    }
  }
}
