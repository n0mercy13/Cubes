using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.StaticData;
using Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private GameConfig _gameConfig;

        private void OnValidate()
        {
            if(_sceneData == null)
                throw new ArgumentNullException(nameof(_sceneData));

            if(_gameConfig == null)
                throw new ArgumentNullException(nameof(_gameConfig));
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(_sceneData);
            builder
                .RegisterInstance(_gameConfig);
            builder
                .RegisterEntryPoint<Bootstrap>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<GameFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<CubesHandler>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<InputActions>(Lifetime.Singleton)
                .AsSelf();
            builder
                .Register<InputService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder
                .Register<RandomService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    } 
}
