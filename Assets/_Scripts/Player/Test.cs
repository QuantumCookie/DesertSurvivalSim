using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float aMax = 35;
    public float aMin = 16;
    public float aMedian = 25;
    
    public float bMax = 42;
    public float bMin = 32;
    public float bMedian = 37;

    public float bVal;
    
    [Range(16, 35)]
    public float aVal;

    private float InverseLerp(float a, float b, float val)
    {
        return (val - a) / (b - a);
    }

    private float Lerp(float a, float b, float t)
    {
        return a * t + (1 - t) * b;
    }
    
    private void OnDrawGizmos()
    {
        if(aVal > aMedian)
        {
            float t = InverseLerp(aMedian, aMax, aVal);
            bVal = Lerp(bMedian, bMax, t);
        }
        else
        {
            float t = InverseLerp(aMin, aMedian, aVal);
            bVal = Lerp(bMin, bMedian, t);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + transform.forward * aMin, transform.forward * (aMax - aMin));
        Gizmos.DrawRay(transform.position + transform.forward * aMin + transform.up, transform.forward * aVal);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + transform.forward * bMin + transform.up * 2, transform.forward * (bMax - bMin));
        Gizmos.DrawRay(transform.position + transform.forward * bMedian + transform.up * 3, transform.forward * bVal);
    }
}
