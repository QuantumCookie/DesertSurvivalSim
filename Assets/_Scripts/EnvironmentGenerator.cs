﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AwesomeNamespace;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public LayerMask groundMask;

    [Header("Tree Cluster")] [Space] public Cluster treeCluster;
    [Header("Cactus Cluster")] [Space] public Cluster cactusCluster;
    [Header("Rock Cluster")] [Space] public Cluster rockCluster;
    [Header("Secondary Bush")] [Space]
    public GameObject[] bushPrefab;
    public float bushRadius;
    [Header("General Cluster")] [Space] public Cluster generalCluster;

    private List<GameObject> trees, rocks, cacti, general;

    private void Start()
    {
        Initialize();
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        GenerateRockClusters();
        GenerateTreeClusters();
        GenerateCactusClusters();
        GenerateGeneralCluster();
        GenerateSecondaryBushes();
        yield return null;
    }

    private void Initialize()
    {
        trees = new List<GameObject>();
        rocks = new List<GameObject>();
        cacti = new List<GameObject>();
        general = new List<GameObject>();
    }
    
    private void GenerateTreeClusters()
    {
        for (int i = 0; i < treeCluster.subclusters; i++)
        {
            Vector3 subPos = treeCluster.location.position + Random.insideUnitSphere * treeCluster.radius;//y coord doesn't matter
            subPos.y = 0;

            List<Vector2> points = UniformPoissonDiskSampler.SampleCircle(new Vector2(subPos.x, subPos.z), treeCluster.subclusterRadius, treeCluster.objectRadius);

            for (int j = 0; j < points.Count; j++)
            {
                Vector3 treePos = new Vector3(points[j].x, 0, points[j].y);
                
                RaycastHit hit;
                if (!Physics.Raycast(treePos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) return;
                treePos.y = hit.point.y;

                //if (Physics.OverlapSphere(treePos, treeRadius, resourceMask).Length > 0)continue;
                if (Random.value > treeCluster.objectProbability) continue;

                GameObject go = Instantiate(GetRandom(treeCluster.objectPrefab), treePos, Quaternion.identity);
                go.transform.localScale = go.transform.localScale * (Vector3.Distance(subPos, treePos) / treeCluster.subclusterRadius) * Random.Range(0.5f, 1f);
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                
                trees.Add(go);
            }
        }
    }

    private void GenerateCactusClusters()
    {
        for (int i = 0; i < cactusCluster.subclusters; i++)
        {
            Vector3 subPos = cactusCluster.location.position + Random.insideUnitSphere * cactusCluster.radius;//y coord doesn't matter
            subPos.y = 0;

            List<Vector2> points = UniformPoissonDiskSampler.SampleCircle(new Vector2(subPos.x, subPos.z), cactusCluster.subclusterRadius, cactusCluster.objectRadius);

            for (int j = 0; j < points.Count; j++)
            {
                Vector3 treePos = new Vector3(points[j].x, 0, points[j].y);
                
                RaycastHit hit;
                if (!Physics.Raycast(treePos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) return;
                treePos.y = hit.point.y;

                //if (Physics.OverlapSphere(treePos, treeRadius, resourceMask).Length > 0)continue;
                if (Random.value > cactusCluster.objectProbability) continue;
                
                GameObject go = Instantiate(GetRandom(cactusCluster.objectPrefab), treePos, Quaternion.identity);
                float scaleFactor = (Vector3.Distance(subPos, treePos) / cactusCluster.subclusterRadius) * Random.Range(1f, 2f);
                go.transform.localScale = go.transform.localScale * Mathf.Max(0.5f, scaleFactor);
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                
                cacti.Add(go);
            }
        }
    }
    
    private void GenerateRockClusters()
    {
        for (int i = 0; i < rockCluster.subclusters; i++)
        {
            Vector3 subPos = rockCluster.location.position + Random.insideUnitSphere * rockCluster.radius;//y coord doesn't matter
            subPos.y = 0;

            List<Vector2> points = UniformPoissonDiskSampler.SampleCircle(new Vector2(subPos.x, subPos.z), rockCluster.subclusterRadius, rockCluster.objectRadius);
//            Debug.Log(points.Count);

            for (int j = 0; j < points.Count; j++)
            {
                Vector3 treePos = new Vector3(points[j].x, 0, points[j].y);
                treePos += (treePos - subPos) * UniformPoissonDiskSampler.NextGaussian();
                
                RaycastHit hit;
                if (!Physics.Raycast(treePos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) return;
                treePos.y = hit.point.y;

                //if (Physics.OverlapSphere(treePos, treeRadius, resourceMask).Length > 0)continue;
                if (Random.value > rockCluster.objectProbability) continue;
                
                GameObject go = Instantiate(GetRandom(rockCluster.objectPrefab), treePos, Quaternion.identity);
                float scaleFactor = (1 / (1 + 0.1f * Vector3.Distance(treePos, subPos)));
                go.transform.localScale = go.transform.localScale * scaleFactor * Random.Range(0.7f, 1f);
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                
                rocks.Add(go);
            }
        }
    }

    private void GenerateGeneralCluster()
    {
        List<Vector2> points = UniformPoissonDiskSampler.SampleCircle(new Vector2(generalCluster.location.position.x, generalCluster.location.position.z), generalCluster.radius, generalCluster.objectRadius);
//        Debug.Log("Success " + points.Count);

        for (int j = 0; j < points.Count; j++)
        {
            Vector3 treePos = new Vector3(points[j].x, 0, points[j].y);
            treePos += treePos * (1 - UniformPoissonDiskSampler.NextGaussian());
            
            RaycastHit hit;
            if (!Physics.Raycast(treePos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) continue;
            treePos.y = hit.point.y;

//                if (Physics.OverlapSphere(treePos, generalCluster.objectRadius, resourceMask).Length > 0)continue;
            if (Random.value > generalCluster.objectProbability) continue;
            
            GameObject go = Instantiate(GetRandom(generalCluster.objectPrefab), treePos, Quaternion.identity);
            float scaleFactor = 1;
            go.transform.localScale = go.transform.localScale * scaleFactor * Random.Range(0.7f, 1f);
            go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            
            general.Add(go);
        }
        
        /*for (int i = 0; i < generalCluster.subclusters; i++)
        {
            Vector3 subPos = generalCluster.location.position + Random.insideUnitSphere * generalCluster.radius;//y coord doesn't matter
            subPos.y = 0;

            List<Vector2> points = UniformPoissonDiskSampler.SampleCircle(new Vector2(subPos.x, subPos.z), generalCluster.subclusterRadius, generalCluster.objectRadius);
            Debug.Log(points.Count);

            for (int j = 0; j < points.Count; j++)
            {
                Vector3 treePos = new Vector3(points[j].x, 0, points[j].y);
                treePos += (treePos - subPos) * UniformPoissonDiskSampler.NextGaussian();
                
                RaycastHit hit;
                if (!Physics.Raycast(treePos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) return;
                treePos.y = hit.point.y;

                if (Physics.OverlapSphere(treePos, generalCluster.objectRadius, resourceMask).Length > 0)continue;
                if (Random.value > generalCluster.objectProbability) continue;
                
                GameObject go = Instantiate(GetRandom(generalCluster.objectPrefab), treePos, Quaternion.identity);
                float scaleFactor = 1;
                go.transform.localScale = go.transform.localScale * scaleFactor * Random.Range(0.7f, 1f);
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                
                general.Add(go);
            }
        }*/
    }

    private void GenerateSecondaryBushes()
    {
        GenerateAround(trees);
        GenerateAround(rocks);
        GenerateAround(cacti);
        GenerateAround(general);
    }

    private void GenerateAround(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                Vector3 bushPos = list[i].transform.position + Random.insideUnitSphere * UniformPoissonDiskSampler.NextGaussian() * bushRadius;
                
                RaycastHit hit;
                if (!Physics.Raycast(bushPos + Vector3.up * 200, Vector3.down, out hit, 250, groundMask)) return;
                bushPos.y = hit.point.y;
                
                GameObject go = Instantiate(GetRandom(bushPrefab), bushPos, Quaternion.identity);
                float scaleFactor = (1 / (1 + 0.2f * Vector3.Distance(bushPos, list[i].transform.position)));
                go.transform.localScale = go.transform.localScale * Mathf.Min(scaleFactor, list[i].transform.localScale.x) * Random.Range(0.7f, 1f);
                go.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            }
        }
    }

    private GameObject GetRandom(GameObject[] arr)
    {
        return arr[Mathf.FloorToInt(Random.value * (arr.Length - 1))];
    }
}

[System.Serializable]
public class Cluster
{
    public GameObject[] objectPrefab;
    public Transform location;
    public float radius = 10;
    public int subclusters = 5;
    public float subclusterRadius = 5;
    public float objectRadius = 3;
    public float objectProbability = 1;
}
