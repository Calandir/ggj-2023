using System.Collections.Generic;
using UnityEngine;

public static class MiscUtils
{
	public static bool IsGameOver = false;

	public static Vector3Int Vector3ToVector3Int(Vector3 vector3)
	{
		return new Vector3Int(
			(int)Mathf.Round(vector3.x - 0.5f),
			(int)Mathf.Round(vector3.y - 0.5f),
			(int)Mathf.Round(vector3.z - 0.5f));
	}

	// https://stackoverflow.com/questions/273313/randomize-a-listt
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
