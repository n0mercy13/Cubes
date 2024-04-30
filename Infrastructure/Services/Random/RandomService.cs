using UnityEngine;

namespace Infrastructure.Services
{
    public partial class RandomService
    {
        private readonly System.Random _random;
        private readonly int _seed = 1;

        public RandomService() 
        { 
            _random = new System.Random(_seed);
        }
    }

    public partial class RandomService : IRandomService
    {
        public Color GetColor()
        {
            float r = (float)_random.NextDouble();
            float g = (float)_random.NextDouble();
            float b = (float)_random.NextDouble();
            float a = 1.0f;

            return new Color(r, g, b, a);
        }

        public Vector3 Range(Vector3 minPoint, Vector3 maxPoint)
        {
            float x = Range(minPoint.x, maxPoint.x);
            float y = Range(minPoint.y, maxPoint.y);
            float z = Range(minPoint.z, maxPoint.z);

            return new Vector3(x, y, z);
        }

        public float Range(float minNumber, float maxNumber) =>
            minNumber + (maxNumber - minNumber) * (float)_random.NextDouble();

        public int Range(int minNumber, int maxNumber) =>
            minNumber + (int)((maxNumber - minNumber) * _random.NextDouble());

        public bool IsSuccessful(float chance) =>
            chance >= _random.NextDouble();
    }
}
