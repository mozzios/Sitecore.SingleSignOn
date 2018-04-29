using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.SingleSignOn.DataAccess.ConnectionString
{
    public static class SqlConnectionString
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
            }
        }
    }
}
