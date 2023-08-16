using System;
using Features.MazeBuilder;
using Features.MazeBuilder.Impl;
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
        private MazeManager _mazeBuilder;
        
        public override void InstallBindings()
        {
            InstallFeatures();
            InstallModules();
        }
        
        private void InstallFeatures()
        {
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IMazeManager)).To<MazeManager>().FromInstance(_mazeBuilder).AsCached();
        }
        
        private void InstallModules()
        {
            Container.Bind<IMazeGenerationFacade>().To<MazeGenerationFacade>().AsCached();        
            Container.Bind<IMazeGenerationService>().To<MazeGenerationService>().AsCached();        
        }
    }
}