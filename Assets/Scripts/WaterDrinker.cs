using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDrinker : MonoBehaviour
{
	private LevelTilemapSingleton m_levelTilemapManager = null;

	private static HashSet<Vector3Int> s_consumedWaterLocations = new HashSet<Vector3Int>();

	private WaterManager m_waterManager = null;

	private void Start()
	{
		m_levelTilemapManager = LevelTilemapSingleton.Instance;
		m_waterManager = WaterManager.Instance;
	}

	private void Update()
	{
		Vector3Int position = GetPositionAsVector3Int(transform.position);
		if (s_consumedWaterLocations.Contains(position))
		{
			return;
		}

		TileBase tile = m_levelTilemapManager.Tilemap.GetTile(position);
		if (tile is WaterTile)
		{
			m_waterManager.AwardWater(WaterTile.WATER_PER_TILE);
			s_consumedWaterLocations.Add(position);
		}
	}

	private Vector3Int GetPositionAsVector3Int(Vector3 vector3)
	{
		// Offset so drinking happens from the bottom of the root
		vector3 -= new Vector3(0.0f, 1.5f, 0.0f);

		Vector3Int position = new Vector3Int((int)Mathf.Round(vector3.x - 0.5f), (int)Mathf.Round(vector3.y + 0.5f), (int)Mathf.Round(vector3.z - 0.5f));

		return position;
	}
}
