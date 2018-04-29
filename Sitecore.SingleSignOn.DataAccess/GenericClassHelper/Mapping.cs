using System;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Sitecore.SingleSignOn.DataAccess.GenericClassHelper
{
    public static class Mapping
    {
        public static TEntity ReflectType<TEntity>(IDataRecord dr) where TEntity : class, new()
        {
            TEntity instanceToPopulate = new TEntity();

            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties
            (BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pi in propertyInfos)
            {
                DatabaseFieldAttribute[] datafieldAttributeArray = pi.GetCustomAttributes
                (typeof(DatabaseFieldAttribute), false) as DatabaseFieldAttribute[];

                if (datafieldAttributeArray != null && datafieldAttributeArray.Length == 1)
                {
                    DatabaseFieldAttribute dfa = datafieldAttributeArray[0];

                    object dbValue = dr[dfa.Name];

                    if (dbValue != null)
                    {
                        pi.SetValue(instanceToPopulate, Convert.ChangeType
                        (dbValue, pi.PropertyType, CultureInfo.InvariantCulture), null);
                    }
                }
            }

            return instanceToPopulate;
        }
    }
}
