using UnityEngine;
using VContainer.Unity;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class Bootstrap
    {
        private readonly ICubesHandler _cubesHandler;
        private readonly IGameFactory _gameFactory;
        private readonly Vector3 _platformSpawnPosition;

        public Bootstrap(ICubesHandler cubesHandler, IGameFactory gameFactory, SceneData sceneData)
        {
            _cubesHandler = cubesHandler;
            _gameFactory = gameFactory;
            _platformSpawnPosition = sceneData.PlatformSpawnPoint.position;
        }
    }

    public partial class Bootstrap : IStartable
    {
        public void Start()
        {
            _gameFactory.CreatePlatform(_platformSpawnPosition);
            _cubesHandler.SpawnCubesAsync();
        }
    }
}