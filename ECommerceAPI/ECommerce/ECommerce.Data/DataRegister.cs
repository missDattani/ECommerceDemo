using ECommerce.Data.DBRepository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictionary = new Dictionary<Type, Type>()
            {
                {typeof(IDataAccessRepository), typeof(DataAccessRepository)},
            };
            return serviceDictionary;
        }
    }
}
