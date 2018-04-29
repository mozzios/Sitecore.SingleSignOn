using Sitecore.SingleSignOn.DataAccess.GenericClassHelper;
using System;

namespace Sitecore.SingleSignOn.DataAccess.DatabaseModels
{
    public class Member
    {
        [DatabaseField("Id")]
        public Guid Id { get; set; }
        [DatabaseField("Fullname")]
        public string Fullname { get; set; }
        [DatabaseField("Username")]
        public string Username { get; set; }
        [DatabaseField("Password")]
        public string Password { get; set; }
        [DatabaseField("CreatedAt")]
        public string CreatedAt { get; set; }
        [DatabaseField("UpdatedAt")]
        public string UpdatedAt { get; set; }
    }
}
