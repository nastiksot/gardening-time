using CodeBase.Data;
using CodeBase.Inventory;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadLevelState, string>("Game");
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(state: new State(coins: 0));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Apple));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Rose));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Cactus));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Bonsai));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Pumpkin));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Orange));
            progress.state.inventoryPlants.Add(new Plant(count: 1, type: PlantType.Plum));
            return progress;
        }
    }
}