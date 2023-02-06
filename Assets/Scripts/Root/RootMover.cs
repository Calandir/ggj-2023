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

    public Vector3 BeingPushedDirection { get; internal set; }

    [SerializeField]
    private GameObject m_debugTargetObj;

    private void Start()
    {
        m_debugTargetObj.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        RootsController.RootFinishedAction += RootFinished;
    }
    private void OnDisable()
    {
        RootsController.RootFinishedAction -= RootFinished;
    }

    internal void Rotate(float input)
    {
        float degrees = input * m_rotationMultiplier * Time.deltaTime;
        Vector3 newEuler = transform.eulerAngles;
        newEuler.z -= degrees;
        transform.eulerAngles = newEuler;
    }

    internal void RotateTowards(Vector3 destination)
    {
        m_debugTargetObj.gameObject.SetActive(true);
        m_debugTargetObj.transform.position = destination;

        var posDelta = (destination - transform.position).normalized;
        Vector3 directionTarget = posDelta;
        transform.up = Vector3.MoveTowards(transform.up, directionTarget, 0.25f * Time.deltaTime);
    }

    internal void Grow(float controlledMultiplier)
    {
        Vector3 movement = controlledMultiplier * m_growMultiplier * CurrentSlowedMultiplier * Time.deltaTime * transform.up;
        transform.position += movement;

        if (BeingPushedDirection.magnitude > float.Epsilon)
        {
            Vector3 pushedMovement = 0.75f * Time.deltaTime * BeingPushedDirection;
            transform.position += pushedMovement;
        }
    }
    private void RootFinished(Root root)
    {
        if (root.gameObject == gameObject)
        {
            m_debugTargetObj.gameObject.SetActive(false);
        }
    }
}
