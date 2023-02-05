using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMover : MonoBehaviour
{
    [SerializeField]
    private float m_rotationMultiplier = 40f;
    [SerializeField]
    private float m_growMultiplier = 0.5f;

    public bool Slowed { get; internal set; }
    [SerializeField]
    private float m_slowedMultiplier = 0.7f;
    private float CurrentSlowedMultiplier => Slowed ? m_slowedMultiplier : 1f;

    internal void Rotate(float input)
    {
        float degrees = input * m_rotationMultiplier * Time.deltaTime;
        Vector3 newEuler = transform.eulerAngles;
        newEuler.z -= degrees;
        transform.eulerAngles = newEuler;
    }

    internal void RotateTowards(Vector3 destination)
    {
        var posDelta = (destination - transform.position).normalized;
        Vector3 directionTarget = posDelta;
        transform.up = Vector3.MoveTowards(transform.up, directionTarget, 0.1f * Time.deltaTime);
    }

    internal void Grow(float controlledMultiplier)
    {
        Vector3 movement = controlledMultiplier * m_growMultiplier * CurrentSlowedMultiplier * Time.deltaTime * transform.up;
        transform.position += movement;
    }
}
