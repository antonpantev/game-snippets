using UnityEngine;
using UnityEngine.Tilemaps;

namespace Toolbox
{
    public class TilemapDebug : MonoBehaviour
    {
        public float size = 0.5f;
        public Color color = Color.white;
        public bool depthTest;

        Tilemap tilemap;

        void Start()
        {
            tilemap = GetComponent<Tilemap>();
        }

        void Update()
        {
            tilemap.DebugDraw(size, color, 0f, depthTest);
        }
    }
}