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
		Vector3Int tilePosition = MiscUtils.Vector3ToVector3Int(transform.position);
		TileBase detectedTile = m_levelTilemapManager.GetTile(tilePosition);
        Tilemap detectedTilemap = m_levelTilemapManager.GetTilemapForTile(tilePosition);

        if (detectedTile is WaterTile waterTile)
		{
			WaterTileData waterData = detectedTilemap.GetInstantiatedObject(tilePosition).GetComponent<WaterTileData>();

            if (waterData.HasWater)
            {
				waterTile.ConsumeWater(waterData);
                waterTile.RefreshTile(tilePosition, detectedTilemap);
            }
        }
		else if (detectedTile is RockTile)
		{
			m_hasCollidedWithRock = true;
		}
		else if (detectedTile is StreamTile)
		{
			var rotation = detectedTilemap.GetTransformMatrix(tilePosition).rotation.eulerAngles;
            Debug.Log($"Stream Tile Rotation: {rotation}");
		}

        m_isInRoughDirt = detectedTile is RoughDirtTile;
    }
}
