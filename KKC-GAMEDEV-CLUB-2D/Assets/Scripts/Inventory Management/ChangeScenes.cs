using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{

    public void GoToSceneNumber()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum++);
            //SceneName is just a placeholder
     //for the actual name of the Scene you want to use
    }


}