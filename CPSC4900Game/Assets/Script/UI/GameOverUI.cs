using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text hintText;

    void Start(){
        string hint = PlayerPrefs.GetString("LastHint", "You died.");
        if (hintText) hintText.text = hint;
    }

    public void OnRetry(){ SceneManager.LoadScene("TheCarpetedFloor"); }
    public void OnChapters(){ SceneManager.LoadScene("Boot"); } 
}
