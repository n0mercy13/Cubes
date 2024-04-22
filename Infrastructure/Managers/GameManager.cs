using System;
using UnityEngine;
using Codebase.Logic;
using Codebase.StaticData;
using Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public partial class GameManager
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameplayInput _gameplayInput;
        private readonly IRandomService _randomService;
        private readonly Camera _camera;
        private readonly RaycastHit[] _hits;
        private readonly Vector3 _initialCubesPosition;
        private readonly LayerMask _layerMask;
        private readonly float _explosionForce;
        private readonly float _explosionRadius;
        private readonly int _initialCubesNumber;
        private readonly int _maxDistance = 20;
        private readonly int _minNewCubes;
        private readonly int _maxNewCubes;

        public GameManager(
            IGameFactory gameFactory,
            IGameplayInput gameplayInput,
            IRandomService randomService,
            GameConfig gameConfig,
            SceneData sceneData)
        {
            _gameFactory = gameFactory;
            _gameplayInput = gameplayInput;
            _randomService = randomService;
            _camera = sceneData.Camera;
            _hits = new RaycastHit[1];
            _initialCubesPosition = sceneData.InitialSpawnPoint.position;
            _initialCubesNumber = gameConfig.InitialCubesNumber;
            _maxDistance = gameConfig.MaxRaycastDistance;
            _layerMask = gameConfig.RaycastLayerMask;
            _explosionForce = gameConfig.ExplosionForce;
            _explosionRadius = gameConfig.ExplosionRadius;
            _minNewCubes = gameConfig.MinCubesOnClick;
            _maxNewCubes = gameConfig.MaxCubesOnClick;

            _gameplayInput.Selected += OnSelected;
        }

        private void OnSelected(Vector2 mousePosition)
        {
            if(TryGetCube(mousePosition, out Cube cube))
            {
                int generation = cube.Generation;
                int cubesNumber = _randomService.Range(_minNewCubes, _maxNewCubes);
                Vector3 spawnPosition = cube.transform.position;
                cube.Remove();

                if (_randomService.IsSuccessful(
                    GetChance(generation)) == false)
                     return;

                for(int i = 0; i < cubesNumber; i++)
                {
                    int newGeneration = generation + 1;
                    Cube newCube = _gameFactory.CreateCube(spawnPosition);
                    newCube.Generation = newGeneration;
                    newCube.SetColor(_randomService.GetColor());
                    newCube.SetScale(GetScale(newGeneration));
                    newCube.AddForce(spawnPosition, _explosionForce, _explosionRadius);
                }
            }
        }

        private bool TryGetCube(Vector2 mousePosition, out Cube cube)
        {
            cube = null;
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            int hits = Physics.RaycastNonAlloc(ray, _hits, _maxDistance, _layerMask);

            if (hits > 0
                && _hits[0].collider.TryGetComponent(out cube))
                    return true;

            return false;
        }

        private Vector3 GetScale(int cubeGeneration)
        {
            int initialScale = 1;
            float step = 0.5f;

            return Vector3.one * GetGeometricProgressionMember(
                initialScale, step, cubeGeneration);
        }

        private float GetChance(int cubeGeneration)
        {
            int initialChance = 1;
            float step = 0.5f;

            return GetGeometricProgressionMember(
                initialChance, step, cubeGeneration);
        }

        private float GetGeometricProgressionMember(
            float initialValue, float step, int memberNumber) =>
            initialValue * Mathf.Pow(step, memberNumber - 1);
    }

    public partial class GameManager : IInitializable
    {
        public void Initialize()
        {
            for(int i = 0; i < _initialCubesNumber;  i++)
            {
                _gameFactory.CreateCube(_initialCubesPosition);
            }
        }
    }

    public partial class GameManager : IDisposable
    {
        public void Dispose()
        {
            _gameplayInput.Selected -= OnSelected;
        }
    }
}
