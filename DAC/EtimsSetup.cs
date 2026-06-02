// Decompiled with JetBrains decompiler
// Type: eTims.EtimsSetup
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("EtimsSetup")]
[Serializable]
public class EtimsSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField(DisplayName = "Step Index")]
  public virtual int? StepIndex { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Device Initialized")]
  public virtual bool? IsDeviceInitialized { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Tax Code Configured")]
  public virtual bool? IsTaxCodeConfigured { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Item Classification Code Fetched")]
  public virtual bool? IsClassCodeFetched { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Item Classification Code Mapped with item classes")]
  public virtual bool? IsClassCodeMapped { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Item Classification Code Mapped With Inventory Items")]
  public virtual bool? IsClassCodeMappedWithInventory { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Packaging Unit Uploaded")]
  public virtual bool? IsPackagingUnitUploaded { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Unit Of Quantity Uploaded")]
  public virtual bool? IsUnitOfQuantityUploaded { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is inventory Item Updated With Revenue Authority Standard Codes")]
  public virtual bool? IsStockItemUpdated { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Acumatica UOMs Mapped with Revenue Authority UOMs")]
  public virtual bool? IsUomsMapped { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Inventory Items Registered With Tims")]
  public virtual bool? IsRegisteredWithEtims { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Is Etims Numbering Sequence configured")]
  public virtual bool? IsEtimsNumbSeqSet { get; set; }

  public abstract class stepIndex : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EtimsSetup.stepIndex>
  {
  }

  public abstract class isDeviceInitialized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isDeviceInitialized>
  {
  }

  public abstract class isTaxCodeConfigured : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isTaxCodeConfigured>
  {
  }

  public abstract class isClassCodeFetched : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isClassCodeFetched>
  {
  }

  public abstract class isClassCodeMapped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isClassCodeMapped>
  {
  }

  public abstract class isClassCodeMappedWithInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isClassCodeMappedWithInventory>
  {
  }

  public abstract class isPackagingUnitUploaded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isPackagingUnitUploaded>
  {
  }

  public abstract class isUnitOfQuantityUploaded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isUnitOfQuantityUploaded>
  {
  }

  public abstract class isStockItemUpdated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isStockItemUpdated>
  {
  }

  public abstract class isUomsMapped : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EtimsSetup.isUomsMapped>
  {
  }

  public abstract class isRegisteredWithEtims : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isRegisteredWithEtims>
  {
  }

  public abstract class isEtimsNumbSeqSet : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EtimsSetup.isEtimsNumbSeqSet>
  {
  }
}
