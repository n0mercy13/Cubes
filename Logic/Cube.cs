using System;
using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _render;
        [SerializeField] private Rigidbody _rigidbody;

        private Coroutine _selfDestructCoroutine;
        private Transform _transform;

        public int Generation { get; private set; }
        public bool IsSelfDestructing { get; private set; }

        private void OnValidate()
        {
            if (_render == null)
                throw new ArgumentNullException(nameof(_render));

            if(_rigidbody == null)
                throw new ArgumentNullException(nameof(_rigidbody));
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            if (_selfDestructCoroutine != null)
                StopCoroutine(_selfDestructCoroutine);
        }

        public void SetGeneration(int generation)
        {
            if (generation < 1)
                throw new InvalidOperationException(
                    $"{generation} cannot be less than one");

            Generation = generation;
        }

        public void SelfDestructAsync(float lifespan) =>
            _selfDestructCoroutine = StartCoroutine(SelfDestruct(lifespan));

        public void SetColor(Color color) =>
            _render.material.color = color;

        public void SetScale(Vector3 scale) => 
            _transform.localScale = scale;

        public void AddForce(Vector3 explosionPosition, float force, float radius) =>
            _rigidbody.AddExplosionForce(force, explosionPosition, radius);

        public void Remove() =>
            Destroy(gameObject);

        private IEnumerator SelfDestruct(float lifespan)
        {
            IsSelfDestructing = true;

            yield return new WaitForSeconds(lifespan);

            Remove();
        }
    }
}
