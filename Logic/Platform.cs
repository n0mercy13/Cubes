using UnityEngine;
using VContainer;
using Codebase.StaticData;
using Infrastructure.Services;

namespace Codebase.Logic
{
    public class Platform : MonoBehaviour
    {
        private IRandomService _randomService;
        private float _cubeMinLifespan;
        private float _cubeMaxLifespan;

        [Inject]
        private void Construct(IRandomService randomService, GameConfig gameConfig)
        {
            _randomService = randomService;
            _cubeMinLifespan = gameConfig.CubeMinLifespan;
            _cubeMaxLifespan = gameConfig.CubeMaxLifespan;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out Cube cube)
                && cube.IsSelfDestructing == false)
            {
                cube.SetColor(
                    _randomService.GetColor());
                cube.SelfDestructAsync(
                    _randomService.Range(_cubeMinLifespan, _cubeMaxLifespan));
            }
        }
    }
}
