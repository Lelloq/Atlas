using System;
using UnityEngine;

public class Activate_Text : MonoBehaviour
{
    public ToolTip ToolTipScript;

    public TextAsset Text;

    public int startLine;
    public int endLine;

    public TextBoxManager TextBox;

    public bool requireButtonPress;
    private bool waitButtonPress;

    public bool destroyActive;

    private KeyCode Interact;

    // Start is called before the first frame update
    private void Start()
    {
        TextBox = FindObjectOfType<TextBoxManager>();
        Interact = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
    }

    private void Update()
    {
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