// Decompiled with JetBrains decompiler
// Type: eTims.UomMapping
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("UomMapping")]
[Serializable]
public class UomMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(60, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Unit Of Measure")]
  public virtual 
  #nullable disable
  string UnitOfMeasure { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Packaging Unit")]
  [PXSelector(typeof (Search<Packaging.code>), new Type[] {typeof (Packaging.code), typeof (Packaging.codeName)})]
  public virtual string PackagingUnit { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Unit Of Quantity")]
  [PXSelector(typeof (Search<QuantityUnit.code>), new Type[] {typeof (QuantityUnit.code), typeof (QuantityUnit.codeName)})]
  public virtual string UnitOfQuantity { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UomMapping.id>
  {
  }

  public abstract class unitOfMeasure : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UomMapping.unitOfMeasure>
  {
  }

  public abstract class packagingUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UomMapping.packagingUnit>
  {
  }

  public abstract class unitOfQuantity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UomMapping.unitOfQuantity>
  {
  }
}
