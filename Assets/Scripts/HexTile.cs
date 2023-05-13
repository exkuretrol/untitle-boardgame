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

    public Vector2Int offsetCoordinate;

    public Vector3Int cubeCoordinate;

    public List<HexTile> neighbors;

    private bool _isDirty = false;

    public void RollTileType()
    {
        tileType = (HexTileGenerationSettings.TileType)Random.Range(0, 4);
    }

    public void AddTile()
    {
        tile = Instantiate(settings.getTile(tileType), transform);
        if (gameObject.GetComponent<MeshCollider>() == null)
        {
            MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        transform.SetParent(tile.transform);
    }

    private void OnValidate()
    {
        if (tile == null)
        {
            return;
        }
        
        _isDirty = true;
    }

    private void Update()
    {
        if (!_isDirty) return;
        
        if (Application.isPlaying)
        {
            Destroy(tile);
        }
        else
        {
            DestroyImmediate(tile, true);
        }
            
        AddTile();
        _isDirty = false;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var neighbor in neighbors)
        {
            var transformPosition = transform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transformPosition, 0.1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transformPosition, neighbor.transform.position);
        }
    }
}