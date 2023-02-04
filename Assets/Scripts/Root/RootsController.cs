using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootsController : MonoBehaviour
{
    [SerializeField]
    private Root m_startingRoot;
    public Root StartingRoot => m_startingRoot;

    [SerializeField]
    private Root m_controlledRoot;
    public Root ControlledRoot => m_controlledRoot;

    bool moveHeld;
    Vector2 lastMovementInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        lastMovementInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {lastMovementInput}");
        moveHeld = true;
    }

    public void Update()
    {
        if (moveHeld && lastMovementInput.magnitude > float.Epsilon)
        {
            if (m_controlledRoot.CanGrow)
            {
                m_controlledRoot.Movement.Rotate(lastMovementInput.x);
            }
        }

        Root[] roots = GetAllRoots();
        foreach (var root in roots)
        {
            if (root.CanGrow)
            {
                float controlledMultiplier = (root == m_controlledRoot) ? 1f : 0.25f;
                root.Movement.Grow(controlledMultiplier);
            }
        }
    }

    private Root[] GetAllRoots()
    {
        Queue<Root> roots = new Queue<Root>();
        List<Root> rootsToReturn = new List<Root>();
        roots.Enqueue(m_startingRoot);
        while (roots.Count > 0)
        {
            var root = roots.Dequeue();
            rootsToReturn.Add(root);
            foreach (var child in root.ChildRoots)
            {
                roots.Enqueue(child);
            }
        }
        return rootsToReturn.ToArray();
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        float fire = context.ReadValue<float>();
        Debug.Log($"Fire Input: {fire}");
        if (fire == 1f)
        {
            Root[] newRoots = ControlledRoot.Split();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        //Debug.Log($"Look Input: {look}");
    }
}
