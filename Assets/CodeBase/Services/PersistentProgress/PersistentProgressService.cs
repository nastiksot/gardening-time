using CodeBase.Data;

namespace CodeBase.PersistentProgress.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}