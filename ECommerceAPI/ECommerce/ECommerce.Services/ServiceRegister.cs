using ECommerce.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictionary = new Dictionary<Type, Type>()
            {
                {typeof(IDataAccessServices), typeof(DataAccessServices)},
            };
            return serviceDictionary;
        }
    }
}
