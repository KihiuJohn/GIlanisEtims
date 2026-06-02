// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryItemExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class InventoryItemExtEtims : PXCacheExtension<
#nullable disable
InventoryItem>
{
  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Code")]
  public virtual string UsrItemCode { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Item Registered with KRA")]
  public virtual bool? UsrisRegistered { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code")]
  [PXSelector(typeof (Search4<ItemClassificationCodees.itemClassificationCode, Aggregate<GroupBy<ItemClassificationCodees.itemClassificationCode>>>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string UsrItemClassificationCode { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code", Enabled = false)]
  public virtual string UsrTaxCode { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code", Enabled = false)]
  public virtual string UsrTaxCodes { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Packaging Unit", Enabled = false)]
  public virtual string UsrPackagingUnit { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Unit of Quantity", Enabled = false)]
  public virtual string UsrUnitOfQuantity { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Product Type")]
  [PXSelector(typeof (Search<ProductTypeZRA.code>), new Type[] {typeof (ProductTypeZRA.code)}, DescriptionField = typeof (ProductTypeZRA.codeName))]
  public virtual string UsrProductType { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Code", Enabled = false)]
  public virtual string UsrItemCodes { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Codes")]
  [PXSelector(typeof (Search<ItemClassificationCodees.itemClassificationCode>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string UsrItemClassificationCodes { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Select")]
  public virtual bool? UsrSelect { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? UsrSelected { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "ISale ID")]
  public virtual int? UsrIsaleId { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "ISale ID Export", Enabled = false)]
  public virtual int? UsrIsaleIdExpo { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Item Classification Code Export")]
  [PXSelector(typeof (Search4<ItemClassificationCodees.itemClassificationCode, Aggregate<GroupBy<ItemClassificationCodees.itemClassificationCode>>>), new Type[] {typeof (ItemClassificationCodees.itemClassificationCode), typeof (ItemClassificationCodees.itemClassName), typeof (ItemClassificationCodees.itemClassLevel)})]
  public virtual string UsrItemClassificationCodeExpo { get; set; }

  [PXDBString(25, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Tax Code Export", Enabled = false)]
  public virtual string UsrTaxCodesExpo { get; set; }

  public abstract class usrItemCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrItemCode>
  {
  }

  public abstract class usrisRegistered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItemExtEtims.usrisRegistered>
  {
  }

  public abstract class usrItemClassificationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrItemClassificationCode>
  {
  }

  public abstract class usrTaxCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrTaxCode>
  {
  }

  public abstract class usrTaxCodes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrTaxCodes>
  {
  }

  public abstract class usrPackagingUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrPackagingUnit>
  {
  }

  public abstract class usrUnitOfQuantity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrUnitOfQuantity>
  {
  }

  public abstract class usrProductType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrProductType>
  {
  }

  public abstract class usrItemCodes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrItemCodes>
  {
  }

  public abstract class usrItemClassificationCodes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrItemClassificationCodes>
  {
  }

  public abstract class usrSelect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryItemExtEtims.usrSelect>
  {
  }

  public abstract class usrSelected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryItemExtEtims.usrSelected>
  {
  }

  public abstract class usrIsaleId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryItemExtEtims.usrIsaleId>
  {
  }

  public abstract class usrIsaleIdExpo : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryItemExtEtims.usrIsaleIdExpo>
  {
  }

  public abstract class usrItemClassificationCodeExpo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrItemClassificationCodeExpo>
  {
  }

  public abstract class usrTaxCodesExpo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryItemExtEtims.usrTaxCodesExpo>
  {
  }
}
