using System;
using UnityEngine;

public class Activate_Text_NPC : MonoBehaviour
{
    public GameObject Snowman;
    public GameObject Goblin;
    public GameObject Slime;

    public ToolTip ToolTipScript;

    private TextAsset Text;
    public TextAsset Text1;
    public TextAsset Text2;
    public TextAsset Text3;
    public TextAsset Text4;

    private bool Snowmen;
    private bool Goblins;
    private bool Slimes;

    public int startLine;
    public int endLine;

    public TextBoxManager TextBox;

    public bool requireButtonPress;
    private bool waitButtonPress;

    public bool destroyActive;

    private KeyCode Interact;

    private InGameData Data;

    // Start is called before the first frame update
    private void Start()
    {
        TextBox = FindObjectOfType<TextBoxManager>();
        Interact = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
        Data = GameObject.Find("GameData").GetComponent<InGameData>();
    }

    private void Update()
    {
        Snowmen = false;
        Goblins = false;
        Slimes = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.name == "EvilSnowman")
            {
                Snowmen = true;
            }
            else if (enemy.name == "Horrifying_Goblin")
            {
                Goblins = true;
            }
            else if (enemy.name == "Slime")
            {
                Slimes = true;
            }
        }

        if (!Snowmen && Data.CurrentQuestProgress == 0)
        {
            Data.CurrentQuestProgress = 1;
        }
        else if (!Goblins && Data.CurrentQuestProgress == 1)
        {
            Data.CurrentQuestProgress = 2;
        }
        else if (!Slimes && Data.CurrentQuestProgress == 2)
        {
            Data.CurrentQuestProgress = 3;
        }

        if (Data.CurrentQuestProgress == 0)
        {
            Text = Text1;
        }
        else if (Data.CurrentQuestProgress == 1)
        {
            Text = Text2;
        }
        else if (Data.CurrentQuestProgress == 2)
        {
            Text = Text3;
        }
        else
        {
            Text = Text4;
            Data.CurrentQuestProgress = 4;
        }

        if (waitButtonPress && Input.GetKeyDown(Interact) && !(TextBox.isActive))
        {
            TextBox.ReloadScript(Text);
            TextBox.currentLine = startLine;
            TextBox.endLine = endLine;
            TextBox.EnableTextBox();

            if (destroyActive)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (requireButtonPress)
            {
                ToolTipScript.ShowToolTip("Press 'E' To Interact");
                waitButtonPress = true;
                return;
            }

            TextBox.ReloadScript(Text);
            TextBox.currentLine = startLine;
            TextBox.endLine = endLine;
            TextBox.EnableTextBox();

            if (destroyActive)
            {
                Destroy(gameObject);
            }
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