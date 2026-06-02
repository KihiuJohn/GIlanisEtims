// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSetupExtEtims
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.AR;

public class ARSetupExtEtims : PXCacheExtension<
#nullable disable
ARSetup>
{
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string UsrEtimsNumberingSequence { get; set; }

  [PXDBString(10)]
  [PXUIField]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string UsrEtimsMemoNumberingSeq { get; set; }

  public abstract class usrEtimsNumberingSequence : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetupExtEtims.usrEtimsNumberingSequence>
  {
  }

  public abstract class usrEtimsMemoNumberingSeq : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSetupExtEtims.usrEtimsMemoNumberingSeq>
  {
  }
}
