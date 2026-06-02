// Decompiled with JetBrains decompiler
// Type: eTims.RegisterNonItemsMaint
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

public class RegisterNonItemsMaint : PXGraph<RegisterNonItemsMaint>
{
  public PXCancel<InventoryItem> Cancel;
  public PXFilter<RegisterItems> FilterView;
  public PXProcessingJoin<InventoryItem, InnerJoin<INSite, On<InventoryItem.dfltSiteID, Equal<INSite.siteID>>>, Where<INSite.siteCD, Equal<Current<RegisterItems.warehouse>>, And<InventoryItem.stkItem, Equal<False>, And<InventoryItemExtEtims.usrisRegistered, IsNull>>>> NonInvItems;

  public RegisterNonItemsMaint()
  {
    ((PXProcessing<InventoryItem>) this.NonInvItems).SetProcessCaption("Register");
    ((PXProcessing<InventoryItem>) this.NonInvItems).SetProcessAllCaption("Register All");
    // ISSUE: method pointer
    ((PXProcessingBase<InventoryItem>) this.NonInvItems).SetProcessDelegate(new PXProcessingBase<InventoryItem>.ProcessListDelegate(ProcessSelectedInvItems));
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
          INSite inSite = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) branchId
          })));
          if (inSite == null)
            PXProcessing<InventoryItem>.SetError(index, "No warehouse found for the current branch.");
          PXTrace.WriteInformation($"Warehouse for branch: {inSite.SiteCD}, {inSite.BranchID}");
          INItemSite inItemSite = ((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) instance, new object[2]
          {
            (object) inventoryItem.InventoryID,
            (object) inSite.SiteID
          })));
          if (inItemSite == null)
            PXProcessing<InventoryItem>.SetError(index, $"The item '{inventoryItem.InventoryCD}' is not available for warehouse '{inSite.SiteCD}'.");
          DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) branchId
          })));
          if (deviceInitialise == null)
            PXProcessing<InventoryItem>.SetError(index, "Device information not found for the current branch.");
          PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
          if (((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) branchId
          }))) == null)
            PXProcessing<InventoryItem>.SetError(index, "Device URLs not found for the current branch.");
          RefreshToken refreshToken = ((RefreshToken)(PXSelectBase<RefreshToken, PXSelect<RefreshToken, Where<RefreshToken.pin, Equal<Required<RefreshToken.pin>>>>.Config>.Select((PXGraph) instance, new object[1]
          {
            (object) deviceInitialise.KraPin
          })));
          if (refreshToken == null)
            PXProcessing<InventoryItem>.SetError(index, "Please Refresh Access Token to be able to continue.");
          INItemSiteExtEtims extension = ((PXGraph) instance).Caches[typeof (INItemSite)].GetExtension<INItemSiteExtEtims>((object) inItemSite);
          if (extension != null && extension.UsrIsRegistered.GetValueOrDefault())
            PXProcessing<InventoryItem>.SetError(index, $"Item '{inventoryItem.Descr}' has already been registered with eTIMS for branch {branch.BranchCD}.");
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
            itemTyCd = "3",
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
            currentStock = 0M,
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
              string parameter = refreshToken.AccessToken.Trim();
              PXTrace.WriteInformation("Access Token: " + parameter);
              httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
              HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
              result.EnsureSuccessStatusCode();
              EtimsItemsRes etimsItemsRes = JsonConvert.DeserializeObject<EtimsItemsRes>(result.Content.ReadAsStringAsync().Result);
              PXTrace.WriteInformation($"responseData {etimsItemsRes}, {etimsItemsRes.status}, {etimsItemsRes.item}, {etimsItemsRes.item.id}");
              if (etimsItemsRes.status)
              {
                PXDatabase.Update<InventoryItem>(new PXDataFieldParam[3]
                {
                  (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrisRegistered>((object) true),
                  (PXDataFieldParam) new PXDataFieldAssign<InventoryItemExtEtims.usrIsaleId>((object) etimsItemsRes.item.id),
                  (PXDataFieldParam) new PXDataFieldRestrict<InventoryItem.inventoryID>((object) inventoryItem.InventoryID)
                });
                PXTrace.WriteInformation($"item registered, {extension.UsrIsRegistered}");
                PXProcessing<InventoryItem>.SetInfo(index, $"item registered, {extension.UsrIsRegistered}");
              }
              else
                PXProcessing<InventoryItem>.SetError(index, "The item was not registered on eTIMS.");
            }
            catch (Exception ex)
            {
              PXTrace.WriteError("Error sending data to eTIMS: " + ex.Message);
            }
          }
        }
      }
    }
  }
}
