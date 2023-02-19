namespace CodeBase.Infrastructure.Services
{
    public class ServiceLocator
    {
        static ServiceLocator s_Instance;
        public static ServiceLocator Container => s_Instance ?? (s_Instance = new ServiceLocator());

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