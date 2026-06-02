// Decompiled with JetBrains decompiler
// Type: eTims.timsCodMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using Newtonsoft.Json;
using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using ZRASmartInvoice;

#nullable disable
namespace eTims;

public class timsCodMaint : PXGraph<timsCodMaint>
{
  public PXSave<StandardCodes> Save;
  public PXCancel<StandardCodes> Cancel;
  public PXSelect<StandardCodes> StandardCodesView;
  [PXImport(typeof (QuantityUnit))]
  public PXSelect<QuantityUnit> QuantityView;
  [PXImport(typeof (Packaging))]
  public PXSelect<Packaging> PackageView;
  [PXImport(typeof (KRATaxCodes))]
  public PXSelect<KRATaxCodes> CodesEtimsView;
  [PXImport(typeof (PaymentMethodKRA))]
  public PXSelect<PaymentMethodKRA> PaymentMethodKraView;
  [PXImport(typeof (CreditReasonCode))]
  public PXSelect<CreditReasonCode> CreditReasonCodeView;
  [PXImport(typeof (DebitReasonCode))]
  public PXSelect<DebitReasonCode> DebitReasonCodeView;
  [PXImport(typeof (ProductTypeZRA))]
  public PXSelect<ProductTypeZRA> ProductTypeZraView;
  [PXImport(typeof (TransactionTypeZRA))]
  public PXSelect<TransactionTypeZRA> TransactionTypeZraView;
  [PXImport(typeof (TransactionProgress))]
  public PXSelect<TransactionProgress> TransactionProgressView;
  [PXImport(typeof (StockInOut))]
  public PXSelect<StockInOut> StockInOutView;
  [PXImport(typeof (SalesReceiptType))]
  public PXSelect<SalesReceiptType> SalesReceiptView;
  [PXImport(typeof (ImportItem))]
  public PXSelect<ImportItem> ImportView;
  [PXImport(typeof (PurchaseReceiptType))]
  public PXSelect<PurchaseReceiptType> PurchaseTypeView;
  [PXImport(typeof (RegistrationType))]
  public PXSelect<RegistrationType> RegistrationTypeView;
  [PXImport(typeof (SalesCategory))]
  public PXSelect<SalesCategory> SalesCategoryView;
  public PXAction<StandardCodes> RequestStandardCode;

