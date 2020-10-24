using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSolver : MonoBehaviour
{
    public Transform target;
    int segmentCount;
    IKSegment[] segments;
    Transform[] segmentPoints;
    Vector3[] points;
    float[] lengths;
    float totalLength;

    // Start is called before the first frame update
    void Start()
    {
        segments = GetComponentsInChildren<IKSegment>();
        segmentCount = segments.Length;
        segmentPoints = new Transform[segmentCount + 1];

        for (int i = 0; i < segmentCount; i++)
            segmentPoints[i] = segments[i].transform;

        segmentPoints[segmentCount] = segments[segmentCount - 1].next;

        lengths = new float[segmentCount];
        points = new Vector3[segmentCount + 1];
        MapLength();
    }

    private void OnEnable()
    {
        if (target == null)
        {
            target = transform.Find("Target");
            if (target == null)
                enabled = false;
        }
    }

    private void LateUpdate()
    {
        Solve();
    }

    private void MapToPoints()
    {
        for (int i = 0; i <= segmentCount; i++)
            points[i] = segmentPoints[i].position;
    }

    private void MapToSegments()
    {
        for (int i = 0; i < segmentCount; i++)
            segments[i].SetEndPos(points[i+1]);
    }

    private void MapLength()
    {
        for (int i = 0; i < segmentCount; i++)
            lengths[i] = segments[i].length;
    }

    private bool run;

    private void Solve()
    {
        Vector3 targetPos = target.position;

        MapToPoints();

        for (int i = 0; i < 1; i++) // Yes. Only iterate once, for testing.
        {
            // Backward
            points[segmentCount] = targetPos;
            for (int b = segmentCount - 1; b > 0; b--)
            {
                points[b] = (points[b] - points[b + 1]).normalized * lengths[b] + points[b + 1];
            }

            // Forward
            for (int f = 1; f <= segmentCount; f++)
            {
                points[f] = (points[f] - points[f - 1]).normalized * lengths[f - 1] + points[f - 1];
            }
        }

        run = run != Input.GetKey(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.Space) || run)
        {
            MapToSegments();
        }
    }

    void OnDrawGizmos()
    {
        if (points == null) return;
        Vector3 lastPoint = transform.position;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point, 0.25f);
            Gizmos.DrawLine(point, lastPoint);
            lastPoint = point;
        }
    }
}
