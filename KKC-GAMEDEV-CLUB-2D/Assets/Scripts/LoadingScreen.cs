using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    // declared variables
    public GameObject LoadingScreen;
    public Image LoadingBarFill;



    // Loading Screen Method 
    public void LoadSceneMethod(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }


    //Coroutine
    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            // clamping loading bar boundaries
            float progressValue = mathf.Clamp01(operation.progress / 0.9f);
            //filling the bar
            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }

}