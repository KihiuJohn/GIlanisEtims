// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceEntry_ExtensionEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.SO;

public class SOInvoiceEntry_ExtensionEtims : PXGraphExtension<SOInvoiceEntry>
{
  protected void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARInvoice row = (ARInvoice) e.Row;
    if (row == null)
      return;
    bool flag1 = ((ARRegister) row).DocType == "CRM";
    bool flag2 = ((ARRegister) row).DocType == "DRM";
    bool flag3 = ((ARRegister) row).DocType == "INV";
    if (PXCache<ARRegister>.GetExtension<ARRegisterExtEtims>((ARRegister) row) == null)
      return;
    if (((ARRegister) row).DocType == "INV")
    {
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrOriginalCRNInvoiceNo>(cache, (object) row, false);
    }
    if (((ARRegister) row).DocType == "CRM")
    {
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, true);
    }
    if (((ARRegister) row).DocType == "DRM")
    {
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginalReceiptNbr>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrOriginal>(cache, (object) row, false);
      PXUIFieldAttribute.SetVisible<ARRegisterExtEtims.usrZRACreditReason>(cache, (object) row, false);
    }
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrIsEtimsValidated>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrRsltMessage>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrTvDate>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrCUNumber>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrInvoiceNumber>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrInternalData>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrReceiptSignature>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARRegisterExtEtims.usrQRCodee>(cache, (object) row, false);
  }
}
