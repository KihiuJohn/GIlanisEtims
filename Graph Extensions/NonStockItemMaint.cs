// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockItemMaint_Extension_Etims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

#nullable disable
namespace PX.Objects.IN;

public class NonStockItemMaint_Extension_Etims : PXGraphExtension<NonStockItemMaint>
{
  public PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Current<InventoryItem.inventoryID>>>> BranchEtimsView;
  public PXAction<InventoryItem> AddAllBranches;
  public PXAction<InventoryItem> RegisterItem;
  public PXAction<InventoryItem> RegisterExItem;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Add Branches")]
  protected void addAllBranches()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    if (current == null || !current.InventoryID.HasValue)
      return;
    foreach (PXResult<Branch> pxResult in PXSelectBase<Branch, PXSelect<Branch>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      Branch branch = ((Branch)(pxResult));
      if (PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) branch.BranchID
      }).Count <= 0)
        ((PXSelectBase<BranchEtims>) this.BranchEtimsView).Insert(new BranchEtims()
        {
          InventoryID = current.InventoryID,
          BranchID = branch.BranchID,
          IsRegistered = new bool?(false)
        });
    }
    ((PXGraph) this.Base).Actions.PressSave();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Register Item to KRA")]
  protected void registerItem()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (branch == null)
      throw new PXException("Branch not found");
    PXTrace.WriteInformation("branch :" + branch.BranchCD);
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    PXTrace.WriteInformation("item is not null");
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(current);
    if (extension == null)
      return;
    BranchEtims branchEtims = ((BranchEtims)(PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) current.InventoryID,
      (object) branchId
    })));
    if (branchEtims == null)
      throw new PXException($"Branch {branch.BranchCD.Trim()} has not been added for non stock item {current.Descr.Trim()}");
    if (branchEtims.IsRegistered.GetValueOrDefault() && branchEtims.Isaleid.HasValue)
      throw new PXException($"Non stock Item '{current.Descr}' has already been registered with Etims.");
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) current.BaseUnit
    })));
    if (uomMapping == null)
      return;
    string packagingUnit = uomMapping.PackagingUnit;
    string unitOfQuantity = uomMapping.UnitOfQuantity;
    PXTrace.WriteInformation($"{packagingUnit}, {unitOfQuantity}, {extension.UsrTaxCodes}");
    string content = JsonConvert.SerializeObject((object) new ItemPayload()
    {
      tpin = deviceInitialise.KraPin,
      bhfId = deviceInitialise.KraBranchID,
      itemCd = current.InventoryCD.Trim(),
      itemCdDf = current.InventoryCD.Trim(),
      itemClsCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) current)?.UsrItemClassificationCode?.Trim(),
      itemTyCd = "3",
      itemNm = current.Descr?.Trim(),
      itemStdNm = current.Descr?.Trim(),
      orgnNatCd = (current.CountryOfOrigin ?? "KE"),
      pkgUnitCd = packagingUnit,
      qtyUnitCd = unitOfQuantity,
      taxTyCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) current)?.UsrTaxCodes?.Trim(),
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
    PXTrace.WriteInformation("itemPayload, " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = deviceUrls.RegisterUrl.Trim();
        string parameter = deviceToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result.EnsureSuccessStatusCode();
        EtimsItemsRes etimsItemsRes = JsonConvert.DeserializeObject<EtimsItemsRes>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {etimsItemsRes}, {etimsItemsRes.status}, {etimsItemsRes.item}, {etimsItemsRes.item.id}");
        if (!etimsItemsRes.status)
          throw new PXSetPropertyException("The item was not registered on eTims");
        branchEtims.IsRegistered = new bool?(true);
        branchEtims.Isaleid = new int?(etimsItemsRes.item.id);
        GraphHelper.Caches<BranchEtims>((PXGraph) this.Base).Update(branchEtims);
        ((PXGraph) this.Base).Actions.PressSave();
        PXTrace.WriteInformation("item was successfully to eTims");
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to VSDC: " + ex.Message);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Register Export Item to KRA")]
  protected void registerExItem()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (branch == null)
      throw new PXException("Branch not found");
    PXTrace.WriteInformation("branch :" + branch.BranchCD);
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    RefreshToken refreshToken = ((RefreshToken)(PXSelectBase<RefreshToken, PXSelect<RefreshToken, Where<RefreshToken.pin, Equal<Required<RefreshToken.pin>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) deviceInitialise.KraPin
    })));
    if (refreshToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    PXTrace.WriteInformation("item is not null");
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(current);
    if (extension == null)
      return;
    BranchEtims branchEtims = ((BranchEtims)(PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) current.InventoryID,
      (object) branchId
    })));
    if (branchEtims == null)
      throw new PXException($"Branch {branch.BranchCD.Trim()} has not been added for non stock item {current.Descr.Trim()}");
    if (branchEtims.IsRegistered.GetValueOrDefault())
      throw new PXException($"Non stock Item '{current.Descr}' has already been registered with Etims.");
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) current.BaseUnit
    })));
    if (uomMapping == null)
      return;
    string packagingUnit = uomMapping.PackagingUnit;
    string unitOfQuantity = uomMapping.UnitOfQuantity;
    PXTrace.WriteInformation($"{packagingUnit}, {unitOfQuantity}, {extension.UsrTaxCodes}");
    string content = JsonConvert.SerializeObject((object) new ItemPayload()
    {
      tpin = deviceInitialise.KraPin,
      bhfId = deviceInitialise.KraBranchID,
      itemCd = current.InventoryCD.Trim(),
      itemCdDf = current.InventoryCD.Trim(),
      itemClsCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) current)?.UsrItemClassificationCodeExpo?.Trim(),
      itemTyCd = "3",
      itemNm = current.Descr?.Trim(),
      itemStdNm = current.Descr?.Trim(),
      orgnNatCd = (current.CountryOfOrigin ?? "KE"),
      pkgUnitCd = packagingUnit,
      qtyUnitCd = unitOfQuantity,
      taxTyCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) current)?.UsrTaxCodesExpo?.Trim(),
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
    PXTrace.WriteInformation("itemPayload, " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = deviceUrls.RegisterUrl.Trim();
        string parameter = refreshToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result.EnsureSuccessStatusCode();
        EtimsItemsRes etimsItemsRes = JsonConvert.DeserializeObject<EtimsItemsRes>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {etimsItemsRes}, {etimsItemsRes.status}, {etimsItemsRes.item}, {etimsItemsRes.item.id}");
        if (!etimsItemsRes.status)
          throw new PXSetPropertyException("The item was not registered on eTims");
        branchEtims.IsRegistered = new bool?(true);
        branchEtims.Isaleid = new int?(etimsItemsRes.item.id);
        GraphHelper.Caches<BranchEtims>((PXGraph) this.Base).Update(branchEtims);
        ((PXGraph) this.Base).Actions.PressSave();
        PXTrace.WriteInformation("item was successfully to eTims");
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to VSDC: " + ex.Message);
    }
  }

  protected void InventoryItem_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    InventoryItem row = (InventoryItem) e.Row;
    if (row == null)
      return;
    PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(row);
    TaxCdes taxCdes = ((TaxCdes)(PXSelectBase<TaxCdes, PXSelect<TaxCdes, Where<TaxCdes.taxCategories, Equal<Required<InventoryItem.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.TaxCategoryID
    })));
    if (row != null && taxCdes != null)
    {
      cache.SetValueExt<InventoryItemExtEtims.usrTaxCodes>((object) row, (object) taxCdes.TaxCode);
      cache.SetValueExt<InventoryItemExtEtims.usrTaxCodesExpo>((object) row, (object) "C");
      PXTrace.WriteInformation("tax code, " + taxCdes.TaxCode);
    }
    else
    {
      cache.RaiseExceptionHandling<InventoryItem.taxCategoryID>((object) row, (object) row.TaxCategoryID, (Exception) new PXSetPropertyException("Tax code not found for tax category " + row.TaxCategoryID));
      PXTrace.WriteInformation("Tax code not found for tax category " + row.TaxCategoryID);
    }
  }

  protected void InventoryItem_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    InventoryItem row = (InventoryItem) e.Row;
    if (row == null || !Utility.IsActive())
      return;
    PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(row);
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<InventoryItem.baseUnit>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.BaseUnit
    })));
    _ = ((InventoryClassification)(PXSelectBase<InventoryClassification, PXSelect<InventoryClassification, Where<InventoryClassification.inventoryId, Equal<Required<InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.InventoryCD
    })));
    ItemClasses itemClasses = ((ItemClasses)(PXSelectBase<ItemClasses, PXSelect<ItemClasses, Where<ItemClasses.itemClassID, Equal<Required<ItemClasses.itemClassID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.ItemClassID
    })));
    if (itemClasses != null)
      cache.SetValueExt<InventoryItemExtEtims.usrItemClassificationCode>((object) row, (object) itemClasses.ItemClassificationCode);
    if (uomMapping == null)
      return;
    string packagingUnit = uomMapping.PackagingUnit;
    string unitOfQuantity = uomMapping.UnitOfQuantity;
    cache.SetValueExt<InventoryItemExtEtims.usrPackagingUnit>((object) row, (object) packagingUnit);
    cache.SetValueExt<InventoryItemExtEtims.usrUnitOfQuantity>((object) row, (object) unitOfQuantity);
    string str1 = "3";
    PXTrace.WriteInformation("formattedInventoryID: " + row.InventoryID?.ToString("D7"));
    string str2 = packagingUnit + unitOfQuantity;
    cache.SetValueExt<InventoryItemExtEtims.usrProductType>((object) row, (object) str1);
  }

  protected void InventoryItem_UsrItemClassificationCode_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    InventoryItem row = (InventoryItem) e.Row;
    if (row == null || !Utility.IsActive())
      return;
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(row);
    if (extension == null || extension.UsrItemClassificationCode == null)
      return;
    if (((InventoryClassification)(PXSelectBase<InventoryClassification, PXSelect<InventoryClassification, Where<InventoryClassification.invId, Equal<Required<InventoryClassification.invId>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.InventoryID
    }))) != null)
    {
      PXDatabase.Update<InventoryClassification>(new PXDataFieldParam[2]
      {
        (PXDataFieldParam) new PXDataFieldAssign<InventoryClassification.itemClassificationCode>((object) extension.UsrItemClassificationCode),
        (PXDataFieldParam) new PXDataFieldRestrict<InventoryClassification.invId>((object) row.InventoryID)
      });
      PXTrace.WriteInformation("Updated InventoryClassification.ItemClassificationCode to: " + extension.UsrItemClassificationCode);
    }
    else
      PXTrace.WriteWarning("InventoryClassification not found.");
  }
}
