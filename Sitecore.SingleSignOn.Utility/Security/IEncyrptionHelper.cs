using Sitecore.SingleSignOn.Utility.Enums;

namespace Sitecore.SingleSignOn.Utility.Security
{
    public interface IEncryptionHelper
    {
        string EncryptString(string text, EncryptionTypeEnums type);
        string DecryptString(string text, EncryptionTypeEnums type);
    }
}
