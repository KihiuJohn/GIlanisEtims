// Decompiled with JetBrains decompiler
// Type: eTims.EtimsSetupMaint
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Objects.AR;
using PX.Objects.IN;
using System;

#nullable disable
namespace eTims;

public class EtimsSetupMaint : PXGraph<EtimsSetupMaint>
{
  public PXSave<EtimsSetup> Save;
  public PXCancel<EtimsSetup> Cancel;
  public PXSelect<EtimsSetup> SetupWizard;
  public PXAction<EtimsSetup> PreviousStep;
  public PXAction<EtimsSetup> NextStep;

  public EtimsSetupMaint()
  {
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    int num1;
    if (current != null)
    {
      int? stepIndex = current.StepIndex;
      if (stepIndex.HasValue)
      {
        stepIndex = current.StepIndex;
        int num2 = 0;
        num1 = stepIndex.GetValueOrDefault() == num2 & stepIndex.HasValue ? 1 : 0;
      }
      else
        num1 = 1;
    }
    else
      num1 = 0;
    if (num1 == 0)
      return;
    current.StepIndex = new int?(1);
  }

  [PXButton]
  [PXUIField(DisplayName = "Previous Step", Enabled = true)]
  protected void previousStep()
  {
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    int num1 = 1;
    int? stepIndex1 = current.StepIndex;
    int num2 = num1;
    if (stepIndex1.GetValueOrDefault() > num2 & stepIndex1.HasValue)
    {
      int? stepIndex2 = current.StepIndex;
      switch (stepIndex2.Value)
      {
        case 1:
          this.NavigateToDeviceInitialize();
          if (current.IsDeviceInitialized.GetValueOrDefault())
          {
            if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Device Initialization", "eTIMS device has already been configured successfully. Return to the previous step?", (MessageButtons) 1) != (WebDialogResult) 1)
              return;
            EtimsSetup etimsSetup = current;
            stepIndex2 = etimsSetup.StepIndex;
            int? nullable = stepIndex2;
            etimsSetup.StepIndex = nullable.HasValue ? new int?(nullable.GetValueOrDefault() - 1) : new int?();
            return;
          }
          break;
        case 2:
          this.NavigateToTaxCode(false);
          break;
        case 3:
          this.FetchClassCode(false);
          break;
        case 4:
          this.MapClassCodeWithItemClass(false);
          break;
        case 5:
          this.MapClassCodeWithInventoryItem(false);
          break;
        case 6:
          this.UploadPackagingUnit(false);
          break;
        case 7:
          this.UploadUnitOfQuantity(false);
          break;
        case 8:
          this.MapAcumaticaUOMs(false);
          break;
        case 9:
          this.UpdateStockAndNonstockItems(false);
          break;
        case 10:
          this.ConfigureEtimsNumSequence(false);
          break;
        default:
          return;
      }
    }
    ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
    ((PXGraph) this).Actions.PressSave();
  }

  [PXButton]
  [PXUIField(DisplayName = "Next Step", Enabled = true)]
  protected void nextStep()
  {
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    int length = Enum.GetValues(typeof (EtimsSetupMaint.SetupSteps)).Length;
    int? stepIndex = current.StepIndex;
    int num = length;
    if (stepIndex.GetValueOrDefault() < num & stepIndex.HasValue)
    {
      switch (current.StepIndex.Value)
      {
        case 1:
          this.NavigateToDeviceInitialize();
          break;
        case 2:
          this.NavigateToTaxCode(true);
          break;
        case 3:
          this.FetchClassCode(true);
          break;
        case 4:
          this.MapClassCodeWithItemClass(true);
          break;
        case 5:
          this.MapClassCodeWithInventoryItem(true);
          break;
        case 6:
          this.UploadPackagingUnit(true);
          break;
        case 7:
          this.UploadUnitOfQuantity(true);
          break;
        case 8:
          this.MapAcumaticaUOMs(true);
          break;
        case 9:
          this.UpdateStockAndNonstockItems(true);
          break;
        case 10:
          this.ConfigureEtimsNumSequence(true);
          break;
        default:
          return;
      }
    }
    ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
    ((PXGraph) this).Actions.PressSave();
  }

