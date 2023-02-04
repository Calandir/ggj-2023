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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            FitToSpline();
        }
    }

    private void FitToSpline()
    {
        var splineFrom = m_spline.Spline;
        var splineTo = m_controller.spline;
        splineTo.Clear();
        foreach (var knot in splineFrom)
        {
            var rotatedVector = math.mul(knot.Rotation, UnityEngine.Vector3.right);
            float3 adjustedPos = knot.Position + (rotatedVector * 0.64f);
            splineTo.InsertPointAt(0, adjustedPos);
            splineTo.SetTangentMode(0, ShapeTangentMode.Continuous);
        }
        m_controller.enabled = false;
        m_controller.enabled = true;
        m_controller.RefreshSpriteShape();
    }
}
