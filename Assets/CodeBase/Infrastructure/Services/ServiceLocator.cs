namespace CodeBase.Infrastructure.Services
{
    public class ServiceLocator
    {
        static ServiceLocator _instance;
        public static ServiceLocator Container => _instance ?? (_instance = new ServiceLocator());

        public void RegisterSingle<TService>(TService implementation)
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>()
        {
            return Implementation<TService>.ServiceInstance;
        }

        static class Implementation<TService>
        {
            public static TService ServiceInstance;
        }
    }
}