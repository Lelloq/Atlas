using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MMPanel;
    public GameObject SMPanel;
    public SvLdProgression GameScene;

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void ExitGame()
    {
        StartCoroutine(Quitting());
    }

    public void SettingsMenu()
    {
        MMPanel.SetActive(false);
        SMPanel.SetActive(true);
    }

    public void BacktoMainMenu()
    {
        MMPanel.SetActive(true);
        SMPanel.SetActive(false);
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(GameScene.GetSavedScene());
        GameScene.LoadGame();
    }

    private IEnumerator Quitting()
    {
        yield return new WaitForSeconds(4);
        Application.Quit();
    }
}