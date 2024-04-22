using System;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface IGameplayInput
    {
        event Action<Vector2> Selected;
    }
}