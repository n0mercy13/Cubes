using UnityEngine;

namespace Infrastructure.Services
{
    public interface IRandomService
    {
        Color GetColor();
        bool IsSuccessful(float chance);
        int Range(int minNumber, int maxNumber);
    }
}