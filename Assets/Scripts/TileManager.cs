using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Dictionary<Vector3Int, HexTile> _tiles;
    private void Awake()
    {
        _tiles = new Dictionary<Vector3Int, HexTile>();
        
        var hexTiles = gameObject.GetComponentsInChildren<HexTile>();
        foreach (var hexTile in hexTiles)
        {
            RegisterTile(hexTile);
        }
        
        foreach (var hexTile in hexTiles)
        {
            hexTile.neighbors = GetNeighbors(hexTile);
        }
    }

    private void RegisterTile(HexTile tile)
    {
        _tiles.Add(tile.cubeCoordinate, tile);
    }

    private List<HexTile> GetNeighbors(HexTile tile)
    {
        List<HexTile> neighbors = new List<HexTile>();
        Vector3Int[] neighborCoords = {
            new(1, -1, 0),
            new(1, 0, -1),
            new(0, 1, -1),
            new(-1, 1, 0),
            new(-1, 0, 1),
            new(0, -1, 1),
        };

        foreach (var neighborCoord in neighborCoords)
        {
            var tileCoord = tile.cubeCoordinate;
            // try to get value, and assign to out variable
            if (_tiles.TryGetValue(tileCoord + neighborCoord, out HexTile neighbor))
            {
                neighbors.Add(neighbor);
                // Debug.Log($"{tile.name} has neighbor {neighbor.name}");
            }
        }
        return neighbors;
    }
}