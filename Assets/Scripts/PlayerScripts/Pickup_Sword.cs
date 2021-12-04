using System;
using UnityEngine;

public class Pickup_Sword : MonoBehaviour
{
    public ToolTip ToolTipScript;
    public GameObject Sword;
    public GameObject PlayerSword;
    private bool waitButtonPress;
    private KeyCode Interact;
    private InGameData Data;

    // Start is called before the first frame update
    private void Start()
    {
        Interact = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
        Data = GameObject.Find("GameData").GetComponent<InGameData>();
        if (Data.GotSword == true) { Sword.SetActive(false); }
    }

    // Update is called once per frame
    private void Update()
    {
        if (waitButtonPress && Input.GetKeyDown(Interact))
        {
            Sword.SetActive(false);
            PlayerSword.SetActive(true);
            Data.GotSword = true;
            Data.AddWeapon(Sword);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            ToolTipScript.ShowToolTip("Press 'E' To Interact");
            waitButtonPress = true;
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            ToolTipScript.HideToolTip();
            waitButtonPress = false;
        }
    }
}