// Decompiled with JetBrains decompiler
// Type: eTims.GetExportMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class GetExportMaint : PXGraph<GetExportMaint>
{
  public PXSave<RequestGetExport> Save;
  public PXCancel<RequestGetExport> Cancel;
  public PXSelect<RequestGetExport> RequestExportView;
  public PXSelect<GetExportItem> ExportItemView;
  public PXAction<RequestGetExport> RequestImport;

  [PXButton]
  [PXUIField(DisplayName = "Request Imports From ZRA")]
  protected void requestImport()
  {
    RequestGetExport current = ((PXSelectBase<RequestGetExport>) this.RequestExportView).Current;
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
    var data = new
    {
      tpin = current.Zrapin,
      bhfId = current.BranchID.Trim(),
      lastReqDt = "20240110000000"
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
        ZRAExport zraExport = JsonConvert.DeserializeObject<ZRAExport>(result2);
        PXTrace.WriteInformation($"responseData {zraExport}");
        PXTrace.WriteInformation("response data result code; " + zraExport.resultCd);
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInfo.BranchOfficeID,
          DocumentType = "Get Import",
          RequestLogOne = result2,
          ResponseLog = content
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this).Insert(zraLogs);
        ((PXGraph) this).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        if (zraExport.resultCd == "000")
        {
          current.ResultMessage = zraExport.resultMsg;
          PXTrace.WriteInformation("result message: " + zraExport.resultMsg);
          ((PXSelectBase<RequestGetExport>) this.RequestExportView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        else
        {
          current.ResultMessage = zraExport.resultMsg;
          PXTrace.WriteInformation("result message: " + zraExport.resultMsg);
          PXTrace.WriteError("Failed with result code: " + zraExport.resultCd);
          ((PXSelectBase<RequestGetExport>) this.RequestExportView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        if (zraExport != null && zraExport.data != null && zraExport.data.itemList != null)
        {
          PXTrace.WriteInformation($"responseData data item classification list counts, (Count: {zraExport.data.itemList.Count})");
          Dictionary<string, GetExportItem> dictionary = new Dictionary<string, GetExportItem>();
          foreach (PXResult<GetExportItem> pxResult in ((PXSelectBase<GetExportItem>) this.ExportItemView).Select(Array.Empty<object>()))
          {
            GetExportItem getExportItem = ((GetExportItem)(pxResult));
            dictionary[getExportItem.DeclarationNumber] = getExportItem;
          }
          foreach (ExportItems exportItems in zraExport.data.itemList)
          {
            GetExportItem getExportItem;
            if (dictionary.TryGetValue(exportItems.dclNo, out getExportItem))
            {
              getExportItem.Hscode = exportItems.hsCd;
              getExportItem.ItemSequence = new int?(exportItems.itemSeq);
              getExportItem.DeclarationDate = exportItems.dclDe;
              getExportItem.DeclarationNumber = exportItems.dclNo;
              getExportItem.ItemName = exportItems.itemNm;
              getExportItem.ExportCountryCode = exportItems.exptNatCd;
              getExportItem.CountryOfOrigin = exportItems.orgnNatCd;
              getExportItem.ImportItemStatus = exportItems.imptItemsttsCd;
              getExportItem.Package = new Decimal?(exportItems.pkg);
              getExportItem.PackagingCode = exportItems.pkgUnitCd;
              getExportItem.Quantity = new Decimal?(exportItems.qty);
              getExportItem.UnitOfQuantity = exportItems.qtyUnitCd;
              getExportItem.GrossWeight = new Decimal?(exportItems.totWt);
              getExportItem.NetWeight = new Decimal?(exportItems.netWt);
              getExportItem.SupplierName = exportItems.spplrNm;
              getExportItem.AgentName = exportItems.agntNm;
              getExportItem.ForeignCurrencyAmount = new Decimal?(exportItems.invcFcurAmt);
              getExportItem.ForeignCurrency = exportItems.invcFcurCd;
              getExportItem.ExchangeRate = new Decimal?(exportItems.invcFcurExcrt);
              getExportItem.DecRefNbr = exportItems.dclRefNum;
              ((PXSelectBase<GetExportItem>) this.ExportItemView).Update(getExportItem);
            }
            else
              ((PXSelectBase<GetExportItem>) this.ExportItemView).Insert(new GetExportItem()
              {
                TaskCode = exportItems.taskCd,
                Hscode = exportItems.hsCd,
                ItemSequence = new int?(exportItems.itemSeq),
                DeclarationDate = exportItems.dclDe,
                DeclarationNumber = exportItems.dclNo,
                ItemName = exportItems.itemNm,
                ExportCountryCode = exportItems.exptNatCd,
                CountryOfOrigin = exportItems.orgnNatCd,
                ImportItemStatus = exportItems.imptItemsttsCd,
                Package = new Decimal?(exportItems.pkg),
                PackagingCode = exportItems.pkgUnitCd,
                Quantity = new Decimal?(exportItems.qty),
                UnitOfQuantity = exportItems.qtyUnitCd,
                GrossWeight = new Decimal?(exportItems.totWt),
                NetWeight = new Decimal?(exportItems.netWt),
                SupplierName = exportItems.spplrNm,
                AgentName = exportItems.agntNm,
                ForeignCurrencyAmount = new Decimal?(exportItems.invcFcurAmt),
                ForeignCurrency = exportItems.invcFcurCd,
                ExchangeRate = new Decimal?(exportItems.invcFcurExcrt),
                DecRefNbr = exportItems.dclRefNum
              });
          }
          ((PXSelectBase) this.ExportItemView).Cache.Persist((PXDBOperation) 2);
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
    }
  }
}
