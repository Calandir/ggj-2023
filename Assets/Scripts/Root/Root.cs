using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private List<Root> m_childRoots;
    public List<Root> ChildRoots => m_childRoots;

    [SerializeField]
    private bool m_canGrow = true;
    public bool CanGrow => m_canGrow;

    [SerializeField]
    private RootMover m_movement;
    public RootMover Movement => m_movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
