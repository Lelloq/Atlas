using System.Collections.Generic;
using UnityEngine;

public class InGameData : MonoBehaviour
{
    private static InGameData DataHolder;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (DataHolder == null)
        {
            DataHolder = this;
        }
        else { Destroy(gameObject); }
    }

    [SerializeField]
    public string CurrentSceneWithPlayer;

    public bool NewScene = true;
    public Vector2 CurrentPosition;
    public List<float> PositionsX = new List<float>();
    public List<float> PositionsY = new List<float>();
    public List<string> SceneNames = new List<string>();
    public List<string> CurrentWeapons = new List<string>();
    public List<string> CurrentConsumables = new List<string>();
    public int CurrentQuestProgress = 0;
    public bool GotSword = false;

    public void AddWeapon(GameObject Weapon)
    {
        CurrentWeapons.Add(Weapon.name);
    }

    public void RemoveWeapon(GameObject Weapon)
    {
        CurrentWeapons.Remove(Weapon.name);
    }

    public void AddConsumable(GameObject Item)
    {
        CurrentConsumables.Add(Item.name);
    }

    public void RemoveConsumable(GameObject Item)
    {
        CurrentConsumables.Remove(Item.name);
    }

    public List<string> GetWeapons()
    {
        return CurrentWeapons;
    }

    public List<string> GetItems()
    {
        return CurrentConsumables;
    }

    public void ResetData()
    {
        CurrentSceneWithPlayer = "Game";
        NewScene = true;
        CurrentPosition = new Vector2(-2,-2);
        PositionsX = new List<float>();
        PositionsY = new List<float>();
        SceneNames = new List<string>();
        CurrentWeapons = new List<string>();
        CurrentConsumables = new List<string>();
        CurrentQuestProgress = 0;
        GotSword = false;
    }
}