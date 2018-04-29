using System.Runtime.Caching;

namespace Sitecore.SingleSignOn.BusinessLogic.Caching
{
    public static class CacheHelper
    {
        private static MemoryCache _cache = null;

        public static MemoryCache MemberCache
        {
            get
            {
                if (_cache == null)
                    _cache = new MemoryCache("MemberCache");

                return _cache;
            }
        }
    }
}