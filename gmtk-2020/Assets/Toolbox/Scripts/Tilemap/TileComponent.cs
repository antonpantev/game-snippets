using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Toolbox
{
    /// <summary>
    /// A component that also takes up a Tile in a Tilemap. If the Tilemap is
    /// not specified it will default to the Tilemap in the parent Game Object.
    /// </summary>
    public class TileComponent : MonoBehaviour
    {
        public Tilemap tilemap;

        internal Tile tile;
        internal Vector3 curPos;
        internal bool isQuitting;

        public virtual void Awake()
        {
            if (tilemap == null)
            {
                tilemap = GetComponentInParent<Tilemap>();
            }

            tile = ScriptableObject.CreateInstance<Tile>();
            curPos = transform.position;
            SetTileSafe(transform.position);
        }

        public virtual void ClearTile()
        {
            Vector3Int curCell = tilemap.WorldToCell(curPos);

            if (tilemap.GetTile(curCell) == tile)
            {
                tilemap.SetTile(curCell, null);
            }
        }

        /// <summary>
        /// Sets a cell at the given location in the tilemap to a tile
        /// representing the game object. Call this method whenever the game
        /// object moves to a new location on the tilemap.
        /// </summary>
        public virtual void SetTile(Vector3 newPos)
        {
            ClearTile();

            tilemap.SetTile(tilemap.WorldToCell(newPos), tile);

            curPos = newPos;
        }

        /// <summary>
        /// Sets a cell at the given location in the tilemap to a tile
        /// representing the game object only if the location is empty.
        /// Call this method whenever the game object moves to a new
        /// location on the tilemap.
        /// </summary>
        public virtual void SetTileSafe(Vector3 newPos)
        {
            ClearTile();

            if (tilemap.GetTile(newPos) == null)
            {
                tilemap.SetTile(tilemap.WorldToCell(newPos), tile);
            }

            curPos = newPos;
        }

        public virtual LinePath FindLinePathClosest(Vector3 start, Vector3 goal)
        {
            return AStar.FindLinePathClosest(tilemap, start, goal);
        }

        public virtual List<Vector3> FindPathClosest(Vector3 start, Vector3 goal)
        {
            return AStar.FindPathClosest(tilemap, start, goal);
        }

        void OnApplicationQuit()
        {
            isQuitting = true;
        }

        public virtual void OnDestroy()
        {
            if (!isQuitting)
            {
                ClearTile();
            }
        }
    }
}