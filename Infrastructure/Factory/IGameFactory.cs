using Codebase.Logic;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameFactory
    {
        Cube CreateCube(Vector3 position);
    }
}