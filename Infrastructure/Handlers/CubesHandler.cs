using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Logic;
using Codebase.StaticData;
using Infrastructure.Services;

namespace Codebase.Infrastructure
{
    public partial class CubesHandler
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameplayInput _gameplayInput;
        private readonly IRandomService _randomService;
        private readonly RaycastHit[] _hits;
        private readonly Vector3 _initialCubesPosition;
        private readonly LayerMask _layerMask;
        private readonly List<Cube> _activeCubes;
        private readonly Camera _camera;
        private readonly float _explosionForce;
        private readonly float _explosionRadius;
        private readonly int _initialCubesNumber;
        private readonly int _maxDistance;
        private readonly int _minNewCubes;
        private readonly int _maxNewCubes;

        public CubesHandler(
            IGameFactory gameFactory,
            IGameplayInput gameplayInput,
            IRandomService randomService,
            GameConfig gameConfig,
            SceneData sceneData)
        {
            _gameFactory = gameFactory;
            _gameplayInput = gameplayInput;
            _randomService = randomService;
            _activeCubes = new List<Cube>();
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
            if(TryGetCube(mousePosition, out Cube cube) == false)
                return;

            int generation = cube.Generation;
            Vector3 position = cube.transform.position;

            _activeCubes.Remove(cube);
            cube.Remove();

            if (TryCreateNewCubes(generation))
            {
                int quantity = _randomService.Range(_minNewCubes, _maxNewCubes);
                CreateCubes(quantity, generation, position);
            }
            else
            {
                CreateExplosion(generation, position);
            }
        }

        private void CreateExplosion(int generation, Vector3 cubePosition)
        {
            foreach (Cube cube in _activeCubes)
                cube.AddForce(cubePosition, _explosionForce * generation, _explosionRadius * generation);
        }

        private bool TryCreateNewCubes(int generation)
        {
            float chance = GetChance(generation);

            if (_randomService.IsSuccessful(chance))
                return true;

            return false;
        }

        private void CreateCubes(int quantity, int previousGeneration, Vector3 position)
        {
            for(int i = 0; i < quantity; i++)
            {
                int generation = previousGeneration + 1;
                Cube newCube = _gameFactory.CreateCube(position);
                newCube.SetGeneration(generation);
                newCube.SetColor(_randomService.GetColor());
                newCube.SetScale(GetScale(generation));
                _activeCubes.Add(newCube);
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

    public partial class CubesHandler : IInitializable
    {
        public void Initialize() => 
            CreateCubes(_initialCubesNumber, previousGeneration: 0, _initialCubesPosition);
    }

    public partial class CubesHandler : IDisposable
    {
        public void Dispose()
        {
            _gameplayInput.Selected -= OnSelected;
        }
    }
}
