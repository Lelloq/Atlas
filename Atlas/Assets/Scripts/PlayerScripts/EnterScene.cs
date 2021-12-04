using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class EnterScene : MonoBehaviour
{

    public ToolTip ToolTipScript;
    public bool requireButtonPress;
    public string sceneName;
    private bool waitButtonPress;

    // Update is called once per frame
    void Update()
    {
        if (waitButtonPress && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            if (requireButtonPress)
            {
                ToolTipScript.ShowToolTip("Press 'E' To Interact");
                waitButtonPress = true;
                return;
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
