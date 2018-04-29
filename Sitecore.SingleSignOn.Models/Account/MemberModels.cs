using System;

namespace Sitecore.SingleSignOn.Models.Account
{
    public class MemberModels
    {
        public Guid MemberId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int LoginMethod { get; set; }
    }
}
