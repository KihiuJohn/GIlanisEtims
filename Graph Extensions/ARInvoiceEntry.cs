// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntry_Extension_Etims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using GDLOrderCustomization;
using Newtonsoft.Json;
using PX.Data;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntry_Extension_Etims : PXGraphExtension<ARInvoiceEntry>
{
  public PXAction<ARInvoice> GetSales;
  public PXAction<ARInvoice> ETIMSValidation;
  public PXAction<ARInvoice> ETIMSRevalidate;

  protected void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null)
      return;
    bool flag1 = ((ARRegister) row).DocType == "CRM";
    bool flag2 = ((ARRegister) row).DocType == "DRM";
    bool flag3 = ((ARRegister) row).DocType == "INV";
    bool flag4 = Utility.IsActive();
    if (PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row) == null)
      return;
    if (flag4)
    {
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRACreditReason>(cache, (object) row, flag4);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRADebitReason>(cache, (object) row, flag4);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, flag4);
    }
    if (((ARRegister) row).DocType == "INV")
    {
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRACreditReason>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRADebitReason>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalCRNInvoiceNo>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
    }
    if (((ARRegister) row).DocType == "CRM")
    {
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, true);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRADebitReason>(cache, (object) row, false);
    }
    if (((ARRegister) row).DocType == "DRM")
    {
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRACreditReason>(cache, (object) row, false);
    }
    PXUIFieldAttribute.SetRequired<ARRegisterExtEtims.usrZRACreditReason>(cache, flag1);
    PXUIFieldAttribute.SetRequired<ARRegisterExtEtims.usrZRADebitReason>(cache, flag2);
    PXUIFieldAttribute.SetRequired<ARRegisterExtEtims.usrZRAPaymentType>(cache, true);
    PXUIFieldAttribute.SetRequired<ARRegisterExtEtims.usrZRASalesType>(cache, true);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrIsEtimsValidated>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrRsltMessage>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrTvDate>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrCUNumber>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrInvoiceNumber>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrInternalData>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrReceiptSignature>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrQRCodee>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrZRAReceiptNbr>(cache, (object) row, false);
    bool flag5 = ((ARRegister) row).Status == "N" || ((ARRegister) row).Status == "C";
    cache.Graph.Actions["ETIMSValidation"].SetEnabled(flag5);
    cache.Graph.Actions["GetSales"].SetEnabled(flag5);
    cache.Graph.Actions["ETIMSRevalidate"].SetEnabled(flag5);
    ((PXAction) this.ETIMSValidation).SetVisible(flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRAPaymentType>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRASalesType>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrSalesCategory>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrStockInOutType>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrIsEtimsValidated>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrRsltMessage>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrTvDate>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrCUNumber>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrInvoiceNumber>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrInternalData>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrReceiptSignature>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrQRCodee>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginal>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRAReceiptCode>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRAReceiptNbr>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRACreditReason>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRADebitReason>(cache, (object) row, flag4);
    PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalCRNInvoiceNo>(cache, (object) row, flag4);
  }

  protected void ARTran_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag = Utility.IsActive();
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrPackagingUnit>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrUnitOfQty>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrItemClassificationCode>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrTaxType>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrCountryCode>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<ARTranExtEtims.usrIsItemRegistered>(cache, (object) null, flag);
    PXTrace.WriteInformation("INTran_RowSelected event triggered.");
    if ((ARTran) e.Row != null)
      ;
  }

  protected void ARInvoice_UsrIsEtimsValidated_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || !Utility.IsActive() || PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row) == null)
      return;
    e.NewValue = (object) false;
  }

  protected void ARTran_UOM_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null || !Utility.IsActive() || PXCache<ARTran>.GetExtension<ARTranExtEtims>(row) == null)
      return;
    UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.UOM
    })));
    if (uomMapping == null)
      return;
    cache.SetValueExt<ARTranExtEtims.usrPackagingUnit>((object) row, (object) uomMapping.PackagingUnit);
    cache.SetValueExt<ARTranExtEtims.usrUnitOfQty>((object) row, (object) uomMapping.UnitOfQuantity);
  }

  protected void ARTran_InventoryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null)
      return;
    PXCache<ARTran>.GetExtension<ARTranExtEtims>(row);
    InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.InventoryID
    })));
    INSite inSite = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.branchID, Equal<Required<INSite.branchID>>, And<INSite.siteCD, NotEqual<Required<INSite.siteCD>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.BranchID,
      (object) "INTR"
    })));
    if (inSite == null)
      return;
    PXTrace.WriteInformation($"warehouse, {inSite.SiteID} {inSite.SiteCD}");
    if (inventoryItem.StkItem.GetValueOrDefault())
    {
      INItemSite inItemSite = ((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.siteID, Equal<Required<INItemSite.siteID>>, And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) inSite.SiteID,
        (object) row.InventoryID
      })));
      if (inItemSite == null)
        return;
      PXTrace.WriteInformation($"item details warehouse, {inItemSite.SiteID}, {inItemSite.InventoryID}");
      if (inventoryItem == null)
        return;
      InventoryItemExtEtims extension1 = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
      INItemSiteExtEtims extension2 = PXCache<INItemSite>.GetExtension<INItemSiteExtEtims>(inItemSite);
      PXTrace.WriteInformation($"item extension, {extension1.UsrItemCodes}, {extension1.UsrItemClassificationCode}, {extension1.UsrTaxCodes}");
      PXTrace.WriteInformation($"in item site, {extension2.UsrIsRegistered}, {extension2.UsrIsaleID}");
      if (extension1 != null)
      {
        if (extension1.UsrItemClassificationCode != null)
          cache.SetValueExt<ARTranExtEtims.usrItemClassificationCode>((object) row, (object) extension1.UsrItemClassificationCode);
        if (extension1.UsrItemCodes != null)
          cache.SetValueExt<ARTranExtEtims.usrItemCode>((object) row, (object) extension1.UsrItemCodes);
        if (extension1.UsrTaxCodes != null)
          cache.SetValueExt<ARTranExtEtims.usrTaxType>((object) row, (object) extension1.UsrTaxCodes);
        if (extension2.UsrIsRegistered.HasValue)
          cache.SetValueExt<ARTranExtEtims.usrIsItemRegistered>((object) row, (object) extension2.UsrIsRegistered);
        if (extension2.UsrIsaleID.HasValue)
          cache.SetValueExt<ARTranExtEtims.usrIsaleID>((object) row, (object) extension2.UsrIsaleID);
      }
    }
    else if (inventoryItem != null)
    {
      InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
      BranchEtims branchEtims = ((BranchEtims)(PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) inventoryItem.InventoryID,
        (object) row.BranchID
      })));
      if (extension != null)
      {
        if (extension.UsrItemClassificationCode != null)
          cache.SetValueExt<ARTranExtEtims.usrItemClassificationCode>((object) row, (object) extension.UsrItemClassificationCode);
        if (extension.UsrTaxCodes != null)
          cache.SetValueExt<ARTranExtEtims.usrTaxType>((object) row, (object) extension.UsrTaxCodes);
        if (extension.UsrisRegistered.HasValue)
          cache.SetValueExt<ARTranExtEtims.usrIsItemRegistered>((object) row, (object) branchEtims.IsRegistered);
        if (extension.UsrIsaleId.HasValue)
          cache.SetValueExt<ARTranExtEtims.usrIsItemRegistered>((object) row, (object) branchEtims.Isaleid);
      }
    }
  }

  protected void ARTran_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ARTran row = (ARTran) e.Row;
    if (row == null)
      return;
    PXTrace.WriteInformation("tax category, " + row.TaxCategoryID);
    if (PXCache<ARTran>.GetExtension<ARTranExtEtims>(row) == null)
      return;
    TaxCdes taxCdes = ((TaxCdes)(PXSelectBase<TaxCdes, PXSelect<TaxCdes, Where<TaxCdes.taxCategories, Equal<Required<TaxCdes.taxCategories>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.TaxCategoryID
    })));
    if (taxCdes == null)
      return;
    PXTrace.WriteInformation("taxcode, " + taxCdes.TaxCode);
    cache.SetValueExt<ARTranExtEtims.usrTaxType>((object) row, (object) taxCdes.TaxCode);
  }

  protected void ARInvoice_CustomerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row) == null)
      return;
    PaymentMethod paymentMethod = ((PaymentMethod)(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.PaymentMethodID
    })));
    if (paymentMethod == null)
    {
      PXTrace.WriteInformation("payment is null");
      cache.SetValueExt<ARRegisterExtEtims.usrZRAPaymentType>((object) row, (object) "01");
    }
    if (paymentMethod != null)
    {
      PaymentMethodExttEtims extension = PXCache<PaymentMethod>.GetExtension<PaymentMethodExttEtims>(paymentMethod);
      if (extension == null)
        return;
      cache.SetValueExt<ARRegisterExtEtims.usrZRAPaymentType>((object) row, (object) extension.UsrKraPaymentCode);
    }
    if (((ARRegister) row).DocType == "INV")
      cache.SetValueExt<ARRegisterExtEtims.usrZRASalesType>((object) row, (object) "S");
    else if (((ARRegister) row).DocType == "CRM")
    {
      cache.SetValueExt<ARRegisterExtEtims.usrZRASalesType>((object) row, (object) "R");
    }
    else
    {
      if (!(((ARRegister) row).DocType == "DRM"))
        return;
      cache.SetValueExt<ARRegisterExtEtims.usrZRASalesType>((object) row, (object) "S");
    }
  }

  protected void ARInvoice_UsrOriginalReceiptNbr_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null)
      return;
    ARRegisterExtEtims extension1 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row);
    if (extension1 == null)
      return;
    PXTrace.WriteInformation($"Original receipt number updated: {extension1.UsrOriginalReceiptNbr}");
    ARRegister arRegister = ((ARRegister)(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegisterExtEtims.usrOriginal, Equal<Required<ARRegisterExtEtims.usrOriginal>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) extension1.UsrOriginalReceiptNbr
    })));
    if (arRegister == null)
    {
      PXTrace.WriteInformation("reg is null");
    }
    else
    {
      PXTrace.WriteInformation($"ARRegister record found: DocType = {arRegister.DocType}, RefNbr = {arRegister.RefNbr}");
      ARRegisterExtEtims extension2 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>(arRegister);
      if (extension2 == null)
        return;
      PXTrace.WriteInformation("Setting UsrOriginalCRNInvoiceNo to: " + extension2.UsrInvoiceNumber);
      cache.SetValueExt<ARRegisterExtEtims.usrOriginalCRNInvoiceNo>((object) row, (object) extension2.UsrInvoiceNumber);
    }
  }

  protected void ARInvoice_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    ARRegisterExtEtims extension = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row);
    if (row == null || !(((ARRegister) row).DocType == "CRM") && !(((ARRegister) row).DocType == "DRM") || ((ARRegister) row).OrigRefNbr == null)
      return;
    cache.SetValueExt<ARRegisterExtEtims.usrOriginalReceiptNbr>((object) row, (object) extension.UsrOriginal);
    cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRACreditReason>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRADebitReason>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRACreditReason>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRADebitReason>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRAReceiptCode>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRAPaymentType>((object) row, (object) null);
    cache.SetValueExt<ARRegisterExtEtims.usrZRASalesType>((object) row, (object) null);
    cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) row, (object) null);
    cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) row, (object) null);
  }

  protected void ARInvoice_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null || ((ARRegister) row).DocType != "CRM")
      return;
    ARRegisterExtEtims extension1 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row);
    if (extension1.UsrOriginalReceiptNbr.HasValue)
      return;
    ARTran arTran = GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).FirstOrDefault<ARTran>();
    if (arTran == null || string.IsNullOrEmpty(arTran.OrigInvoiceNbr))
      return;
    ARInvoice arInvoice = ((ARInvoice)(PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<ARDocType.invoice>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) arTran.OrigInvoiceNbr
    })));
    if (arInvoice == null)
      return;
    ARRegisterExtEtims extension2 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) arInvoice);
    if (!extension2.UsrOriginal.HasValue)
      return;
    extension1.UsrOriginalReceiptNbr = extension2.UsrOriginal;
    cache.Update((object) row);
  }

  protected void _(
    Events.FieldVerifying<ARRegisterExtEtims.usrCustomrTin> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<ARRegisterExtEtims.usrCustomrTin>, object, object>) e).NewValue != null && Utility.IsActive() && ((Events.FieldVerifyingBase<Events.FieldVerifying<ARRegisterExtEtims.usrCustomrTin>, object, object>) e).NewValue.ToString().Length != 10)
      throw new PXSetPropertyException("Customer TPin must be exactly 10 characters long.");
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Get Sales Info")]
  protected void getSales()
  {
    try
    {
      ARInvoice current = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      ARRegisterExtEtims extension = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
      string requestUri = string.Empty;
      DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current).BranchID
      })));
      DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current).BranchID
      })));
      DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current).BranchID
      })));
      if (deviceToken == null)
        throw new PXException("Please Refresh Access Token to be able to continue");
      if (((ARRegister) current)?.DocType == "CRM")
      {
        requestUri = $"{deviceUrls.UpdateImportUrl.Trim()}/invoice/{extension.UsrOriginalReceiptNbr}";
        PXTrace.WriteInformation("Get sale url credit note: " + requestUri);
      }
      else if (((ARRegister) current)?.DocType == "INV")
      {
        requestUri = deviceUrls.SalesUrl.Trim();
        PXTrace.WriteInformation("Get sales url invoice " + requestUri);
      }
      string empty = string.Empty;
      using (HttpClient httpClient = new HttpClient())
      {
        string parameter = deviceToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result1 = httpClient.GetAsync(requestUri).Result;
        if (!result1.IsSuccessStatusCode)
          return;
        string result2 = result1.Content.ReadAsStringAsync().Result;
        PXTrace.WriteInformation("Response from Get sales API: " + result2);
        ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(result2);
        if (responseData.status && !(((ARRegister) current)?.DocType == "INV") && ((ARRegister) current)?.DocType == "CRM")
        {
          ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.cfmDt);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.intrlData);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.rcptSign);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.long_url);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{deviceInfo.SalesControlUnitID}/{responseData.credit.invcNo}");
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.invcNo);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{deviceInfo.SalesControlUnitID}/{responseData.credit.invcNo}");
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.credit.long_url);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
          ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
          ((PXSelectBase<ARInvoice>) this.Base.Document).Update(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
          ((PXGraph) this.Base).Persist();
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Get sales exception: " + ex.Message);
      throw;
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "ETIMS VALIDATION")]
  protected void etIMSValidation()
  {
    try
    {
      ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      ARTran current2 = ((PXSelectBase<ARTran>) this.Base.Transactions).Current;
      if (PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) current1) == null)
        return;
      ARInvoiceExtens extension1 = PXCache<ARInvoice>.GetExtension<ARInvoiceExtens>(current1);
      if (extension1 == null)
        return;
      if (!string.IsNullOrWhiteSpace(extension1.UsrCUInvoiceNo) && !string.IsNullOrWhiteSpace(extension1.UsrCUSerialNo))
        throw new PXException("Invoice has already been transmitted to KRA.");
      Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (branch == null)
        throw new PXException("Branch not found");
      PXTrace.WriteInformation($"branch :{branch.BranchCD}, Invoice branch ID: {((ARRegister) current1).BranchID}");
      DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (deviceInitialise == null)
        throw new PXException($"Device information not found for the current branch ({branch.BranchCD.Trim()}).");
      PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
      DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (deviceToken == null)
        throw new PXException("Please Refresh Access Token to be able to continue");
      if (deviceInfo == null)
        return;
      Decimal d1 = 0M;
      Decimal d2 = 0M;
      Decimal d3 = 0M;
      Decimal num1 = 0M;
      Decimal d4 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = Math.Round(16.000000M, 2);
      Decimal num4 = 0M;
      Decimal num5 = 0M;
      Decimal num6 = Math.Round(8.000000M, 2);
      Decimal d5 = 0M;
      Decimal d6 = 0M;
      Decimal d7 = 0M;
      Decimal num7 = 0M;
      Decimal d8 = 0M;
      foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
      {
        ARTaxTran arTaxTran = ((ARTaxTran)(pxResult));
        Decimal? nullable = ((TaxDetail) arTaxTran).TaxRate;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        if (!(valueOrDefault1 == 0.000000M))
        {
          if (!(valueOrDefault1 == 8.000000M))
          {
            if (valueOrDefault1 == 16.000000M && ((TaxDetail) arTaxTran).TaxID == "02")
            {
              Decimal num8 = d2;
              nullable = ((TaxTran) arTaxTran).TaxableAmt;
              Decimal valueOrDefault2 = nullable.GetValueOrDefault();
              d2 = num8 + valueOrDefault2;
              num3 = Math.Round(16.000000M, 2);
              Decimal num9 = d6;
              nullable = ((TaxTran) arTaxTran).TaxAmt;
              Decimal valueOrDefault3 = nullable.GetValueOrDefault();
              d6 = num9 + valueOrDefault3;
              d6 = Math.Round(d6, 2);
              d2 = Math.Round(d2, 2);
            }
          }
          else
          {
            Decimal num10 = d4;
            nullable = ((TaxTran) arTaxTran).TaxableAmt;
            Decimal valueOrDefault4 = nullable.GetValueOrDefault();
            d4 = num10 + valueOrDefault4;
            num6 = Math.Round(8.000000M, 2);
            Decimal num11 = d8;
            nullable = ((TaxTran) arTaxTran).TaxAmt;
            Decimal valueOrDefault5 = nullable.GetValueOrDefault();
            d8 = num11 + valueOrDefault5;
            d8 = Math.Round(d8, 2);
            d4 = Math.Round(d4, 2);
          }
        }
        else if (((TaxDetail) arTaxTran).TaxID == "04")
        {
          Decimal num12 = d3;
          nullable = ((TaxTran) arTaxTran).TaxableAmt;
          Decimal valueOrDefault6 = nullable.GetValueOrDefault();
          d3 = num12 + valueOrDefault6;
          num4 = Math.Round(0.000000M, 2);
          Decimal num13 = d7;
          nullable = ((TaxTran) arTaxTran).TaxAmt;
          Decimal valueOrDefault7 = nullable.GetValueOrDefault();
          d7 = num13 + valueOrDefault7;
          d7 = Math.Round(d7, 2);
          d3 = Math.Round(d3, 2);
        }
        else if (((TaxDetail) arTaxTran).TaxID == "09")
        {
          Decimal num14 = d1;
          nullable = ((TaxTran) arTaxTran).TaxableAmt;
          Decimal valueOrDefault8 = nullable.GetValueOrDefault();
          d1 = num14 + valueOrDefault8;
          num2 = Math.Round(0.000000M, 2);
          Decimal num15 = d5;
          nullable = ((TaxTran) arTaxTran).TaxAmt;
          Decimal valueOrDefault9 = nullable.GetValueOrDefault();
          d5 = num15 + valueOrDefault9;
          d5 = Math.Round(d5, 2);
          d1 = Math.Round(d1, 2);
        }
      }
      var array = GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).Where<ARTran>((Func<ARTran, bool>) (tran => tran.InventoryID.HasValue)).Select((tran, index) =>
      {
        ARTranExtEtims extension2 = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<ARTranExtEtims>((object) tran);
        Decimal num16 = Math.Round(tran.Qty.GetValueOrDefault(), 2);
        Math.Abs(tran.Qty.GetValueOrDefault());
        PXTrace.WriteInformation($"qty, {num16}, LineNbr: {tran.LineNbr}");
        Math.Abs(tran.TranAmt.GetValueOrDefault());
        UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.UOM
        })));
        Decimal valueOrDefault = ((Decimal?) ((TaxCdes)(PXSelectBase<TaxCdes, PXSelect<TaxCdes, Where<TaxCdes.taxCategories, Equal<Required<InventoryItem.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.TaxCategoryID
        })))?.TaxRate).GetValueOrDefault();
        Decimal num17 = valueOrDefault / 100M;
        Decimal num18 = 0M;
        Decimal num19 = 0M;
        Decimal num20 = 0M;
        Decimal num21 = 0M;
        Decimal num22 = 0M;
        Decimal num23 = 0M;
        Decimal num24 = 0M;
        if (tran.TaxCategoryID == "TAXABLE" || tran.TaxCategoryID == "TAXWHT5WVAT2" || tran.TaxCategoryID == "TAXWVAT2" || tran.TaxCategoryID == "VATWHT5")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault(), 2);
          num20 = Math.Round(num19 * num16, 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          num21 = Math.Round((num20 - num24) * num17 / (1M + num17), 2);
          num18 = Math.Round((num20 - num24) / (1M + num17), 2);
          num22 = Math.Round(num20 - num24, 2);
          PXTrace.WriteInformation($"INCLUSIVE tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} percentage rate={num17}, tax rate={valueOrDefault}");
        }
        else if (tran.TaxCategoryID == "16EXCL")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault() * (1M + num17), 2);
          num20 = Math.Round(num19 * num16, 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          num22 = Math.Round(num20 - num24, 2);
          num18 = Math.Round(num22 / (1M + num17), 2);
          num21 = Math.Round(num22 - num18, 2);
          PXTrace.WriteInformation($"EXCLUSIVE tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} percentage rate={num17}, tax rate={valueOrDefault}");
        }
        else if (tran.TaxCategoryID == "ZERO RATE" || tran.TaxCategoryID == "EXEMPT")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault(), 2);
          num20 = Math.Round(num19 * num16, 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          Decimal num25 = Math.Round(tran.DiscAmt.GetValueOrDefault(), 2);
          num21 = Math.Round(0.000000M, 2);
          num22 = Math.Round(num20 - num24, 2);
          num18 = Math.Round(num20 - num24, 2);
          PXTrace.WriteInformation($"ZERO tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} dcAmt2={num25}");
        }
        string packagingUnit = uomMapping.PackagingUnit;
        string unitOfQuantity = uomMapping.UnitOfQuantity;
        PXTrace.WriteInformation($"pack and quantity codes: {packagingUnit}{unitOfQuantity}");
        InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.InventoryID
        })));
        if (inventoryItem == null)
          PXTrace.WriteInformation("inv is null");
        else
          PXTrace.WriteInformation($"inv is not null, {inventoryItem.InventoryID}, {inventoryItem.InventoryCD}");
        InventoryItemExtEtims extension3 = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
        if (extension3 == null)
          PXTrace.WriteInformation("INVENTORY EXTENSION IS EMPTY");
        else
          PXTrace.WriteInformation($"INV EXT, {extension3.UsrItemClassificationCode}, {extension3.UsrTaxCodes}");
        if (string.IsNullOrEmpty(inventoryItem.InventoryCD) || string.IsNullOrEmpty(extension3.UsrItemClassificationCode) || string.IsNullOrEmpty(extension3.UsrTaxCodes))
          throw new PXException($"Please check if item classification code, tax code for item id {inventoryItem.InventoryCD.Trim()} has been configured");
        int num26 = -1;
        string str1;
        string str2;
        if (inventoryItem.StkItem.GetValueOrDefault())
        {
          INItemSite inItemSite1 = ((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.siteID, Equal<Required<INItemSite.siteID>>, And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.SiteID,
            (object) tran.InventoryID
          })));
          if (inItemSite1 == null)
            PXTrace.WriteInformation("item site is empty");
          INItemSiteExtEtims extension4 = PXCache<INItemSite>.GetExtension<INItemSiteExtEtims>(inItemSite1);
          int? usrIsaleId;
          int num27;
          if (extension4 == null)
          {
            num27 = 1;
          }
          else
          {
            usrIsaleId = extension4.UsrIsaleID;
            num27 = !usrIsaleId.HasValue ? 1 : 0;
          }
          if (num27 != 0)
          {
            INSite inSite = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
            {
              (object) tran.SiteID
            })));
            if (inSite != null)
            {
              INItemSite inItemSite2 = ((INItemSite)(PXSelectBase<INItemSite, PXSelectJoin<INItemSite, InnerJoin<INSite, On<INItemSite.siteID, Equal<INSite.siteID>>>, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INSite.branchID, Equal<Required<INSite.branchID>>, And<INItemSiteExtEtims.usrIsaleID, IsNotNull>>>>.Config>.Select((PXGraph) this.Base, new object[2]
              {
                (object) tran.InventoryID,
                (object) inSite.BranchID
              })));
              if (inItemSite2 != null)
                extension4 = PXCache<INItemSite>.GetExtension<INItemSiteExtEtims>(inItemSite2);
            }
          }
          Location location = ((Location)(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) tran.CustomerID
          })));
          if (location == null)
            throw new PXException("Customer location record not found.");
          if (location.CTaxZoneID == "ZERORATE")
          {
            if (string.IsNullOrWhiteSpace(extension3.UsrItemClassificationCodeExpo?.ToString()))
              throw new PXException("Export Item Classification Code is required, make sure item is register on eTiMS.");
            str1 = !string.IsNullOrWhiteSpace(extension3.UsrTaxCodesExpo?.ToString()) ? extension3.UsrItemClassificationCodeExpo.ToString().Trim() : throw new PXException("Export Tax Code is required, make sure item is register on eTiMS.");
            str2 = extension3.UsrTaxCodesExpo.ToString().Trim();
          }
          else
          {
            if (string.IsNullOrWhiteSpace(extension3.UsrItemClassificationCode?.ToString()))
              throw new PXException("Local Item Classification Code is required, make sure item is register on eTiMS.");
            if (string.IsNullOrWhiteSpace(extension3.UsrTaxCodes?.ToString()))
              throw new PXException("Local Tax Code is required, make sure item is register on eTiMS.");
            usrIsaleId = extension2.UsrIsaleID;
            num26 = usrIsaleId ?? extension4.UsrIsaleID ?? -1;
            str1 = extension3.UsrItemClassificationCode.ToString().Trim();
            str2 = extension3.UsrTaxCodes.ToString().Trim();
          }
        }
        else
        {
          BranchEtims branchEtims = ((BranchEtims)(PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.InventoryID,
            (object) tran.BranchID
          })));
          if (branchEtims == null)
            throw new PXException($"Branch {branch.BranchCD.Trim()} has not been added for non stock item {tran.TranDesc.Trim()}");
          num26 = extension2.UsrIsaleID ?? branchEtims.Isaleid ?? -1;
          str1 = extension3.UsrItemClassificationCode.ToString().Trim();
          str2 = extension3.UsrTaxCodes.ToString().Trim();
        }
        if (num26 == -1)
          throw new PXException($"Item {tran.InventoryID} does not have an iSaleID in branch.");
        var data = new
        {
          itemSeq = index + 1,
          itemID = num26,
          itemCd = inventoryItem.InventoryCD.Trim(),
          itemClsCd = str1,
          itemNm = tran.TranDesc ?? "",
          itemNmDef = tran.TranDesc ?? "",
          bcd = "",
          pkgUnitCd = packagingUnit.Trim(),
          pkg = num16,
          qtyUnitCd = unitOfQuantity.Trim(),
          qty = num16,
          prc = num19,
          splyAmt = num20,
          dcRt = num23,
          dcAmt = num24,
          taxTyCd = str2,
          taxblAmt = num18,
          taxAmt = num21,
          totAmt = num22
        };
        return data;
      }).ToArray();
      Dictionary<string, Decimal> dictionary1 = new Dictionary<string, Decimal>();
      Dictionary<string, Decimal> dictionary2 = new Dictionary<string, Decimal>();
      foreach (var source in array.GroupBy(item => item.taxTyCd))
      {
        Decimal num28 = source.Sum(item => item.taxblAmt);
        dictionary1[source.Key] = num28;
        PXTrace.WriteInformation($"Category: {source.Key}, Total vatTaxblAmt: {num28}");
      }
      foreach (var source in array.GroupBy(item => item.taxTyCd))
      {
        Decimal num29 = source.Sum(item => item.taxAmt);
        dictionary2[source.Key] = num29;
        PXTrace.WriteInformation($"Category: {source.Key}, Total TaxAmt: {num29}");
      }
      Dictionary<string, Decimal> dictionary3 = new Dictionary<string, Decimal>()
      {
        {
          "A",
          d1
        },
        {
          "B",
          d2
        },
        {
          "C1",
          d3
        },
        {
          "D",
          num1
        },
        {
          "E",
          d4
        }
      };
      Dictionary<string, Decimal> dictionary4 = new Dictionary<string, Decimal>()
      {
        {
          "A",
          d5
        },
        {
          "B",
          d6
        },
        {
          "C1",
          d7
        },
        {
          "D",
          num7
        },
        {
          "E",
          d8
        }
      };
      foreach (string key in dictionary3.Keys.ToList<string>())
      {
        Decimal num30;
        if (dictionary1.TryGetValue(key, out num30) && dictionary3[key] != num30)
          dictionary3[key] = num30;
      }
      foreach (string key in dictionary4.Keys.ToList<string>())
      {
        Decimal num31;
        if (dictionary2.TryGetValue(key, out num31) && dictionary4[key] != num31)
          dictionary4[key] = num31;
      }
      Decimal num32 = dictionary3["A"];
      Decimal num33 = dictionary3["B"];
      Decimal num34 = dictionary3["C1"];
      Decimal num35 = dictionary3["D"];
      Decimal num36 = dictionary3["E"];
      Decimal num37 = dictionary4["A"];
      Decimal num38 = dictionary4["B"];
      Decimal num39 = dictionary4["C1"];
      Decimal num40 = dictionary4["D"];
      Decimal num41 = dictionary4["E"];
      Decimal num42 = Math.Round(dictionary3.Values.Sum(), 2);
      Decimal num43 = Math.Round(dictionary4.Values.Sum(), 2);
      Decimal num44 = Math.Round(num42 + num43, 2);
      PXTrace.WriteInformation($"KRA TOTALS: Total Taxable Amount: {num42}, Total Tax Amount: {num43}, Total Amount: {num44}");
      Decimal num45 = Math.Round(((PXSelectBase<ARInvoice>) this.Base.Document).Current.VatTaxableTotal.GetValueOrDefault() + ((PXSelectBase<ARInvoice>) this.Base.Document).Current.VatExemptTotal.GetValueOrDefault(), 2);
      Decimal num46 = Math.Round(((Decimal?) ((PXSelectBase<ARInvoice>) this.Base.Document).Current?.TaxTotal).GetValueOrDefault(), 2);
      Decimal num47 = Math.Round(((Decimal?) ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.OrigDocAmt).GetValueOrDefault(), 2);
      PXTrace.WriteInformation($"ACUMATICA TOTALS: Total Taxable Amount: {num45}, Total Tax Amount: {num46}, Total Amount: {num47}");
      Decimal num48 = num45 == num42 ? num45 : num42;
      Decimal num49 = num46 == num43 ? num46 : num43;
      Decimal num50 = num47 == num44 ? num47 : num44;
      GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).SelectMany(tran =>
      {
        PXResultset<INSite> pxResultset = PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
        return pxResultset.Count == 0 ? null : GraphHelper.RowCast<INSite>((IEnumerable) pxResultset).Select(site =>
        {
          INSiteStatus inSiteStatus = ((INSiteStatus)(PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.InventoryID,
            (object) site.SiteID
          })));
          InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) tran.InventoryID
          })));
          if (inventoryItem == null)
          {
            PXTrace.WriteInformation($"Inventory item '{tran.InventoryID}' does not exist.");
            return null;
          }
          if (inSiteStatus == null)
          {
            PXTrace.WriteInformation($"Inventory item '{inventoryItem.InventoryCD}' does not exist in warehouse '{site.SiteCD}'.");
            return null;
          }
          PXTrace.WriteInformation($"Inventory '{inventoryItem.InventoryCD}' has {inSiteStatus.QtyOnHand} in warehouse '{site.SiteCD}'.");
          var data2 = new
          {
            itemCd = inventoryItem.InventoryCD.Trim(),
            qtyOnHand = inSiteStatus.QtyOnHand.GetValueOrDefault()
          };
          return data2;
        }).Where(result => result != null).GroupBy(item => item.itemCd).Select(group => new
        {
          itemCd = group.Key,
          rsdQty = group.Sum(x => x.qtyOnHand)
        });
      }).ToArray();
      PXTrace.WriteInformation("fetch stock item list");
      ARRegisterExtEtims extension5 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) current1);
      int num51 = 0;
      if (((ARRegister) current1)?.DocType == "CRM")
      {
        if (!extension5.UsrOriginalReceiptNbr.HasValue || extension5.UsrOriginalReceiptNbr.Value == 0)
          throw new PXException("Etims Original Receipt Number is required for CRM documents and cannot be zero.");
        num51 = extension5.UsrOriginalReceiptNbr.Value;
      }
      string str3 = ((ARRegister) current1)?.DocType == "CRM" ? "Credit" : "Sale";
      string salesControlUnitId = ((ARRegister) current1)?.DocType == "CRM" ? deviceInfo.SalesControlUnitID : (string) null;
      string usrZraCreditReason = ((ARRegister) current1)?.DocType == "CRM" ? extension5.UsrZRACreditReason : (string) null;
      string usrZraDebitReason = ((ARRegister) current1)?.DocType == "DRM" ? extension5.UsrZRADebitReason : (string) null;
      string usrLpo = extension5.UsrLpo != null ? extension5.UsrLpo : (string) null;
      string usrZraReceiptCode = extension5.UsrZRAReceiptCode != null ? extension5.UsrZRAReceiptCode : (string) null;
      string str4 = ((ARRegister) current1)?.DocType == "INV" || ((ARRegister) current1)?.DocType == "DRM" || ((ARRegister) current1)?.DocType == "CSL" ? "S" : "R";
      string str5 = "01";
      string usrStockInOutType = extension5.UsrStockInOutType;
      PXTrace.WriteInformation($"customer id, {((ARRegister) current1).CustomerID}");
      BAccount baccount = ((BAccount)(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<BAccount.bAccountID, NotEqual<Required<BAccount.bAccountID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) ((ARRegister) current1).CustomerID,
        (object) 2
      })));
      Location location1 = ((Location)(PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefLocationID
      })));
      PXTrace.WriteInformation($"baccount id, {baccount.BAccountID}");
      string acctName = baccount.AcctName;
      PXTrace.WriteInformation("customerName, " + acctName);
      string str6 = location1.TaxRegistrationID;
      if (str6 == null)
      {
        PXTrace.WriteInformation("Tpin for the selected customer is no set, if the customer is a cash sales please insert customer tpin");
        if (extension5.UsrCustomrTin != null)
        {
          str6 = extension5.UsrCustomrTin.ToString().Trim();
          PXTrace.WriteInformation("customer tpin for the cash sales customer is, " + str6);
        }
      }
      Decimal num52 = Math.Round(((Decimal?) ((PXSelectBase<ARInvoice>) this.Base.Document).Current?.LineDiscTotal).GetValueOrDefault(), 2);
      Decimal num53 = Math.Round(((Decimal?) ((PXSelectBase<ARInvoice>) this.Base.Document).Current?.DetailExtPriceTotal).GetValueOrDefault(), 2);
      Decimal num54 = num52 != 0M ? num52 * 100M / num53 : 0M;
      PXTrace.WriteInformation($"cash disc, {num52} cash rate {num54} cury Total {num48}");
      RequestData requestData1 = new RequestData();
      requestData1.tin = deviceInitialise.KraPin;
      requestData1.bhfId = deviceInitialise.KraBranchID;
      requestData1.trdInvcNo = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr;
      requestData1.orgInvcNo = num51;
      requestData1.invcNo = 0;
      requestData1.custTin = str6 ?? " ";
      requestData1.custNm = acctName;
      requestData1.custType = "NEW";
      requestData1.custMblNo = "";
      requestData1.custEmail = "";
      requestData1.custID = baccount.AcctCD.Trim();
      requestData1.salesTyCd = "N";
      requestData1.rcptTyCd = str4;
      requestData1.pmtTyCd = str5;
      requestData1.salesSttsCd = "02";
      ARInvoice current3 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      DateTime? docDate;
      DateTime dateTime;
      string str7;
      if (current3 == null)
      {
        str7 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current3).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str7 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str7 = dateTime.ToString("yyyyMMddHHmmss");
        }
      }
      requestData1.cfmDt = str7;
      ARInvoice current4 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      string str8;
      if (current4 == null)
      {
        str8 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current4).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str8 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str8 = dateTime.ToString("yyyyMMdd");
        }
      }
      requestData1.salesDt = str8;
      requestData1.totItemCnt = array.Length;
      ARInvoice current5 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      string str9;
      if (current5 == null)
      {
        str9 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current5).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str9 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str9 = dateTime.ToString("yyyyMMddHHmmss");
        }
      }
      requestData1.stockRlsDt = str9;
      requestData1.cnclReqDt = (string) null;
      requestData1.cnclDt = (string) null;
      requestData1.rfdDt = (string) null;
      requestData1.rfdRsnCd = usrZraCreditReason;
      requestData1.dbtRsnCd = usrZraDebitReason;
      requestData1.taxblAmtA = num32;
      requestData1.taxblAmtB = num33;
      requestData1.taxblAmtC = num34;
      requestData1.taxblAmtD = num35;
      requestData1.taxblAmtE = num36;
      requestData1.taxRtA = num2;
      requestData1.taxRtB = num3;
      requestData1.taxRtC = num4;
      requestData1.taxRtD = num5;
      requestData1.taxRtE = num6;
      requestData1.taxAmtA = num37;
      requestData1.taxAmtB = num38;
      requestData1.taxAmtC = num39;
      requestData1.taxAmtD = num40;
      requestData1.taxAmtE = num41;
      requestData1.totTaxblAmt = num48;
      requestData1.totTaxAmt = num49;
      requestData1.totAmt = num50;
      requestData1.prchrAcptcYn = "N";
      requestData1.remark = str3;
      requestData1.regrId = "ADMIN";
      requestData1.regrNm = "ADMIN";
      requestData1.modrId = "ADMIN";
      requestData1.modrNm = "ADMIN";
      ReceiptEtims receiptEtims = new ReceiptEtims();
      receiptEtims.custTin = str6 ?? (string) null;
      dateTime = DateTime.Now;
      receiptEtims.rcptPbctDt = dateTime.ToString("yyyyMMddHHmmss");
      receiptEtims.prchrAcptcYn = "N";
      receiptEtims.rptNo = 0;
      requestData1.receipt = receiptEtims;
      requestData1.itemList = array.Select(item => new Item()
      {
        itemSeq = item.itemSeq,
        itemId = item.itemID,
        itemCd = item.itemCd,
        itemClsCd = item.itemClsCd,
        itemNm = item.itemNm,
        itemNmDef = item.itemNm,
        pkgUnitCd = item.pkgUnitCd,
        pkg = item.pkg,
        qtyUnitCd = item.qtyUnitCd,
        qty = item.qty,
        prc = item.prc,
        splyAmt = item.splyAmt,
        dcRt = item.dcRt,
        dcAmt = item.dcAmt,
        taxTyCd = item.taxTyCd,
        taxblAmt = item.taxblAmt,
        taxAmt = item.taxAmt,
        totAmt = item.totAmt
      }).ToList<Item>();
      RequestData requestData2 = requestData1;
      string content = JsonConvert.SerializeObject((object) requestData2);
      PXTrace.WriteInformation("ZRA Validation Request Details: " + content);
      string requestUri;
      if (requestData2.rcptTyCd == "S")
      {
        requestUri = deviceUrls.SalesUrl.Trim();
        PXTrace.WriteInformation("ETIMS VALIDATION - URL: " + requestUri);
      }
      else
      {
        requestUri = deviceUrls.UpdateImportUrl.Trim();
        PXTrace.WriteInformation("ETIMS VALIDATION - URL: " + requestUri);
      }
      string str10 = string.Empty;
      using (HttpClient httpClient = new HttpClient())
      {
        try
        {
          string parameter = deviceToken.AccessToken.Trim();
          PXTrace.WriteInformation("Access Token: " + parameter);
          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
          HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
          if (result1.IsSuccessStatusCode)
          {
            str10 = result1.Content.ReadAsStringAsync().Result;
            PXTrace.WriteInformation("Response from eTIMS API: " + str10);
            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(str10);
            if (responseData.status)
            {
              ARRegisterExtEtims extension6 = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.vsdcRcptPbctDate);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.intrlData);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.rcptSign);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.long_url);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.sale.invcNo}");
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.sale.invcNo);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.sale.invcNo}");
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.long_url);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
              ((PXSelectBase<ARInvoice>) this.Base.Document).Update(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXGraph) this.Base).Persist();
              ZraLogs zraLogs = new ZraLogs()
              {
                BranchId = deviceInitialise?.KraBranchID,
                DocumentType = "Sales",
                DocumentNbr = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr,
                RequestLogOne = content,
                ResponseLog = str10 == null || str10.Length <= 2000 ? str10 : str10.Substring(0, 2000)
              };
              GraphHelper.Caches<ZraLogs>((PXGraph) this.Base).Insert(zraLogs);
              ((PXGraph) this.Base).Persist(typeof (ZraLogs), (PXDBOperation) 2);
            }
            else
            {
              ARRegisterExtEtims extension7 = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
              if (string.IsNullOrEmpty(extension5.UsrQRCodee) && string.IsNullOrEmpty(extension5.UsrInvoiceNumber))
              {
                if (((ARRegister) current1)?.DocType == "INV")
                {
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.cfmDt);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.intrlData);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.rcptSign);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension7.UsrCUNumber}/{responseData.existingSale.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.invcNo);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension7.UsrCUNumber}/{responseData.existingSale.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                }
                else if (((ARRegister) current1)?.DocType == "CRM")
                {
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.cfmDt);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.intrlData);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.rcptSign);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension7.UsrCUNumber}/{responseData.creditNote.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.invcNo);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension7.UsrCUNumber}/{responseData.creditNote.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                }
              }
              ((PXSelectBase<ARInvoice>) this.Base.Document).Update(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXGraph) this.Base).Persist();
              throw new Exception("Failed with result message: " + responseData.message);
            }
          }
          else
          {
            string result2 = result1.Content.ReadAsStringAsync().Result;
            PXTrace.WriteInformation($" {result1.StatusCode}, Response: {result2}");
            throw new Exception($"Failed with status code: {result1.StatusCode}");
          }
        }
        catch (Exception ex)
        {
          ZraLogs zraLogs = new ZraLogs()
          {
            BranchId = deviceInitialise?.KraBranchID,
            DocumentType = "Sales",
            DocumentNbr = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr,
            RequestLogOne = content,
            ResponseLog = str10 == null || str10.Length <= 2000 ? str10 : str10.Substring(0, 2000)
          };
          GraphHelper.Caches<ZraLogs>((PXGraph) this.Base).Insert(zraLogs);
          ((PXGraph) this.Base).Persist(typeof (ZraLogs), (PXDBOperation) 2);
          PXTrace.WriteError(ex);
          PXTrace.WriteError("Error during transmission: " + ex.Message);
          throw;
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("ETIMS Validation Exception: " + ex.Message);
      throw;
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "ETIMS Revalidation")]
  protected void etIMSRevalidate()
  {
    try
    {
      ARInvoice current1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      ARTran current2 = ((PXSelectBase<ARTran>) this.Base.Transactions).Current;
      if (PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) current1) == null || PXCache<ARInvoice>.GetExtension<ARInvoiceExtens>(current1) == null)
        return;
      Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (branch == null)
        throw new PXException("Branch not found");
      DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (deviceInitialise == null)
        throw new PXException($"Device information not found for the current branch ({branch.BranchCD.Trim()}).");
      DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((ARRegister) current1).BranchID
      })));
      if (deviceToken == null)
        throw new PXException("Please Refresh Access Token to be able to continue");
      if (deviceInfo == null)
        return;
      Decimal d1 = 0M;
      Decimal d2 = 0M;
      Decimal d3 = 0M;
      Decimal num1 = 0M;
      Decimal d4 = 0M;
      Decimal num2 = 0M;
      Decimal num3 = Math.Round(16.000000M, 2);
      Decimal num4 = 0M;
      Decimal num5 = 0M;
      Decimal num6 = Math.Round(8.000000M, 2);
      Decimal d5 = 0M;
      Decimal d6 = 0M;
      Decimal d7 = 0M;
      Decimal num7 = 0M;
      Decimal d8 = 0M;
      foreach (PXResult<ARTaxTran> pxResult in ((PXSelectBase<ARTaxTran>) this.Base.Taxes).Select(Array.Empty<object>()))
      {
        ARTaxTran arTaxTran = ((ARTaxTran)(pxResult));
        Decimal? nullable = ((TaxDetail) arTaxTran).TaxRate;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        if (!(valueOrDefault1 == 0.000000M))
        {
          if (!(valueOrDefault1 == 8.000000M))
          {
            if (valueOrDefault1 == 16.000000M && ((TaxDetail) arTaxTran).TaxID == "02")
            {
              Decimal num8 = d2;
              nullable = ((TaxTran) arTaxTran).TaxableAmt;
              Decimal valueOrDefault2 = nullable.GetValueOrDefault();
              d2 = num8 + valueOrDefault2;
              num3 = Math.Round(16.000000M, 2);
              Decimal num9 = d6;
              nullable = ((TaxTran) arTaxTran).TaxAmt;
              Decimal valueOrDefault3 = nullable.GetValueOrDefault();
              d6 = num9 + valueOrDefault3;
              d6 = Math.Round(d6, 2);
              d2 = Math.Round(d2, 2);
            }
          }
          else
          {
            Decimal num10 = d4;
            nullable = ((TaxTran) arTaxTran).TaxableAmt;
            Decimal valueOrDefault4 = nullable.GetValueOrDefault();
            d4 = num10 + valueOrDefault4;
            num6 = Math.Round(8.000000M, 2);
            Decimal num11 = d8;
            nullable = ((TaxTran) arTaxTran).TaxAmt;
            Decimal valueOrDefault5 = nullable.GetValueOrDefault();
            d8 = num11 + valueOrDefault5;
            d8 = Math.Round(d8, 2);
            d4 = Math.Round(d4, 2);
          }
        }
        else if (((TaxDetail) arTaxTran).TaxID == "04")
        {
          Decimal num12 = d3;
          nullable = ((TaxTran) arTaxTran).TaxableAmt;
          Decimal valueOrDefault6 = nullable.GetValueOrDefault();
          d3 = num12 + valueOrDefault6;
          num4 = Math.Round(0.000000M, 2);
          Decimal num13 = d7;
          nullable = ((TaxTran) arTaxTran).TaxAmt;
          Decimal valueOrDefault7 = nullable.GetValueOrDefault();
          d7 = num13 + valueOrDefault7;
          d7 = Math.Round(d7, 2);
          d3 = Math.Round(d3, 2);
        }
        else if (((TaxDetail) arTaxTran).TaxID == "09")
        {
          Decimal num14 = d1;
          nullable = ((TaxTran) arTaxTran).TaxableAmt;
          Decimal valueOrDefault8 = nullable.GetValueOrDefault();
          d1 = num14 + valueOrDefault8;
          num2 = Math.Round(0.000000M, 2);
          Decimal num15 = d5;
          nullable = ((TaxTran) arTaxTran).TaxAmt;
          Decimal valueOrDefault9 = nullable.GetValueOrDefault();
          d5 = num15 + valueOrDefault9;
          d5 = Math.Round(d5, 2);
          d1 = Math.Round(d1, 2);
        }
      }
      var array = GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).Where<ARTran>((Func<ARTran, bool>) (tran => tran.InventoryID.HasValue)).Select((tran, index) =>
      {
        ARTranExtEtims extension1 = ((PXSelectBase) this.Base.Transactions).Cache.GetExtension<ARTranExtEtims>((object) tran);
        Decimal num16 = Math.Round(tran.Qty.GetValueOrDefault(), 2);
        Math.Abs(tran.Qty.GetValueOrDefault());
        PXTrace.WriteInformation($"qty, {num16}, LineNbr: {tran.LineNbr}");
        Math.Abs(tran.TranAmt.GetValueOrDefault());
        UomMapping uomMapping = ((UomMapping)(PXSelectBase<UomMapping, PXSelect<UomMapping, Where<UomMapping.unitOfMeasure, Equal<Required<UomMapping.unitOfMeasure>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.UOM
        })));
        Decimal valueOrDefault = ((Decimal?) ((TaxCdes)(PXSelectBase<TaxCdes, PXSelect<TaxCdes, Where<TaxCdes.taxCategories, Equal<Required<InventoryItem.taxCategoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.TaxCategoryID
        })))?.TaxRate).GetValueOrDefault();
        Decimal num17 = valueOrDefault / 100M;
        Decimal num18 = 0M;
        Decimal num19 = 0M;
        Decimal num20 = 0M;
        Decimal num21 = 0M;
        Decimal num22 = 0M;
        Decimal num23 = 0M;
        Decimal num24 = 0M;
        if (tran.TaxCategoryID == "TAXABLE" || tran.TaxCategoryID == "TAXWHT5WVAT2" || tran.TaxCategoryID == "TAXWVAT2" || tran.TaxCategoryID == "VATWHT5")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault(), 2);
          num20 = Math.Round(num19 * num16, 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          num21 = Math.Round((num20 - num24) * num17 / (1M + num17), 2);
          num18 = Math.Round((num20 - num24) / (1M + num17), 2);
          num22 = Math.Round(num20 - num24, 2);
          PXTrace.WriteInformation($"INCLUSIVE tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} percentage rate={num17}, tax rate={valueOrDefault}");
        }
        else if (tran.TaxCategoryID == "16EXCL")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault() * (1M + num17), 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          num22 = Math.Round(num20 - num24, 2);
          num18 = Math.Round(num22 / (1M + num17), 2);
          num21 = Math.Round(num22 - num18, 2);
          PXTrace.WriteInformation($"EXCLUSIVE tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} percentage rate={num17}, tax rate={valueOrDefault}");
        }
        else if (tran.TaxCategoryID == "ZERO RATE" || tran.TaxCategoryID == "EXEMPT")
        {
          num19 = Math.Round(tran.UnitPrice.GetValueOrDefault(), 2);
          num20 = Math.Round(num19 * num16, 2);
          num23 = Math.Round(tran.DiscPct.GetValueOrDefault(), 2);
          num24 = num23 != 0M ? Math.Round(num20 * (num23 / 100M), 2) : 0M;
          Decimal num25 = Math.Round(tran.DiscAmt.GetValueOrDefault(), 2);
          num21 = Math.Round(0.000000M, 2);
          num22 = Math.Round(num20 - num24, 2);
          num18 = Math.Round(num20 - num24, 2);
          PXTrace.WriteInformation($"ZERO tax rate: prc={num19} splyAmt={num20} txbleAmt={num18} txAmt={num21} totAmt={num22} dcRt={num23} dcAmt={num24} dcAmt2={num25}");
        }
        string packagingUnit = uomMapping.PackagingUnit;
        string unitOfQuantity = uomMapping.UnitOfQuantity;
        PXTrace.WriteInformation($"pack and quantity codes: {packagingUnit}{unitOfQuantity}");
        InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) tran.InventoryID
        })));
        InventoryItemExtEtims extension2 = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
        if (extension2 == null)
          PXTrace.WriteInformation("INVENTORY EXTENSION IS EMPTY");
        if (string.IsNullOrEmpty(inventoryItem.InventoryCD) || string.IsNullOrEmpty(extension2.UsrItemClassificationCode) || string.IsNullOrEmpty(extension2.UsrTaxCodes))
          throw new PXException($"Please check if item classification code, tax code for item id {inventoryItem.InventoryCD.Trim()} has been configured");
        int num26 = -1;
        string str1;
        string str2;
        if (inventoryItem.StkItem.GetValueOrDefault())
        {
          INItemSite inItemSite1 = ((INItemSite)(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.siteID, Equal<Required<INItemSite.siteID>>, And<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.SiteID,
            (object) tran.InventoryID
          })));
          if (inItemSite1 == null)
            PXTrace.WriteInformation("item site is empty");
          INItemSiteExtEtims extension3 = PXCache<INItemSite>.GetExtension<INItemSiteExtEtims>(inItemSite1);
          int? usrIsaleId;
          int num27;
          if (extension3 == null)
          {
            num27 = 1;
          }
          else
          {
            usrIsaleId = extension3.UsrIsaleID;
            num27 = !usrIsaleId.HasValue ? 1 : 0;
          }
          if (num27 != 0)
          {
            INSite inSite = ((INSite)(PXSelectBase<INSite, PXSelect<INSite, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
            {
              (object) tran.SiteID
            })));
            if (inSite != null)
            {
              INItemSite inItemSite2 = ((INItemSite)(PXSelectBase<INItemSite, PXSelectJoin<INItemSite, InnerJoin<INSite, On<INItemSite.siteID, Equal<INSite.siteID>>>, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INSite.branchID, Equal<Required<INSite.branchID>>, And<INItemSiteExtEtims.usrIsaleID, IsNotNull>>>>.Config>.Select((PXGraph) this.Base, new object[2]
              {
                (object) tran.InventoryID,
                (object) inSite.BranchID
              })));
              if (inItemSite2 != null)
                extension3 = PXCache<INItemSite>.GetExtension<INItemSiteExtEtims>(inItemSite2);
            }
          }
          Location location = ((Location)(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) tran.CustomerID
          })));
          if (location == null)
            throw new PXException("Customer location record not found.");
          if (location.CTaxZoneID == "ZERORATE")
          {
            if (string.IsNullOrWhiteSpace(extension2.UsrItemClassificationCodeExpo?.ToString()))
              throw new PXException("Export Item Classification Code is required, make sure item is register on eTiMS.");
            str1 = !string.IsNullOrWhiteSpace(extension2.UsrTaxCodesExpo?.ToString()) ? extension2.UsrItemClassificationCodeExpo.ToString().Trim() : throw new PXException("Export Tax Code is required, make sure item is register on eTiMS.");
            str2 = extension2.UsrTaxCodesExpo.ToString().Trim();
          }
          else
          {
            if (string.IsNullOrWhiteSpace(extension2.UsrItemClassificationCode?.ToString()))
              throw new PXException("Local Item Classification Code is required, make sure item is register on eTiMS.");
            if (string.IsNullOrWhiteSpace(extension2.UsrTaxCodes?.ToString()))
              throw new PXException("Local Tax Code is required, make sure item is register on eTiMS.");
            usrIsaleId = extension1.UsrIsaleID;
            num26 = usrIsaleId ?? extension3.UsrIsaleID ?? -1;
            str1 = extension2.UsrItemClassificationCode.ToString().Trim();
            str2 = extension2.UsrTaxCodes.ToString().Trim();
          }
        }
        else
        {
          BranchEtims branchEtims = ((BranchEtims)(PXSelectBase<BranchEtims, PXSelect<BranchEtims, Where<BranchEtims.inventoryID, Equal<Required<BranchEtims.inventoryID>>, And<BranchEtims.branchID, Equal<Required<BranchEtims.branchID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.InventoryID,
            (object) tran.BranchID
          })));
          if (branchEtims == null)
            throw new PXException($"Branch {branch.BranchCD.Trim()} has not been added for non stock item {tran.TranDesc.Trim()}");
          num26 = extension1.UsrIsaleID ?? branchEtims.Isaleid ?? -1;
          str1 = extension2.UsrItemClassificationCode.ToString().Trim();
          str2 = extension2.UsrTaxCodes.ToString().Trim();
        }
        if (num26 == -1)
          throw new PXException($"Item {tran.InventoryID} does not have an iSaleID in branch.");
        var data = new
        {
          itemSeq = index + 1,
          itemID = num26,
          itemCd = inventoryItem.InventoryCD.Trim(),
          itemClsCd = str1,
          itemNm = tran.TranDesc ?? "",
          itemNmDef = tran.TranDesc ?? "",
          bcd = "",
          pkgUnitCd = packagingUnit.Trim(),
          pkg = num16,
          qtyUnitCd = unitOfQuantity.Trim(),
          qty = num16,
          prc = num19,
          splyAmt = num20,
          dcRt = num23,
          dcAmt = num24,
          taxTyCd = str2,
          taxblAmt = num18,
          taxAmt = num21,
          totAmt = num22
        };
        return data;
      }).ToArray();
      Dictionary<string, Decimal> dictionary1 = new Dictionary<string, Decimal>();
      Dictionary<string, Decimal> dictionary2 = new Dictionary<string, Decimal>();
      foreach (var source in array.GroupBy(item => item.taxTyCd))
      {
        Decimal num28 = source.Sum(item => item.taxblAmt);
        dictionary1[source.Key] = num28;
        PXTrace.WriteInformation($"Category: {source.Key}, Total vatTaxblAmt: {num28}");
      }
      foreach (var source in array.GroupBy(item => item.taxTyCd))
      {
        Decimal num29 = source.Sum(item => item.taxAmt);
        dictionary2[source.Key] = num29;
        PXTrace.WriteInformation($"Category: {source.Key}, Total TaxAmt: {num29}");
      }
      Dictionary<string, Decimal> dictionary3 = new Dictionary<string, Decimal>()
      {
        {
          "A",
          d1
        },
        {
          "B",
          d2
        },
        {
          "C1",
          d3
        },
        {
          "D",
          num1
        },
        {
          "E",
          d4
        }
      };
      Dictionary<string, Decimal> dictionary4 = new Dictionary<string, Decimal>()
      {
        {
          "A",
          d5
        },
        {
          "B",
          d6
        },
        {
          "C1",
          d7
        },
        {
          "D",
          num7
        },
        {
          "E",
          d8
        }
      };
      foreach (string key in dictionary3.Keys.ToList<string>())
      {
        Decimal num30;
        if (dictionary1.TryGetValue(key, out num30) && dictionary3[key] != num30)
          dictionary3[key] = num30;
      }
      foreach (string key in dictionary4.Keys.ToList<string>())
      {
        Decimal num31;
        if (dictionary2.TryGetValue(key, out num31) && dictionary4[key] != num31)
          dictionary4[key] = num31;
      }
      Decimal num32 = dictionary3["A"];
      Decimal num33 = dictionary3["B"];
      Decimal num34 = dictionary3["C1"];
      Decimal num35 = dictionary3["D"];
      Decimal num36 = dictionary3["E"];
      Decimal num37 = dictionary4["A"];
      Decimal num38 = dictionary4["B"];
      Decimal num39 = dictionary4["C1"];
      Decimal num40 = dictionary4["D"];
      Decimal num41 = dictionary4["E"];
      Decimal num42 = Math.Round(dictionary3.Values.Sum(), 2);
      Decimal num43 = Math.Round(dictionary4.Values.Sum(), 2);
      Decimal num44 = Math.Round(num42 + num43, 2);
      PXTrace.WriteInformation($"KRA TOTALS: Total Taxable Amount: {num42}, Total Tax Amount: {num43}, Total Amount: {num44}");
      Decimal valueOrDefault10 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.VatTaxableTotal.GetValueOrDefault();
      Decimal? nullable1 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.VatExemptTotal;
      Decimal valueOrDefault11 = nullable1.GetValueOrDefault();
      Decimal num45 = Math.Round(valueOrDefault10 + valueOrDefault11, 2);
      ARInvoice current3 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      Decimal? nullable2;
      if (current3 == null)
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = current3.TaxTotal;
      nullable1 = nullable2;
      Decimal num46 = Math.Round(nullable1.GetValueOrDefault(), 2);
      ARInvoice current4 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      Decimal? nullable3;
      if (current4 == null)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = ((ARRegister) current4).OrigDocAmt;
      nullable1 = nullable3;
      Decimal num47 = Math.Round(nullable1.GetValueOrDefault(), 2);
      PXTrace.WriteInformation($"ACUMATICA TOTALS: Total Taxable Amount: {num45}, Total Tax Amount: {num46}, Total Amount: {num47}");
      Decimal num48 = num45 == num42 ? num45 : num42;
      Decimal num49 = num46 == num43 ? num46 : num43;
      Decimal num50 = num47 == num44 ? num47 : num44;
      GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).SelectMany(tran =>
      {
        PXResultset<INSite> pxResultset = PXSelectBase<INSite, PXSelect<INSite>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
        return pxResultset.Count == 0 ? null : GraphHelper.RowCast<INSite>((IEnumerable) pxResultset).Select(site =>
        {
          INSiteStatus inSiteStatus = ((INSiteStatus)(PXSelectBase<INSiteStatus, PXSelect<INSiteStatus, Where<INSiteStatus.inventoryID, Equal<Required<INSiteStatus.inventoryID>>, And<INSiteStatus.siteID, Equal<Required<INSiteStatus.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) tran.InventoryID,
            (object) site.SiteID
          })));
          InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) tran.InventoryID
          })));
          if (inventoryItem == null)
          {
            PXTrace.WriteInformation($"Inventory item '{tran.InventoryID}' does not exist.");
            return null;
          }
          if (inSiteStatus == null)
          {
            PXTrace.WriteInformation($"Inventory item '{inventoryItem.InventoryCD}' does not exist in warehouse '{site.SiteCD}'.");
            return null;
          }
          PXTrace.WriteInformation($"Inventory '{inventoryItem.InventoryCD}' has {inSiteStatus.QtyOnHand} in warehouse '{site.SiteCD}'.");
          var data2 = new
          {
            itemCd = inventoryItem.InventoryCD.Trim(),
            qtyOnHand = inSiteStatus.QtyOnHand.GetValueOrDefault()
          };
          return data2;
        }).Where(result => result != null).GroupBy(item => item.itemCd).Select(group => new
        {
          itemCd = group.Key,
          rsdQty = group.Sum(x => x.qtyOnHand)
        });
      }).ToArray();
      PXTrace.WriteInformation("fetch stock item list");
      ARRegisterExtEtims extension4 = PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) current1);
      int num51 = 0;
      if (((ARRegister) current1)?.DocType == "CRM")
      {
        if (!extension4.UsrOriginalReceiptNbr.HasValue || extension4.UsrOriginalReceiptNbr.Value == 0)
          throw new PXException("Etims Original Receipt Number is required for CRM documents and cannot be zero.");
        num51 = extension4.UsrOriginalReceiptNbr.Value;
      }
      string str3 = ((ARRegister) current1)?.DocType == "CRM" ? "Credit" : "Sale";
      string salesControlUnitId = ((ARRegister) current1)?.DocType == "CRM" ? deviceInfo.SalesControlUnitID : (string) null;
      string usrZraCreditReason = ((ARRegister) current1)?.DocType == "CRM" ? extension4.UsrZRACreditReason : (string) null;
      string usrZraDebitReason = ((ARRegister) current1)?.DocType == "DRM" ? extension4.UsrZRADebitReason : (string) null;
      string usrLpo = extension4.UsrLpo != null ? extension4.UsrLpo : (string) null;
      string usrZraReceiptCode = extension4.UsrZRAReceiptCode != null ? extension4.UsrZRAReceiptCode : (string) null;
      string str4 = ((ARRegister) current1)?.DocType == "INV" || ((ARRegister) current1)?.DocType == "DRM" || ((ARRegister) current1)?.DocType == "CSL" ? "S" : "R";
      string str5 = "01";
      string usrStockInOutType = extension4.UsrStockInOutType;
      string str6 = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr + "-RSGN";
      BAccount baccount = ((BAccount)(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>, And<BAccount.bAccountID, NotEqual<Required<BAccount.bAccountID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) ((ARRegister) current1).CustomerID,
        (object) 2
      })));
      Location location1 = ((Location)(PXSelectBase<Location, PXSelect<Location, Where<Location.locationID, Equal<Required<Location.locationID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefLocationID
      })));
      PXTrace.WriteInformation($"baccount id, {baccount.BAccountID}");
      string acctName = baccount.AcctName;
      PXTrace.WriteInformation("customerName, " + acctName);
      string str7 = location1.TaxRegistrationID;
      if (str7 == null)
      {
        PXTrace.WriteInformation("Tpin for the selected customer is no set, if the customer is a cash sales please insert customer tpin");
        if (extension4.UsrCustomrTin != null)
        {
          str7 = extension4.UsrCustomrTin.ToString().Trim();
          PXTrace.WriteInformation("customer tpin for the cash sales customer is, " + str7);
        }
      }
      ARInvoice current5 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      Decimal? nullable4;
      if (current5 == null)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = current5.LineDiscTotal;
      nullable1 = nullable4;
      Decimal num52 = Math.Round(nullable1.GetValueOrDefault(), 2);
      ARInvoice current6 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      Decimal? nullable5;
      if (current6 == null)
      {
        nullable1 = new Decimal?();
        nullable5 = nullable1;
      }
      else
        nullable5 = current6.DetailExtPriceTotal;
      nullable1 = nullable5;
      Decimal num53 = Math.Round(nullable1.GetValueOrDefault(), 2);
      Decimal num54 = num52 != 0M ? num52 * 100M / num53 : 0M;
      PXTrace.WriteInformation($"cash disc, {num52} cash rate {num54} cury Total {num48}");
      RequestData requestData1 = new RequestData();
      requestData1.tin = deviceInitialise.KraPin;
      requestData1.bhfId = deviceInitialise.KraBranchID;
      requestData1.trdInvcNo = str6;
      requestData1.orgInvcNo = num51;
      requestData1.invcNo = 0;
      requestData1.custTin = str7 ?? " ";
      requestData1.custNm = acctName;
      requestData1.custType = "NEW";
      requestData1.custMblNo = "";
      requestData1.custEmail = "";
      requestData1.custID = baccount.AcctCD.Trim();
      requestData1.salesTyCd = "N";
      requestData1.rcptTyCd = str4;
      requestData1.pmtTyCd = str5;
      requestData1.salesSttsCd = "02";
      ARInvoice current7 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      DateTime? docDate;
      DateTime dateTime;
      string str8;
      if (current7 == null)
      {
        str8 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current7).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str8 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str8 = dateTime.ToString("yyyyMMddHHmmss");
        }
      }
      requestData1.cfmDt = str8;
      ARInvoice current8 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      string str9;
      if (current8 == null)
      {
        str9 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current8).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str9 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str9 = dateTime.ToString("yyyyMMdd");
        }
      }
      requestData1.salesDt = str9;
      requestData1.totItemCnt = array.Length;
      ARInvoice current9 = ((PXSelectBase<ARInvoice>) this.Base.Document).Current;
      string str10;
      if (current9 == null)
      {
        str10 = (string) null;
      }
      else
      {
        docDate = ((ARRegister) current9).DocDate;
        ref DateTime? local = ref docDate;
        if (!local.HasValue)
        {
          str10 = (string) null;
        }
        else
        {
          dateTime = local.GetValueOrDefault();
          str10 = dateTime.ToString("yyyyMMddHHmmss");
        }
      }
      requestData1.stockRlsDt = str10;
      requestData1.cnclReqDt = (string) null;
      requestData1.cnclDt = (string) null;
      requestData1.rfdDt = (string) null;
      requestData1.rfdRsnCd = usrZraCreditReason;
      requestData1.dbtRsnCd = usrZraDebitReason;
      requestData1.taxblAmtA = num32;
      requestData1.taxblAmtB = num33;
      requestData1.taxblAmtC = num34;
      requestData1.taxblAmtD = num35;
      requestData1.taxblAmtE = num36;
      requestData1.taxRtA = num2;
      requestData1.taxRtB = num3;
      requestData1.taxRtC = num4;
      requestData1.taxRtD = num5;
      requestData1.taxRtE = num6;
      requestData1.taxAmtA = num37;
      requestData1.taxAmtB = num38;
      requestData1.taxAmtC = num39;
      requestData1.taxAmtD = num40;
      requestData1.taxAmtE = num41;
      requestData1.totTaxblAmt = num48;
      requestData1.totTaxAmt = num49;
      requestData1.totAmt = num50;
      requestData1.prchrAcptcYn = "N";
      requestData1.remark = str3;
      requestData1.regrId = "ADMIN";
      requestData1.regrNm = "ADMIN";
      requestData1.modrId = "ADMIN";
      requestData1.modrNm = "ADMIN";
      ReceiptEtims receiptEtims = new ReceiptEtims();
      receiptEtims.custTin = str7 ?? (string) null;
      dateTime = DateTime.Now;
      receiptEtims.rcptPbctDt = dateTime.ToString("yyyyMMddHHmmss");
      receiptEtims.prchrAcptcYn = "N";
      receiptEtims.rptNo = 0;
      requestData1.receipt = receiptEtims;
      requestData1.itemList = array.Select(item => new Item()
      {
        itemSeq = item.itemSeq,
        itemId = item.itemID,
        itemCd = item.itemCd,
        itemClsCd = item.itemClsCd,
        itemNm = item.itemNm,
        itemNmDef = item.itemNm,
        pkgUnitCd = item.pkgUnitCd,
        pkg = item.pkg,
        qtyUnitCd = item.qtyUnitCd,
        qty = item.qty,
        prc = item.prc,
        splyAmt = item.splyAmt,
        dcRt = item.dcRt,
        dcAmt = item.dcAmt,
        taxTyCd = item.taxTyCd,
        taxblAmt = item.taxblAmt,
        taxAmt = item.taxAmt,
        totAmt = item.totAmt
      }).ToList<Item>();
      RequestData requestData2 = requestData1;
      string content = JsonConvert.SerializeObject((object) requestData2);
      PXTrace.WriteInformation("ZRA Validation Request Details: " + content);
      string requestUri;
      if (requestData2.rcptTyCd == "S")
      {
        requestUri = deviceUrls.SalesUrl.Trim();
        PXTrace.WriteInformation("ETIMS VALIDATION - URL: " + requestUri);
      }
      else
      {
        requestUri = deviceUrls.UpdateImportUrl.Trim();
        PXTrace.WriteInformation("ETIMS VALIDATION - URL: " + requestUri);
      }
      string str11 = string.Empty;
      using (HttpClient httpClient = new HttpClient())
      {
        try
        {
          string parameter = deviceToken.AccessToken.Trim();
          PXTrace.WriteInformation("Access Token: " + parameter);
          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
          HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
          if (result1.IsSuccessStatusCode)
          {
            str11 = result1.Content.ReadAsStringAsync().Result;
            PXTrace.WriteInformation("Response from eTIMS API: " + str11);
            ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(str11);
            if (responseData.status)
            {
              ARRegisterExtEtims extension5 = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.vsdcRcptPbctDate);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.intrlData);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.rcptSign);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.long_url);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension5.UsrCUNumber}/{responseData.sale.invcNo}");
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.sale.invcNo);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension5.UsrCUNumber}/{responseData.sale.invcNo}");
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.kraResult.result.long_url);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
              ((PXSelectBase<ARInvoice>) this.Base.Document).Update(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXGraph) this.Base).Persist();
              ZraLogs zraLogs = new ZraLogs()
              {
                BranchId = deviceInitialise?.KraBranchID,
                DocumentType = "Sales",
                DocumentNbr = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr,
                RequestLogOne = content,
                ResponseLog = str11 == null || str11.Length <= 2000 ? str11 : str11.Substring(0, 2000)
              };
              GraphHelper.Caches<ZraLogs>((PXGraph) this.Base).Insert(zraLogs);
              ((PXGraph) this.Base).Persist(typeof (ZraLogs), (PXDBOperation) 2);
            }
            else
            {
              ARRegisterExtEtims extension6 = ((PXSelectBase) this.Base.Document).Cache.GetExtension<ARRegisterExtEtims>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
              if (string.IsNullOrEmpty(extension4.UsrQRCodee) && string.IsNullOrEmpty(extension4.UsrInvoiceNumber))
              {
                if (((ARRegister) current1)?.DocType == "INV")
                {
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.cfmDt);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.intrlData);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.rcptSign);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.existingSale.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.invcNo);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.existingSale.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.existingSale.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                }
                else if (((ARRegister) current1)?.DocType == "CRM")
                {
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrTvDate>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.cfmDt);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrRsltMessage>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.message);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInternalData>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.intrlData);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrReceiptSignature>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.rcptSign);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrQRCodee>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrCUNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrInvoiceNumber>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.creditNote.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrIsEtimsValidated>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARRegisterExtEtims.usrOriginal>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.invcNo);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUInvoiceNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) $"{extension6.UsrCUNumber}/{responseData.creditNote.invcNo}");
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsQrCode>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) responseData.creditNote.long_url);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrTimsUploadFlag>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) true);
                  ((PXSelectBase) this.Base.Document).Cache.SetValueExt<ARInvoiceExtens.usrCUSerialNo>((object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current, (object) deviceInfo.SalesControlUnitID);
                }
              }
              ((PXSelectBase<ARInvoice>) this.Base.Document).Update(((PXSelectBase<ARInvoice>) this.Base.Document).Current);
              ((PXGraph) this.Base).Persist();
              throw new Exception("Failed with result message: " + responseData.message);
            }
          }
          else
          {
            string result2 = result1.Content.ReadAsStringAsync().Result;
            PXTrace.WriteInformation($" {result1.StatusCode}, Response: {result2}");
            throw new Exception($"Failed with status code: {result1.StatusCode}");
          }
        }
        catch (Exception ex)
        {
          ZraLogs zraLogs = new ZraLogs()
          {
            BranchId = deviceInitialise?.KraBranchID,
            DocumentType = "Sales",
            DocumentNbr = ((ARRegister) ((PXSelectBase<ARInvoice>) this.Base.Document).Current)?.RefNbr,
            RequestLogOne = content,
            ResponseLog = str11 == null || str11.Length <= 2000 ? str11 : str11.Substring(0, 2000)
          };
          GraphHelper.Caches<ZraLogs>((PXGraph) this.Base).Insert(zraLogs);
          ((PXGraph) this.Base).Persist(typeof (ZraLogs), (PXDBOperation) 2);
          PXTrace.WriteError(ex);
          PXTrace.WriteError("Error during transmission: " + ex.Message);
          throw;
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("ETIMS Validation Exception: " + ex.Message);
      throw;
    }
  }
}
