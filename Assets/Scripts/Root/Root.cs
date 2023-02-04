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
        rootLeft.transform.localEulerAngles = new Vector3(
            rootLeft.transform.localEulerAngles.x, rootLeft.transform.localEulerAngles.y, rootLeft.transform.localEulerAngles.z - 45f);
        Root rootRight = Instantiate(m_newRootPrefab);
        rootRight.Initialise();
        rootRight.transform.localEulerAngles = new Vector3(
            rootRight.transform.localEulerAngles.x, rootRight.transform.localEulerAngles.y, rootRight.transform.localEulerAngles.z + 45f);
        m_childRoots = new Root[] { rootLeft, rootRight};
        return m_childRoots;
    }

    private void Initialise()
    {
        m_canGrow = true;
    }
}
