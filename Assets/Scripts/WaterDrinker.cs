using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDrinker : MonoBehaviour
{
	private LevelTilemapSingleton m_levelTilemapManager = null;

	private void Start()
	{
		m_levelTilemapManager = LevelTilemapSingleton.Instance;
	}

	private void Update()
	{
		Vector3Int position = GetPositionAsVector3Int(transform.position);

		TileBase tile = m_levelTilemapManager.Tilemap.GetTile(position);

		if (tile is WaterTile waterTile)
		{
			waterTile.ConsumeWater();
		}
	}

	private Vector3Int GetPositionAsVector3Int(Vector3 vector3)
	{
		return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
	}
}
