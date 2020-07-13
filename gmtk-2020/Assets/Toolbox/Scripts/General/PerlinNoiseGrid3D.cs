using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// A grid of Perlin Noise values at a given scale.
    /// </summary>
    public class PerlinNoiseGrid3D
    {
        public int width;
        public int height;
        public int depth;
        public Vector3 offset;
        public Vector3 scale;

        public float this[int x, int y, int z]
        {
            get
            {
                float i = offset.x + (float)x / width * scale.x;
                float j = offset.y + (float)y / height * scale.y;
                float k = offset.z + (float)z / depth * scale.z;

                float AB = Mathf.PerlinNoise(i, j);
                float BC = Mathf.PerlinNoise(j, k);
                float AC = Mathf.PerlinNoise(i, k);

                float BA = Mathf.PerlinNoise(j, i);
                float CB = Mathf.PerlinNoise(k, j);
                float CA = Mathf.PerlinNoise(k, i);

                float ABC = AB + BC + AC + BA + CB + CA;
                return Mathf.Clamp01(ABC / 6f);
            }
        }

        /// <summary>
        /// Creates a grid of Perlin Noise values.
        /// </summary>
        /// <param name="width">Width of the grid.</param>
        /// <param name="height">Height of the grid.</param>
        /// <param name="perlinScale">Scale of the grid. Increase to zoom out and decrease to zoom in.</param>
        public PerlinNoiseGrid3D(int width, int height, int depth, float perlinScale)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            offset = new Vector3(Random.value * 100000, Random.value * 100000, Random.value * 100000);

            float r1Aspect = (float)width / depth;
            float r2Aspect = (float)height / depth;
            scale = new Vector3(perlinScale * r1Aspect, perlinScale * r2Aspect, perlinScale);
        }
    }
}