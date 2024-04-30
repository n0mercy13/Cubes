using System;
using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Transform MinSpawnPoint { get; private set; }
        [field: SerializeField] public Transform MaxSpawnPoint { get; private set; }
        [field: SerializeField] public Transform PlatformSpawnPoint { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CoroutineRunner CoroutineRunner { get; private set; }

        private void OnValidate()
        {
            if (MinSpawnPoint == null)
                throw new ArgumentNullException(nameof(MinSpawnPoint));

            if (MaxSpawnPoint == null)
                throw new ArgumentNullException(nameof(MaxSpawnPoint));

            if (PlatformSpawnPoint == null)
                throw new ArgumentNullException(nameof(PlatformSpawnPoint));

            if (Camera == null)
                throw new ArgumentNullException(nameof(Camera));

            if (CoroutineRunner == null)
                throw new ArgumentNullException(nameof(CoroutineRunner));
        }
    }
}
