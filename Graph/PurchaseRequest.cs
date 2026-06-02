// Decompiled with JetBrains decompiler
// Type: eTims.PurchaseRequest
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class PurchaseRequest : PXGraph<PurchaseRequest>
{
  public PXSave<RequestPurchases> Save;
  public PXCancel<RequestPurchases> Cancel;
  public PXSelect<RequestPurchases> RequestPurchaseView;
  public PXAction<RequestPurchases> RequestPurchase;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Request Purchase")]
  protected void requestPurchase()
  {
    RequestPurchases current = ((PXSelectBase<RequestPurchases>) this.RequestPurchaseView).Current;
    DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) (PXAccess.GetBranchID() ?? throw new PXException("Branch not Found"))
      }))) ?? throw new PXException("Current branch ID is not available.")).BranchCD
    })));
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInfo.BranchOfficeID}, {deviceInfo.KraPin}");
    if (current == null)
      return;
    string kraPin = deviceInfo.KraPin;
    string str1 = current.BranchId.Trim();
    DateTime? latestResquest = current.LatestResquest;
    ref DateTime? local = ref latestResquest;
    string str2 = (local.HasValue ? local.GetValueOrDefault().ToString("yyyyMMddHHmmss") : (string) null) ?? DateTime.Now.ToString("yyyyMMddHHmmss");
    var data = new
    {
      tpin = kraPin,
      bhfId = str1,
      lastReqDt = str2
    };
    string content = JsonConvert.SerializeObject((object) data);
    PXTrace.WriteInformation("requestData " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = current.RequestUrl.ToString();
        PXTrace.WriteInformation(requestUri ?? "");
        HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result1.EnsureSuccessStatusCode();
        string result2 = result1.Content.ReadAsStringAsync().Result;
        SaleResponse saleResponse = JsonConvert.DeserializeObject<SaleResponse>(result2);
        PXTrace.WriteInformation($"responseData {saleResponse}");
        PXTrace.WriteInformation("response data result code; " + saleResponse.resultCd);
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInfo.BranchOfficeID,
          DocumentType = "Get Purchases",
          RequestLogOne = result2,
          ResponseLog = content
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this).Insert(zraLogs);
        ((PXGraph) this).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        if (saleResponse.resultCd == "000")
        {
          int count = saleResponse.data.saleList != null ? saleResponse.data.saleList.Count : 0;
          PXTrace.WriteInformation($"responseData data item classification list counts, (Count: {count})");
          current.PurchasesFetched = new int?(count);
          current.ResultMessage = saleResponse.resultMsg;
          PXTrace.WriteInformation("result message: " + saleResponse.resultMsg);
          ((PXSelectBase<RequestPurchases>) this.RequestPurchaseView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        else
        {
          current.ResultMessage = saleResponse.resultMsg;
          PXTrace.WriteInformation("result message: " + saleResponse.resultMsg);
          PXTrace.WriteError("Failed with result code: " + saleResponse.resultCd);
          ((PXSelectBase<RequestPurchases>) this.RequestPurchaseView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        if (saleResponse != null && saleResponse.data != null && saleResponse.data.saleList != null)
        {
          PXTrace.WriteInformation($"responseData data item classification list counts, (Count: {saleResponse.data.saleList.Count})");
          Dictionary<int?, PurchaseZRA> dictionary1 = new Dictionary<int?, PurchaseZRA>();
          Dictionary<int?, ZRAPurchaseItem> dictionary2 = new Dictionary<int?, ZRAPurchaseItem>();
          ZRAPurchaseMaint instance = PXGraph.CreateInstance<ZRAPurchaseMaint>();
          foreach (PXResult<PurchaseZRA> pxResult in ((PXSelectBase<PurchaseZRA>) instance.PurchaseZRAView).Select(Array.Empty<object>()))
          {
            PurchaseZRA purchaseZra = ((PurchaseZRA)(pxResult));
            dictionary1[purchaseZra.SpplrInvcNo] = purchaseZra;
          }
          foreach (PXResult<ZRAPurchaseItem> pxResult in ((PXSelectBase<ZRAPurchaseItem>) instance.ZRAPurchaseItemView).Select(Array.Empty<object>()))
          {
            ZRAPurchaseItem zraPurchaseItem = ((ZRAPurchaseItem)(pxResult));
            dictionary2[zraPurchaseItem.ItemSeq] = zraPurchaseItem;
          }
          foreach (Sale sale in saleResponse.data.saleList)
          {
            DateTime? nullable1 = new DateTime?();
            if (!string.IsNullOrEmpty(sale.cfmDt))
            {
              DateTime result3;
              if (DateTime.TryParseExact(sale.cfmDt, "yyyy-MM-dd HH:mm:ss", (IFormatProvider) null, DateTimeStyles.None, out result3))
                nullable1 = new DateTime?(result3);
              else
                PXTrace.WriteError("Failed to parse CfmDt: " + sale.cfmDt);
            }
            DateTime? nullable2 = new DateTime?();
            if (!string.IsNullOrEmpty(sale.salesDt))
            {
              DateTime result4;
              if (DateTime.TryParseExact(sale.salesDt, "yyyyMMdd", (IFormatProvider) null, DateTimeStyles.None, out result4))
                nullable2 = new DateTime?(result4);
              else
                PXTrace.WriteError("Failed to parse SalesDt: " + sale.salesDt);
            }
            DateTime? nullable3 = new DateTime?();
            if (!string.IsNullOrEmpty(sale.stockRlsDt))
            {
              DateTime result5;
              if (DateTime.TryParseExact(sale.stockRlsDt, "yyyy-MM-dd HH:mm:ss", (IFormatProvider) null, DateTimeStyles.None, out result5))
                nullable3 = new DateTime?(result5);
              else
                PXTrace.WriteError("Failed to parse StockRlsDt: " + sale.stockRlsDt);
            }
            PurchaseZRA purchaseZra = new PurchaseZRA()
            {
              SpplrTpin = sale.spplrTpin,
              SpplrBhfId = sale.spplrBhfId,
              SpplrInvcNo = new int?(sale.spplrInvcNo),
              RcptTyCd = sale.rcptTyCd,
              PmtTyCd = sale.pmtTyCd,
              CfmDt = nullable1,
              SalesDt = nullable2,
              StockRlsDt = nullable3,
              TotItemCnt = new int?(sale.totItemCnt),
              TotTaxblAmt = new Decimal?(sale.totTaxblAmt),
              TotTaxAmt = new Decimal?(sale.totTaxAmt),
              TotAmt = new Decimal?(sale.totAmt),
              Remark = sale.remark
            };
            Dictionary<int?, PurchaseZRA> dictionary3 = dictionary1;
            int? nullable4 = purchaseZra.SpplrInvcNo;
            int? key1 = new int?(nullable4.GetValueOrDefault());
            if (!dictionary3.ContainsKey(key1))
            {
              ((PXSelectBase<PurchaseZRA>) instance.PurchaseZRAView).Insert(purchaseZra);
              PXTrace.WriteInformation($"Inserted PurchaseZRA: {purchaseZra.SpplrInvcNo}");
            }
            else
              PXTrace.WriteInformation($"PurchaseZRA with SpplrInvcNo {purchaseZra.SpplrInvcNo} already exists.");
            foreach (SaleItem saleItem in sale.itemList)
            {
              ZRAPurchaseItem zraPurchaseItem = new ZRAPurchaseItem()
              {
                SpplrInvcNo = new int?(sale.spplrInvcNo),
                ItemSeq = new int?(saleItem.itemSeq),
                ItemCd = saleItem.itemCd,
                ItemClsCd = saleItem.itemClsCd,
                ItemNm = saleItem.itemNm,
                PkgUnitCd = saleItem.pkgUnitCd,
                Pkg = new Decimal?((Decimal) (int) saleItem.pkg),
                QtyUnitCd = saleItem.qtyUnitCd,
                Qty = new Decimal?((Decimal) (int) saleItem.qty),
                Prc = new Decimal?(saleItem.prc),
                SplyAmt = new Decimal?(saleItem.splyAmt),
                Dcrt = new Decimal?(saleItem.dcRt),
                DcAmt = new Decimal?(saleItem.dcAmt),
                VatCatCd = saleItem.vatCatCd,
                VatTaxblAmt = new Decimal?(saleItem.vatTaxblAmt),
                ExciseTaxblAmt = new Decimal?(saleItem.exciseTaxblAmt),
                IplTaxblAmt = new Decimal?(saleItem.iplTaxblAmt),
                TlTaxblAmt = new Decimal?(saleItem.tlTaxblAmt),
                TaxblAmt = new Decimal?(saleItem.taxblAmt),
                VatAmt = new Decimal?(saleItem.vatAmt),
                IplAmt = new Decimal?(saleItem.iplAmt),
                TlAmt = new Decimal?(saleItem.tlAmt),
                ExciseTxAmt = new Decimal?(saleItem.exciseTxAmt),
                TotAmt = new Decimal?(saleItem.totAmt)
              };
              Dictionary<int?, ZRAPurchaseItem> dictionary4 = dictionary2;
              nullable4 = zraPurchaseItem.ItemSeq;
              int? key2 = new int?(nullable4.GetValueOrDefault());
              if (!dictionary4.ContainsKey(key2))
                ((PXSelectBase<ZRAPurchaseItem>) instance.ZRAPurchaseItemView).Insert(zraPurchaseItem);
              else
                PXTrace.WriteInformation($"Purchase item name {zraPurchaseItem.ItemNm} already exists.");
            }
          }
          ((PXSelectBase) instance.PurchaseZRAView).Cache.Persist((PXDBOperation) 2);
          ((PXSelectBase) instance.ZRAPurchaseItemView).Cache.Persist((PXDBOperation) 2);
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to ZRA Smart Invoice: " + ex.Message);
    }
  }
}
