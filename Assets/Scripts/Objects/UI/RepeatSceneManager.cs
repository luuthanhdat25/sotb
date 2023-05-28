using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatSceneManager : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }
}
