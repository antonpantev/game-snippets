using UnityEngine;

namespace Toolbox
{
    /// <summary>
    /// A grid of Perlin Noise values at a given scale.
    /// </summary>
    public class PerlinNoiseGrid
    {
        public int width;
        public int height;
        public Vector2 offset;
        public Vector2 scale;

        public float this[int x, int y]
        {
            get
            {
                float i = offset.x + (float)x / width * scale.x;
                float j = offset.y + (float)y / height * scale.y;

                return Mathf.Clamp01(Mathf.PerlinNoise(i, j));
            }
        }

        /// <summary>
        /// Creates a grid of Perlin Noise values.
        /// </summary>
        /// <param name="width">Width of the grid.</param>
        /// <param name="height">Height of the grid.</param>
        /// <param name="perlinScale">Scale of the grid. Increase to zoom out and decrease to zoom in.</param>
        public PerlinNoiseGrid(int width, int height, float perlinScale)
        {
            this.width = width;
            this.height = height;

            offset = new Vector2(Random.value * 100000, Random.value * 100000);

            float rAspect = (float)width / height;
            scale = new Vector2(perlinScale * rAspect, perlinScale);
        }
    }
}