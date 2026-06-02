// Decompiled with JetBrains decompiler
// Type: eTims.ClassificationCodesMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

#nullable disable
namespace eTims;

public class ClassificationCodesMaint : PXGraph<ClassificationCodesMaint>
{
  public PXSave<ClassCodeRequests> Save;
  public PXCancel<ClassCodeRequests> Cancel;
  public PXSelect<ClassCodeRequests> RequestView;
  [PXImport(typeof (ItemClassificationCodees))]
  public PXSelect<ItemClassificationCodees> ClassificationCodesView;
  public PXAction<ClassCodeRequests> RequestItemCode;

  public ClassificationCodesMaint() => ((PXSelectBase) this.RequestView).Cache.AllowDelete = false;

  [PXButton]
  [PXUIField(DisplayName = "Request Item Codes")]
  protected void requestItemCode()
  {
    ClassCodeRequests current = ((PXSelectBase<ClassCodeRequests>) this.RequestView).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Branch not Found");
    PXTrace.WriteInformation($"current branch :{branchId}");
    DeviceInfo deviceInfo = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    }))) != null ? ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    }))) : throw new PXException("Current branch ID is not available.");
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInfo.BranchOfficeID}, {deviceInfo.KraPin}");
    DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    })));
    if (deviceToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = current.RequestUrl.ToString();
        PXTrace.WriteInformation(requestUri ?? "");
        string parameter = deviceToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result = httpClient.GetAsync(requestUri).Result;
        result.EnsureSuccessStatusCode();
        EtimsResponse etimsResponse = JsonConvert.DeserializeObject<EtimsResponse>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {etimsResponse}");
        PXTrace.WriteInformation("response data result code; " + etimsResponse.resultCd);
        if (etimsResponse.resultCd == "000")
        {
          current.ResultMessage = etimsResponse.resultMsg;
          PXTrace.WriteInformation("result message: " + etimsResponse.resultMsg);
          ((PXSelectBase<ClassCodeRequests>) this.RequestView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        else
        {
          current.ResultMessage = etimsResponse.resultMsg;
          PXTrace.WriteInformation("result message: " + etimsResponse.resultMsg);
          PXTrace.WriteError("Failed with result code: " + etimsResponse.resultCd);
          ((PXSelectBase<ClassCodeRequests>) this.RequestView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        if (etimsResponse != null && etimsResponse.kraResult != null && etimsResponse.kraResult.itemClsList != null)
        {
          PXTrace.WriteInformation($"responseData data item classification list counts, (Count: {etimsResponse.kraResult.itemClsList.Count})");
          Dictionary<string, ItemClassificationCodees> dictionary = new Dictionary<string, ItemClassificationCodees>();
          foreach (PXResult<ItemClassificationCodees> pxResult in ((PXSelectBase<ItemClassificationCodees>) this.ClassificationCodesView).Select(Array.Empty<object>()))
          {
            ItemClassificationCodees classificationCodees = ((ItemClassificationCodees)(pxResult));
            dictionary[classificationCodees.ItemClassificationCode] = classificationCodees;
          }
          foreach (EtimsItem itemCls in etimsResponse.kraResult.itemClsList)
          {
            string str = itemCls.itemClsNm;
            if (str.Length > 250)
              str = str.Substring(0, 250);
            ItemClassificationCodees classificationCodees;
            if (dictionary.TryGetValue(itemCls.itemClsCd, out classificationCodees))
            {
              classificationCodees.ItemClassName = str;
              classificationCodees.ItemClassLevel = itemCls.itemClsLvl.ToString();
              ((PXSelectBase<ItemClassificationCodees>) this.ClassificationCodesView).Update(classificationCodees);
            }
            else
              ((PXSelectBase<ItemClassificationCodees>) this.ClassificationCodesView).Insert(new ItemClassificationCodees()
              {
                ItemClassificationCode = itemCls.itemClsCd,
                ItemClassName = str,
                ItemClassLevel = itemCls.itemClsLvl.ToString()
              });
          }
          ((PXSelectBase) this.ClassificationCodesView).Cache.Persist((PXDBOperation) 2);
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
      throw new PXException("There is no search result");
    }
  }
}
