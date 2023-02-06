using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "WaterTile", menuName = "Tiles/WaterTile")]
public class WaterTile : Tile
{
	[SerializeField]
	private Sprite m_hasWaterSprite;

	[SerializeField]
	private Sprite m_noWaterSprite;

	[SerializeField]
	private float m_WaterPerTile = 1.0f;

	public WaterTileData Data => gameObject.GetComponent<WaterTileData>();

    public GameObject m_Prefab;

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        tileData.gameObject = m_Prefab;
        WaterTileData waterData = tileData.gameObject.GetComponent<WaterTileData>();
        base.GetTileData(location, tilemap, ref tileData);
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		sprite = m_hasWaterSprite;
        var waterData = go.GetComponent<WaterTileData>();
        if (waterData) waterData.Initialise(m_WaterPerTile);

		return base.StartUp(position, tilemap, go);
	}

    internal void ConsumeWater(WaterTileData waterData)
    {
        waterData.ConsumeWater();
        sprite = m_noWaterSprite;
    }
}
