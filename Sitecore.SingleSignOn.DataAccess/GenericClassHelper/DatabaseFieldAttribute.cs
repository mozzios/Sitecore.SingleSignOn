using System;

namespace Sitecore.SingleSignOn.DataAccess.GenericClassHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DatabaseFieldAttribute : Attribute
    {
        private readonly string _name;

        public DatabaseFieldAttribute(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
