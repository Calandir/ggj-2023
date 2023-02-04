using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMover : MonoBehaviour
{
    internal void PointTowards(Vector2 look)
    {
        transform.right = look;
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
