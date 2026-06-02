// Decompiled with JetBrains decompiler
// Type: MyProject.InventoryItemExtension
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;

#nullable enable
namespace MyProject;

public class InventoryItemExtension : PXCacheExtension<
#nullable disable
InventoryItem>
{
  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "My Custom Field for UI only")]
  public virtual string MyCustomField { get; set; }

  public abstract class myCustomField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtension.myCustomField>
  {
  }
}
