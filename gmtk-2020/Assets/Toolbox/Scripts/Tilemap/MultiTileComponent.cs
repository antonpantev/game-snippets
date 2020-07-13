using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Toolbox
{
    /// <summary>
    /// A component that also takes up multiple Tiles in a Tilemap. If the
    /// Tilemap is not specified it will default to the Tilemap in the parent
    /// Game Object.
    /// </summary>
    public class MultiTileComponent : TileComponent
    {
        public int cellWidth = 2;
        public int cellHeight = 2;

        public virtual void ForEachCell(Vector3 position, Action<Vector3Int> action)
        {
            Vector3Int minCell = tilemap.WorldToCell(position);
            minCell.x -= Mathf.FloorToInt(cellWidth / 2f);
            minCell.y -= Mathf.FloorToInt(cellHeight / 2f);

            for (int i = 0; i < cellWidth; i++)
            {
                for (int j = 0; j < cellHeight; j++)
                {
                    Vector3Int curCell = new Vector3Int(minCell.x + i, minCell.y + j, minCell.z);
                    action(curCell);
                }
            }
        }

        public virtual void ForEachCell(Action<Vector3Int> action)
        {
            ForEachCell(curPos, action);
        }

        public override void ClearTile()
        {
            ForEachCell(cell =>
            {
                if (tilemap.GetTile(cell) == tile)
                {
                    tilemap.SetTile(cell, null);
                }
            });
        }

        /// <summary>
        /// Sets multiple cells around the given location in the tilemap to a
        /// tile representing the game object. Call this method whenever the
        /// game object moves to a new location on the tilemap.
        /// </summary>
        /// <remarks>
        /// Be very deliberate with the given position. Usually an even length
        /// should have a coordinate on the edge of a cell and an odd length
        /// should have a coordinate at the center of a cell. 
        /// </remarks>
        public override void SetTile(Vector3 newPos)
        {
            ClearTile();

            ForEachCell(newPos, cell =>
            {
                tilemap.SetTile(cell, tile);
            });

            curPos = newPos;
        }

        /// <summary>
        /// Sets multiple cells around the given location in the tilemap to a
        /// tile representing the game object if the cells are empty. Call
        /// this method whenever the game object moves to a new location on
        /// the tilemap.
        /// </summary>
        /// <remarks>
        /// Be very deliberate with the given position. Usually an even length
        /// should have a coordinate on the edge of a cell and an odd length
        /// should have a coordinate at the center of a cell.
        /// </remarks>
        public override void SetTileSafe(Vector3 newPos)
        {
            ClearTile();

            ForEachCell(newPos, cell =>
            {
                if (tilemap.GetTile(cell) == null)
                {
                    tilemap.SetTile(cell, tile);
                }
            });

            curPos = newPos;
        }
    }
}
