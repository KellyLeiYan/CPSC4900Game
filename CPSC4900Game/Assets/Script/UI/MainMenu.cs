using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Name of your first chapter scene")]
    public string firstChapterSceneName = "TheCarpetedFloor";  // change to chapter 1 scene name

    public void OnPlayButton()
    {
        SceneManager.LoadScene(firstChapterSceneName);
    }

    public void OnQuitButton()
    {
        // works in build; does nothing in editor
        Application.Quit();
    }
}
