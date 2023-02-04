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
    }

    private void FitToSpline()
    {
        var splineFrom = m_spline.Spline;
        var splineTo = m_controller.spline;
        splineTo.Clear();

        if (splineFrom.Knots.Count() > 1)
        {
            float3 rotatedVector;
            float3 adjustedPos;
            foreach (var knot in splineFrom)
            {
                rotatedVector = math.mul(knot.Rotation, UnityEngine.Vector3.right);
                adjustedPos = knot.Position + (rotatedVector * 0.64f);
                splineTo.InsertPointAt(0, adjustedPos);
                splineTo.SetTangentMode(0, ShapeTangentMode.Continuous);
            }
            rotatedVector = math.mul(m_root.transform.rotation, UnityEngine.Vector3.right);
            adjustedPos = (float3)(m_root.transform.position) + (rotatedVector * 0.64f);
            if (splineTo.GetPointCount() > 0 && (splineTo.GetPosition(0) - (UnityEngine.Vector3)adjustedPos).sqrMagnitude < 0.001f)
            {
            }
            else
            {
                splineTo.InsertPointAt(0, adjustedPos);
            }

            m_controller.enabled = false;
            m_controller.enabled = true;
            m_controller.RefreshSpriteShape();
        }
    }
}
