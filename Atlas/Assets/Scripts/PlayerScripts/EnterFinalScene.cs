using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class EnterFinalScene : MonoBehaviour
{
    private InGameData Data;

    private void Start()
    {
        Data = GameObject.Find("GameData").GetComponent<InGameData>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Data.CurrentQuestProgress == 4)
            {
                    SceneManager.LoadScene("Final");
            }
        }
    }


}
