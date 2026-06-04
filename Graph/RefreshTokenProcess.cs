using Newtonsoft.Json;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class RefreshTokenProcess : PXGraph<RefreshTokenProcess>
{
  public PXCancel<DeviceInitialise> Cancel;

  [PXFilterable(new Type[] { })]
  public PXProcessing<DeviceInitialise, Where<DeviceInitialise.active, Equal<True>>> ProcessingView;

  public RefreshTokenProcess()
  {
    PXTrace.WriteInformation("[RefreshTokenProcess] graph constructed - Setting delegate");
    this.ProcessingView.SetProcessCaption("Refresh Token");
    this.ProcessingView.SetProcessAllCaption("Refresh All Tokens");
    this.ProcessingView.SetProcessDelegate(new PXProcessingBase<DeviceInitialise>.ProcessListDelegate(Process));
    PXTrace.WriteInformation("[RefreshTokenProcess] delegate set");
  }

  private static void Process(List<DeviceInitialise> list)
  {
    try
    {
      PXTrace.WriteInformation($"[RefreshTokenProcess] Process STARTED. Row count = {list?.Count ?? -1}");
      if (list == null || list.Count == 0)
      {
        PXTrace.WriteInformation("[RefreshTokenProcess] List is null or empty - throwing exception");
        throw new PXException("No DeviceInitialise rows to process.");
      }

      PXTrace.WriteInformation($"[RefreshTokenProcess] Creating DeviceInitializeMaint graph");
      DeviceInitializeMaint graph = PXGraph.CreateInstance<DeviceInitializeMaint>();
      PXTrace.WriteInformation($"[RefreshTokenProcess] Graph created. Starting loop for {list.Count} devices");
      
      for (int index = 0; index < list.Count; ++index)
      {
        DeviceInitialise device = list[index];
        PXTrace.WriteInformation($"[RefreshTokenProcess] Processing device index {index}, BranchID={device?.BranchID}");
        if (device == null) 
        {
          PXTrace.WriteInformation("[RefreshTokenProcess] Device is null, skipping");
          continue;
        }
        try
        {
          PXTrace.WriteInformation($"[RefreshTokenProcess] Calling RefreshToken for BranchID={device.BranchID}");
          RefreshToken(graph, device);
          PXTrace.WriteInformation($"[RefreshTokenProcess] RefreshToken succeeded for BranchID={device.BranchID}");
          PXProcessing<DeviceInitialise>.SetInfo(index, $"Token refreshed for branch {device.BranchID}.");
        }
        catch (Exception ex)
        {
          PXTrace.WriteError($"[RefreshTokenProcess] BranchID={device.BranchID} failed: {ex}");
          PXProcessing<DeviceInitialise>.SetError(index, new PXException(ex.Message));
        }
      }
      PXTrace.WriteInformation($"[RefreshTokenProcess] Process COMPLETED");
    }
    catch (Exception ex)
    {
      PXTrace.WriteError($"[RefreshTokenProcess] Process method exception: {ex}");
      throw;
    }
  }

  public static void RefreshToken(DeviceInitializeMaint graph, DeviceInitialise device)
  {
    if (device == null)
      throw new PXException("Device row is null.");

    DeviceInitialiseExtEtims ext = PXCache<DeviceInitialise>.GetExtension<DeviceInitialiseExtEtims>(device);
    string serverUrl = ext?.UsrServerUrl;

    PXTrace.WriteInformation($"[RefreshTokenProcess] BranchID={device.BranchID} Active={device.Active} KraPin={device.KraPin} KraBranchID={device.KraBranchID} DeviceSerialNo={device.DeviceSerialNo} Platform={device.Platform} ServerUrl={serverUrl ?? "<null>"}");

    if (string.IsNullOrWhiteSpace(serverUrl))
      throw new PXException($"Server URL is not configured on DeviceInitialise for BranchID {device.BranchID}.");
    if (string.IsNullOrWhiteSpace(device.KraPin))
      throw new PXException($"KRA PIN is missing on BranchID {device.BranchID}.");
    if (string.IsNullOrWhiteSpace(device.DeviceSerialNo))
      throw new PXException($"Device Serial No is missing on BranchID {device.BranchID}.");

    var data = new
    {
      pin = device.KraPin,
      username = device.DeviceSerialNo,
      password = device.Password,
      platform = device.Platform
    };
    string payload = JsonConvert.SerializeObject((object)data);
    PXTrace.WriteInformation($"[RefreshTokenProcess] POST {serverUrl}  Payload: {payload}");

    using (HttpClient httpClient = new HttpClient())
    {
      httpClient.Timeout = TimeSpan.FromSeconds(60);
      HttpResponseMessage response;
      try
      {
        response = httpClient.PostAsync(serverUrl, new StringContent(payload, Encoding.UTF8, "application/json")).Result;
      }
      catch (Exception ex)
      {
        throw new PXException($"HTTP POST to {serverUrl} threw: {ex.GetBaseException().Message}");
      }
      string body = response.Content.ReadAsStringAsync().Result ?? "";
      PXTrace.WriteInformation($"[RefreshTokenProcess] Response {(int)response.StatusCode} {response.StatusCode}: {body}");

      if (!response.IsSuccessStatusCode)
        throw new PXException($"POST {serverUrl} returned {(int)response.StatusCode} {response.StatusCode}. Body: {Truncate(body, 500)}");

      TokenResponse tokenResponse;
      try { tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(body); }
      catch (Exception ex) { throw new PXException($"Could not parse token response: {ex.Message}. Body: {Truncate(body, 300)}"); }

      if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
        throw new PXException($"Token endpoint did not return an access_token. Body: {Truncate(body, 300)}");

      DeviceToken deviceToken = PXSelect<DeviceToken,
          Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>
          .Select((PXGraph)graph, new object[] { device.BranchID });
      if (deviceToken == null)
      {
        deviceToken = new DeviceToken { TokenBranch = device.BranchID, AccessToken = tokenResponse.access_token };
        ((PXSelectBase<DeviceToken>)graph.DeviceTokenView).Insert(deviceToken);
      }
      else
      {
        deviceToken.AccessToken = tokenResponse.access_token;
        ((PXSelectBase<DeviceToken>)graph.DeviceTokenView).Update(deviceToken);
      }
      ((PXGraph)graph).Actions.PressSave();
    }
  }

  private static string Truncate(string s, int n)
  {
    if (string.IsNullOrEmpty(s)) return s;
    return s.Length <= n ? s : s.Substring(0, n) + "...";
  }
}
