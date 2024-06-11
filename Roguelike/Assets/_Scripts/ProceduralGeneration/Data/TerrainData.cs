using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TerrainData", fileName = "TerrainData")]
public class TerrainData : UpdatableData
{
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool useFalloffMap;
    [Range(1f, 5f)]
    public float falloffEvaluateA;
    [Range(1f, 10f)]
    public float falloffEvaluateB;

}
