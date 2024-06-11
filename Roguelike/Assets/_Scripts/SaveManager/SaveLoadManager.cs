using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Save;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    string filePath;
    private void Awake()
    {
        filePath = Application.persistentDataPath;
        if (SceneManager.GetActiveScene().name == "HubLocation")
        {
            SetZoneId(PlayerPrefs.GetInt("currenntSave"), 0);
        }
        else if (SceneManager.GetActiveScene().name == "Location1")
        {
            SetZoneId(PlayerPrefs.GetInt("currenntSave"), 1);
        }
    }

    //Проверки
    public int? CheckSaveStatus(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        if (!File.Exists(currentFilePath))
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();

        return save.zoneId;
    }

    //Основные функции
    public void InitiateSave(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Create);

        Save save = new Save();

        save.SaveStartVariables(0, 0, new Location (0, 0), new Location(0, 0), 0);

        bf.Serialize(fs, save);
        fs.Close();
    }

    //Сетеры
    public void SetIsNewSession(int saveId, bool isNewSession)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.isNewSession = isNewSession;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetCurrensy(int saveId, int goldCount)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.goldCount = goldCount;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetZoneId(int saveId, int zoneId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.zoneId = zoneId;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetSeed(int saveId, int seed)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.seed = seed;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetPlayer(int saveId, int x, int y, int startIngredientId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.playerLocation.x = x;
        save.playerLocation.y = y;
        save.startIngredientId = startIngredientId;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetBoss(int saveId, int x, int y)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.bossLocation.x = x;
        save.bossLocation.y = y;

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetBigPointOfInterest(int saveId, int POIId, int x, int y)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.pointsOfInterest.Add(new POI(POIId, x, y));

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void SetSmallPointOfInterest(int saveId, int SPOIId, int x, int y)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.smallPointsOfInterest.Add(new SPOI(SPOIId, x, y));

        bf.Serialize(fs, save);
        fs.Close();
    }

    //Гетеры
    public Save GetSave(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        if (!File.Exists(currentFilePath))
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();

        return save;
    }

    public bool GetIsNewSession(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        bool isNewSession = save.isNewSession;

        fs.Close();
        return isNewSession;
    }

    public int GetSeed(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        int seed = save.seed;

        fs.Close();
        return seed;
    }

    public int GetStartIngredientId(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        int startIngredientId = save.startIngredientId;

        fs.Close();
        return startIngredientId;
    }

    //Очистка значений
    public void ClearBigPointOfInterest(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.pointsOfInterest.Clear();

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void ClearSmallPointOfInterest(int saveId)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.smallPointsOfInterest.Clear();

        bf.Serialize(fs, save);
        fs.Close();
    }

    //Прибавление
    public void AddCurrensy(int saveId, int goldCount)
    {
        string currentFilePath = filePath + "/save_" + saveId + ".gamesave";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(currentFilePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        fs.Close();
        fs = new FileStream(currentFilePath, FileMode.Create);

        save.goldCount += goldCount;

        bf.Serialize(fs, save);
        fs.Close();
    }
}

[System.Serializable]
public class Save
{
    [System.Serializable]
    public struct Loc
    {
        public int x, y;

        public Loc(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.Serializable]
    public struct POI
    {
        public int id;
        public Loc location;

        public POI(int id, int x, int y)
        {
            this.id = id;
            location = new Loc(x, y);
        }
    }

    [System.Serializable]
    public struct SPOI
    {
        public int id;
        public Loc location;

        public SPOI(int id, int x, int y)
        {
            this.id = id;
            location = new Loc(x, y);
        }
    }

    public int zoneId;
    public int goldCount;

    public int sessionGoldCount;
    public bool isNewSession;

    public int seed;
    public Loc bossLocation;
    public Loc playerLocation;
    public int startIngredientId;

    public List<POI> pointsOfInterest = new List<POI>();
    public List<SPOI> smallPointsOfInterest = new List<SPOI>();

    public void SaveStartVariables(int sessionGoldCount, int zoneSeed, Location bossLocation, Location playerLocation, int startIngredientId)
    {
        this.sessionGoldCount = sessionGoldCount;

        this.seed = zoneSeed;
        this.bossLocation.x = bossLocation.x;
        this.bossLocation.y = bossLocation.y;
        this.playerLocation.x = playerLocation.x;
        this.playerLocation.y = playerLocation.y;
        this.startIngredientId = startIngredientId;
    }

    public void SavePointsOfInterest(List<PointOfInterest> AddedPointsOfInterest)
    {
        foreach (PointOfInterest addedPoint in AddedPointsOfInterest)
        {
            POI point;
            point.location.x = addedPoint.location.x;
            point.location.y = addedPoint.location.y;
            point.id = addedPoint.id;
            pointsOfInterest.Add(point);
        }
    }

    public void SaveSmallPointsOfInterest(List<SmallPointOfInterest> AddedSmallPointsOfInterests)
    {
        foreach (SmallPointOfInterest addedPoint in AddedSmallPointsOfInterests)
        {
            SPOI point;
            point.location.x = addedPoint.location.x;
            point.location.y = addedPoint.location.y;
            point.id = addedPoint.id;
            smallPointsOfInterest.Add(point);
        }
    }
}

public class Location
{
    public int x, y;

    public Location (int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PointOfInterest
{
    public int id;
    public Location location;

    public PointOfInterest(int id, int x, int y)
    {
        this.id = id;
        this.location.x = x;
        this.location.y = y;
    }
    public PointOfInterest(int id, Location location)
    {
        this.id = id;
        this.location = location;
    }
}

public class SmallPointOfInterest
{
    public int id;
    public Location location;

    public SmallPointOfInterest(int id, int x, int y)
    {
        this.id = id;
        this.location.x = x;
        this.location.y = y;
    }
    public SmallPointOfInterest(int id, Location location)
    {
        this.id = id;
        this.location = location;
    }
}