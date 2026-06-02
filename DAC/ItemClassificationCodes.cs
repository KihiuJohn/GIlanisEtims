// Decompiled with JetBrains decompiler
// Type: eTims.ItemClassificationCodees
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ItemClassificationCodees")]
[Serializable]
public class ItemClassificationCodees : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code")]
  public virtual 
  #nullable disable
  string ItemClassificationCode { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class Name")]
  public virtual string ItemClassName { get; set; }

  [PXDBString(10, IsFixed = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Class Level")]
  public virtual string ItemClassLevel { get; set; }

  [PXDBString(10, IsFixed = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Type Code")]
  public virtual string TaxTypeCode { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Used")]
  public virtual string Used { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch ID")]
  public virtual string ZRABranch { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ItemClassificationCodees.id>
  {
  }

  public abstract class itemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClassificationCodees.itemClassificationCode>
  {
  }

  public abstract class itemClassName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClassificationCodees.itemClassName>
  {
  }

  public abstract class itemClassLevel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClassificationCodees.itemClassLevel>
  {
  }

  public abstract class taxTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClassificationCodees.taxTypeCode>
  {
  }

  public abstract class used : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemClassificationCodees.used>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ItemClassificationCodees.status>
  {
  }

  public abstract class zRABranch : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ItemClassificationCodees.zRABranch>
  {
  }
}
