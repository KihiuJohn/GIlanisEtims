// Decompiled with JetBrains decompiler
// Type: eTims.ZRAPurchaseMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class ZRAPurchaseMaint : PXGraph<ZRAPurchaseMaint, PurchaseZRA>
{
  public PXSelect<PurchaseZRA> PurchaseZRAView;
  public PXSelect<ZRAPurchaseItem, Where<ZRAPurchaseItem.spplrInvcNo, Equal<Current<PurchaseZRA.spplrInvcNo>>>> ZRAPurchaseItemView;
  public PXAction<PurchaseZRA> Accept;
  public PXAction<PurchaseZRA> Reject;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Accept")]
  protected void accept()
  {
    PurchaseZRA current1 = ((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Current;
    ZRAPurchaseItem current2 = ((PXSelectBase<ZRAPurchaseItem>) this.ZRAPurchaseItemView).Current;
    if (current1 == null)
      return;
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    POReceipt poReceipt = new POReceipt()
    {
      ReceiptType = "RT",
      ReceiptNbr = "<NEW>",
      VendorID = current1.VendorName,
      InvoiceNbr = current1.SpplrInvcNo.ToString()
    };
    ((PXSelectBase<POReceipt>) instance.Document).Insert(poReceipt);
    foreach (PXResult<ZRAPurchaseItem> pxResult in ((PXSelectBase<ZRAPurchaseItem>) this.ZRAPurchaseItemView).Select(Array.Empty<object>()))
    {
      ZRAPurchaseItem zraPurchaseItem = ((ZRAPurchaseItem)(pxResult));
      POReceiptLine poReceiptLine = new POReceiptLine()
      {
        InventoryID = zraPurchaseItem.InventoryID,
        Qty = zraPurchaseItem.Qty
      };
      ((PXSelectBase<POReceiptLine>) instance.transactions).Insert(poReceiptLine);
    }
    ((PXGraph) instance).Actions.PressSave();
    string receiptNbr = ((PXSelectBase<POReceipt>) instance.Document).Current?.ReceiptNbr;
    if (!string.IsNullOrEmpty(receiptNbr))
    {
      current1.POReceipt = receiptNbr;
      ((PXSelectBase) this.ZRAPurchaseItemView).Cache.Update((object) current1);
      ((PXGraph) this).Actions.PressSave();
    }
    current1.Status = "A";
    ((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Update(current1);
    ((PXGraph) this).Actions.PressSave();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Reject")]
  protected void reject()
  {
    PurchaseZRA current = ((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Current;
    if (current == null)
      return;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Current branch ID is not available.");
    PXTrace.WriteInformation($"current branch :{branchId}");
    Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    })));
    if (branch == null)
      throw new PXException("Branch not found");
    PXTrace.WriteInformation("branch :" + branch.BranchCD);
    DeviceInitialise deviceInitialise = ((DeviceInitialise)(PXSelectBase<DeviceInitialise, PXSelect<DeviceInitialise, Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    })));
    if (deviceInitialise == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInitialise.BranchID}, {deviceInitialise.KraPin}, {deviceInitialise.KraBranchID}");
    DeviceInfo deviceInfo = ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    })));
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    })));
    if (deviceInfo == null)
      return;
    List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
    int num = 1;
    foreach (PXResult<ZRAPurchaseItem> pxResult in ((PXSelectBase<ZRAPurchaseItem>) this.ZRAPurchaseItemView).Select(Array.Empty<object>()))
    {
      ZRAPurchaseItem zraPurchaseItem = ((ZRAPurchaseItem)(pxResult));
      Dictionary<string, object> dictionary = new Dictionary<string, object>()
      {
        {
          "itemSeq",
          (object) zraPurchaseItem.ItemSeq
        },
        {
          "itemCd",
          (object) zraPurchaseItem.ItemCd.Trim()
        },
        {
          "itemClsCd",
          (object) zraPurchaseItem.ItemClsCd?.Trim()
        },
        {
          "itemNm",
          (object) zraPurchaseItem.ItemNm.Trim()
        },
        {
          "bcd",
          (object) null
        },
        {
          "pkgUnitCd",
          (object) zraPurchaseItem.PkgUnitCd
        },
        {
          "pkg",
          (object) zraPurchaseItem.Pkg
        },
        {
          "qtyUnitCd",
          (object) zraPurchaseItem.QtyUnitCd
        },
        {
          "qty",
          (object) zraPurchaseItem.Qty
        },
        {
          "prc",
          (object) zraPurchaseItem.Prc
        },
        {
          "splyAmt",
          (object) zraPurchaseItem.SplyAmt
        },
        {
          "dcRt",
          (object) zraPurchaseItem.Dcrt
        },
        {
          "dcAmt",
          (object) zraPurchaseItem.DcAmt
        },
        {
          "iplCatCd",
          (object) null
        },
        {
          "tlCatCd",
          (object) null
        },
        {
          "exciseCatCd",
          (object) null
        },
        {
          "taxblAmt",
          (object) zraPurchaseItem.TaxblAmt
        },
        {
          "vatCatCd",
          (object) zraPurchaseItem.VatCatCd
        },
        {
          "iplTaxblAmt",
          (object) null
        },
        {
          "tlTaxblAmt",
          (object) null
        },
        {
          "exciseTaxblAmt",
          (object) null
        },
        {
          "taxAmt",
          (object) zraPurchaseItem.VatAmt
        },
        {
          "iplAmt",
          (object) null
        },
        {
          "tlAmt",
          (object) null
        },
        {
          "exciseTxAmt",
          (object) null
        },
        {
          "totAmt",
          (object) zraPurchaseItem.TotAmt
        }
      };
      dictionaryList.Add(dictionary);
      ++num;
    }
    Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
    dictionary1.Add("tpin", (object) deviceInitialise.KraPin);
    dictionary1.Add("bhfId", (object) deviceInitialise.KraBranchID);
    dictionary1.Add("cisInvcNo", (object) current.SpplrInvcNo);
    dictionary1.Add("orgInvcNo", (object) 0);
    dictionary1.Add("spplrTpin", (object) current.SpplrTpin);
    dictionary1.Add("spplrBhfId", (object) current.SpplrBhfId);
    dictionary1.Add("spplrNm", (object) null);
    dictionary1.Add("spplrInvcNo", (object) current.SpplrInvcNo);
    dictionary1.Add("regTyCd", (object) "M");
    dictionary1.Add("pchsTyCd", (object) "N");
    dictionary1.Add("rcptTyCd", (object) current.ReceiptType);
    dictionary1.Add("pmtTyCd", (object) current.PmtTyCd);
    dictionary1.Add("pchsSttsCd", (object) current.TransactionType);
    DateTime? cfmDt = current.CfmDt;
    ref DateTime? local1 = ref cfmDt;
    dictionary1.Add("cfmDt", (object) (local1.HasValue ? local1.GetValueOrDefault().ToString("yyyyMMddHHmmss") : (string) null));
    cfmDt = current.CfmDt;
    ref DateTime? local2 = ref cfmDt;
    dictionary1.Add("pchsDt", (object) (local2.HasValue ? local2.GetValueOrDefault().ToString("yyyyMMdd") : (string) null));
    dictionary1.Add("cnclReqDt", (object) null);
    dictionary1.Add("cnclDt", (object) null);
    dictionary1.Add("totItemCnt", (object) dictionaryList.Count);
    dictionary1.Add("totTaxblAmt", (object) current.TotTaxblAmt);
    dictionary1.Add("totTaxAmt", (object) current.TotTaxAmt);
    dictionary1.Add("totAmt", (object) current.TotAmt);
    dictionary1.Add("remark", (object) current.Remarks);
    dictionary1.Add("regrNm", (object) "ADMIN");
    dictionary1.Add("regrId", (object) "ADMIN");
    dictionary1.Add("modrNm", (object) "ADMIN");
    dictionary1.Add("modrId", (object) "ADMIN");
    dictionary1.Add("itemList", (object) dictionaryList);
    string content = JsonConvert.SerializeObject((object) dictionary1);
    PXTrace.WriteInformation("ZRA Validation Request Details: " + content);
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    using (HttpClient httpClient = new HttpClient())
    {
      try
      {
        string requestUri = deviceUrls.PurchaseUrl.Trim();
        PXTrace.WriteInformation("url-" + requestUri);
        HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result1.EnsureSuccessStatusCode();
        string result2 = result1.Content.ReadAsStringAsync().Result;
        PXTrace.WriteInformation("Response from eTIMS API: " + result2);
        ResponseData responseData = JsonConvert.DeserializeObject<ResponseData>(result2);
        PXTrace.WriteInformation($"response data1: {responseData}");
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInitialise.KraBranchID,
          DocumentType = "Reject Purchase",
          RequestLogOne = content,
          ResponseLog = result2
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this).Insert(zraLogs);
        ((PXGraph) this).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        if (!responseData.status)
        {
          current.Status = "R";
          ((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
      }
      catch (Exception ex)
      {
        PXTrace.WriteError("Error occurred while processing the request: " + ex.Message);
      }
    }
  }

  protected void PurchaseZRA_Status_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Current == null || e.NewValue != null)
      return;
    e.NewValue = (object) "H";
  }

  protected void PurchaseZRA_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PurchaseZRA current = ((PXSelectBase<PurchaseZRA>) this.PurchaseZRAView).Current;
    if (current != null)
      PXUIFieldAttribute.SetEnabled<PurchaseZRA.status>(cache, (object) current, false);
    if (!(current.Status == "R") && !(current.Status == "A"))
      return;
    ((PXAction) this.Accept).SetEnabled(false);
    ((PXAction) this.Reject).SetEnabled(false);
    cache.AllowDelete = false;
  }
}
