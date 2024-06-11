using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private SaveLoadManager _saveLoadManager;
    public enum DrawMode
    {
        NoiseMap,
        FalloffMap,
        Mesh
    };
    public DrawMode drawMode;

    public TerrainData terrainData;
    public NoiseData noiseData;

    public EnviromentGenerator enviromentGenerator;

    public GameObject meshObject;

    public bool autoUpdate;

    public const int mapChunkSize = 255;

    MeshData meshData;
    float[,] falloffMap;



    private void Awake()
    {
        if (_saveLoadManager.GetIsNewSession(PlayerPrefs.GetInt("currenntSave")))
        {
            _saveLoadManager.SetSeed(PlayerPrefs.GetInt("currenntSave"), Random.Range(0, 2147483647));
            Debug.Log("Новый сид: " + _saveLoadManager.GetSeed(PlayerPrefs.GetInt("currenntSave")));
        }
        else
        {
            Debug.Log("Загруженный сид: " + _saveLoadManager.GetSeed(PlayerPrefs.GetInt("currenntSave")));
        }

        GenerateMap(_saveLoadManager.GetSeed(PlayerPrefs.GetInt("currenntSave")));

        meshObject.AddComponent<MeshCollider>();

        if (_saveLoadManager.GetIsNewSession(PlayerPrefs.GetInt("currenntSave")))
        {
            Debug.Log("new");
            enviromentGenerator.EnviromentGeneration(meshData, mapChunkSize);
        }
        else
        {
            Debug.Log("old");
            enviromentGenerator.EnviromentLoading(meshData, mapChunkSize);
        }

        _saveLoadManager.SetIsNewSession(PlayerPrefs.GetInt("currenntSave"), false);
    }

    public void GenerateMap(int seed)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, terrainData.falloffEvaluateA, terrainData.falloffEvaluateB);

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (terrainData.useFalloffMap)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        
        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve));
        
        meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve);
    }
}