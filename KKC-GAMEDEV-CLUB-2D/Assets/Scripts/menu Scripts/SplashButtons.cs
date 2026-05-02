using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashButton : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    
    
    public void Load()
    {
        SceneManager.LoadScene("LoadScene", LoadSceneMode.Single);
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }
    public void Credits()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("ExitScreen", LoadSceneMode.Single);
        //Actually find grammar for exiting game window
    }
    
    
}
