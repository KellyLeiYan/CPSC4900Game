using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour {
    [SerializeField] string Opening = "Opening";
    [SerializeField] bool showLoading = true;

    void Awake(){
        DontDestroyOnLoad(gameObject);                 // keep this object across scenes
        Application.targetFrameRate = 60;              // optional
        // TODO: load settings, init audio mixer, warm up Addressables, etc.
    }

    void Start(){
        if (showLoading) ShowBasicLoadingOverlay();
        // Load gameplay scene asynchronously
        SceneManager.LoadSceneAsync(Opening, LoadSceneMode.Single);
    }

    void ShowBasicLoadingOverlay(){
        // optional: create a Canvas + “Loading…” Text here, or reference a prefab
    }
}
