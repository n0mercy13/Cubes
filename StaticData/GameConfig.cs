using System;
using UnityEngine;
using Codebase.Logic;

namespace Codebase.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public Cube Prefab { get; private set; }
        [field: SerializeField, Range(1, 10)] public int InitialCubesNumber { get; private set; } = 5;
        [field: SerializeField, Range(0f, 500f)] public float ExplosionForce { get; private set; } = 50f;
        [field: SerializeField, Range(0f, 20f)] public float ExplosionRadius { get; private set; } = 10f;
        [field: SerializeField, Range(0, 100)] public int MaxRaycastDistance { get; private set; } = 70;
        [field: SerializeField] public LayerMask RaycastLayerMask { get; private set; }

        public readonly int MinCubesOnClick = 2;
        public readonly int MaxCubesOnClick = 6;

    }
}
