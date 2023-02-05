using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
	private Tilemap m_tilemap;

	[SerializeField] TileBase TileDirt;
	[SerializeField] TileBase TileWater;
	[SerializeField] TileBase TileRock;

	[SerializeField] public float PROPORTION_WATER_AT_TOP;
	[SerializeField] public float PROPORTION_WATER_AT_BOTTOM;
	[SerializeField] public float PROPORTION_ROCKS_AT_TOP;
	[SerializeField] public float PROPORTION_ROCKS_AT_BOTTOM;

	[SerializeField] public int X_MIN;
	[SerializeField] public int Y_MIN;
	[SerializeField] public int X_SIZE;
	[SerializeField] public int Y_SIZE;

	private void Awake()
	{
		m_tilemap = GetComponent<Tilemap>();
		Generate();
	}

	void Generate()
	{
		BoundsInt bounds = new BoundsInt(xMin: X_MIN, yMin: Y_MIN, zMin: 0, sizeX: X_SIZE, sizeY: Y_SIZE, sizeZ: 1);
		TileBase[] tileArray = new TileBase[X_SIZE * Y_SIZE];

		for (int row = 0; row < Y_SIZE; row++)
		{
			List<TileBase> newRow = new List<TileBase>(X_SIZE);

			int numWaterTiles = (int)(X_SIZE * Mathf.Lerp(PROPORTION_WATER_AT_BOTTOM, PROPORTION_WATER_AT_TOP, (float)row / Y_SIZE));
			for (int i = 0; i < numWaterTiles; i++)
			{
				newRow.Add(TileWater);
			}
			int numRockTiles = (int)(X_SIZE * Mathf.Lerp(PROPORTION_ROCKS_AT_BOTTOM, PROPORTION_ROCKS_AT_TOP, (float)row / Y_SIZE));
			for (int i = 0; i < numRockTiles; i++)
			{
				newRow.Add(TileRock);
			}
			while (newRow.Count < X_SIZE)
			{
				newRow.Add(TileDirt);
			}

			newRow.Shuffle();
			for (int i = 0; i < X_SIZE; i++)
			{
				tileArray[row * X_SIZE + i] = newRow[i];
			}
		}

		m_tilemap.SetTilesBlock(bounds, tileArray);
	}
}
