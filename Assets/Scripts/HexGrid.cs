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

    public void clear()
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            LayoutGrid();
        }
    }

    private void LayoutGrid()
    {
        clear();
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex R{y}, C{x}");
                HexTile hextile = tile.AddComponent<HexTile>();
                hextile.settings = settings;
                hextile.RollTileType();
                hextile.AddTile();
                tile.transform.position = Utilities.GetPositionForHexFromCoordinate(x, y, isFlatTopped, radius);

                hextile.offsetCoordinate = new Vector2Int(x, y);
                hextile.cubeCoordinate = Utilities.OffsetToCube(hextile.offsetCoordinate);
                tile.transform.SetParent(transform);
            }
        }
    }

}
