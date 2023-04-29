using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

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

    private void clear()
    {
        List<GameObject> childrens = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject children = transform.GetChild(i).gameObject;
            childrens.Add(children);
        }

        foreach (GameObject children in childrens)
        {
            DestroyImmediate(children);
        }
    }

    public void LayoutGrid()
    {
        clear();
        int i = 0;
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                int randomChunkIndex = Random.Range(0, chunks.Count - 1);
                GameObject chunk = Instantiate(chunks[randomChunkIndex], transform);
                // GameObject chunk = Instantiate(chunks[i++], transform);
                chunk.name = $"Chunk R{x}, C{y}";
                chunk.transform.position = Utilities.GetPositionForHexFromCoordinate(chunkSize * x, chunkSize * y);
                transform.SetParent(chunk.transform);
            }
        }
    }
}