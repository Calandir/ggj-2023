using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootEndHitbox : MonoBehaviour
{
	private LevelTilemapSingleton m_levelTilemapManager = null;
	private WaterManager m_waterManager = null;

	private static HashSet<Vector3Int> s_consumedWaterLocations = new HashSet<Vector3Int>();

	private void Start()
	{
		m_levelTilemapManager = LevelTilemapSingleton.Instance;
		m_waterManager = WaterManager.Instance;
	}

	private void Update()
	{
		Vector3Int position = MiscUtils.Vector3ToVector3Int(transform.position);
		TileBase detectedTile = m_levelTilemapManager.Tilemap.GetTile(position);

		if (detectedTile is WaterTile && !s_consumedWaterLocations.Contains(position))
		{
			m_waterManager.AwardWater(WaterTile.WATER_PER_TILE);
			s_consumedWaterLocations.Add(position);
		}
	}
}