  [PXButton]
  [PXUIField(DisplayName = "Request Standard Codes")]
  protected void requestStandardCode()
  {
    StandardCodes current = ((PXSelectBase<StandardCodes>) this.StandardCodesView).Current;
    int? branchId = PXAccess.GetBranchID();
    if (!branchId.HasValue)
      throw new PXException("Branch not Found");
    DeviceInfo deviceInfo = ((Branch)(PXSelectBase<Branch, PXSelect<Branch, Where<Branch.branchID, Equal<Required<Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    }))) != null ? ((DeviceInfo)(PXSelectBase<DeviceInfo, PXSelect<DeviceInfo, Where<DeviceInfo.resBranchID, Equal<Required<DeviceInfo.resBranchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    }))) : throw new PXException("Current branch ID is not available.");
    if (deviceInfo == null || deviceInfo.SalesControlUnitID == null)
      throw new PXException("Device information not found for the current branch.");
    PXTrace.WriteInformation($"Device information: {deviceInfo.BranchOfficeID}, {deviceInfo.KraPin}");
    DeviceToken deviceToken = ((DeviceToken)(PXSelectBase<DeviceToken, PXSelect<DeviceToken, Where<DeviceToken.tokenBranch, Equal<Required<DeviceToken.tokenBranch>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) branchId
    })));
    if (deviceToken == null)
      throw new PXException("Please Refresh Access Token to be able to continue");
    if (current == null)
      return;
    try
    {
      using (HttpClient httpClient = new HttpClient())
      {
        string requestUri = current.RequestUrl.ToString();
        PXTrace.WriteInformation(requestUri ?? "");
        string parameter = deviceToken.AccessToken.Trim();
        PXTrace.WriteInformation("Access Token: " + parameter);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", parameter);
        HttpResponseMessage result = httpClient.GetAsync(requestUri).Result;
        result.EnsureSuccessStatusCode();
        CodesResponse codesResponse = JsonConvert.DeserializeObject<CodesResponse>(result.Content.ReadAsStringAsync().Result);
        PXTrace.WriteInformation($"responseData {codesResponse}");
        PXTrace.WriteInformation("response data result code; " + codesResponse.resultCd);
        if (codesResponse.resultCd == "000")
        {
          current.ResultMessage = codesResponse.resultMsg;
          PXTrace.WriteInformation($"result message: {codesResponse.kraResult.clsList}");
          PXTrace.WriteInformation("result message: " + codesResponse.resultMsg);
          ((PXSelectBase<StandardCodes>) this.StandardCodesView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        else
        {
          current.ResultMessage = codesResponse.resultMsg;
          PXTrace.WriteInformation("result message: " + codesResponse.resultMsg);
          PXTrace.WriteError("Failed with result code: " + codesResponse.resultCd);
          ((PXSelectBase<StandardCodes>) this.StandardCodesView).Update(current);
          ((PXGraph) this).Actions.PressSave();
        }
        if (codesResponse != null && codesResponse.kraResult != null && codesResponse.kraResult.clsList != null)
        {
          Dictionary<string, Packaging> dictionary1 = new Dictionary<string, Packaging>();
          Dictionary<string, QuantityUnit> dictionary2 = new Dictionary<string, QuantityUnit>();
          Dictionary<string, KRATaxCodes> dictionary3 = new Dictionary<string, KRATaxCodes>();
          Dictionary<string, PaymentMethodKRA> dictionary4 = new Dictionary<string, PaymentMethodKRA>();
          Dictionary<string, CreditReasonCode> dictionary5 = new Dictionary<string, CreditReasonCode>();
          Dictionary<string, DebitReasonCode> dictionary6 = new Dictionary<string, DebitReasonCode>();
          Dictionary<string, ProductTypeZRA> dictionary7 = new Dictionary<string, ProductTypeZRA>();
          Dictionary<string, TransactionTypeZRA> dictionary8 = new Dictionary<string, TransactionTypeZRA>();
          Dictionary<string, TransactionProgress> dictionary9 = new Dictionary<string, TransactionProgress>();
          Dictionary<string, StockInOut> dictionary10 = new Dictionary<string, StockInOut>();
          Dictionary<string, SalesReceiptType> dictionary11 = new Dictionary<string, SalesReceiptType>();
          Dictionary<string, ImportItem> dictionary12 = new Dictionary<string, ImportItem>();
          Dictionary<string, PurchaseReceiptType> dictionary13 = new Dictionary<string, PurchaseReceiptType>();
          Dictionary<string, RegistrationType> dictionary14 = new Dictionary<string, RegistrationType>();
          Dictionary<string, SalesCategory> dictionary15 = new Dictionary<string, SalesCategory>();
          foreach (PXResult<SalesCategory> pxResult in ((PXSelectBase<SalesCategory>) this.SalesCategoryView).Select(Array.Empty<object>()))
          {
            SalesCategory salesCategory = ((SalesCategory)(pxResult));
            dictionary15[salesCategory.Code] = salesCategory;
          }
          foreach (PXResult<Packaging> pxResult in ((PXSelectBase<Packaging>) this.PackageView).Select(Array.Empty<object>()))
          {
            Packaging packaging = ((Packaging)(pxResult));
            dictionary1[packaging.Code] = packaging;
          }
          foreach (PXResult<QuantityUnit> pxResult in ((PXSelectBase<QuantityUnit>) this.QuantityView).Select(Array.Empty<object>()))
          {
            QuantityUnit quantityUnit = ((QuantityUnit)(pxResult));
            dictionary2[quantityUnit.Code] = quantityUnit;
          }
          foreach (PXResult<KRATaxCodes> pxResult in ((PXSelectBase<KRATaxCodes>) this.CodesEtimsView).Select(Array.Empty<object>()))
          {
            KRATaxCodes kraTaxCodes = ((KRATaxCodes)(pxResult));
            dictionary3[kraTaxCodes.TaxCode] = kraTaxCodes;
          }
          foreach (PXResult<PaymentMethodKRA> pxResult in ((PXSelectBase<PaymentMethodKRA>) this.PaymentMethodKraView).Select(Array.Empty<object>()))
          {
            PaymentMethodKRA paymentMethodKra = ((PaymentMethodKRA)(pxResult));
            dictionary4[paymentMethodKra.Code] = paymentMethodKra;
          }
          foreach (PXResult<CreditReasonCode> pxResult in ((PXSelectBase<CreditReasonCode>) this.CreditReasonCodeView).Select(Array.Empty<object>()))
          {
            CreditReasonCode creditReasonCode = ((CreditReasonCode)(pxResult));
            dictionary5[creditReasonCode.Code] = creditReasonCode;
          }
          foreach (PXResult<DebitReasonCode> pxResult in ((PXSelectBase<DebitReasonCode>) this.DebitReasonCodeView).Select(Array.Empty<object>()))
          {
            DebitReasonCode debitReasonCode = ((DebitReasonCode)(pxResult));
            dictionary6[debitReasonCode.Code] = debitReasonCode;
          }
          foreach (PXResult<ProductTypeZRA> pxResult in ((PXSelectBase<ProductTypeZRA>) this.ProductTypeZraView).Select(Array.Empty<object>()))
          {
            ProductTypeZRA productTypeZra = ((ProductTypeZRA)(pxResult));
            dictionary7[productTypeZra.Code] = productTypeZra;
          }
          foreach (PXResult<TransactionTypeZRA> pxResult in ((PXSelectBase<TransactionTypeZRA>) this.TransactionTypeZraView).Select(Array.Empty<object>()))
          {
            TransactionTypeZRA transactionTypeZra = ((TransactionTypeZRA)(pxResult));
            dictionary8[transactionTypeZra.Code] = transactionTypeZra;
          }
          foreach (PXResult<TransactionProgress> pxResult in ((PXSelectBase<TransactionProgress>) this.TransactionProgressView).Select(Array.Empty<object>()))
          {
            TransactionProgress transactionProgress = ((TransactionProgress)(pxResult));
            dictionary9[transactionProgress.Code] = transactionProgress;
          }
          foreach (PXResult<StockInOut> pxResult in ((PXSelectBase<StockInOut>) this.StockInOutView).Select(Array.Empty<object>()))
          {
            StockInOut stockInOut = ((StockInOut)(pxResult));
            dictionary10[stockInOut.Code] = stockInOut;
          }
          foreach (PXResult<SalesReceiptType> pxResult in ((PXSelectBase<SalesReceiptType>) this.SalesReceiptView).Select(Array.Empty<object>()))
          {
            SalesReceiptType salesReceiptType = ((SalesReceiptType)(pxResult));
            dictionary11[salesReceiptType.Code] = salesReceiptType;
          }
          foreach (PXResult<ImportItem> pxResult in ((PXSelectBase<ImportItem>) this.ImportView).Select(Array.Empty<object>()))
          {
            ImportItem importItem = ((ImportItem)(pxResult));
            dictionary12[importItem.Code] = importItem;
          }
          foreach (PXResult<PurchaseReceiptType> pxResult in ((PXSelectBase<PurchaseReceiptType>) this.PurchaseTypeView).Select(Array.Empty<object>()))
          {
            PurchaseReceiptType purchaseReceiptType = ((PurchaseReceiptType)(pxResult));
            dictionary13[purchaseReceiptType.Code] = purchaseReceiptType;
          }
          foreach (PXResult<RegistrationType> pxResult in ((PXSelectBase<RegistrationType>) this.RegistrationTypeView).Select(Array.Empty<object>()))
          {
            RegistrationType registrationType = ((RegistrationType)(pxResult));
            dictionary14[registrationType.Code] = registrationType;
          }
          foreach (CodesItem cls in codesResponse.kraResult.clsList)
          {
            if (cls.dtlList != null)
            {
              foreach (StandCodes dtl in cls.dtlList)
              {
                if (cls.cdClsNm == "Taxation Type")
                {
                  KRATaxCodes kraTaxCodes;
                  if (dictionary3.TryGetValue(dtl.cd, out kraTaxCodes))
                  {
                    kraTaxCodes.CodeDescription = dtl.cdNm ?? string.Empty;
                    kraTaxCodes.TaxRate = new Decimal?(dtl.userDfnCd1 != null ? Convert.ToDecimal(dtl.userDfnCd1) : 0M);
                    ((PXSelectBase<KRATaxCodes>) this.CodesEtimsView).Update(kraTaxCodes);
                  }
                  else
                    ((PXSelectBase<KRATaxCodes>) this.CodesEtimsView).Insert(new KRATaxCodes()
                    {
                      TaxCode = dtl.cd,
                      TaxRate = new Decimal?(dtl.userDfnCd1 != null ? Convert.ToDecimal(dtl.userDfnCd1) : 0M),
                      CodeDescription = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Quantity Unit")
                {
                  QuantityUnit quantityUnit;
                  if (dictionary2.TryGetValue(dtl.cd, out quantityUnit))
                  {
                    quantityUnit.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<QuantityUnit>) this.QuantityView).Update(quantityUnit);
                  }
                  else
                    ((PXSelectBase<QuantityUnit>) this.QuantityView).Insert(new QuantityUnit()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Payment Type")
                {
                  PaymentMethodKRA paymentMethodKra;
                  if (dictionary4.TryGetValue(dtl.cd, out paymentMethodKra))
                  {
                    paymentMethodKra.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<PaymentMethodKRA>) this.PaymentMethodKraView).Update(paymentMethodKra);
                  }
                  else
                    ((PXSelectBase<PaymentMethodKRA>) this.PaymentMethodKraView).Insert(new PaymentMethodKRA()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Packing Unit")
                {
                  Packaging packaging;
                  if (dictionary1.TryGetValue(dtl.cd, out packaging))
                  {
                    packaging.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<Packaging>) this.PackageView).Update(packaging);
                  }
                  else
                    ((PXSelectBase<Packaging>) this.PackageView).Insert(new Packaging()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Credit Note Reason")
                {
                  CreditReasonCode creditReasonCode;
                  if (dictionary5.TryGetValue(dtl.cd, out creditReasonCode))
                  {
                    creditReasonCode.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<CreditReasonCode>) this.CreditReasonCodeView).Update(creditReasonCode);
                  }
                  else
                    ((PXSelectBase<CreditReasonCode>) this.CreditReasonCodeView).Insert(new CreditReasonCode()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Reason for Debit note")
                {
                  DebitReasonCode debitReasonCode;
                  if (dictionary6.TryGetValue(dtl.cd, out debitReasonCode))
                  {
                    debitReasonCode.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<DebitReasonCode>) this.DebitReasonCodeView).Update(debitReasonCode);
                  }
                  else
                    ((PXSelectBase<DebitReasonCode>) this.DebitReasonCodeView).Insert(new DebitReasonCode()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Item Type")
                {
                  ProductTypeZRA productTypeZra;
                  if (dictionary7.TryGetValue(dtl.cd, out productTypeZra))
                  {
                    productTypeZra.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<ProductTypeZRA>) this.ProductTypeZraView).Update(productTypeZra);
                  }
                  else
                    ((PXSelectBase<ProductTypeZRA>) this.ProductTypeZraView).Insert(new ProductTypeZRA()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Transaction Type")
                {
                  TransactionTypeZRA transactionTypeZra;
                  if (dictionary8.TryGetValue(dtl.cd, out transactionTypeZra))
                  {
                    transactionTypeZra.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<TransactionTypeZRA>) this.TransactionTypeZraView).Update(transactionTypeZra);
                  }
                  else
                    ((PXSelectBase<TransactionTypeZRA>) this.TransactionTypeZraView).Insert(new TransactionTypeZRA()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Sale Status")
                {
                  TransactionProgress transactionProgress;
                  if (dictionary9.TryGetValue(dtl.cd, out transactionProgress))
                  {
                    transactionProgress.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<TransactionProgress>) this.TransactionProgressView).Update(transactionProgress);
                  }
                  else
                    ((PXSelectBase<TransactionProgress>) this.TransactionProgressView).Insert(new TransactionProgress()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Stock I/O Type")
                {
                  StockInOut stockInOut;
                  if (dictionary10.TryGetValue(dtl.cd, out stockInOut))
                  {
                    stockInOut.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<StockInOut>) this.StockInOutView).Update(stockInOut);
                  }
                  else
                    ((PXSelectBase<StockInOut>) this.StockInOutView).Insert(new StockInOut()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Sales Receipt Type")
                {
                  SalesReceiptType salesReceiptType;
                  if (dictionary11.TryGetValue(dtl.cd, out salesReceiptType))
                  {
                    salesReceiptType.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<SalesReceiptType>) this.SalesReceiptView).Update(salesReceiptType);
                  }
                  else
                    ((PXSelectBase<SalesReceiptType>) this.SalesReceiptView).Insert(new SalesReceiptType()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Import Item Status")
                {
                  ImportItem importItem;
                  if (dictionary12.TryGetValue(dtl.cd, out importItem))
                  {
                    importItem.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<ImportItem>) this.ImportView).Update(importItem);
                  }
                  else
                    ((PXSelectBase<ImportItem>) this.ImportView).Insert(new ImportItem()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Purchase Receipt Type")
                {
                  PurchaseReceiptType purchaseReceiptType;
                  if (dictionary13.TryGetValue(dtl.cd, out purchaseReceiptType))
                  {
                    purchaseReceiptType.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<PurchaseReceiptType>) this.PurchaseTypeView).Update(purchaseReceiptType);
                  }
                  else
                    ((PXSelectBase<PurchaseReceiptType>) this.PurchaseTypeView).Insert(new PurchaseReceiptType()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
                else if (cls.cdClsNm == "Sale category")
                {
                  SalesCategory salesCategory;
                  if (dictionary15.TryGetValue(dtl.cd, out salesCategory))
                  {
                    salesCategory.CodeName = dtl.cdNm ?? string.Empty;
                    ((PXSelectBase<SalesCategory>) this.SalesCategoryView).Update(salesCategory);
                  }
                  else
                    ((PXSelectBase<SalesCategory>) this.SalesCategoryView).Insert(new SalesCategory()
                    {
                      Code = dtl.cd,
                      CodeName = dtl.cdNm
                    });
                }
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Error sending data to eTims: " + ex.Message);
      throw new PXException("There is no search result");
    }
  }
}
