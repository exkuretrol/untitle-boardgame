using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class HexTile : MonoBehaviour
{
    public HexTileGenerationSettings settings;

    public HexTileGenerationSettings.TileType tileType;

    public GameObject tile;

    public GameObject fow;

    public Vector2Int offsetCoordinate;

    public Vector3Int cubeCoordinate;

    public List<HexTile> neighbors;

    private bool isDirty = false;

    public void RollTileType()
    {
        tileType = (HexTileGenerationSettings.TileType)Random.Range(0, 4);
    }

    public void AddTile()
    {
        tile = Instantiate(settings.getTile(tileType), transform);
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        transform.SetParent(tile.transform);
    }

    private void OnValidate()
    {
        if (tile == null)
        {
            return;
        }
        
        isDirty = true;
    }

    private void Update()
    {
        if (isDirty)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(tile);
            }
            else
            {
                DestroyImmediate(tile);
            }
            
            AddTile();
            isDirty = false;
        }
    }
}