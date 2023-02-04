using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class RootBodySpawning : MonoBehaviour
{
    [SerializeField]
    private SplineContainer m_spline;

    [SerializeField]
    private float m_minDistanceBetweenSplineKnots = 1f;

    [SerializeField]
    private float m_distance = -1;

    [SerializeField]
    private GameObject m_segmentPrefab;

    private void Awake()
    {
        m_spline.transform.SetParent(null);
        m_spline.transform.localPosition = Vector3.zero;
        BezierKnot knot = new BezierKnot(transform.position);
        m_spline.Spline.Add(knot);
    }

    private void Update()
    {
        if (m_distance == -1 || m_distance >= m_minDistanceBetweenSplineKnots)
        {
            BezierKnot knot = new BezierKnot(transform.position);
            m_spline.Spline.Add(knot);
            GameObject segment = Instantiate(m_segmentPrefab, m_spline.transform);
            segment.transform.position = transform.position;
            segment.transform.up = -transform.up;
        }

        var lastKnot = m_spline.Spline.Knots.Last();
        var distance = Vector3.Distance(transform.position, lastKnot.Position);
        m_distance = distance;
    }
}