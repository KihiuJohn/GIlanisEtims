using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace eTims;

public class DeviceInitialiseExtEtims : PXCacheExtension<DeviceInitialise>
{
  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Server Url")]
  public virtual string UsrServerUrl { get; set; }
  public abstract class usrServerUrl : BqlType<IBqlString, string>.Field<usrServerUrl> { }
}
