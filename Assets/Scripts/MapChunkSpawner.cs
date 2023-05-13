using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Networking;

public class MapChunkSpawner : MonoBehaviour
{
    [EditorButton(nameof(LayoutGrid), "Roll", activityType: ButtonActivityType.Everything)]
    public Vector2Int mapSize;

    public List<GameObject> chunks;

    public int chunkSize;
    
    private void OnEnable()
    {
        LayoutGrid();
    }

    private void Clear()
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }

        foreach (GameObject child in children)
        {
            if (Application.isEditor) DestroyImmediate(child, true);
            if (Application.isPlaying) Destroy(child);
        }
    }

    public void LayoutGrid()
    {
        Clear();
        int i = 0;
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                // int randomChunkIndex = Random.Range(0, chunks.Count - 1);
                // GameObject chunk = Instantiate(chunks[randomChunkIndex], transform);
                GameObject chunk = Instantiate(chunks[i++], transform);
                chunk.name = $"Chunk R{y}, C{x}";
                chunk.transform.position = Utilities.GetPositionForHexFromCoordinate(chunkSize * x, chunkSize * y);
                transform.SetParent(chunk.transform);

                Vector2Int chunkCornerCoordinate = new Vector2Int(chunkSize * x, chunkSize * y);
                foreach (Transform child in chunk.transform)
                {
                    HexTile tile = child.gameObject.GetComponent<HexTile>();
                    tile.offsetCoordinate += new Vector2Int(chunkCornerCoordinate.x, chunkCornerCoordinate.y);
                    tile.cubeCoordinate = Utilities.OffsetToCube(tile.offsetCoordinate);
                    tile.gameObject.name = $"Hex R{tile.offsetCoordinate.y}, C{tile.offsetCoordinate.x}";
                }
            }
        }
    }
}