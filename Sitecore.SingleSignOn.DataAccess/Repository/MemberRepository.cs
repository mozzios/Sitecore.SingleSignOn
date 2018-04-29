using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Sitecore.SingleSignOn.DataAccess.ConnectionString;
using Sitecore.SingleSignOn.DataAccess.DatabaseModels;
using Sitecore.SingleSignOn.DataAccess.GenericClassHelper;
using Sitecore.SingleSignOn.DataAccess.SqlHelper;
using Sitecore.SingleSignOn.Models.Account;

namespace Sitecore.SingleSignOn.DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public MemberModels GetByUsername(string username)
        {
            MemberModels result = null;
            DataManager dM = new DataManager();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Username", username));

            using (SqlDataReader sqlData = dM.ExecuteReader(Sql.SqlResource.GetMemberByUsername, parameters))
            {
                if (sqlData.HasRows)
                {
                    sqlData.Read();
                    Member member = Mapping.ReflectType<Member>(sqlData);
                    result = ModelConversion.GetMemberModels(member);
                }
            }

            return result;
        }

        public bool Insert(MemberModels member)
        {
            DataManager dM = new DataManager();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("Id", Guid.NewGuid()));
            parameters.Add(new SqlParameter("Fullname", member.Fullname));
            parameters.Add(new SqlParameter("Username", member.Username));
            parameters.Add(new SqlParameter("Password", member.Password));
            parameters.Add(new SqlParameter("CreatedAt", DateTime.Now));
            parameters.Add(new SqlParameter("UpdatedAt", DateTime.Now));

            return dM.ExecuteNonQuery(Sql.SqlResource.InsertMember, parameters) > 0;
        }
    }
}
