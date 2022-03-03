using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.DataContract.Entities.Enum
{
    public class CommonEnums
    {
        public enum AuditActivityEnum
        {
            Category=1,
            CategoryAll = 2,
            CategoryByID = 3,
            CategoryByName = 4,
            Product,
            ProductAll,
            ProductByID,
            ProductByCategory,
            ProductByName,
            Order,
            Inventory,
            shiping,

        }
    }
}
