using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileGen/Generation Settings")]
public class HexTileGenerationSettings : ScriptableObject
{
    public enum TileType
    {
        Plain,
        Forest,
        Hill,
        Water
    }

    public GameObject Plain;
    public GameObject Forest;
    public GameObject Hill;
    public GameObject Water;

    public GameObject getTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Plain:
                return Plain;
            case TileType.Forest:
                return Forest;
            case TileType.Hill:
                return Hill;
            case TileType.Water:
                return Water;
        }
        return null;
    }
}
