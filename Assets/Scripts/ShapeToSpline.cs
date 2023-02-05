using SpriteShapeExtras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class ShapeToSpline : MonoBehaviour
{
    [SerializeField]
    private SpriteShapeController m_controller;

    [SerializeField]
    private SplineContainer m_spline;

    [SerializeField]
    private Root m_root;

    [SerializeField]
    private EdgeCollider2D m_collider;

    private void Start()
    {
        transform.SetParent(null);
        transform.localPosition = UnityEngine.Vector3.zero;
        transform.localRotation = UnityEngine.Quaternion.identity;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) 
        {
            FitToSpline();
        }
        m_controller.BakeCollider();
    }

    private void FitToSpline()
    {
        var splineFrom = m_spline.Spline;
        var splineTo = m_controller.spline;
        splineTo.Clear();

        m_collider.points = new UnityEngine.Vector2[0];

        UnityEngine.Vector2[] newPoints = new UnityEngine.Vector2[splineFrom.Knots.Count() - 1];

        float3 adjustedPos;
        int i = 0;
        foreach (var knot in splineFrom)
        {
            adjustedPos = knot.Position;
            splineTo.InsertPointAt(0, knot.Position);
            splineTo.SetTangentMode(0, ShapeTangentMode.Continuous);

            if (i < splineFrom.Knots.Count()-1)
            {
                newPoints[i] = new UnityEngine.Vector2(knot.Position.x, knot.Position.y);
            }

            i++;
        }
        adjustedPos = (float3)(m_root.transform.position);
        splineTo.InsertPointAt(0, adjustedPos);

        m_controller.enabled = false;
        m_controller.enabled = true;
        m_controller.RefreshSpriteShape();

        m_collider.points = newPoints;
    }
}
