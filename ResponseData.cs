// Decompiled with JetBrains decompiler
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace eTims;

public class ReceiptEtims
{
  public string custTin { get; set; }
  public string custMblNo { get; set; }
  public string rcptPbctDt { get; set; }
  public string trdeNm { get; set; }
  public string adrs { get; set; }
  public string topMsg { get; set; }
  public string btmMsg { get; set; }
  public string prchrAcptcYn { get; set; }
  public int rptNo { get; set; }
}

public class Item
{
  public int itemSeq { get; set; }
  public int itemId { get; set; }
  public string itemCd { get; set; }
  public string itemClsCd { get; set; }
  public string itemNm { get; set; }
  public string itemNmDef { get; set; }
  public string bcd { get; set; }
  public string pkgUnitCd { get; set; }
  public Decimal pkg { get; set; }
  public string qtyUnitCd { get; set; }
  public Decimal qty { get; set; }
  public Decimal prc { get; set; }
  public Decimal splyAmt { get; set; }
  public Decimal dcRt { get; set; }
  public Decimal dcAmt { get; set; }
  public string taxTyCd { get; set; }
  public Decimal taxblAmt { get; set; }
  public Decimal taxAmt { get; set; }
  public Decimal totAmt { get; set; }
}

public class RequestData
{
  public string tin { get; set; }
  public string bhfId { get; set; }
  public string trdInvcNo { get; set; }
  public int invcNo { get; set; }
  public string custType { get; set; }
  public string custMblNo { get; set; }
  public string custID { get; set; }
  public string custEmail { get; set; }
  public int orgInvcNo { get; set; }
  public string custTin { get; set; }
  public string custNm { get; set; }
  public string salesTyCd { get; set; }
  public string rcptTyCd { get; set; }
  public string pmtTyCd { get; set; }
  public string salesSttsCd { get; set; }
  public string cfmDt { get; set; }
  public string salesDt { get; set; }
  public string stockRlsDt { get; set; }
  public string cnclReqDt { get; set; }
  public string cnclDt { get; set; }
  public string rfdDt { get; set; }
  public string rfdRsnCd { get; set; }
  public string dbtRsnCd { get; set; }
  public int totItemCnt { get; set; }
  public Decimal taxblAmtA { get; set; }
  public Decimal taxblAmtB { get; set; }
  public Decimal taxblAmtC { get; set; }
  public Decimal taxblAmtD { get; set; }
  public Decimal taxblAmtE { get; set; }
  public Decimal taxRtA { get; set; }
  public Decimal taxRtB { get; set; }
  public Decimal taxRtC { get; set; }
  public Decimal taxRtD { get; set; }
  public Decimal taxRtE { get; set; }
  public Decimal taxAmtA { get; set; }
  public Decimal taxAmtB { get; set; }
  public Decimal taxAmtC { get; set; }
  public Decimal taxAmtD { get; set; }
  public Decimal taxAmtE { get; set; }
  public Decimal cashDcRt { get; set; }
  public Decimal cashDcAmt { get; set; }
  public Decimal totTaxblAmt { get; set; }
  public Decimal totTaxAmt { get; set; }
  public Decimal totAmt { get; set; }
  public string prchrAcptcYn { get; set; }
  public string remark { get; set; }
  public string regrId { get; set; }
  public string regrNm { get; set; }
  public string modrId { get; set; }
  public string modrNm { get; set; }
  public ReceiptEtims receipt { get; set; }
  public List<Item> itemList { get; set; }
}

public class InitializeData
{
  public string tpin { get; set; }
  public string bhfId { get; set; }
  public string dvcSrlNo { get; set; }
}

public class InitilizeResponseData
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public InitializeResData data { get; set; }
}

public class InitializeResData
{
  public Info info { get; set; }
}

public class Info
{
  public string tin { get; set; }
  public string taxprNm { get; set; }
  public string bhfId { get; set; }
  public string dvcId { get; set; }
  public string sdcId { get; set; }
  public string mrcNo { get; set; }
  public string cmcKey { get; set; }
}

public class ResponseData
{
  public bool status { get; set; }
  public string message { get; set; }
  public KraResult kraResult { get; set; }
  public SaleRes sale { get; set; }
  public ExistSaleRes existingSale { get; set; }
  public ExistCreditRes creditNote { get; set; }
  public ExistCreditResponse credit { get; set; }
}

public class KraResult
{
  public string message { get; set; }
  public KraResultData result { get; set; }
}

public class KraResultData
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public string resultDt { get; set; }
  public string long_url { get; set; }
  public int rcptNo { get; set; }
  public int totRcptNo { get; set; }
  public string intrlData { get; set; }
  public string rcptSign { get; set; }
  public string vsdcRcptPbctDate { get; set; }
  public string sdcId { get; set; }
  public string mrcNo { get; set; }
}

