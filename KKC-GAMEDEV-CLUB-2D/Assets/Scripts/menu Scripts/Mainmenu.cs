using UnityEngine;
using UnityEngine.SceneManagement;
public class Mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("Mainscene");
    }

    public void OnExitClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
