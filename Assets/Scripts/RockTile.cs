using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "RockTile", menuName = "Tiles/RockTile")]
public class RockTile : Tile
{
	[SerializeField]
	private Sprite m_sprite;

	public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData(position, tilemap, ref tileData);
	}
}
