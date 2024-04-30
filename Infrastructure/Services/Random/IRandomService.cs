using UnityEngine;

namespace Infrastructure.Services
{
    public interface IRandomService
    {
        Color GetColor();
        bool IsSuccessful(float chance);
        Vector3 Range(Vector3 minPoint, Vector3 maxPoint);
        int Range(int minNumber, int maxNumber);
        float Range(float minNumber, float maxNumber);
    }
}