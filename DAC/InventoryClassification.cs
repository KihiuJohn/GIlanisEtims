// Decompiled with JetBrains decompiler
// Type: eTims.InventoryClassification
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("InventoryClassification")]
[Serializable]
public class InventoryClassification : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Inventory Id")]
  public virtual 
  #nullable disable
  string InventoryId { get; set; }

  [PXDBString(266, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXDBString(20, IsFixed = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code")]
  [PXSelector(typeof (Search<ItemClassificationCodees.itemClassificationCode>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string ItemClassificationCode { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class")]
  public virtual string UsrItemClass { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inv Id")]
  public virtual int? InvId { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryClassification.id>
  {
  }

  public abstract class inventoryId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryClassification.inventoryId>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryClassification.description>
  {
  }

  public abstract class itemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryClassification.itemClassificationCode>
  {
  }

  public abstract class usrItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryClassification.usrItemClass>
  {
  }

  public abstract class invId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryClassification.invId>
  {
  }
}
