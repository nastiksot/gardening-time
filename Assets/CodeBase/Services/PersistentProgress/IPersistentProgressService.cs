using CodeBase.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.PersistentProgress.Services
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}