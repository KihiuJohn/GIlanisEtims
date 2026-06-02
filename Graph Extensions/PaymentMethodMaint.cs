// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodMaint_Extension_Etims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using eTims;
using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class PaymentMethodMaint_Extension_Etims : PXGraphExtension<PaymentMethodMaint>
{
  protected void PaymentMethod_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PaymentMethod row = (PaymentMethod) e.Row;
    if (row == null)
      return;
    bool flag = Utility.IsActive();
    if (PXCache<PaymentMethod>.GetExtension<PaymentMethodExtEtims>(row) == null)
      return;
    PXUIFieldAttribute.SetVisible<PaymentMethodExtEtims.usrKraPaymentCode>(cache, (object) row, flag);
  }
}
