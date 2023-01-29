using CodeBase.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService: IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}