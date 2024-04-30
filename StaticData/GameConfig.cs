using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public Cube CubePrefab { get; private set; }
        [field: SerializeField] public Platform PlatformPrefab { get; private set; }
        [field: SerializeField, Range(1, 10)] public int InitialCubesNumber { get; private set; } = 5;
        [field: SerializeField, Range(0f, 500f)] public float ExplosionForce { get; private set; } = 50f;
        [field: SerializeField, Range(0f, 20f)] public float ExplosionRadius { get; private set; } = 10f;
        [field: SerializeField, Range(0, 100)] public int MaxRaycastDistance { get; private set; } = 70;
        [field: SerializeField] public LayerMask RaycastLayerMask { get; private set; }
        [field: SerializeField, Min(1)] public int MinCubesOnClick { get; private set; } = 2;
        [field: SerializeField, Min(1)] public int MaxCubesOnClick { get; private set; } = 6;
        [field: SerializeField, Range(0f, 10f)] public float SpawnDelay { get; private set; } = 1;
        [field: SerializeField, Range(0f, 10f)] public float CubeMinLifespan { get; private set; } = 2f;
        [field: SerializeField, Range(0f, 10f)] public float CubeMaxLifespan { get; private set; } = 5f;

        private void OnValidate()
        {
            if(CubePrefab == null)
                throw new ArgumentNullException(nameof(CubePrefab));

            if(PlatformPrefab == null)
                throw new ArgumentNullException(nameof(PlatformPrefab));
        }
    }
}
