using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "RockTile", menuName = "Tiles/WaterTile")]
public class WaterTile : Tile
{
	[SerializeField]
	private float m_startingWaterPerTile = 1.0f;

	private float m_currentWater;

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		m_currentWater = m_startingWaterPerTile;

		return base.StartUp(position, tilemap, go);
	}

	public void ConsumeWater()
	{
		if (m_currentWater <= 0.0f)
		{
			return;
		}

		WaterManager.Instance.AwardWater(m_currentWater);

		m_currentWater = 0.0f;
	}
}
