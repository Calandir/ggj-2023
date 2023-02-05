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
    private SpriteRenderer m_endObj;
    public SpriteRenderer EndObj => m_endObj;

    private RootEndHitbox m_rootEndHitbox = null;

	private void Start()
	{
		m_rootEndHitbox = GetComponentInChildren<RootEndHitbox>(includeInactive: true);
	}

	private void Update()
	{
		if (m_canGrow && m_rootEndHitbox.HasCollidedWithRock)
        {
            Kill();
        }
        m_movement.Slowed = m_rootEndHitbox.IsInRoughDirt;
	}

	internal Root[] Split(Root newRootPrefab)
    {
        WaterManager.Instance.SpendWater(WaterManager.Instance.WaterLossPerSplit);
        Debug.Log("Split");
        Finished();
        Root rootLeft = Instantiate(newRootPrefab, transform.position, transform.rotation);
        rootLeft.Initialise();
        rootLeft.transform.localEulerAngles = new Vector3(
            rootLeft.transform.localEulerAngles.x, rootLeft.transform.localEulerAngles.y, rootLeft.transform.localEulerAngles.z - 45f);
        Root rootRight = Instantiate(newRootPrefab, transform.position, transform.rotation);
        rootRight.Initialise();
        rootRight.transform.localEulerAngles = new Vector3(
            rootRight.transform.localEulerAngles.x, rootRight.transform.localEulerAngles.y, rootRight.transform.localEulerAngles.z + 45f);
        m_childRoots = new Root[] { rootLeft, rootRight};
        return m_childRoots;
    }

    private void Initialise()
    {
        m_canGrow = true;
        RootsController.RootCreatedAction?.Invoke(this);
    }

    private void Finished()
    {
        m_canGrow = false;
        m_rigidbody.isKinematic = true;
        m_endObj.gameObject.SetActive(false);
        RootsController.RootFinishedAction?.Invoke(this);
    }

    private void Kill()
    {
        Finished();

        RootsController.Instance.SwitchRoot();
    }
}
