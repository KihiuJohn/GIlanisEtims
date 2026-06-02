// Decompiled with JetBrains decompiler
// Type: eTims.EtimsKraCodeMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using System;

#nullable disable
namespace eTims;

public class EtimsKraCodeMaint : PXGraph<EtimsKraCodeMaint>
{
  public PXSave<EtimsKraCodeMaint.MasterTable> Save;
  public PXCancel<EtimsKraCodeMaint.MasterTable> Cancel;
  [PXImport(typeof (KRATaxCodes))]
  public PXSelect<KRATaxCodes> CodesEtimsView;
  [PXImport(typeof (PaymentMethodKRA))]
  public PXSelect<PaymentMethodKRA> PaymentMethodKraView;
  [PXImport(typeof (QuantityUnit))]
  public PXSelect<QuantityUnit> QuantityView;
  [PXImport(typeof (Packaging))]
  public PXSelect<Packaging> PackageView;
  public PXFilter<EtimsKraCodeMaint.MasterTable> MasterView;

  [Serializable]
  public class MasterTable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
