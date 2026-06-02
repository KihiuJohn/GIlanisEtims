// Decompiled with JetBrains decompiler
// Type: eTims.RefreshTokenEntry
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using System;
using System.Net.Http;
using System.Text;

#nullable disable
namespace eTims;

public class RefreshTokenEntry : PXGraph<RefreshTokenEntry>
{
  public PXSave<eTims.RefreshToken> Save;
  public PXCancel<eTims.RefreshToken> Cancel;
  public PXSelect<eTims.RefreshToken> RefreshTokenView;
  public PXAction<eTims.RefreshToken> RefreshToken;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Refresh Token")]
  protected void refreshToken()
  {
    eTims.RefreshToken current = ((PXSelectBase<eTims.RefreshToken>) this.RefreshTokenView).Current;
    string requestUri = current.AuthUrl.Trim();
    var data = new
    {
      pin = current.Pin,
      username = current.Username,
      password = current.Password,
      platform = current.Platfrom
    };
    string content = JsonConvert.SerializeObject((object) data);
    PXTrace.WriteInformation("Refresh access Token request, " + content);
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        HttpResponseMessage result = httpClient.PostAsync(requestUri, (HttpContent) new StringContent(content, Encoding.UTF8, "application/json")).Result;
        result.EnsureSuccessStatusCode();
        TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"Refresh access token response, {tokenResponse}");
        ((PXSelectBase) this.RefreshTokenView).Cache.SetValueExt<eTims.RefreshToken.accessToken>((object) ((PXSelectBase<eTims.RefreshToken>) this.RefreshTokenView).Current, (object) tokenResponse.access_token);
        ((PXSelectBase<eTims.RefreshToken>) this.RefreshTokenView).Update(((PXSelectBase<eTims.RefreshToken>) this.RefreshTokenView).Current);
        ((PXGraph) this).Persist();
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      throw new PXException("Error retrieving access token.");
    }
  }
}
