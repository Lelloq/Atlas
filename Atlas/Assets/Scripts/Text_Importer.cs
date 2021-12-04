using UnityEngine;

public class Text_Importer : MonoBehaviour
{
    public TextAsset textFile;
    public string[] textlines;

    // Start is called before the first frame update
    private void Start()
    {
        if (textFile != null)
        {
            textlines = (textFile.text.Split('\n'));
        }
    }
}