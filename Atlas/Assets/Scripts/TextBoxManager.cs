using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public Text InternalText;

    public TextAsset textFile;
    public string[] textlines;

    public int currentLine;
    public int endLine;

    public MovementAlt2 player;

    public bool isActive;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<MovementAlt2>();


        if (textFile != null)
        {
            textlines = (textFile.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = textlines.Length - 1;
            string sentence = InternalText.text;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        if (isActive)
        {
            EnableTextBox();
        }
        else if (!isActive)
        {
            DisableTextBox();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isActive) { return; }

        InternalText.text = textlines[currentLine];
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentLine++;

            string sentence = textlines[currentLine];
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));

        }

        if (currentLine > endLine)
        {
            DisableTextBox();
        }
    }

    public void EnableTextBox()
    {
        Time.timeScale = 0;
        isActive = true;
        textBox.SetActive(true);
        player.canMove = false;
        currentLine = 0;
    }

    public void DisableTextBox()
    {
        Time.timeScale = 1;
        isActive = false;
        textBox.SetActive(false);
        player.canMove = true;
    }

    IEnumerator TypeSentence (string sentence)
    {
        textlines[currentLine] = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textlines[currentLine] += letter;
            yield return null;
        }
    }

    public void ReloadScript(TextAsset Text)
    {
        if (Text != null)
        {
            textlines = new string[1];
            textlines = (Text.text.Split('\n'));
        }
    }
}