using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class RootsController : MonoBehaviour
{
    [SerializeField]
    private Root m_startingRoot;
    public Root StartingRoot => m_startingRoot;

    [SerializeField]
    private Root m_controlledRoot;
    public Root ControlledRoot { get => m_controlledRoot; set => SetControlledRoot(value); }

    private void SetControlledRoot(Root value)
    {
        m_controlledRoot.EndObj.sprite = RootEndSprite;
        m_controlledRoot = value;
        m_controlledRoot.EndObj.sprite = HighlightedRootEndSprite;
    }

    bool moveHeld;
    Vector2 lastMovementInput;

    [SerializeField]
    private Root m_newRootPrefab;


    [SerializeField]
    public Sprite RootEndSprite;

    [SerializeField]
    public Sprite HighlightedRootEndSprite;


    public static RootsController Instance;

    public static Action<Root> RootCreatedAction;
    public static Action<Root> RootFinishedAction;

    private void Awake()
    {
        Instance = this;

        MiscUtils.IsGameOver = false;
	}

    private void Start()
    {
        ControlledRoot = m_startingRoot;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        lastMovementInput = context.ReadValue<Vector2>();
        //Debug.Log($"Move Input: {lastMovementInput}");
        moveHeld = true;
    }

    public void Update()
    {
        if (MiscUtils.IsGameOver)
        {
            return;
        }

        if (moveHeld && lastMovementInput.magnitude > float.Epsilon)
        {
            if (ControlledRoot.CanGrow)
            {
                ControlledRoot.Movement.Rotate(lastMovementInput.x);
            }
        }


        Root[] nonControlledRoots = GetAllRoots().Where(_x => _x != ControlledRoot).ToArray();
        foreach (var root in nonControlledRoots)
        {
            if (root.CanGrow)
            {
                Vector3 destination = GetClosestTileOfType(root.transform.position, typeof(WaterTile));
                root.Movement.RotateTowards(destination);
            }
        }

        Root[] roots = GetAllRoots();
        foreach (var root in roots)
        {
            if (root.CanGrow)
            {
                float controlledMultiplier = (root == ControlledRoot) ? 1f : 0.5f;
                root.Movement.Grow(controlledMultiplier);
            }
        }
    }

    Vector3Int GetClosestTileOfType(Vector3 worldPosition, Type targetTileType)
    {
        var tilemap = LevelTilemapSingleton.Instance.Tilemap;
        var grid = tilemap.layoutGrid;
        Vector3Int closestTile = Vector3Int.zero;
        float closestDistance = float.MaxValue;

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile && tile.GetType() == targetTileType)
            {
                Vector3 cellCenterWorldPos = grid.LocalToWorld(grid.CellToLocalInterpolated(position + new Vector3(.5f, .5f, 0f)));
                float distance = Vector3.Distance(cellCenterWorldPos, worldPosition);
                if (distance < closestDistance)
                {
                    closestTile = position;
                    closestDistance = distance;
                }
            }
        }

        return closestTile;
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
            Root[] newRoots = ControlledRoot.Split(m_newRootPrefab);
            ControlledRoot = newRoots[1];
        }
    }

    public void OnSwitchInput(InputAction.CallbackContext context)
    {
        float fire = context.ReadValue<float>();
        Debug.Log($"Switch Input: {fire}");
        if (fire == 1f)
        {
            SwitchRoot();
        }
    }

    public void SwitchRoot()
    {
        var roots = GetAllRoots().Where(_x => _x.CanGrow).ToArray();
        if (roots.Length  > 0)
        {
            int controlledRootIndex = Array.IndexOf(roots, ControlledRoot);
            int newIndex = (controlledRootIndex + 1) % roots.Length;
            ControlledRoot = roots[newIndex];
        }
        else
        {
            GameObject obj = GameObject.Find("Canvas");
            obj = obj.transform.Find("GameOverPanel").gameObject;
            obj.SetActive(true);
            Text text = obj.transform.Find("Text").GetComponent<Text>();
            text.text = "All your roots have died!";
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        //Debug.Log($"Look Input: {look}");
    }

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}
}
