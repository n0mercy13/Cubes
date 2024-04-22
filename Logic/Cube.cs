using System;
using UnityEngine;

namespace Codebase.Logic
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _render;
        [SerializeField] private Rigidbody _rigidbody;

        private Transform _transform;

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

        public int Generation { get; set; } = 1;

        public void SetColor(Color color) =>
            _render.material.color = color;

        public void SetScale(Vector3 scale) => 
            _transform.localScale = scale;

        public void AddForce(Vector3 explosionPosition, float force, float radius) =>
            _rigidbody.AddExplosionForce(force, explosionPosition, radius);

        public void Remove() =>
            Destroy(gameObject);
    }
}
