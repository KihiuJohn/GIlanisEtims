// Decompiled with JetBrains decompiler
// Type: eTims.ItemClasses
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ItemClasses")]
[Serializable]
public class ItemClasses : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(100, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class")]
  public virtual 
  #nullable disable
  string ItemClass { get; set; }

  [PXDBString(20, IsFixed = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code")]
  [PXSelector(typeof (Search<ItemClassificationCodees.itemClassificationCode>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string ItemClassificationCode { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class Descr")]
  public virtual string ItemClassDescr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class ID")]
  public virtual int? ItemClassID { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemClasses.id>
  {
  }

  public abstract class itemClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemClasses.itemClass>
  {
  }

  public abstract class itemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClasses.itemClassificationCode>
  {
  }

  public abstract class itemClassDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClasses.itemClassDescr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemClasses.itemClassID>
  {
  }
}
