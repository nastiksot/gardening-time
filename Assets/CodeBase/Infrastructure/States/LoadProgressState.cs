using CodeBase.Data;
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
            progress.state.inventoryPlants.Add(new PlantData(type: 0, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 1, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type:2, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 3, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 4, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 5, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 6, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 7, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 8, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 9, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 10, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 11, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 12, count: 1)); 
            progress.state.inventoryPlants.Add(new PlantData(type: 13, count: 1)); 
            return progress;
        }
    }
}