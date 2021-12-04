using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SvLdProgression : MonoBehaviour
{
    [Serializable]
    public class PlayerData
    {
        public string SavedScene;
        public int QuestProgress;
        public bool GotSword;
        public List<float> SavedPositionsX = new List<float>();
        public List<float> SavedPositionsY = new List<float>();
        public List<string> SavedSceneNames = new List<string>();
        public List<string> Weapons = new List<string>();
        public List<string> Consumables = new List<string>();
    }

    private InGameData DataHolder;
    public GameObject Player;

    private void Start()
    {
        if (GameObject.Find("GameData"))
        {
            DataHolder = GameObject.Find("GameData").GetComponent<InGameData>();
        }
        if (DataHolder.SceneNames.Count == 0)
        {
            DataHolder.SceneNames.Add("Game");
            DataHolder.PositionsX.Add(-2);
            DataHolder.PositionsY.Add(-2);
        }
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void Update()
    {
        if (GameObject.Find("Player"))
        {
            DataHolder.CurrentPosition = Player.transform.position;
            for (int i = 0; i < DataHolder.SceneNames.Count; i++)
            {
                if (DataHolder.SceneNames[i] == SceneManager.GetActiveScene().name)
                {
                    DataHolder.PositionsX[i] = DataHolder.CurrentPosition.x;
                    DataHolder.PositionsY[i] = DataHolder.CurrentPosition.y;
                    break;
                }
            }
        }
        if (GameObject.Find("Player") && SceneManager.GetActiveScene().name != "Final")
        {
            DataHolder.CurrentSceneWithPlayer = SceneManager.GetActiveScene().name;
        }
        else if(SceneManager.GetActiveScene().name == "Final")
        {
            File.Delete(Application.persistentDataPath + "/SaveData/PlayerData.sav");
            DataHolder.ResetData();
        }
    }

    private void OnSceneChanged(Scene Current, Scene New)
    {
        if (GameObject.Find("Player"))
        {
            Player = GameObject.Find("Player");
            for (int i = 0; i < DataHolder.SceneNames.Count; i++)
            {
                if (DataHolder.SceneNames[i] == New.name)
                {
                    DataHolder.NewScene = false;
                    Player.transform.position = new Vector2(DataHolder.PositionsX[i], DataHolder.PositionsY[i]);
                    break;
                }
            }
            if (DataHolder.NewScene && New.name != "Menu")
            {
                DataHolder.PositionsX.Add(Player.transform.position.x);
                DataHolder.PositionsY.Add(Player.transform.position.y);
                DataHolder.SceneNames.Add(New.name);
            }
        }
        if (New.name == "Menu")
        {
            SaveGame();
        }
    }

    private void OnApplicationQuit()
    {
        if (DataHolder.CurrentSceneWithPlayer == "" || DataHolder.CurrentSceneWithPlayer == "Final")
        {
            DataHolder.CurrentSceneWithPlayer = "Game";
            SaveGame();
        }
        else { SaveGame(); }
    }

    public string GetSavedScene()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/SaveData/PlayerData.sav", FileMode.Open);
            PlayerData Scenes = (PlayerData)bf.Deserialize(File);
            File.Close();
            return Scenes.SavedScene;
        }
        return "Game";
    }

    public void LoadGame()
    {
        if (!(Directory.Exists(Application.persistentDataPath + "/SaveData")))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        if (File.Exists(Application.persistentDataPath + "/SaveData/PlayerData.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream File = new FileStream(Application.persistentDataPath + "/SaveData/PlayerData.sav", FileMode.Open);
            PlayerData Playerstats = (PlayerData)bf.Deserialize(File);
            DataHolder.PositionsX = Playerstats.SavedPositionsX;
            DataHolder.PositionsY = Playerstats.SavedPositionsY;
            DataHolder.SceneNames = Playerstats.SavedSceneNames;
            DataHolder.CurrentWeapons = Playerstats.Weapons;
            DataHolder.CurrentConsumables = Playerstats.Consumables;
            DataHolder.CurrentQuestProgress = Playerstats.QuestProgress;
            DataHolder.GotSword = Playerstats.GotSword;
            for (int i = 0; i < Playerstats.SavedSceneNames.Count; i++)
            {
                if (SceneManager.GetActiveScene().name == DataHolder.SceneNames[i])
                {
                    Player.transform.position = new Vector2(DataHolder.PositionsX[i], DataHolder.PositionsY[i]);
                    break;
                }
            }
            File.Close();
        }
    }

    public void SaveGame()
    {
        PlayerData Playerstats = new PlayerData
        {
            SavedScene = DataHolder.CurrentSceneWithPlayer,
            SavedPositionsX = DataHolder.PositionsX,
            SavedPositionsY = DataHolder.PositionsY,
            SavedSceneNames = DataHolder.SceneNames,
            Weapons = DataHolder.CurrentWeapons,
            Consumables = DataHolder.CurrentConsumables,
            QuestProgress = DataHolder.CurrentQuestProgress,
            GotSword = DataHolder.GotSword,
        };
        BinaryFormatter bf = new BinaryFormatter();
        FileStream File = new FileStream(Application.persistentDataPath + "/SaveData/PlayerData.sav", FileMode.Create);
        bf.Serialize(File, Playerstats);
        File.Close();
    }

    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/SaveData/PlayerData.sav");
        DataHolder.ResetData();
        //UnityEditor.AssetDatabase.Refresh();
    }
}