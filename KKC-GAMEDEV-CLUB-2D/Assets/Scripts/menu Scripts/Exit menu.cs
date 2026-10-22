using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
    
    
    public void Load()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Debug.Log("Quit button is pressed");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
   
   
    
}
