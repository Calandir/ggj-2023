using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private Root[] m_childRoots;
    public Root[] ChildRoots => m_childRoots;

    [SerializeField]
    private bool m_canGrow = true;
    public bool CanGrow => m_canGrow;

    [SerializeField]
    private RootMover m_movement;
    public RootMover Movement => m_movement;

    [SerializeField]
    private Root m_newRootPrefab;

    internal Root[] Split()
    {
        Debug.Log("Split");
        m_canGrow = false;
        m_rigidbody.isKinematic = true;
        Root rootLeft = Instantiate(m_newRootPrefab);
        rootLeft.Initialise();
        Root rootRight = Instantiate(m_newRootPrefab);
        rootRight.Initialise();
        m_childRoots = new Root[] { rootLeft, rootRight};
        return m_childRoots;
    }

    private void Initialise()
    {
        m_canGrow = true;
    }
}
