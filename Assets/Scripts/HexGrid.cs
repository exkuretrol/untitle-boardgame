using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [EditorButton(nameof(LayoutGrid))]
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius = 1f;
    public bool isFlatTopped = false;
    public HexTileGenerationSettings settings;

    private void Clear()
    {
        var children = new List<GameObject>();
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            if (Application.isEditor) DestroyImmediate(child, true);
            if (Application.isPlaying) Destroy(child);
            
        }
    }

    // private void OnValidate()
    // {
    //     if (Application.isPlaying)
    //     {
    //         LayoutGrid();
    //     }
    // }

    private void LayoutGrid()
    {
        Clear();
        for (var y = 0; y < gridSize.y; y++)
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                var tile = new GameObject($"Hex R{y}, C{x}");
                var hexTile = tile.AddComponent<HexTile>();
                hexTile.settings = settings;
                hexTile.RollTileType();
                hexTile.AddTile();
                tile.transform.position = Utilities.GetPositionForHexFromCoordinate(x, y, isFlatTopped, radius);

                hexTile.offsetCoordinate = new Vector2Int(x, y);
                hexTile.cubeCoordinate = Utilities.OffsetToCube(hexTile.offsetCoordinate);
                tile.transform.SetParent(transform);
            }
        }
    }

}
