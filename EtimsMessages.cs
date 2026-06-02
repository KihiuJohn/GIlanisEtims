// Decompiled with JetBrains decompiler
// Type: eTims.EtimsMessages
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Common;

#nullable disable
namespace eTims;

[PXLocalizable]
public class EtimsMessages
{
  public const string InvClassCodeNotEmpty = "Item classification code cannot be empty.";
  public const string TranMessageCode = "Item code field cannot be empty";
  public const string TranMessageClassCode = "item classification codes field cannot be empty";
  public const string TranMessageCountry = "country of origin field cannot be empty";
  public const string TranMessageTaxType = "Tax Type field cannot be empty";
  public const string TranMessageRegistered = "Item is not registered with VSDC";
  public const string MapInvWithClassCode = "Not all item classes have been mapped successfully with an item classification code.";
  public const string TokenError = "Error during invoice validation.";
  public const string DeviceInform = "Device information not found for the current branch.";
  public const string NoResult = "There is no search result";
  public const string BranchAvailability = "Current branch ID is not available.";
  public const string NoBranch = "Branch not Found";
  public const string RefreshToken = "Please Refresh Access Token to be able to continue";
}