  protected void NavigateToDeviceInitialize()
  {
    int count = PXSelectBase<DeviceInfo, PXSelect<DeviceInfo>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (count > 0)
    {
      current.IsDeviceInitialized = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Device Initialization", "eTIMS device has already been configured successfully. Continue to the next step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + 1) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsDeviceInitialized = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Device Initialization", "Initialize eTIMS device", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<DeviceInitializeMaint>(), true, "Device Initialize");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void NavigateToTaxCode(bool isNextStep)
  {
    PXResultset<TaxCdes> pxResultset = PXSelectBase<TaxCdes, PXSelect<TaxCdes>.Config>.Select((PXGraph) this, Array.Empty<object>());
    bool flag = false;
    foreach (PXResult<TaxCdes> pxResult in pxResultset)
    {
      TaxCdes taxCdes = ((TaxCdes)(pxResult));
      if (!string.IsNullOrEmpty(taxCdes.TaxCode) && !string.IsNullOrEmpty(taxCdes.TaxCategories))
        flag = true;
    }
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (flag)
    {
      current.IsTaxCodeConfigured = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Tax Codes", isNextStep ? "eTIMS tax code have already been configured successfully. Continue to the next step?" : "eTIMS tax code have already been configured successfully. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsTaxCodeConfigured = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Configure Acumatica tax categories with eTIMS KRA codes", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<TaxCodeMaint>(), true, "Kra tax codes");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void FetchClassCode(bool isNextStep)
  {
    int count = PXSelectBase<ItemClassificationCodees, PXSelect<ItemClassificationCodees>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (count > 0)
    {
      current.IsClassCodeFetched = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Item Classification Codes", isNextStep ? "item Classification codes have already been configured successfully. Continue to the next step?" : "item Classification codes have already been configured successfully. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsClassCodeFetched = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Fetch item classification codes from eTIMS server", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<ClassificationCodesMaint>(), true, "item class codes");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void MapClassCodeWithItemClass(bool isNextStep)
  {
    PXResultset<ItemClasses> pxResultset = PXSelectBase<ItemClasses, PXSelect<ItemClasses>.Config>.Select((PXGraph) this, Array.Empty<object>());
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    bool flag = true;
    foreach (PXResult<ItemClasses> pxResult in pxResultset)
    {
      ItemClasses itemClasses = ((ItemClasses)(pxResult));
      if (string.IsNullOrEmpty(itemClasses.ItemClass) || string.IsNullOrEmpty(itemClasses.ItemClassificationCode) || string.IsNullOrEmpty(itemClasses.ItemClassDescr))
      {
        PXTrace.WriteError($"Item Class record with ID {itemClasses.ItemClass} has empty required fields.");
        flag = false;
      }
    }
    if (flag)
    {
      current.IsClassCodeMapped = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Mapping Item class with Item Classification Codes", isNextStep ? "All item classes have been mapped successfully with KRA item Classification codes. Continue to the next step?" : "All item classes have been mapped successfully with KRA item Classification codes. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Map KRA Item Classification code with Item classes", (MessageButtons) 1) == (WebDialogResult) 1)
    {
      current.IsClassCodeMapped = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      ((PXGraph) this).Actions.PressSave();
      ((PXSelectBase) this.SetupWizard).Cache.Clear();
      throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<ItemClassesMaint>(), true, "Map Class Code with Item Classes");
    }
  }

  protected void MapClassCodeWithInventoryItem(bool isNextStep)
  {
    PXResultset<InventoryClassification> pxResultset = PXSelectBase<InventoryClassification, PXSelect<InventoryClassification>.Config>.Select((PXGraph) this, Array.Empty<object>());
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    bool flag = true;
    foreach (PXResult<InventoryClassification> pxResult in pxResultset)
    {
      InventoryClassification inventoryClassification = ((InventoryClassification)(pxResult));
      if (string.IsNullOrEmpty(inventoryClassification.InventoryId) || string.IsNullOrEmpty(inventoryClassification.ItemClassificationCode) || string.IsNullOrEmpty(inventoryClassification.Description))
      {
        PXTrace.WriteError($"Inventory item record with ID {inventoryClassification.InventoryId} has empty required fields.");
        flag = false;
      }
    }
    if (flag)
    {
      current.IsClassCodeMappedWithInventory = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Mapping Inventory Item with Item Classification Codes", isNextStep ? "All inventory items have been mapped successfully with KRA item Classification codes. Continue to the next step?" : "All inventory items have been mapped successfully with KRA item Classification codes. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsClassCodeMappedWithInventory = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Map KRA Item Classification code with Inventory Item", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<InventoryClassificationMaint>(), true, "Map Class Code with Inventory Item");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void UploadPackagingUnit(bool isNextStep)
  {
    int count = PXSelectBase<Packaging, PXSelect<Packaging>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (count > 0)
    {
      current.IsPackagingUnitUploaded = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Packaging Unit", isNextStep ? "KRA packaging unit have been uploaded successfully. Continue to the next step?" : "KRA packaging unit have been uploaded successfully. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsPackagingUnitUploaded = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Upload KRA packaging unit to Acumatica", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<PackageMaint>(), true, "Upload Packaging Unit");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void UploadUnitOfQuantity(bool isNextStep)
  {
    int count = PXSelectBase<QuantityUnit, PXSelect<QuantityUnit>.Config>.Select((PXGraph) this, Array.Empty<object>()).Count;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (count > 0)
    {
      current.IsUnitOfQuantityUploaded = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Unit Of Quantity", isNextStep ? "KRA quantity unit have been uploaded successfully. Continue to the next step?" : "KRA quantity unit have been uploaded successfully. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsUnitOfQuantityUploaded = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Upload KRA quantity unit to Acumatica", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<QuantityMaint>(), true, "Upload Unit of Quantity");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void MapAcumaticaUOMs(bool isNextStep)
  {
    PXResultset<UomMapping> pxResultset = PXSelectBase<UomMapping, PXSelect<UomMapping>.Config>.Select((PXGraph) this, Array.Empty<object>());
    bool flag = true;
    foreach (PXResult<UomMapping> pxResult in pxResultset)
    {
      UomMapping uomMapping = ((UomMapping)(pxResult));
      if (string.IsNullOrEmpty(uomMapping.UnitOfMeasure) || string.IsNullOrEmpty(uomMapping.PackagingUnit) || string.IsNullOrEmpty(uomMapping.UnitOfQuantity))
      {
        PXTrace.WriteError($"UOM record with unit of measure {uomMapping.UnitOfMeasure} has empty required fields.");
        flag = false;
      }
    }
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (flag)
    {
      current.IsUomsMapped = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Mapping UOMs with eTIMS UOMs", isNextStep ? "All Acumatica UOMs have been mapped successfully with KRA UOMs. Continue to the next step?" : "All Acumatica UOMs have been mapped successfully with KRA UOMs. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsUomsMapped = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Map Acumatica UOMs with eTIMS KRA UOMs ", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<UnitMappingMaint>(), true, "Map eTIMS UOMs with Acumatica UOMs");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void UpdateStockAndNonstockItems(bool isNextStep)
  {
    PXResultset<InventoryItem> pxResultset = PXSelectBase<InventoryItem, PXSelect<InventoryItem, Where<InventoryItemExtEtims.usrTaxCodes, IsNull, And<InventoryItem.itemClassID, IsNotNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    bool flag = pxResultset == null || pxResultset.Count == 0;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (flag)
    {
      current.IsStockItemUpdated = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask(" Tax Codes and Item Codes on the Inventory Items", isNextStep ? "Tax codes and Item codes for all Inventory items have been configured successfully. Continue to the next step?" : "Tax codes and Item codes for all Inventory items have been configured successfully. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsStockItemUpdated = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("Update tax code and item code for Stock and Nonstock Items ", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<ItemsMaint>(), true, "Update Stock and Nonstock Items");
      ((PXGraph) this).Actions.PressSave();
    }
  }

  protected void ConfigureEtimsNumSequence(bool isNextStep)
  {
    ARSetup arSetup = ((ARSetup)(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    if (arSetup == null)
      throw new PXException("ARSetup record not found. Please ensure ARSetup is configured correctly.");
    string numberingSequence = PXCacheEx.GetExtension<ARSetupExtEtims>((IBqlTable) arSetup)?.UsrEtimsNumberingSequence;
    EtimsSetup current = ((PXSelectBase<EtimsSetup>) this.SetupWizard).Current;
    if (current == null)
      return;
    if (string.IsNullOrEmpty(numberingSequence))
    {
      current.IsEtimsNumbSeqSet = new bool?(false);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("The eTIMS Numbering Sequence in ARSetup is not configured. Please configure it before proceeding.", (MessageButtons) 1) == (WebDialogResult) 1)
        throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<ARSetupMaint>(), true, "configure etims numbering sequence field");
      ((PXGraph) this).Actions.PressSave();
    }
    else
    {
      current.IsEtimsNumbSeqSet = new bool?(true);
      ((PXSelectBase<EtimsSetup>) this.SetupWizard).Update(current);
      if (((PXSelectBase<EtimsSetup>) this.SetupWizard).Ask("eTIMS Numbering Sequence", isNextStep ? "The eTIMS Numbering Sequence in ARSetup is already configured. Continue to the next step?" : "The eTIMS Numbering Sequence in ARSetup is already configured. Return to the previous step?", (MessageButtons) 1) == (WebDialogResult) 1)
      {
        EtimsSetup etimsSetup = current;
        int? stepIndex = etimsSetup.StepIndex;
        int num = isNextStep ? 1 : -1;
        etimsSetup.StepIndex = stepIndex.HasValue ? new int?(stepIndex.GetValueOrDefault() + num) : new int?();
      }
      ((PXGraph) this).Actions.PressSave();
    }
  }

  public enum SetupSteps
  {
    DeviceInitialize = 1,
    TaxCode = 2,
    FetchClassCode = 3,
    MapClassCodeWithItemClass = 4,
    MapClassCodeWithInventoryItem = 5,
    UploadPackagingUnit = 6,
    UploadUnitOfQuantity = 7,
    MapAcumaticaUOMs = 8,
    UpdateStockAndNonstockItems = 9,
    ConfigureEtimsNumSequence = 10, // 0x0000000A
  }
}
