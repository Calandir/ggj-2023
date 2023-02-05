using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileData : MonoBehaviour
{
    [SerializeField]
    private float m_currentWater;

    public bool HasWater => m_currentWater > float.Epsilon;

    internal void Initialise(float startingWater)
    {
        m_currentWater = startingWater;
    }

    internal void ConsumeWater()
    {
        if (!HasWater)
        {
            return;
        }

        WaterManager.Instance.AwardWater(m_currentWater);
        m_currentWater = 0.0f;
    }
}
