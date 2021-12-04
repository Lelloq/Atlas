using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject NarrationBox;
    private KeyCode Menu;
    private KeyCode Interact;
    private bool bMenuOpened = false;

    private void Start()
    {
        Menu = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Open Menu", "Escape"));
        Interact = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(Menu) && bMenuOpened == false && NarrationBox.activeSelf)
        {
            bMenuOpened = true;
            Time.timeScale = 0;
            PauseCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(Menu) && bMenuOpened == true)
        {
            ResumeGame();
            PauseCanvas.SetActive(false);
        }
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        bMenuOpened = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}