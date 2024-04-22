using System;
using UnityEngine;

namespace Codebase.StaticData
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Transform InitialSpawnPoint { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }

        private void OnValidate()
        {
            if(InitialSpawnPoint == null)
                throw new ArgumentNullException(nameof(InitialSpawnPoint));

            if(Camera == null)
                throw new ArgumentNullException(nameof(Camera));
        }
    }
}
