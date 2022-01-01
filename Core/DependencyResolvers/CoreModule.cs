using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddMemoryCache(); // .net'in kendisinin. .net core kendisi otomatik injection yapıyor.
            //serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6000";
            });
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
