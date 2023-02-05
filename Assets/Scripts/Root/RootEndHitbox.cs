using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootEndHitbox : MonoBehaviour
{
	public bool HasCollidedWithRock => m_hasCollidedWithRock;
	private bool m_hasCollidedWithRock = false;

	private LevelTilemapSingleton m_levelTilemapManager = null;
	private WaterManager m_waterManager = null;

	public static HashSet<Vector3Int> s_consumedWaterLocations = new HashSet<Vector3Int>();

	[SerializeField]
	private bool m_isInRoughDirt = false;
	public bool IsInRoughDirt => m_isInRoughDirt;

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
		else if (detectedTile is RockTile)
		{
			m_hasCollidedWithRock = true;
		}

        m_isInRoughDirt = detectedTile is RoughDirtTile;
    }
}
