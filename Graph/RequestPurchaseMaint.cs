// Decompiled with JetBrains decompiler
// Type: eTims.RequestPurchaseMaint
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

public class RequestPurchaseMaint : PXGraph<RequestPurchaseMaint>
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
    string tpin = current.Tpin;
    string str1 = current.BranchId.Trim();
    DateTime? latestResquest = current.LatestResquest;
    ref DateTime? local = ref latestResquest;
    string str2 = (local.HasValue ? local.GetValueOrDefault().ToString("yyyyMMddHHmmss") : (string) null) ?? DateTime.Now.ToString("yyyyMMddHHmmss");
    var data = new
    {
      tpin = tpin,
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
        SaleResponse saleResponse = JsonConvert.DeserializeObject<SaleResponse>(result1.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {saleResponse}");
        PXTrace.WriteInformation("response data result code; " + saleResponse.resultCd);
        if (saleResponse.resultCd == "000")
        {
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
            dictionary2[zraPurchaseItem.SpplrInvcNo] = zraPurchaseItem;
          }
          foreach (Sale sale in saleResponse.data.saleList)
          {
            DateTime? nullable1 = new DateTime?();
            DateTime result2;
            if (!string.IsNullOrEmpty(sale.cfmDt) && DateTime.TryParseExact(sale.cfmDt, "yyyy-MM-dd HH:mm:ss", (IFormatProvider) null, DateTimeStyles.None, out result2))
              nullable1 = new DateTime?(result2);
            DateTime? nullable2 = new DateTime?();
            DateTime result3;
            if (!string.IsNullOrEmpty(sale.salesDt) && DateTime.TryParseExact(sale.salesDt, "yyyyMMdd", (IFormatProvider) null, DateTimeStyles.None, out result3))
              nullable2 = new DateTime?(result3);
            DateTime? nullable3 = new DateTime?();
            DateTime result4;
            if (!string.IsNullOrEmpty(sale.stockRlsDt) && DateTime.TryParseExact(sale.stockRlsDt, "yyyy-MM-dd HH:mm:ss", (IFormatProvider) null, DateTimeStyles.None, out result4))
              nullable3 = new DateTime?(result4);
            PurchaseZRA purchaseZra = new PurchaseZRA()
            {
              SpplrTpin = sale.spplrTpin,
              SpplrBhfId = sale.spplrBhfId,
              SpplrInvcNo = new int?(sale.spplrInvcNo),
              RcptTyCd = sale.rcptTyCd,
              PmtTyCd = sale.pmtTyCd,
              CfmDt = new DateTime?(DateTime.Parse(sale.cfmDt)),
              SalesDt = new DateTime?(DateTime.Parse(sale.salesDt)),
              StockRlsDt = sale.stockRlsDt != null ? new DateTime?(DateTime.Parse(sale.stockRlsDt)) : new DateTime?(),
              TotItemCnt = new int?(sale.totItemCnt),
              TotTaxblAmt = new Decimal?(sale.totTaxblAmt),
              TotTaxAmt = new Decimal?(sale.totTaxAmt),
              TotAmt = new Decimal?(sale.totAmt),
              Remark = sale.remark
            };
            if (!dictionary1.ContainsKey(new int?(purchaseZra.SpplrInvcNo.GetValueOrDefault())))
              ((PXSelectBase<PurchaseZRA>) instance.PurchaseZRAView).Insert(purchaseZra);
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
              if (!dictionary2.ContainsKey(new int?(zraPurchaseItem.SpplrInvcNo.GetValueOrDefault())))
                ((PXSelectBase<ZRAPurchaseItem>) instance.ZRAPurchaseItemView).Insert(zraPurchaseItem);
            }
          }
          ((PXGraph) this).Actions.PressSave();
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to ZRA Smart Invoice: " + ex.Message);
      throw new PXException("There is no search result");
    }
  }
}
