using UnityEngine;

public static class MiscUtils
{
	public static Vector3Int Vector3ToVector3Int(Vector3 vector3)
	{
		return new Vector3Int(
			(int)Mathf.Round(vector3.x - 0.5f),
			(int)Mathf.Round(vector3.y - 0.5f),
			(int)Mathf.Round(vector3.z - 0.5f));
	}
}
