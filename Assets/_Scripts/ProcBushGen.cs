﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcBushGen : MonoBehaviour
{
	public LayerMask groundLayer;
	
	public GameObject treePrefab;
	[Range(0, 1)] public float treeProbability = 1;
	public Vector2 treeArea = new Vector2(1000, 1000);
	public float treeRadius = 10f;
	private List<GameObject> trees;

	public GameObject rockPrefab;
	[Range(0, 1)] public float rockProbability = 1;
	public float rockArea = 10f;
	
	public GameObject bushPrefab;
	[Range(0, 1)] public float bushProbability = 1;
	public float bushArea = 20f;
	public float bushRadius = 1f;
	
	private void Start()
	{
		GenerateTrees();
		GenerateRock();
		GenerateBush();
	}

	private void GenerateTrees()
	{
		List<Vector2> points = PoissonDiscSampling.GeneratePoints(treeRadius, treeArea, 10);
		trees = new List<GameObject>();

		for (int i = 0; i < points.Count; i++)
		{
			if(Random.value < treeProbability)
			{
				points[i] = points[i] - treeArea * 0.5f;

				Vector3 position = new Vector3(points[i].x, 0, points[i].y);
				
				RaycastHit hit;
				if (!Physics.Raycast(position + Vector3.up * 20, Vector3.down, out hit, 30, groundLayer)) return;
				position.y = hit.point.y;
				//Debug.Log(hit.point);
				
				trees.Add(Instantiate(treePrefab, position, Quaternion.identity));
			}
		}
	}

	private void GenerateRock()
	{
		for (int i = 0; i < trees.Count; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				if(Random.value > rockProbability)continue;
				
				Vector2 direction = Random.insideUnitCircle * PoissonDiscSampling.NextGaussian() * rockArea;

				Vector3 position = trees[i].transform.position + new Vector3(direction.x, 0, direction.y);
				position.y = trees[i].transform.position.y;
				
				Instantiate(rockPrefab, position,
					Quaternion.identity);
			}
		}
	}

	private void GenerateBush()
	{
		for (int i = 0; i < trees.Count; i++)
		{
			/*List<Vector2> points = PoissonDiscSampling.GeneratePoints(bushRadius, bushArea, 30);
			
			for(int j = 0; j < points.Count; j++)	
			{
				if(Random.value > bushProbability)
				{
					Instantiate(bushPrefab, trees[j].transform.position + new Vector3(points[j].x, 0, points[j].y) - new Vector3(bushArea.x * 0.5f, 0, bushArea.y * 0.5f),
						Quaternion.identity);
				}
			}*/

			for (int j = 0; j < 30; j++)
			{
				if(Random.value > bushProbability)continue;
				
				Vector2 direction = Random.insideUnitCircle * PoissonDiscSampling.NextGaussian() * bushArea;
				
				Vector3 position = trees[i].transform.position + new Vector3(direction.x, 0, direction.y);
				position.y = trees[i].transform.position.y;

				Instantiate(bushPrefab, position,
					Quaternion.identity);
			}
		}
	}
}