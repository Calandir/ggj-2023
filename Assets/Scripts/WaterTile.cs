using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "RockTile", menuName = "Tiles/WaterTile")]
public class WaterTile : Tile
{
	public static float WATER_PER_TILE => s_waterPerTile;
	private static float s_waterPerTile = 1.0f;

	//[SerializeField]
	//private Color m_hasWaterColor;

	//[SerializeField]
	//private Color m_noWaterColor;

	[SerializeField]
	private float m_WaterPerTile = 1.0f;

	//private float m_currentWater;

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		//color = m_hasWaterColor;
		//m_currentWater = m_startingWaterPerTile;

		s_waterPerTile = m_WaterPerTile;

		return base.StartUp(position, tilemap, go);
	}

	//public void ConsumeWater()
	//{
	//	if (m_currentWater <= 0.0f)
	//	{
	//		return;
	//	}

	//	WaterManager.Instance.AwardWater(m_currentWater);

	//	m_currentWater = 0.0f;
	//	color = m_noWaterColor;
	//}
}
