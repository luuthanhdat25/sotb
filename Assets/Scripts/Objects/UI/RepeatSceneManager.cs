using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatSceneManager : RepeatMonoBehaviour
{
    public virtual void CombackToMenu()
        => LoadSceneByIndex(0);
    
    public virtual void NextSceneIndex()
        => LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    
    public virtual void LoadSceneByName(string sceneName) 
        => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

    public virtual void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("Invalid scene index.");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
    
    public virtual void ReloadScene() 
        => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    public virtual void QuitGame() => Application.Quit();
}
