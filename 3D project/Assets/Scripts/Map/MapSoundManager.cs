using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSoundManager : MonoBehaviour
{
    [SerializeField] TerrainData Map;

    float[,,] splatmapData;

    void Awake()
    {
        
    }

    MapSoundManager Intance()
    {
        return this;
    }
}