public class SaleRes
{
  public int invcNo { get; set; }
  public int? orgInvcNo { get; set; }
}

public class ExistSaleRes
{
  public int invcNo { get; set; }
  public int? orgInvcNo { get; set; }
  public string cfmDt { get; set; }
  public string intrlData { get; set; }
  public string rcptSign { get; set; }
  public string long_url { get; set; }
}

public class ExistCreditRes
{
  public int invcNo { get; set; }
  public int? orgInvcNo { get; set; }
  public string cfmDt { get; set; }
  public string intrlData { get; set; }
  public string rcptSign { get; set; }
  public string long_url { get; set; }
}

public class ExistCreditResponse
{
  public int invcNo { get; set; }
  public int? orgInvcNo { get; set; }
  public string cfmDt { get; set; }
  public string intrlData { get; set; }
  public string rcptSign { get; set; }
  public string long_url { get; set; }
}

public class CodesResponse
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public string resultDt { get; set; }
  public CodesData kraResult { get; set; }
}

public class CodesData
{
  public List<CodesItem> clsList { get; set; }
}

public class CodesItem
{
  public string cdCls { get; set; }
  public string cdClsNm { get; set; }
  public string userDfnNm1 { get; set; }
  public List<StandCodes> dtlList { get; set; }
}

public class StandCodes
{
  public string cd { get; set; }
  public string cdNm { get; set; }
  public string userDfnCd1 { get; set; }
}

public class EtimsResponse
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public string resultDt { get; set; }
  public EtimsData kraResult { get; set; }
}

public class EtimsData
{
  public List<EtimsItem> itemClsList { get; set; }
}

public class EtimsItem
{
  public string itemClsCd { get; set; }
  public string itemClsNm { get; set; }
  public int itemClsLvl { get; set; }
  public string taxTyCd { get; set; }
  public string mjrTgYn { get; set; }
  public string useYn { get; set; }
}

public class ItemPayload
{
  public string tpin { get; set; }
  public string bhfId { get; set; }
  public string itemCd { get; set; }
  public string itemCdDf { get; set; }
  public string itemClsCd { get; set; }
  public string itemTyCd { get; set; }
  public string itemNm { get; set; }
  public string itemStdNm { get; set; }
  public string addInfo { get; set; }
  public string bcd { get; set; }
  public string btchNo { get; set; }
  public string orgnNatCd { get; set; }
  public string pkgUnitCd { get; set; }
  public string qtyUnitCd { get; set; }
  public string taxTyCd { get; set; }
  public Decimal dftPrc { get; set; }
  public Decimal grpPrcL1 { get; set; }
  public Decimal grpPrcL2 { get; set; }
  public Decimal grpPrcL3 { get; set; }
  public Decimal grpPrcL4 { get; set; }
  public Decimal currentStock { get; set; }
  public string isrcAplcbYn { get; set; }
  public string useYn { get; set; }
  public string regrId { get; set; }
  public string regrNm { get; set; }
  public string modrId { get; set; }
  public string modrNm { get; set; }
  public string exciseTxCatCd { get; set; }
  public string vatCatCd { get; set; }
  public string iplCatCd { get; set; }
  public string tlCatCd { get; set; }
}

public class TokenResponse
{
  public string access_token { get; set; }
  public int expires_in { get; set; }
}

public class EtimsItemsRes
{
  public bool status { get; set; }
  public string resultMsg { get; set; }
  public string resultCd { get; set; }
  public ItemData item { get; set; }
}

public class ItemData
{
  public int id { get; set; }
}

public class ImportRes
{
  public string resultMsg { get; set; }
  public string resultCd { get; set; }
}

public class SaleResponse
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public string resultDt { get; set; }
  public SaleData data { get; set; }
}

public class SaleData
{
  public List<Sale> saleList { get; set; }
}

public class Sale
{
  public string spplrTpin { get; set; }
  public string spplrNm { get; set; }
  public string spplrBhfId { get; set; }
  public int spplrInvcNo { get; set; }
  public string rcptTyCd { get; set; }
  public string pmtTyCd { get; set; }
  public string cfmDt { get; set; }
  public string salesDt { get; set; }
  public string stockRlsDt { get; set; }
  public int totItemCnt { get; set; }
  public Decimal totTaxblAmt { get; set; }
  public Decimal totTaxAmt { get; set; }
  public Decimal totAmt { get; set; }
  public string remark { get; set; }
  public List<SaleItem> itemList { get; set; }
}

