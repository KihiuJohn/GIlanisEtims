// Decompiled with JetBrains decompiler
// Type: eTims.ImportZRA
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.PO;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ImportZRA")]
[Serializable]
public class ImportZRA : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Task Code")]
  [PXSelector(typeof (Search4<GetExportItem.taskCode, Aggregate<GroupBy<GetExportItem.taskCode>>>), new Type[] {typeof (GetExportItem.taskCode), typeof (GetExportItem.declarationDate), typeof (GetExportItem.declarationNumber), typeof (GetExportItem.decRefNbr)})]
  public virtual 
  #nullable disable
  string TaskCode { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Declaration Date")]
  [PXFormula(typeof (Selector<ImportZRA.taskCode, GetExportItem.declarationDate>))]
  public virtual DateTime? DeclarationDate { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Status")]
  [PXStringList(new string[] {"H", "A", "R"}, new string[] {"On Hold", "Approved", "Rejected"})]
  [PXDefault("H")]
  public virtual string Status { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Request Url")]
  public virtual string RequestUrl { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Foreign Currency")]
  [PXFormula(typeof (Selector<ImportZRA.taskCode, GetExportItem.foreignCurrency>))]
  public virtual string ForeignCurrency { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "PONumber")]
  [PXSelector(typeof (Search<POOrder.orderNbr>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.vendorID), typeof (POOrder.vendorID_Vendor_acctName)})]
  public virtual string PONumber { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor ID")]
  public virtual int? VendorID { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "POReceipt")]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr>), new Type[] {typeof (PX.Objects.PO.POReceipt.receiptNbr), typeof (PX.Objects.PO.POReceipt.vendorID), typeof (PX.Objects.PO.POReceipt.vendorID_Vendor_acctName)})]
  public virtual string POReceipt { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Pobill")]
  [PXSelector(typeof (Search<APInvoice.refNbr>), new Type[] {typeof (APInvoice.refNbr), typeof (APInvoice.vendorID), typeof (APInvoice.vendorID_Vendor_acctName)})]
  public virtual string Pobill { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Declaration Number")]
  [PXFormula(typeof (Selector<ImportZRA.taskCode, GetExportItem.declarationNumber>))]
  public virtual string DeclarationNumber { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor Name")]
  [PXSelector(typeof (Search<Vendor.bAccountID>), new Type[] {typeof (Vendor.acctCD), typeof (Vendor.acctName)}, SubstituteKey = typeof (Vendor.acctCD), DescriptionField = typeof (Vendor.acctName))]
  public virtual int? VendorName { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ImportZRA.id>
  {
  }

  public abstract class taskCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.taskCode>
  {
  }

  public abstract class declarationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ImportZRA.declarationDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.status>
  {
  }

  public abstract class requestUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.requestUrl>
  {
  }

  public abstract class foreignCurrency : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ImportZRA.foreignCurrency>
  {
  }

  public abstract class pONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.pONumber>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ImportZRA.vendorID>
  {
  }

  public abstract class pOReceipt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.pOReceipt>
  {
  }

  public abstract class pobill : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ImportZRA.pobill>
  {
  }

  public abstract class declarationNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ImportZRA.declarationNumber>
  {
  }

  public abstract class vendorName : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ImportZRA.vendorName>
  {
  }
}
