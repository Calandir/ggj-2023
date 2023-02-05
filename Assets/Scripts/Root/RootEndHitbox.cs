using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RootEndHitbox : MonoBehaviour
{
	public bool HasCollidedWithRock => m_hasCollidedWithRock;
	private bool m_hasCollidedWithRock = false;

	private LevelTilemapSingleton m_levelTilemapManager = null;
	private WaterManager m_waterManager = null;

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

		if (detectedTile is WaterTile waterTile)
		{
			WaterTileData waterData = m_levelTilemapManager.Tilemap.GetInstantiatedObject(position).GetComponent<WaterTileData>();

            if (waterData.HasWater)
            {
				waterTile.ConsumeWater(waterData);
                waterTile.RefreshTile(position, m_levelTilemapManager.Tilemap);
            }
        }
		else if (detectedTile is RockTile)
		{
			m_hasCollidedWithRock = true;
		}

        m_isInRoughDirt = detectedTile is RoughDirtTile;
    }
}
