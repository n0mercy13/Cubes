using UnityEngine;
using VContainer;
using VContainer.Unity;
using Codebase.Logic;
using Codebase.StaticData;

namespace Codebase.Infrastructure
{
    public partial class GameFactory
    {
        private readonly IObjectResolver _container;
        private readonly Cube _cubePrefab;
        private readonly Platform _platformPrefab;
        private readonly string _parentName = "Cubes";
        private Transform _parent;

        public GameFactory(IObjectResolver container, GameConfig gameConfig)
        {
            _container = container;
            _cubePrefab = gameConfig.CubePrefab;
            _platformPrefab = gameConfig.PlatformPrefab;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public Cube CreateCube(Vector3 position)
        {
            if (_parent == null)
                _parent = new GameObject(_parentName).transform;

            return _container.Instantiate(_cubePrefab, position, Quaternion.identity, _parent);
        }

        public Platform CreatePlatform(Vector3 position) =>
            _container.Instantiate(_platformPrefab, position, Quaternion.identity);
    }
}
