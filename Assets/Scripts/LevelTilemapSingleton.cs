using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelTilemapSingleton : MonoBehaviour
{
	public static LevelTilemapSingleton Instance => s_instance;
	private static LevelTilemapSingleton s_instance = null;

	public Tilemap Tilemap => m_tilemap;

	[SerializeField]
	private Tilemap m_tilemap = null;

	private void Awake()
	{
		if (s_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		s_instance = this;
	}
}
