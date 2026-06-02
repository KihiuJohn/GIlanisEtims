// Decompiled with JetBrains decompiler
// Type: eTims.RefreshToken
// Assembly: eTims, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C61D8E78-3ACD-462F-AD73-596C69F51E41
// Assembly location: C:\Users\Tact\Downloads\ETIMSV12\Bin\eTims.dll

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace eTims;

[PXCacheName("RefreshToken")]
[Serializable]
public class RefreshToken : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Pin")]
  public virtual 
  #nullable disable
  string Pin { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Username")]
  public virtual string Username { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Password")]
  public virtual string Password { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Platform")]
  public virtual string Platfrom { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Auth Url")]
  public virtual string AuthUrl { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Access Token")]
  public virtual string AccessToken { get; set; }

  public abstract class pin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.pin>
  {
  }

  public abstract class username : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.username>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.password>
  {
  }

  public abstract class platfrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.platfrom>
  {
  }

  public abstract class authUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.authUrl>
  {
  }

  public abstract class accessToken : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RefreshToken.accessToken>
  {
  }
}
