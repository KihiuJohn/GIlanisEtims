using Newtonsoft.Json;
using PX.Data;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class DeviceInitializeMaint : PXGraph<DeviceInitializeMaint, DeviceInitialise>
{
  public PXSelect<DeviceInitialise> DeviceView;
  public PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Current<DeviceInitialise.branchID>>>> DeviceInformationView;
  public PXSelect<DeviceUrls, Where<DeviceUrls.servBranch, Equal<Current<DeviceInitialise.branchID>>>> DeviceUrlView;
  public PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Current<DeviceInitialise.branchID>>>> DeviceTokenView;

  public PXAction<DeviceInitialise> RefreshToken;
  public PXAction<DeviceInitialise> Initialise;

  public DeviceInitializeMaint()
  {
    ((PXSelectBase)this.DeviceView).Cache.AllowDelete = !this.IsDeviceActive();
  }

  public virtual void Persist()
  {
    ((PXSelectBase)this.DeviceView).Cache.AllowDelete = !this.IsDeviceActive();
    ((PXGraph)this).Persist();
  }

  private bool IsDeviceActive()
  {
    DeviceInitialise current = ((PXSelectBase<DeviceInitialise>)this.DeviceView).Current;
    return current != null && current.Active.GetValueOrDefault();
  }

  protected virtual void DeviceInitialise_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    DeviceInitialise row = (DeviceInitialise)e.Row;
    if (row == null)
      return;
    ((PXAction)this.RefreshToken).SetEnabled(true);
    ((PXAction)this.RefreshToken).SetVisible(true);
    ((PXAction)this.Initialise).SetEnabled(true);
    ((PXAction)this.Initialise).SetVisible(true);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Refresh Token", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable refreshToken(PXAdapter adapter)
  {
    DeviceInitialise device = ((PXSelectBase<DeviceInitialise>)this.DeviceView).Current
        ?? throw new PXException("No Device Initialise record is selected.");

    if (((PXGraph)this).IsDirty)
      ((PXAction)this.Save).Press();

    object branchId = device.BranchID;
    PXLongOperation.StartOperation((PXGraph)this, delegate
    {
      DeviceInitializeMaint graph = PXGraph.CreateInstance<DeviceInitializeMaint>();
      DeviceInitialise dev = PXSelect<DeviceInitialise,
          Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>
          .Select((PXGraph)graph, new object[] { branchId });
      if (dev == null)
        throw new PXException("Device Initialise record not found.");
      RefreshDeviceToken(graph, dev);
    });
    return adapter.Get();
  }

  internal static void RefreshDeviceToken(DeviceInitializeMaint graph, DeviceInitialise device)
  {
    DeviceInitialiseExtEtims ext = PXCache<DeviceInitialise>.GetExtension<DeviceInitialiseExtEtims>(device);
    string serverUrl = ext?.UsrServerUrl;
    if (string.IsNullOrWhiteSpace(serverUrl))
      throw new PXException("Server URL is not configured for this device.");

    var data = new
    {
      pin = device.KraPin,
      username = device.DeviceSerialNo,
      password = device.Password,
      platform = device.Platform
    };
    string content = JsonConvert.SerializeObject((object)data);
    PXTrace.WriteInformation("Refresh access Token request: " + content);

    using (HttpClient httpClient = new HttpClient())
    {
      httpClient.Timeout = TimeSpan.FromSeconds(60);
      HttpResponseMessage result = httpClient.PostAsync(serverUrl, new StringContent(content, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
      string responseBody = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
      PXTrace.WriteInformation($"Refresh access token response ({(int)result.StatusCode} {result.StatusCode}): {responseBody}");

      if (!result.IsSuccessStatusCode)
        throw new PXException($"Token refresh failed: {result.StatusCode}. {responseBody}");

      TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);
      if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
        throw new PXException("Token refresh response did not contain an access_token.");

      DeviceToken tokenRow = PXSelect<DeviceToken,
          Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>
          .Select((PXGraph)graph, new object[] { device.BranchID });
      PXCache tokenCache = ((PXGraph)graph).Caches[typeof(DeviceToken)];
      if (tokenRow == null)
      {
        tokenRow = new DeviceToken { TokenBranch = device.BranchID, AccessToken = tokenResponse.access_token };
        tokenCache.Insert(tokenRow);
      }
      else
      {
        tokenRow.AccessToken = tokenResponse.access_token;
        tokenCache.Update(tokenRow);
      }
      ((PXGraph)graph).Persist(typeof(DeviceToken), PXDBOperation.Insert | PXDBOperation.Update);
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Initialize", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable initialise(PXAdapter adapter)
  {
    DeviceInitialise current = ((PXSelectBase<DeviceInitialise>)this.DeviceView).Current
        ?? throw new PXException("No DeviceInitialise record is selected.");
    if (((PXGraph)this).IsDirty)
      ((PXAction)this.Save).Press();

    object branchId = current.BranchID;
    PXLongOperation.StartOperation((PXGraph)this, delegate
    {
      DeviceInitializeMaint graph = PXGraph.CreateInstance<DeviceInitializeMaint>();
      DeviceInitialise dev = PXSelect<DeviceInitialise,
          Where<DeviceInitialise.branchID, Equal<Required<DeviceInitialise.branchID>>>>
          .Select((PXGraph)graph, new object[] { branchId });
      if (dev == null)
        throw new PXException("Device Initialise record not found.");
      InitializeDevice(graph, dev);
    });
    return adapter.Get();
  }

  internal static void InitializeDevice(DeviceInitializeMaint graph, DeviceInitialise current)
  {
    try
    {
      string branchId = (current.KraBranchID ?? "").Trim();
      string content = JsonConvert.SerializeObject((object)new InitializeData
      {
        tpin = current.KraPin,
        bhfId = branchId,
        dvcSrlNo = current.DeviceSerialNo
      });
      PXTrace.WriteInformation("VSDC Initialization Request: " + content);
      string serverUrl = PXCache<DeviceInitialise>.GetExtension<DeviceInitialiseExtEtims>(current)?.UsrServerUrl;

      using (HttpClient httpClient = new HttpClient())
      {
        httpClient.Timeout = TimeSpan.FromSeconds(60);
        HttpResponseMessage result1 = httpClient.PostAsync(serverUrl, new StringContent(content, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
        string body = result1.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        PXTrace.WriteInformation($"Initialize response ({(int)result1.StatusCode} {result1.StatusCode}): {body}");

        if (!result1.IsSuccessStatusCode)
          throw new PXException($"Initialize failed: {result1.StatusCode}. {body}");

        InitilizeResponseData initilizeResponseData = JsonConvert.DeserializeObject<InitilizeResponseData>(body);
        PXCache zraCache = GraphHelper.Caches<ZraLogs>((PXGraph)graph);
        zraCache.Insert(new ZraLogs
        {
          BranchId = current.KraPin,
          DocumentType = "Initialization",
          RequestLogOne = content,
          ResponseLog = body
        });
        ((PXGraph)graph).Persist(typeof(ZraLogs), PXDBOperation.Insert);

        PXCache infoCache = ((PXGraph)graph).Caches[typeof(DeviceInfo)];
        DeviceInfo deviceInfo = PXSelect<DeviceInfo,
            Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>
            .Select((PXGraph)graph, new object[] { current.BranchID });

        if (initilizeResponseData != null && initilizeResponseData.resultCd == "000")
        {
          if (deviceInfo == null)
          {
            deviceInfo = new DeviceInfo { ResBranchID = current.BranchID };
            deviceInfo = (DeviceInfo)infoCache.Insert(deviceInfo);
          }
          deviceInfo.ResultMessage = initilizeResponseData.resultMsg;
          if (initilizeResponseData.data?.info != null)
          {
            deviceInfo.KraPin = initilizeResponseData.data.info.tin;
            deviceInfo.TaxpayerName = initilizeResponseData.data.info.taxprNm;
            deviceInfo.BranchOfficeID = initilizeResponseData.data.info.bhfId;
            deviceInfo.SalesControlUnitID = initilizeResponseData.data.info.sdcId;
            deviceInfo.Mrcno = initilizeResponseData.data.info.mrcNo;
          }
          infoCache.Update(deviceInfo);
        }
        else if (initilizeResponseData != null)
        {
          if (deviceInfo == null)
          {
            deviceInfo = new DeviceInfo { ResBranchID = current.BranchID };
            deviceInfo = (DeviceInfo)infoCache.Insert(deviceInfo);
          }
          deviceInfo.ResultMessage = initilizeResponseData.resultMsg;
          infoCache.Update(deviceInfo);
          PXTrace.WriteError("Failed with result code: " + initilizeResponseData.resultCd);
        }
        ((PXGraph)graph).Persist(typeof(DeviceInfo), PXDBOperation.Insert | PXDBOperation.Update);
      }
    }
    catch (PXException) { throw; }
    catch (Exception ex)
    {
      PXTrace.WriteError("eTIMS Initialize Exception: " + ex);
      throw new PXException("eTIMS Initialize Exception: " + ex.Message);
    }
  }
}
