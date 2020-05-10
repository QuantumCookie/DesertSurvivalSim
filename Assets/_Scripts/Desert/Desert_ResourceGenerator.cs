using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Desert_ResourceGenerator : MonoBehaviour
{
    public Terrain terrain;
    public int resolution;
    public LayerMask resourceLayer;
    public Area[] areas;

    private Vector3 origin;
    private float gridSize;
    
    private void Start()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        origin = terrain.transform.position + new Vector3(terrainWidth * 0.5f, 0, terrainLength * 0.5f);
        gridSize = terrainWidth / resolution;
        //StartCoroutine(Generate());
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < areas.Length; i++)
        {
            AreaData[] data = areas[i].data;

            for (int j = 0; j < 1000; j++)
            {
                float r = Random.Range(0, 1);
                
                for (int k = 0; k < data.Length; k++)
                {
                    if (r < data[k].probability)
                    {
                        Vector2 rand = Random.insideUnitCircle * areas[i].radius;
                        Vector3 spawnPosition = origin + new Vector3(areas[i].origin.x + rand.x, 0, areas[i].origin.z + rand.y);
                        
                        if (CanSpawn(spawnPosition, data[k].radius))
                        {
                            Instantiate(data[k].prefab, spawnPosition, Quaternion.identity);
                        }
                    }
                }
            }
        }
        
        /*for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector3 gridCenter = terrain.GetPosition() + new Vector3((x + 0.5f) * gridSize, 0, (z + 0.5f) * gridSize);

                //Instantiate(prefab, gridCenter, Quaternion.identity);
            }
        }*/
    }

    private bool CanSpawn(Vector3 position, float radius)
    {
        return Physics.OverlapSphere(position, radius, resourceLayer).Length == 0;
    }
}

[System.Serializable]
public struct Area
{
    public Vector3 origin;
    public float radius;

    public AreaData[] data;
    //public float probability;
    //public GameObject prefab;
}

[System.Serializable]
public struct AreaData
{
    public ResourceSO resource;
    public GameObject prefab;
    public float probability;
    public float radius;
}
