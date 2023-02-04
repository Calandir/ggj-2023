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

    internal void Rotate(float input)
    {
        float degrees = input * m_rotationMultiplier * Time.deltaTime;
        Vector3 newEuler = transform.eulerAngles;
        newEuler.z -= degrees;
        transform.eulerAngles = newEuler;
    }

    internal void Grow(float controlledMultiplier)
    {
        Vector3 movement = controlledMultiplier * m_growMultiplier * Time.deltaTime * transform.up;
        transform.position += movement;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
