using UnityEngine;
using UnityEngine.SceneManagement;
public class DataCenter : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);

    }
    private int level;
    public void setLevel(int beginner)
    {
        level = beginner;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public int getLevel()
    {
        return level;
    }
}
