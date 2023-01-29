using CodeBase.Data;
using CodeBase.Inventory;
using CodeBase.PersistentProgress.Services;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        readonly GameStateMachine m_StateMachine;
        readonly IPersistentProgressService m_ProgressService;
        readonly ISaveLoadService m_SaveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            m_StateMachine = stateMachine;
            m_ProgressService = progressService;
            m_SaveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            m_StateMachine.Enter<LoadLevelState, string>("Game");
        }

        public void Exit()
        {
        }

        void LoadProgressOrInitNew()
        {
            m_ProgressService.Progress = m_SaveLoadService.LoadProgress() ?? NewProgress();
        }

        PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(state: new State(coins: 0));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Apple, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Rose, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Cactus, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Bonsai, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Pumpkin, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Orange, count: 1));
            progress.state.inventoryPlants.Add(new PlantData(type: PlantType.Plum, count: 1));
            progress.mugsData.Add(new MugsData(guid: "Game_1d07a76a-b413-4cdb-8835-f3679f7e2d3f", plantData: new PlantData(count: 1, type: PlantType.Orange)));
            return progress;
        }
    }
}