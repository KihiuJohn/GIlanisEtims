// Decompiled with JetBrains decompiler
// Type: eTims.Utility
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data.Update;

#nullable disable
namespace eTims;

public static class Utility
{
  public static bool IsActive()
  {
    return PXInstanceHelper.CurrentCompany == 2 || PXInstanceHelper.CurrentCompany == 3;
  }
}
