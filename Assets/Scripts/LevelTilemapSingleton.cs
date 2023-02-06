using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTilemapSingleton : MonoBehaviour
{
	public static LevelTilemapSingleton Instance => s_instance;
	private static LevelTilemapSingleton s_instance = null;

	[SerializeField]
	private List<Tilemap> m_tilemapPriorityList;
    public List<Tilemap> TilemapPriorityList => m_tilemapPriorityList;

	private void Awake()
	{
		if (s_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		s_instance = this;
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}

	public TileBase GetTile(Vector3Int tilePosition)
	{
		for (int i = 0; i < m_tilemapPriorityList.Count; i++)
		{
			TileBase tile = m_tilemapPriorityList[i].GetTile(tilePosition);
			if (tile != null)
			{
				return tile;
			}
		}
		return null;
	}

	public Tilemap GetTilemapForTile(Vector3Int tilePosition)
	{
		for (int i = 0; i < m_tilemapPriorityList.Count; i++)
        {
            TileBase tile = m_tilemapPriorityList[i].GetTile(tilePosition);
			if (tile != null)
			{
				return m_tilemapPriorityList[i];
			}
        }
		return null;
	}
}