public class SaleItem
{
  public int itemSeq { get; set; }
  public string itemCd { get; set; }
  public string itemClsCd { get; set; }
  public string itemNm { get; set; }
  public string bcd { get; set; }
  public string pkgUnitCd { get; set; }
  public Decimal pkg { get; set; }
  public string qtyUnitCd { get; set; }
  public Decimal qty { get; set; }
  public Decimal prc { get; set; }
  public Decimal splyAmt { get; set; }
  public Decimal dcRt { get; set; }
  public Decimal dcAmt { get; set; }
  public string vatCatCd { get; set; }
  public string iplCatCd { get; set; }
  public string tlCatCd { get; set; }
  public string exciseTxCatCd { get; set; }
  public Decimal vatTaxblAmt { get; set; }
  public Decimal exciseTaxblAmt { get; set; }
  public Decimal iplTaxblAmt { get; set; }
  public Decimal tlTaxblAmt { get; set; }
  public Decimal taxblAmt { get; set; }
  public Decimal vatAmt { get; set; }
  public Decimal iplAmt { get; set; }
  public Decimal tlAmt { get; set; }
  public Decimal exciseTxAmt { get; set; }
  public Decimal totAmt { get; set; }
}

public class PurchasesData
{
  public string tpin { get; set; }
  public string bhfId { get; set; }
  public string cisInvcNo { get; set; }
  public int orgInvcNo { get; set; }
  public string spplrTpin { get; set; }
  public string spplrNm { get; set; }
  public string spplrInvcNo { get; set; }
  public string regTyCd { get; set; }
  public string pchsTyCd { get; set; }
  public string rcptTyCd { get; set; }
  public string pmtTyCd { get; set; }
  public string pchsSttsCd { get; set; }
  public string cfmDt { get; set; }
  public string pchsDt { get; set; }
  public int totItemCnt { get; set; }
  public Decimal totTaxblAmt { get; set; }
  public Decimal totTaxAmt { get; set; }
  public Decimal totAmt { get; set; }
  public string remark { get; set; }
  public string regrId { get; set; }
  public string regrNm { get; set; }
  public string modrId { get; set; }
  public string modrNm { get; set; }
  public List<PurchaseItems> itemList { get; set; }
}

public class PurchaseItems
{
  public int itemSeq { get; set; }
  public string itemCd { get; set; }
  public string itemClsCd { get; set; }
  public string itemNm { get; set; }
  public string pkgUnitCd { get; set; }
  public Decimal pkg { get; set; }
  public string qtyUnitCd { get; set; }
  public Decimal qty { get; set; }
  public Decimal prc { get; set; }
  public Decimal splyAmt { get; set; }
  public Decimal dcRt { get; set; }
  public Decimal dcAmt { get; set; }
  public string vatCatCd { get; set; }
  public Decimal taxblAmt { get; set; }
  public Decimal taxAmt { get; set; }
  public Decimal vatTaxblAmt { get; set; }
  public Decimal vatAmt { get; set; }
  public Decimal totAmt { get; set; }
  public string exciseTxCatCd { get; set; }
  public string tlCatCd { get; set; }
  public string iplCatCd { get; set; }
  public string isrccCd { get; set; }
  public string isrccNm { get; set; }
  public int isrcRt { get; set; }
  public int isrcAmt { get; set; }
  public int exciseTaxblAmt { get; set; }
  public int tlTaxblAmt { get; set; }
  public int iplTaxblAmt { get; set; }
  public int iplAmt { get; set; }
  public int tlAmt { get; set; }
  public int exciseTxAmt { get; set; }
}

public class ZRAExport
{
  public string resultCd { get; set; }
  public string resultMsg { get; set; }
  public string resultDt { get; set; }
  public ExportData data { get; set; }
}

public class ExportData
{
  public List<ExportItems> itemList { get; set; }
}

public class ExportItems
{
  public string taskCd { get; set; }
  public string dclDe { get; set; }
  public int itemSeq { get; set; }
  public string dclNo { get; set; }
  public string hsCd { get; set; }
  public string itemNm { get; set; }
  public string imptItemsttsCd { get; set; }
  public string orgnNatCd { get; set; }
  public string exptNatCd { get; set; }
  public Decimal pkg { get; set; }
  public string pkgUnitCd { get; set; }
  public Decimal qty { get; set; }
  public string qtyUnitCd { get; set; }
  public Decimal totWt { get; set; }
  public Decimal netWt { get; set; }
  public string spplrNm { get; set; }
  public string agntNm { get; set; }
  public Decimal invcFcurAmt { get; set; }
  public string invcFcurCd { get; set; }
  public Decimal invcFcurExcrt { get; set; }
  public string dclRefNum { get; set; }
}

public class Adjustment
{
  public string reason { get; set; }
  public string reasonId { get; set; }
  public int itemId { get; set; }
  public Decimal qty { get; set; }
}
