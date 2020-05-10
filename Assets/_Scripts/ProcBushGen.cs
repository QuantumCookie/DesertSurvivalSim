using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcBushGen : MonoBehaviour
{
	public GameObject treePrefab;
	[Range(0, 1)] public float treeProbability = 1;
	public float treeRadius = 10f;
	private List<GameObject> trees;
	
	public GameObject bushPrefab;
	[Range(0, 1)] public float bushProbability = 1;
	public Vector2 bushArea = Vector2.one;
	public float bushRadius = 1f;
	
	private void Start()
	{
		GenerateTrees();
		GenerateBush();
	}

	private void GenerateTrees()
	{
		List<Vector2> points = PoissonDiscSampling.GeneratePoints(treeRadius, new Vector2(1000, 1000), 10);
		trees = new List<GameObject>();

		for (int i = 0; i < points.Count; i++)
		{
			if(Random.value > treeProbability)
			{
				trees.Add(Instantiate(treePrefab, new Vector3(points[i].x, 0, points[i].y), Quaternion.identity));
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
				Vector2 direction = Random.insideUnitCircle * PoissonDiscSampling.NextGaussian() * bushArea.x;
				Instantiate(bushPrefab, trees[i].transform.position + new Vector3(direction.x, 0, direction.y),
					Quaternion.identity);
			}
		}
	}
}