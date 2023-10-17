using ECommerce.Data;
using ECommerce.Services;

namespace ECommerce
{
    public static class RegisterServices
    {
        public static void RegisterService(this IServiceCollection services)
        {
            Configure(services, DataRegister.GetTypes());
            Configure(services, ServiceRegister.GetTypes());
        }
        public static void Configure(IServiceCollection services, Dictionary<Type, Type> dictionary)
        {
            foreach (var type in dictionary)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
