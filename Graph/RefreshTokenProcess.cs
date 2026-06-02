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
  public PXProcessing<DeviceInitialise> Devices;

  public RefreshTokenProcess()
  {
    PXTrace.WriteInformation("[RefreshTokenProcess] graph constructed");
    this.Devices.SetProcessCaption("Refresh Token");
    this.Devices.SetProcessAllCaption("Refresh All Tokens");
    this.Devices.SetProcessDelegate(devices => ProcessSelectedDevices(devices));
  }

  public static void ProcessSelectedDevices(List<DeviceInitialise> devices)
  {
    PXTrace.WriteInformation($"[RefreshTokenProcess] ProcessSelectedDevices fired. Row count = {devices?.Count ?? -1}");
    if (devices == null || devices.Count == 0)
      throw new PXException("No DeviceInitialise rows to process.");

    DeviceInitializeMaint graph = PXGraph.CreateInstance<DeviceInitializeMaint>();
    for (int index = 0; index < devices.Count; ++index)
    {
      DeviceInitialise device = devices[index];
      if (device == null) continue;
      try
      {
        RefreshToken(graph, device);
        PXProcessing<DeviceInitialise>.SetInfo(index, $"Token refreshed for branch {device.BranchID}.");
      }
      catch (Exception ex)
      {
        PXTrace.WriteError($"[RefreshTokenProcess] BranchID={device.BranchID} failed: {ex}");
        PXProcessing<DeviceInitialise>.SetError(index, new PXException(ex.Message));
      }
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
