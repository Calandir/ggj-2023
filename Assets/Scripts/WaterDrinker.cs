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
		return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
	}
}
