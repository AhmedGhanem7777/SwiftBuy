using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Infrastructure.Persistence._Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbContextTypeAttribute : Attribute
    {
        public Type DbContextType { get; }
        public DbContextTypeAttribute(Type dbContextType)
        {
            DbContextType = dbContextType;
        }
    }
}
