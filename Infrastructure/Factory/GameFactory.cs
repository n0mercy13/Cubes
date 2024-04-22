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
        private readonly Cube _prefab;
        private readonly string _parentName;
        private Transform _parent;

        public GameFactory(IObjectResolver container, GameConfig gameConfig)
        {
            _container = container;
            _prefab = gameConfig.Prefab;
        }
    }

    public partial class GameFactory : IGameFactory
    {
        public Cube CreateCube(Vector3 position)
        {
            if (_parent == null)
                _parent = new GameObject(_parentName).transform;

            return _container.Instantiate(_prefab, position, Quaternion.identity, _parent);
        }
    }
}
