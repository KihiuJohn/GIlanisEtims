// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemMaint_Extension_Etims
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

public class InventoryItemMaint_Extension_Etims : PXGraphExtension<InventoryItemMaint>
{
  public PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Current<InventoryItem.inventoryID>>>> BranchEtimsView;
  public PXAction<InventoryItem> RegisterItem;
  public PXAction<InventoryItem> RegisterExpoItem;
  public PXAction<InventoryItem> AdjustItem;

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
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Register Item with ETIMS")]
  protected void registerItem()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    PXTrace.WriteInformation("branch :" + (((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    }))) ?? throw new PXException("Branch not found")).BranchCD);
    INSite inSite1 = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>, And<INSite.siteCD, NotEqual<Required<INSite.siteCD>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) branchId,
      (object) "INTR"
    })));
    if (inSite1 == null)
      throw new PXException("No warehouse found for the current branch.");
    Decimal d = 0M;
    foreach (PXResult<INSite> pxResult1 in PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      INSite inSite2 = ((INSite)(pxResult1));
      foreach (PXResult<INSiteStatus> pxResult2 in PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) inSite2.SiteID
      }))
      {
        INSiteStatus inSiteStatus = ((INSiteStatus)(pxResult2));
        d += inSiteStatus.QtyOnHand.GetValueOrDefault();
      }
      PXTrace.WriteInformation($"Total quantity on hand: {d}");
      PXTrace.WriteInformation($"Checking site {inSite1.SiteCD} for item {current.InventoryCD}");
    }
    PXTrace.WriteInformation($"Warehouse for branch: {inSite1.SiteCD}, {inSite1.BranchID}");
    INItemSite inItemSite1 = ((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) current.InventoryID,
      (object) inSite1.SiteID
    })));
    if (inItemSite1 == null)
      throw new PXException($"The item '{current.InventoryCD.Trim()}' is not available for warehouse '{inSite1.SiteCD.Trim()}'.");
    PXTrace.WriteInformation("Item found for warehouse: " + current.InventoryCD);
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(current);
    if (extension == null)
      return;
    ((PXGraph) this.Base).Caches[typeof (INItemSite)].GetExtension<INItemSiteExtEtims>((object) inItemSite1);
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) current.BaseUnit
    })));
    if (uomMapping == null)
      throw new PXException($"Item '{current.Descr}' has not been mapped with KRA unit of measures");
    if (extension.UsrTaxCodes == null || extension.UsrItemClassificationCode == null)
      throw new PXException($"Item '{current.Descr}' has not been mapped with KRA tax code or item classification code");
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
      itemTyCd = this.DetermineItemType(current.ItemType),
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
      currentStock = Math.Round(d, 2),
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
          throw new Exception("The item was not registered on eTIMS");
        PXCache cach = ((PXGraph) this.Base).Caches[typeof (INItemSite)];
        foreach (PXResult<INItemSite> pxResult in PXSelectBase<INItemSite, PXSelectJoin<INItemSite, InnerJoin<INSite, On<INSite.siteID, Equal<INItemSite.siteID>>>, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INSite.branchID, Equal<Required<INSite.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) current.InventoryID,
          (object) branchId
        }))
        {
          INItemSite inItemSite2 = ((INItemSite)(pxResult));
          if (cach.GetExtension<INItemSiteExtEtims>((object) inItemSite2) != null)
          {
            cach.SetValueExt<INItemSiteExtEtims.usrIsRegistered>((object) inItemSite2, (object) true);
            cach.SetValueExt<INItemSiteExtEtims.usrIsaleID>((object) inItemSite2, (object) etimsItemsRes.item.id);
            cach.Update((object) inItemSite2);
          }
        }
        ((PXGraph) this.Base).Persist();
        ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Update(((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current);
        ((PXGraph) this.Base).Persist();
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInitialise.KraBranchID,
          DocumentNbr = current.InventoryCD,
          DocumentType = "Item Registration",
          RequestLogOne = content,
          ResponseLog = etimsItemsRes.item.ToString()
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this.Base).Insert(zraLogs);
        ((PXGraph) this.Base).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        PXTrace.WriteInformation("ZRA log inserted.");
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
      throw;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Register Export Item with ETIMS")]
  protected void registerExpoItem()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    PXTrace.WriteInformation("branch :" + (((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    }))) ?? throw new PXException("Branch not found")).BranchCD);
    INSite inSite1 = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>, And<INSite.siteCD, NotEqual<Required<INSite.siteCD>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) branchId,
      (object) "INTR"
    })));
    if (inSite1 == null)
      throw new PXException("No warehouse found for the current branch.");
    Decimal d = 0M;
    bool flag = false;
    foreach (PXResult<INSite> pxResult1 in PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      INSite inSite2 = ((INSite)(pxResult1));
      foreach (PXResult<INSiteStatus> pxResult2 in PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) inSite2.SiteID
      }))
      {
        INSiteStatus inSiteStatus = ((INSiteStatus)(pxResult2));
        d += inSiteStatus.QtyOnHand.GetValueOrDefault();
      }
      PXTrace.WriteInformation($"Total quantity on hand: {d}");
      PXTrace.WriteInformation($"Checking site {inSite1.SiteCD} for item {current.InventoryCD}");
      if (((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) inSite2.SiteID
      }))) != null)
      {
        flag = true;
        PXTrace.WriteInformation($"Item {current.InventoryCD} exists in warehouse {inSite2.SiteCD}.");
        break;
      }
    }
    if (!flag)
      throw new PXException($"The item '{current.InventoryCD.Trim()}' is not available in any warehouse under the selected branch.");
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    RefreshToken refreshToken = ((RefreshToken)(PXSelectBase<RefreshToken, PXSelect<RefreshToken, Where<RefreshToken.pin, Equal<Required<RefreshToken.pin>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) deviceInitialise.KraPin
    })));
    if (refreshToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(current);
    if (extension == null)
      return;
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) current.BaseUnit
    })));
    if (uomMapping == null)
      return;
    string packagingUnit = uomMapping.PackagingUnit;
    string unitOfQuantity = uomMapping.UnitOfQuantity;
    PXTrace.WriteInformation($"{packagingUnit}, {unitOfQuantity}, {extension.UsrTaxCodes}");
    ItemPayload[] itemPayloadArray = new ItemPayload[1]
    {
      new ItemPayload()
      {
        tpin = deviceInitialise.KraPin,
        bhfId = deviceInitialise.KraBranchID,
        itemCd = current.InventoryCD.Trim(),
        itemCdDf = current.InventoryCD.Trim(),
        itemClsCd = PXCacheEx.GetExtension<InventoryItemExtEtims>((IBqlTable) current)?.UsrItemClassificationCodeExpo?.Trim(),
        itemTyCd = this.DetermineItemType(current.ItemType),
        itemNm = current.Descr?.Trim(),
        itemStdNm = current.Descr?.Trim(),
        orgnNatCd = current.CountryOfOrigin ?? "KE",
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
        currentStock = Math.Round(d, 2),
        addInfo = "",
        bcd = "",
        btchNo = "",
        grpPrcL1 = 0M,
        grpPrcL2 = 0M,
        grpPrcL3 = 0M,
        grpPrcL4 = 0M
      }
    };
    int num = 0;
    foreach (object obj in itemPayloadArray)
    {
      string content = JsonConvert.SerializeObject(obj);
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
            throw new Exception("The item was not registered on eTIMS");
          if (num == 0)
            ((PXSelectBase) ((InventoryItemMaintBase) this.Base).Item).Cache.SetValueExt<InventoryItemExtEtims.usrIsaleIdExpo>((object) current, (object) etimsItemsRes.item.id);
          ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Update(current);
          ((PXGraph) this.Base).Actions.PressSave();
          PXTrace.WriteInformation("Item was successfully sent to eTIMS and saved to the correct UsrIsaleId field.");
        }
      }
      catch (Exception ex)
      {
        PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
        throw;
      }
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Adjust Item")]
  protected void adjustItem()
  {
    InventoryItem current = ((PXSelectBase<InventoryItem>) ((InventoryItemMaintBase) this.Base).Item).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    PXTrace.WriteInformation("branch :" + (((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    }))) ?? throw new PXException("Branch not found")).BranchCD);
    Decimal num = 0M;
    bool flag = false;
    foreach (PXResult<INSite> pxResult1 in PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      INSite inSite = ((INSite)(pxResult1));
      foreach (PXResult<INSiteStatus> pxResult2 in PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) inSite.SiteID
      }))
      {
        INSiteStatus inSiteStatus = ((INSiteStatus)(pxResult2));
        num += inSiteStatus.QtyOnHand.GetValueOrDefault();
      }
      PXTrace.WriteInformation($"Total quantity on hand: {num}");
      if (((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) current.InventoryID,
        (object) inSite.SiteID
      }))) != null)
      {
        flag = true;
        PXTrace.WriteInformation($"Item {current.InventoryCD} exists in warehouse {inSite.SiteCD}.");
        break;
      }
    }
    if (!flag)
      throw new PXException($"The item '{current.InventoryCD.Trim()}' is not available in any warehouse under the selected branch.");
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) branchId
    })));
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    RefreshToken refreshToken = ((RefreshToken)(PXSelectBase<RefreshToken, PXSelect<RefreshToken, Where<RefreshToken.pin, Equal<Required<RefreshToken.pin>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) deviceInitialise.KraPin
    })));
    if (refreshToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(current);
    if (extension == null || !extension.UsrIsaleIdExpo.HasValue)
      return;
    PXTrace.WriteInformation("sales id exist");
    Adjustment adjustment = new Adjustment()
    {
      reason = "Incoming Adjustment",
      reasonId = "1",
      itemId = extension.UsrIsaleId.Value,
      qty = 100000000M
    };
    PXTrace.WriteInformation("adjuatment payload");
    string content = JsonConvert.SerializeObject((object) adjustment);
    PXTrace.WriteInformation("itemPayload, " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = deviceUrls.SaveStockMaster.Trim();
        string parameter = refreshToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result.EnsureSuccessStatusCode();
        EtimsItemsRes etimsItemsRes = JsonConvert.DeserializeObject<EtimsItemsRes>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {etimsItemsRes}, {etimsItemsRes.status}");
        if (!etimsItemsRes.status)
          throw new Exception("The item was not registered on eTIMS");
        PXTrace.WriteInformation("Item stock adjustment was successfull");
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
    }
  }

  protected void InventoryItem_UsrisRegistered_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    InventoryItem row = (InventoryItem) e.Row;
    if (row == null || !Utility.IsActive() || PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(row) == null)
      return;
    e.NewValue = (object) false;
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

  protected void InventoryItem_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    InventoryItem row = (InventoryItem) e.Row;
    if (row == null)
      return;
    PXTrace.WriteInformation("RowPersisted event triggered");
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
    string countryOfOrigin = row.CountryOfOrigin;
    string itemType = this.DetermineItemType(row.ItemType);
    PXTrace.WriteInformation("formattedInventoryID: " + row.InventoryID?.ToString("D7"));
    string str = packagingUnit + unitOfQuantity;
    cache.SetValueExt<InventoryItemExtEtims.usrProductType>((object) row, (object) itemType);
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

  private string DetermineItemType(string itemType)
  {
    if (itemType == "1" || itemType == "3")
      return itemType;
    if (itemType == "2" || itemType == "F" || itemType == "M")
      return "2";
    return itemType == "A" || itemType == "1" ? "1" : string.Empty;
  }
}
