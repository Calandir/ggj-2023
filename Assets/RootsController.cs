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

    public void OnLookInput(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        Debug.Log($"Look Input: {look}");

        m_controlledRoot.Movement.PointTowards(look);
    }
}
