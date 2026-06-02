// Decompiled with JetBrains decompiler
// Type: eTims.ProductTypeZRA
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("ProductTypeZRA")]
[Serializable]
public class ProductTypeZRA : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? Id { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code")]
  public virtual 
  #nullable disable
  string Code { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Name")]
  public virtual string CodeName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Code Description")]
  public virtual string CodeDescription { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ProductTypeZRA.id>
  {
  }

  public abstract class code : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProductTypeZRA.code>
  {
  }

  public abstract class codeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ProductTypeZRA.codeName>
  {
  }

  public abstract class codeDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ProductTypeZRA.codeDescription>
  {
  }
}
