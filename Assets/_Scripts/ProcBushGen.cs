using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcBushGen : MonoBehaviour
{
    public Vector3 origin;
    public float minX, maxX, minZ, maxZ;

    public GameObject prefab;

    private void Start()
    {
        Vector2 topLeft = new Vector2(minX, maxZ);
        Vector2 bottomRight = new Vector2(maxX, minZ);
        // List<Vector2> points = AwesomeNamespace.UniformPoissonDiskSampler.SampleRectangle(topLeft, bottomRight, 10f);
        List<Vector2> points = AwesomeNamespace.UniformPoissonDiskSampler.SampleCircle(origin, 100, 5f);

        for(int i = 0; i < points.Count; i++)
        {
            Instantiate(prefab, new Vector3(points[i].x, 0, points[i].y), Quaternion.identity);
        }
    }
}
