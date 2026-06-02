// Decompiled with JetBrains decompiler
// Type: eTims.ImportZRAMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class ImportZRAMaint : PXGraph<ImportZRAMaint, ImportZRA>
{
  public PXSelect<ImportZRA> ImportZRAView;
  public PXSelect<GetExportItem, Where<GetExportItem.taskCode, Equal<Current<ImportZRA.taskCode>>>> ZRAImportItemView;
  public PXAction<ImportZRA> Accept;
  public PXAction<ImportZRA> Reject;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Accept")]
  protected void accept()
  {
    ImportZRA current1 = ((PXSelectBase<ImportZRA>) this.ImportZRAView).Current;
    GetExportItem current2 = ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Current;
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    POReceipt poReceipt = new POReceipt()
    {
      ReceiptType = "RT",
      ReceiptNbr = "<NEW>",
      VendorID = current1.VendorName,
      CuryID = current1.ForeignCurrency,
      InvoiceNbr = current1.DeclarationNumber
    };
    ((PXSelectBase<POReceipt>) instance.Document).Insert(poReceipt);
    foreach (PXResult<GetExportItem> pxResult in ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Select(Array.Empty<object>()))
    {
      GetExportItem getExportItem = ((GetExportItem)(pxResult));
      POReceiptLine poReceiptLine = new POReceiptLine()
      {
        InventoryID = getExportItem.InventoryID,
        Qty = getExportItem.Quantity
      };
      ((PXSelectBase<POReceiptLine>) instance.transactions).Insert(poReceiptLine);
    }
    ((PXGraph) instance).Actions.PressSave();
    string receiptNbr = ((PXSelectBase<POReceipt>) instance.Document).Current?.ReceiptNbr;
    if (!string.IsNullOrEmpty(receiptNbr))
    {
      current1.POReceipt = receiptNbr;
      ((PXSelectBase) this.ZRAImportItemView).Cache.Update((object) current1);
      ((PXGraph) this).Actions.PressSave();
    }
    Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (PXAccess.GetBranchID() ?? throw new PXException("Branch not Found"))
    })));
    DeviceInfo deviceInfo = branch != null ? ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    }))) : throw new PXException("Current branch ID is not available.");
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    })));
    if (deviceUrls == null)
      return;
    PXTrace.WriteInformation($"Device information: {deviceInfo.BranchOfficeID}, {deviceInfo.KraPin}");
    if (current1 == null)
      return;
    string kraPin = deviceInfo.KraPin;
    string branchOfficeId = deviceInfo.BranchOfficeID;
    string taskCode = current1.TaskCode;
    string str = (string) null;
    GetExportItem topFirst = ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Select(Array.Empty<object>()).TopFirst;
    if (topFirst != null && topFirst.DeclarationDate != null)
      str = topFirst.DeclarationDate?.ToString();
    List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
    foreach (PXResult<GetExportItem> pxResult in ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Select(Array.Empty<object>()))
    {
      GetExportItem getExportItem = ((GetExportItem)(pxResult));
      InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) getExportItem.InventoryID
      })));
      if (inventoryItem != null)
        PXTrace.WriteInformation($"Inventory item not found for {getExportItem.InventoryID}, {inventoryItem.InventoryCD}");
      Dictionary<string, object> dictionary = new Dictionary<string, object>()
      {
        {
          "itemSeq",
          (object) getExportItem.ItemSequence
        },
        {
          "hsCd",
          (object) getExportItem.Hscode
        },
        {
          "itemClsCd",
          (object) getExportItem.ClassificationCode.Trim()
        },
        {
          "itemCd",
          (object) inventoryItem.InventoryCD.Trim()
        },
        {
          "imptItemSttsCd",
          (object) getExportItem.ImportItemStatus
        },
        {
          "remark",
          (object) getExportItem.Remarks
        },
        {
          "modrNm",
          (object) "ADMIN"
        },
        {
          "modrId",
          (object) "ADMIN"
        }
      };
      dictionaryList.Add(dictionary);
    }
    string content = JsonConvert.SerializeObject((object) new Dictionary<string, object>()
    {
      {
        "tpin",
        (object) kraPin
      },
      {
        "bhfId",
        (object) branchOfficeId
      },
      {
        "taskCd",
        (object) taskCode
      },
      {
        "dclDe",
        (object) str
      },
      {
        "importItemList",
        (object) dictionaryList
      }
    });
    PXTrace.WriteInformation("requestData " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = deviceUrls.UpdateImportUrl.Trim();
        PXTrace.WriteInformation(requestUri ?? "");
        HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result1.EnsureSuccessStatusCode();
        string result2 = result1.Content.ReadAsStringAsync().Result;
        ImportResponse importResponse = JsonConvert.DeserializeObject<ImportResponse>(result2);
        PXTrace.WriteInformation($"responseData {importResponse}");
        PXTrace.WriteInformation("response data result code; " + importResponse.resultCd);
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInfo.BranchOfficeID,
          DocumentType = "Update Imports",
          RequestLogOne = content,
          ResponseLog = result2
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this).Insert(zraLogs);
        ((PXGraph) this).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        if (!(importResponse.resultCd == "000"))
          throw new Exception("The import item was not saved to purchases " + importResponse.resultMsg);
        current1.Status = "A";
        ((PXSelectBase<ImportZRA>) this.ImportZRAView).Update(current1);
        ((PXGraph) this).Actions.PressSave();
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to VSDC: " + ex.Message);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Reject")]
  protected void reject()
  {
    ImportZRA current = ((PXSelectBase<ImportZRA>) this.ImportZRAView).Current;
    Branch branch = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (PXAccess.GetBranchID() ?? throw new PXException("Branch not Found"))
    })));
    DeviceInfo deviceInfo = branch != null ? ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    }))) : throw new PXException("Current branch ID is not available.");
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    DeviceUrls deviceUrls = ((DeviceUrls)(PXSelectBase<DeviceUrls, PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Required<DeviceUrls.servBranch>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branch.BranchCD
    })));
    if (deviceUrls == null)
      return;
    PXTrace.WriteInformation($"Device information: {deviceInfo.BranchOfficeID}, {deviceInfo.KraPin}");
    if (current == null)
      return;
    string kraPin = deviceInfo.KraPin;
    string branchOfficeId = deviceInfo.BranchOfficeID;
    string taskCode = current.TaskCode;
    string str = (string) null;
    GetExportItem topFirst = ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Select(Array.Empty<object>()).TopFirst;
    if (topFirst != null && topFirst.DeclarationDate != null)
      str = topFirst.DeclarationDate?.ToString();
    List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();
    foreach (PXResult<GetExportItem> pxResult in ((PXSelectBase<GetExportItem>) this.ZRAImportItemView).Select(Array.Empty<object>()))
    {
      GetExportItem getExportItem = ((GetExportItem)(pxResult));
      InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) getExportItem.InventoryID
      })));
      if (inventoryItem != null)
        PXTrace.WriteInformation($"Inventory item not found for {getExportItem.InventoryID}, {inventoryItem.InventoryCD}");
      Dictionary<string, object> dictionary = new Dictionary<string, object>()
      {
        {
          "itemSeq",
          (object) getExportItem.ItemSequence
        },
        {
          "hsCd",
          (object) getExportItem.Hscode
        },
        {
          "itemClsCd",
          (object) getExportItem.ClassificationCode.Trim()
        },
        {
          "itemCd",
          (object) inventoryItem.InventoryCD.Trim()
        },
        {
          "imptItemSttsCd",
          (object) getExportItem.ImportItemStatus
        },
        {
          "remark",
          (object) getExportItem.Remarks
        },
        {
          "modrNm",
          (object) "ADMIN"
        },
        {
          "modrId",
          (object) "ADMIN"
        }
      };
      dictionaryList.Add(dictionary);
    }
    string content = JsonConvert.SerializeObject((object) new Dictionary<string, object>()
    {
      {
        "tpin",
        (object) kraPin
      },
      {
        "bhfId",
        (object) branchOfficeId
      },
      {
        "taskCd",
        (object) taskCode
      },
      {
        "dclDe",
        (object) str
      },
      {
        "importItemList",
        (object) dictionaryList
      }
    });
    PXTrace.WriteInformation("requestData " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = deviceUrls.UpdateImportUrl.Trim();
        PXTrace.WriteInformation(requestUri ?? "");
        HttpResponseMessage result1 = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result1.EnsureSuccessStatusCode();
        string result2 = result1.Content.ReadAsStringAsync().Result;
        ImportResponse importResponse = JsonConvert.DeserializeObject<ImportResponse>(result2);
        PXTrace.WriteInformation($"responseData {importResponse}");
        PXTrace.WriteInformation("response data result code; " + importResponse.resultCd);
        ZraLogs zraLogs = new ZraLogs()
        {
          BranchId = deviceInfo.BranchOfficeID,
          DocumentType = "Update Imports",
          RequestLogOne = content,
          ResponseLog = result2
        };
        GraphHelper.Caches<ZraLogs>((PXGraph) this).Insert(zraLogs);
        ((PXGraph) this).Persist(typeof (ZraLogs), (PXDBOperation) 2);
        if (!(importResponse.resultCd == "000"))
          throw new Exception("The import item has not been saved on VSDC, " + importResponse.resultMsg);
        current.Status = "R";
        ((PXSelectBase<ImportZRA>) this.ImportZRAView).Update(current);
        ((PXGraph) this).Actions.PressSave();
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to VSDC: " + ex.Message);
    }
  }

  protected void ImportZRA_Status_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<ImportZRA>) this.ImportZRAView).Current == null || e.NewValue != null)
      return;
    e.NewValue = (object) "H";
  }

  protected void GetExportItem_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PXTrace.WriteInformation("Row Updated");
    GetExportItem row = (GetExportItem) e.Row;
    if (row == null)
      return;
    InventoryItem inventoryItem = ((InventoryItem)(PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryID
    })));
    if (inventoryItem == null)
      return;
    InventoryItemExtEtims extension = PXCache<InventoryItem>.GetExtension<InventoryItemExtEtims>(inventoryItem);
    if (extension != null)
    {
      cache.SetValueExt<GetExportItem.classificationCode>((object) row, (object) extension.UsrItemClassificationCode);
      PXTrace.WriteInformation("Classification Code " + row.ClassificationCode);
    }
  }

  protected void ImportZRA_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    ImportZRA current = ((PXSelectBase<ImportZRA>) this.ImportZRAView).Current;
    if (current == null)
      return;
    cache.SetValueExt<ImportZRA.status>((object) current, (object) "H");
    ((PXSelectBase<ImportZRA>) this.ImportZRAView).Update(current);
    ((PXGraph) this).Actions.PressSave();
  }

  protected void ImportZRA_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ImportZRA current = ((PXSelectBase<ImportZRA>) this.ImportZRAView).Current;
    if (current != null)
      PXUIFieldAttribute.SetEnabled<ImportZRA.status>(cache, (object) current, false);
    if (!(current.Status == "R") && !(current.Status == "A"))
      return;
    ((PXAction) this.Reject).SetEnabled(false);
    ((PXAction) this.Accept).SetEnabled(false);
    PXUIFieldAttribute.SetEnabled<ImportZRA.taskCode>(cache, (object) current, false);
    cache.AllowDelete = false;
  }
}
