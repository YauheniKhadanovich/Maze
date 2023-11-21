using System;
using Features.CameraManagement;
using Features.CameraManagement.Impl;
using Features.MazeManagement;
using Features.MazeManagement.Impl;
using Features.Player.Impl;
using Features.UI.Presenters;
using Features.UI.Presenters.Impl;
using Features.UI.Views;
using Features.UI.Views.Impl;
using Modules.GameController.Facade;
using Modules.GameController.Facade.Impl;
using Modules.GameController.Service;
using Modules.GameController.Service.Impl;
using Modules.MazeGenerator.Facade;
using Modules.MazeGenerator.Facade.Impl;
using Modules.MazeGenerator.Services;
using Modules.MazeGenerator.Services.Impl;
using UnityEngine;

namespace Zenject
{
    public class MazeZenjectInstaller : MonoInstaller
    {
        [SerializeField] 
        private GameView _gameView;
        [SerializeField] 
        private MazeManager _mazeManager;
        [SerializeField] 
        private LevelTimer _levelTimer;
        [SerializeField] 
        private CameraManager _cameraManager;
        
        public override void InstallBindings()
        {
            InstallFeatures();
            InstallModules();
        }
        
        private void InstallFeatures()
        {
            Container.Bind(typeof(IGameView)).To<GameView>().FromInstance(_gameView).AsCached();
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IGameViewPresenter)).To<GameViewPresenter>().AsCached();
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IMazeManager)).To<MazeManager>().FromInstance(_mazeManager).AsCached();
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(ILevelTimer)).To<LevelTimer>().FromInstance(_levelTimer).AsCached();
            Container.Bind(typeof(ICameraManager)).To<CameraManager>().FromInstance(_cameraManager).AsCached();
        }
        
        private void InstallModules()
        {
            Container.Bind<IMazeGenerationFacade>().To<MazeGenerationFacade>().AsCached();        
            Container.Bind<IMazeGenerationService>().To<MazeGenerationService>().AsCached();        
            Container.Bind(typeof(IGameControllerService), typeof(IInitializable)).To<GameControllerService>().AsCached();        
            Container.Bind(typeof(IGameControllerFacade), typeof(IInitializable)).To<GameControllerFacade>().AsCached();        
        }
    }
}