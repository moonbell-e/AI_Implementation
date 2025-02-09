using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Playables;
using static Save;

public class EnviromentGenerator : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private ProceduralEnemyFactory _proceduralEnemyFactory;

    [Header("Prefabs")]
    public GameObject[] playerProps;
    public GameObject[] bossProps;
    public GameObject[] smallPointOfInterestProps;
    public GameObject[] bigPointOfInterestProps;
    public GameObject[] foods;
    public GameObject[] lowBeachObjects;
    public GameObject[] highBeachObjects;
    public GameObject[] lowForestObjects;
    public GameObject[] highForestObjects;

    [Header("Heights")]
    public float waterHeight;
    public float lowBeachHeight;
    public float highBeachHeight;
    public float lowForestHeight;
    public float highForestHeight;

    [Header("Sizes of zones")]
    public int playerZoneSize;
    public int bossZoneSize;

    public void EnviromentGeneration(MeshData meshData, int mapChunkSize)
    {
        Vector3[,] vertexMap = MakeVertexMap(meshData, mapChunkSize);
        bool[,] highMap = MakeHighMap(vertexMap, mapChunkSize);

        highMap = FindPlayer(vertexMap, highMap, mapChunkSize);
        highMap = FindBoss(vertexMap, highMap, mapChunkSize);

        highMap = FindBigPointOfInterest(vertexMap, highMap, mapChunkSize);
        highMap = FindSmallPointOfInterest(vertexMap, highMap, mapChunkSize);

        SpawnOfEnvoroment(vertexMap, highMap, mapChunkSize);
    }

    public void EnviromentLoading(MeshData meshData, int mapChunkSize)
    {
        Vector3[,] vertexMap = MakeVertexMap(meshData, mapChunkSize);
        bool[,] highMap = MakeHighMap(vertexMap, mapChunkSize);

        Save save = _saveLoadManager.GetSave(PlayerPrefs.GetInt("currenntSave"));

        SpawnPlayer(vertexMap, highMap, save.playerLocation.x, save.playerLocation.y, save.startIngredientId);
        SpawnBoss(vertexMap, highMap, save.bossLocation.x, save.bossLocation.y);

        foreach (POI item in save.pointsOfInterest)
        {
            SpawnBigPointOfInterest01(vertexMap, highMap, item.location.x, item.location.y, false);
        }
        foreach (SPOI item in save.smallPointsOfInterest)
        {
            switch (save.smallPointsOfInterest.IndexOf(item) % 3)
            {
                case 0:
                    SpawnSmallPointOfInterest01(vertexMap, highMap, item.location.x, item.location.y, false);
                    break;
                case 1:
                    SpawnSmallPointOfInterest02(vertexMap, highMap, item.location.x, item.location.y, false);
                    break;
                case 2:
                    SpawnSmallPointOfInterest03(vertexMap, highMap, item.location.x, item.location.y, false);
                    break;
            }
        }

        for (int xMod = -playerZoneSize; xMod <= playerZoneSize; xMod++)
        {
            for (int yMod = -playerZoneSize; yMod <= playerZoneSize; yMod++)
            {
                highMap[save.playerLocation.x + xMod, save.playerLocation.y + yMod] = false;
            }
        }
        for (int xMod = -bossZoneSize; xMod <= bossZoneSize; xMod++)
        {
            for (int yMod = -bossZoneSize; yMod <= bossZoneSize; yMod++)
            {
                highMap[save.bossLocation.x + xMod, save.bossLocation.y + yMod] = false;
            }
        }

        SpawnOfEnvoroment(vertexMap, highMap, mapChunkSize);
    }

    //Делаю стартовые вычисления
    public Vector3[,] MakeVertexMap(MeshData meshData, int mapChunkSize)
    {
        Vector3[,] vertexMap = new Vector3[mapChunkSize, mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                vertexMap[x, y] = meshData.vertices[x * mapChunkSize + y] * 2;
            }
        }

        return vertexMap;
    }

    public bool[,] MakeHighMap(Vector3[,] vertexMap, int mapChunkSize)
    {
        bool[,] highMap = new bool[mapChunkSize, mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (vertexMap[x, y].y < waterHeight || vertexMap[x, y].y >= highForestHeight)
                {
                    highMap[x, y] = false;
                } else
                {
                    highMap[x, y] = true;
                }
            }
        }

        return highMap;
    }

    //Спавн игрока
    public bool[,] FindPlayer(Vector3[,] vertexMap, bool[,] highMap, int mapChunkSize)
    {
        bool isPlacable = true;

        for (int x = mapChunkSize; x > mapChunkSize / 2; x--)
        {
            for (int y = 0; y < mapChunkSize / 2; y++)
            {

                for (int xMod = -playerZoneSize; xMod <= playerZoneSize; xMod++)
                {
                    for (int yMod = -playerZoneSize; yMod <= playerZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -playerZoneSize; xMod <= playerZoneSize; xMod++)
                    {
                        for (int yMod = -playerZoneSize; yMod <= playerZoneSize; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetPlayer(PlayerPrefs.GetInt("currenntSave"), x, y, Random.Range(0, foods.Length));
                    SpawnPlayer(vertexMap, highMap, x, y, _saveLoadManager.GetStartIngredientId(PlayerPrefs.GetInt("currenntSave")));

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        for (int x = mapChunkSize; x > mapChunkSize / 2; x--)
        {
            for (int y = mapChunkSize; y > mapChunkSize / 2; y--)
            {

                for (int xMod = -playerZoneSize; xMod <= playerZoneSize; xMod++)
                {
                    for (int yMod = -playerZoneSize; yMod <= playerZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -playerZoneSize; xMod <= playerZoneSize; xMod++)
                    {
                        for (int yMod = -playerZoneSize; yMod <= playerZoneSize; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetPlayer(PlayerPrefs.GetInt("currenntSave"), x, y, Random.Range(0, foods.Length));
                    SpawnPlayer(vertexMap, highMap, x, y, _saveLoadManager.GetStartIngredientId(PlayerPrefs.GetInt("currenntSave")));

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        return highMap;
    }

    public void SpawnPlayer(Vector3[,] vertexMap, bool[,] highMap, int x, int y, int foodId)
    {
        playerProps[0].transform.position = vertexMap[x, y];

        GameObject.Instantiate(playerProps[2], vertexMap[x - 1, y - 2], Quaternion.identity);
        GameObject.Instantiate(foods[foodId], vertexMap[x - 1, y - 2] + new Vector3(0, 1, 0), Quaternion.identity);

        GameObject.Instantiate(playerProps[3], vertexMap[x - 2, y + 3], Quaternion.identity);
        GameObject.Instantiate(playerProps[4], vertexMap[x + 2, y + 3], Quaternion.identity);
        GameObject.Instantiate(playerProps[5], vertexMap[x + 2, y - 2], Quaternion.identity);
        GameObject.Instantiate(playerProps[6], vertexMap[x + 3, y + 2], Quaternion.identity);

        int distance = playerZoneSize * 2 + 1;
        if (!highMap[x + distance, y]) { playerProps[1].transform.position = new Vector3(vertexMap[x + distance, y].x, 13.5f, vertexMap[x + distance, y].z); }
        else
        if (!highMap[x, y - distance]) { playerProps[1].transform.position = new Vector3(vertexMap[x, y - distance].x, 13.5f, vertexMap[x, y - distance].z); }
        else
        if (!highMap[x - distance, y]) { playerProps[1].transform.position = new Vector3(vertexMap[x - distance, y].x, 13.5f, vertexMap[x - distance, y].z); }
        else
        if (!highMap[x, y + distance]) { playerProps[1].transform.position = new Vector3(vertexMap[x - distance, y].x, 13.5f, vertexMap[x, y + distance].z); }
    }

    //Спавн босса

    public bool[,] FindBoss(Vector3[,] vertexMap, bool[,] highMap, int mapChunkSize)
    {
        bool isPlacable = true;

        for (int x = 0; x <= mapChunkSize / 2; x++)
        {
            for (int y = mapChunkSize; y > mapChunkSize / 2; y--)
            {

                for (int xMod = -bossZoneSize; xMod <= bossZoneSize; xMod++)
                {
                    for (int yMod = -bossZoneSize; yMod <= bossZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -bossZoneSize - 1; xMod <= bossZoneSize + 1; xMod++)
                    {
                        for (int yMod = -bossZoneSize - 1; yMod <= bossZoneSize + 1; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetBoss(PlayerPrefs.GetInt("currenntSave"), x, y);
                    SpawnBoss(vertexMap, highMap, x, y);

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        for (int x = 0; x <= mapChunkSize / 2; x++)
        {
            for (int y = 0; y <= mapChunkSize / 2; y++)
            {

                for (int xMod = -bossZoneSize; xMod <= bossZoneSize; xMod++)
                {
                    for (int yMod = -bossZoneSize; yMod <= bossZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -bossZoneSize - 1; xMod <= bossZoneSize + 1; xMod++)
                    {
                        for (int yMod = -bossZoneSize - 1; yMod <= bossZoneSize + 1; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetBoss(PlayerPrefs.GetInt("currenntSave"), x, y);
                    SpawnBoss(vertexMap, highMap, x, y);

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        for (int x = mapChunkSize; x > mapChunkSize / 2; x--)
        {
            for (int y = mapChunkSize; y > mapChunkSize / 2; y--)
            {

                for (int xMod = -bossZoneSize; xMod <= bossZoneSize; xMod++)
                {
                    for (int yMod = -bossZoneSize; yMod <= bossZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -bossZoneSize - 1; xMod <= bossZoneSize + 1; xMod++)
                    {
                        for (int yMod = -bossZoneSize - 1; yMod <= bossZoneSize + 1; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetBoss(PlayerPrefs.GetInt("currenntSave"), x, y);
                    SpawnBoss(vertexMap, highMap, x, y);

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        for (int x = mapChunkSize; x > mapChunkSize / 2; x--)
        {
            for (int y = 0; y <= mapChunkSize / 2; y++)
            {

                for (int xMod = -bossZoneSize; xMod <= bossZoneSize; xMod++)
                {
                    for (int yMod = -bossZoneSize; yMod <= bossZoneSize; yMod++)
                    {
                        if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                        {
                            isPlacable = false;
                            break;
                        }
                        if (!highMap[x + xMod, y + yMod])
                        {
                            isPlacable = false;
                            break;
                        }
                    }
                    if (!isPlacable)
                    {
                        break;
                    }
                }

                if (isPlacable)
                {
                    for (int xMod = -bossZoneSize - 1; xMod <= bossZoneSize + 1; xMod++)
                    {
                        for (int yMod = -bossZoneSize - 1; yMod <= bossZoneSize + 1; yMod++)
                        {
                            highMap[x + xMod, y + yMod] = false;
                        }
                    }

                    _saveLoadManager.SetBoss(PlayerPrefs.GetInt("currenntSave"), x, y);
                    SpawnBoss(vertexMap, highMap, x, y);

                    return highMap;
                }
                else
                {
                    isPlacable = true;
                }
            }
        }

        return highMap;
    }

    public void SpawnBoss(Vector3[,] vertexMap, bool[,] highMap, int x, int y)
    {
        GameObject.Instantiate(bossProps[0], vertexMap[x, y] + new Vector3 (0, 10, 0), Quaternion.identity);

        bossProps[6].transform.position = vertexMap[x - 3, y];

        GameObject.Instantiate(bossProps[7], vertexMap[x + bossZoneSize / 2, y + bossZoneSize / 2], Quaternion.identity);
        GameObject.Instantiate(bossProps[7], vertexMap[x + bossZoneSize / 2, y - bossZoneSize / 2], Quaternion.identity);
        GameObject.Instantiate(bossProps[7], vertexMap[x - bossZoneSize / 2, y + bossZoneSize / 2], Quaternion.identity);
        GameObject.Instantiate(bossProps[7], vertexMap[x - bossZoneSize / 2, y - bossZoneSize / 2], Quaternion.identity);

        GameObject.Instantiate(bossProps[8], vertexMap[x, y], Quaternion.identity);


        bool isPillar = true;

        for (int x2 = -bossZoneSize; x2 <= bossZoneSize; x2++)
        {
            if (x2 == -1)
            {
                GameObject.Instantiate(bossProps[4], vertexMap[x + x2, y + bossZoneSize], Quaternion.Euler(0, 270, 0));
                GameObject.Instantiate(bossProps[4], vertexMap[x + x2, y - bossZoneSize], Quaternion.Euler(0, 270, 0));
            }
            else if (x2 == 0)
            {
                GameObject.Instantiate(bossProps[5], vertexMap[x + x2, y + bossZoneSize], Quaternion.Euler(0, 90, 0));
                GameObject.Instantiate(bossProps[5], vertexMap[x + x2, y - bossZoneSize], Quaternion.Euler(0, 90, 0));
                isPillar = true;
            }
            else if (x2 == 1)
            {
                GameObject.Instantiate(bossProps[4], vertexMap[x + x2, y + bossZoneSize], Quaternion.Euler(0, 90, 0));
                GameObject.Instantiate(bossProps[4], vertexMap[x + x2, y - bossZoneSize], Quaternion.Euler(0, 90, 0));
            }
            else if (isPillar)
            {
                GameObject.Instantiate(bossProps[2], vertexMap[x + x2, y + bossZoneSize], Quaternion.identity);
                GameObject.Instantiate(bossProps[2], vertexMap[x + x2, y - bossZoneSize], Quaternion.identity);
                isPillar = false;
            }
            else
            {
                GameObject.Instantiate(bossProps[3], vertexMap[x + x2, y + bossZoneSize], Quaternion.Euler(0, 90, 0));
                GameObject.Instantiate(bossProps[3], vertexMap[x + x2, y - bossZoneSize], Quaternion.Euler(0, 90, 0));
                isPillar = true;
            }
        }

        for (int y2 = -bossZoneSize + 1; y2 <= bossZoneSize - 1; y2++)
        {
            if (y2 == -1)
            {
                GameObject.Instantiate(bossProps[4], vertexMap[x + bossZoneSize, y + y2], Quaternion.Euler(0, 180, 0));
                GameObject.Instantiate(bossProps[4], vertexMap[x - bossZoneSize, y + y2], Quaternion.Euler(0, 180, 0));
            }
            else if (y2 == 0)
            {
                GameObject.Instantiate(bossProps[1], vertexMap[x + bossZoneSize, y + y2], Quaternion.identity);
                GameObject.Instantiate(bossProps[5], vertexMap[x - bossZoneSize, y + y2], Quaternion.identity);
                isPillar = true;
            }
            else if (y2 == 1)
            {
                GameObject.Instantiate(bossProps[4], vertexMap[x + bossZoneSize, y + y2], Quaternion.identity);
                GameObject.Instantiate(bossProps[4], vertexMap[x - bossZoneSize, y + y2], Quaternion.identity);
            }
            else if (isPillar)
            {
                GameObject.Instantiate(bossProps[2], vertexMap[x + bossZoneSize, y + y2], Quaternion.identity);
                GameObject.Instantiate(bossProps[2], vertexMap[x - bossZoneSize, y + y2], Quaternion.identity);
                isPillar = false;
            }
            else
            {
                GameObject.Instantiate(bossProps[3], vertexMap[x + bossZoneSize, y + y2], Quaternion.identity);
                GameObject.Instantiate(bossProps[3], vertexMap[x - bossZoneSize, y + y2], Quaternion.identity);
                isPillar = true;
            }
        }
    }

    //Спавн точек интереса

    public bool[,] FindBigPointOfInterest(Vector3[,] vertexMap, bool[,] highMap, int mapChunkSize)
    {
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (Random.Range(0, 10000) <= 10)
                {
                    /*int i = Random.Range(1, 3);
                    if (i == 0)
                    {
                        highMap = SpawnBigPointOfInterest01(vertexMap, highMap, x, y, true);
                    } else if (i == 1)
                    {
                        highMap = SpawnBigPointOfInterest02(vertexMap, highMap, x, y, true);
                    }
                    else if (i == 2)
                    {
                        highMap = SpawnBigPointOfInterest03(vertexMap, highMap, x, y, true);
                    }*/
                    highMap = SpawnBigPointOfInterest01(vertexMap, highMap, x, y, true);
                }
            }
        }

        return highMap;
    }

    public bool[,] SpawnBigPointOfInterest01(Vector3[,] vertexMap, bool[,] highMap, int x, int y, bool isNew)
    {
        if (isNew)
        {
            for (int xMod = -4; xMod <= 4; xMod++)
            {
                for (int yMod = -4; yMod <= 4; yMod++)
                {
                    if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                    {
                        return highMap;
                    }
                    if (!highMap[x + xMod, y + yMod])
                    {
                        return highMap;
                    }
                }
            }
        }

        if (isNew)
        {
            _saveLoadManager.SetBigPointOfInterest(PlayerPrefs.GetInt("currenntSave"), 0, x, y);
        }

        for (int xMod = -5; xMod <= 5; xMod++)
        {
            for (int yMod = -5; yMod <= 5; yMod++)
            {
                highMap[x + xMod, y + yMod] = false;
            }
        }

        for (int i = -3; i <= 3; i++)
        {
            GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x + 4, y + i], Quaternion.identity);
        }

        GameObject.Instantiate(bigPointOfInterestProps[1], vertexMap[x + 4, y - 4], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[1], vertexMap[x + 4, y + 4], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[1], vertexMap[x, y - 4], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[1], vertexMap[x, y + 4], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x + 3, y + 4], Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x + 2, y + 4], Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x + 1, y + 4], Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x, y - 3], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x, y - 2], Quaternion.identity);
        GameObject.Instantiate(bigPointOfInterestProps[0], vertexMap[x, y - 1], Quaternion.identity);

        GameObject.Instantiate(bigPointOfInterestProps[Random.Range(2, 5)], vertexMap[x - 1, y - 4], Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(bigPointOfInterestProps[Random.Range(2, 5)], vertexMap[x - 2, y - 4], Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(bigPointOfInterestProps[Random.Range(2, 5)], vertexMap[x - 3, y - 4], Quaternion.Euler(0, 90, 0));
        for (int i = -3; i <= 3; i++)
        {
            GameObject.Instantiate(bigPointOfInterestProps[Random.Range(2, 5)], vertexMap[x - 4, y - i], Quaternion.Euler(0, 180, 0));
        }

        GameObject.Instantiate(bigPointOfInterestProps[5], vertexMap[x, y], Quaternion.identity);

        GameObject.Instantiate(bigPointOfInterestProps[6], vertexMap[x - 2, y - 2], Quaternion.identity);
        GameObject.Instantiate(foods[Random.Range(0, foods.Length)], vertexMap[x - 2, y - 2] + new Vector3(0, 1, 0), Quaternion.identity);

        _proceduralEnemyFactory.CreateRobots(Random.Range(2, 5), vertexMap[x, y], 5);

        return highMap;
    }

    public bool[,] FindSmallPointOfInterest(Vector3[,] vertexMap, bool[,] highMap, int mapChunkSize)
    {
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (Random.Range(0, 10000) <= 8)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            highMap = SpawnSmallPointOfInterest01(vertexMap, highMap, x, y, true);
                            break;
                        case 1:
                            highMap = SpawnSmallPointOfInterest02(vertexMap, highMap, x, y, true);
                            break;
                        case 2:
                            highMap = SpawnSmallPointOfInterest03(vertexMap, highMap, x, y, true);
                            break;
                    }
                }
            }
        }

        return highMap;
    }

    public bool[,] SpawnSmallPointOfInterest01(Vector3[,] vertexMap, bool[,] highMap, int x, int y, bool isNew)
    {
        if (isNew)
        {
            for (int xMod = -2; xMod <= 2; xMod++)
            {
                for (int yMod = -2; yMod <= 2; yMod++)
                {
                    if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                    {
                        return highMap;
                    }
                    if (!highMap[x + xMod, y + yMod])
                    {
                        return highMap;
                    }
                }
            }
        }

        if (isNew)
        {
            _saveLoadManager.SetSmallPointOfInterest(PlayerPrefs.GetInt("currenntSave"), 0, x, y);
        }

        for (int xMod = -3; xMod <= 3; xMod++)
        {
            for (int yMod = -3; yMod <= 3; yMod++)
            {
                highMap[x + xMod, y + yMod] = false;
            }
        }
        
        GameObject foodPosition = GameObject.Instantiate(smallPointOfInterestProps[0], vertexMap[x + 2, y + 2], Quaternion.identity);
        GameObject restingPosition = GameObject.Instantiate(smallPointOfInterestProps[2], vertexMap[x - 1, y - 2], Quaternion.identity);

        GameObject.Instantiate(smallPointOfInterestProps[3], vertexMap[x - 2, y + 1], Quaternion.identity);
        
        _proceduralEnemyFactory.CreateAggressiveHerbals(3, vertexMap[x, y], 5, restingPosition.transform, foodPosition.transform);

        return highMap;
    }

    public bool[,] SpawnSmallPointOfInterest02(Vector3[,] vertexMap, bool[,] highMap, int x, int y, bool isNew)
    {
        if (isNew)
        {
            for (int xMod = -2; xMod <= 2; xMod++)
            {
                for (int yMod = -2; yMod <= 2; yMod++)
                {
                    if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                    {
                        return highMap;
                    }
                    if (!highMap[x + xMod, y + yMod])
                    {
                        return highMap;
                    }
                }
            }
        }

        if (isNew)
        {
            _saveLoadManager.SetSmallPointOfInterest(PlayerPrefs.GetInt("currenntSave"), 1, x, y);
        }

        for (int xMod = -3; xMod <= 3; xMod++)
        {
            for (int yMod = -3; yMod <= 3; yMod++)
            {
                highMap[x + xMod, y + yMod] = false;
            }
        }

        GameObject foodPosition = GameObject.Instantiate(smallPointOfInterestProps[1], vertexMap[x + 2, y + 2], Quaternion.identity);
        GameObject restingPosition = GameObject.Instantiate(smallPointOfInterestProps[2], vertexMap[x - 1, y - 2], Quaternion.identity);

        GameObject.Instantiate(smallPointOfInterestProps[3], vertexMap[x - 2, y + 1], Quaternion.identity);

        _proceduralEnemyFactory.CreatePredators(3, vertexMap[x, y], 5, restingPosition.transform, foodPosition.transform);

        return highMap;
    }
    
    public bool[,] SpawnSmallPointOfInterest03(Vector3[,] vertexMap, bool[,] highMap, int x, int y, bool isNew)
    {
        if (isNew)
        {
            for (int xMod = -2; xMod <= 2; xMod++)
            {
                for (int yMod = -2; yMod <= 2; yMod++)
                {
                    if (x + xMod < 0 || y + yMod < 0 || x + xMod >= 255 || y + yMod >= 255)
                    {
                        return highMap;
                    }
                    if (!highMap[x + xMod, y + yMod])
                    {
                        return highMap;
                    }
                }
            }
        }

        if (isNew)
        {
            _saveLoadManager.SetSmallPointOfInterest(PlayerPrefs.GetInt("currenntSave"), 1, x, y);
        }

        for (int xMod = -3; xMod <= 3; xMod++)
        {
            for (int yMod = -3; yMod <= 3; yMod++)
            {
                highMap[x + xMod, y + yMod] = false;
            }
        }

        GameObject.Instantiate(smallPointOfInterestProps[3], vertexMap[x - 2, y + 1], Quaternion.identity);

        
        switch (Random.Range(0,3))
        {
            case 0:
                _proceduralEnemyFactory.CreateCacti(8, -vertexMap[x, y], 5);
                break;  
            case 1:
                _proceduralEnemyFactory.CreateBurrows(8, vertexMap[x, y], 2);
                break;  
            case 2:
                _proceduralEnemyFactory.CreateMushrooms(8, vertexMap[x, y], 4);
                break;
        }

        return highMap;
    }


    //Спавн окружения

    public void SpawnOfEnvoroment(Vector3[,] vertexMap, bool[,] highMap, int mapChunkSize)
    {
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (highMap[x, y] && Random.Range(0, 1000) <= 30)
                {
                    if (vertexMap[x, y].y <= waterHeight)
                    {

                    }
                    else if (vertexMap[x, y].y <= lowBeachHeight)
                    {
                        LowBeachGenerator(vertexMap[x, y]);
                    }
                    else if (vertexMap[x, y].y <= highBeachHeight)
                    {
                        HighBeachGenerator(vertexMap[x, y]);
                    }
                    else if (vertexMap[x, y].y <= lowForestHeight)
                    {
                        LowForestGenerator(vertexMap[x, y]);
                    }
                    else if (vertexMap[x, y].y <= highForestHeight)
                    {
                        HighForestGenerator(vertexMap[x, y]);
                    }
                    highMap[x, y] = false;
                    highMap[x + 1, y] = false;
                    highMap[x - 1, y] = false;
                    highMap[x, y + 1] = false;
                    highMap[x, y - 1] = false;
                }
            }
        }
    }

    public void LowBeachGenerator(Vector3 verticy)
    {
        GameObject.Instantiate(lowBeachObjects[Random.Range(0, lowBeachObjects.Length)], verticy, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

    public void HighBeachGenerator(Vector3 verticy)
    {
        GameObject.Instantiate(highBeachObjects[Random.Range(0, highBeachObjects.Length)], verticy, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

    public void LowForestGenerator(Vector3 verticy)
    {
        GameObject.Instantiate(lowForestObjects[Random.Range(0, lowForestObjects.Length)], verticy, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

    public void HighForestGenerator(Vector3 verticy)
    {
        GameObject.Instantiate(highForestObjects[Random.Range(0, highForestObjects.Length)], verticy, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

    public void OnValidate()
    {
        if (bossZoneSize % 2 == 1)
        {
            bossZoneSize += 1;
        }
    }
}